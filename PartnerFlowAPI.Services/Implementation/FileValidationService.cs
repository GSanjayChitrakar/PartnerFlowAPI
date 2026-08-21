using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using PartnerFlowAPI.Services.Configuration;
using PartnerFlowAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Services.Implementation
{
    public class FileValidationService : IFileValidationService
    {
        private readonly FileValidationOptions _options;
        public FileValidationService(IOptions<FileValidationOptions> options)
        {
            _options = options.Value;
        }
        private static readonly string[] AllowedExtensions =
        {
        ".pdf", ".jpg", ".jpeg", ".png" , ".msg"
        };

        private static readonly string[] AllowedMimeTypes =
        {
        "application/pdf",
        "image/jpeg",
        "image/png",
        "application/vnd.ms-outlook"
        };
        public async Task<(bool IsValid, string ErrorMessage)> ValidateAsync(string base64Data, string fileName)
        {
            if (string.IsNullOrWhiteSpace(base64Data))
                return (false, "File data is missing");

            // Clean the string
            base64Data = base64Data.Trim();

            // Remove data URI prefix if present
            var commaIndex = base64Data.IndexOf(',');
            if (commaIndex >= 0)
                base64Data = base64Data.Substring(commaIndex + 1);

            byte[] fileBytes;
            try
            {
                fileBytes = Convert.FromBase64String(base64Data);
            }
            catch (FormatException)
            {
                return (false, "Invalid Base64 data");
            }

            return await ValidateBytesAsync(fileBytes, fileName);
        }

        public async Task<(bool IsValid, string ErrorMessage)> ValidateBytesAsync(byte[] fileBytes, string fileName)
        {
            if (fileBytes == null || fileBytes.Length == 0)
                return (false, "File is empty");

            if (fileBytes.Length > _options.MaxFileSizeBytes)
                return (false, "File size exceeds allowed limit");

            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
                return (false, "File type not allowed");

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var mimeType) ||
                !AllowedMimeTypes.Contains(mimeType))
                return (false, "Invalid MIME type");

            if (!IsValidFileSignature(fileBytes, extension))
                return (false, "File signature mismatch");

            if (ContainsMaliciousContent(fileBytes))
                return (false, "Malicious script detected");

            return (true, string.Empty);
        }
        private static readonly string[] DangerousPatterns =
                        {
                            // JavaScript / XSS
                            "<script",
                            "javascript:",
                            "onerror=",
                            "onload=",
                            "eval(",
 
                            // HTML injection
                            "<html",
                            "<iframe",
                            "<object",
                            "<embed",
 
                            // PDF JavaScript
                            "/javascript",
                            "/js",
                            "/openaction",
                            "app.alert",
                            "this.exportdataobject",
 
                            // RCE / command execution
                            "cmd.exe",
                            "powershell",
                            "bash",
                            "sh -c",
                            "system(",
                            "exec(",
                            "runtime.getruntime",
 
                            // SQL injection
                            "union select",
                            "drop table",
                            "or 1=1",
                            "-- ",
 
                            // XML / XXE
                            "<!doctype",
                            "<!entity",
                            "system \"file://",
 
                            // Web shells
                            "<?php",
                            //"<%",
                            "<asp:"
                        };
        private bool ContainsMaliciousContent(byte[] fileBytes)
        {
            // Only scan text-like content
            var text = Encoding.UTF8.GetString(fileBytes);

            foreach (var pattern in DangerousPatterns)
            {
                if (text.Contains(pattern, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
        private bool IsValidFileSignature(byte[] fileBytes, string extension)
        {
            return extension switch
            {
                ".pdf" => fileBytes.Take(4).SequenceEqual(new byte[] { 0x25, 0x50, 0x44, 0x46 }),
                ".jpg" or ".jpeg" => fileBytes.Take(3).SequenceEqual(new byte[] { 0xFF, 0xD8, 0xFF }),
                ".png" => fileBytes.Take(8).SequenceEqual(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }),
                ".msg" => fileBytes.Take(8).SequenceEqual(new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }),
                _ => false
            };
        }

        //// Replace this with ClamAV / enterprise AV
        //private Task<bool> ScanWithAntivirusAsync(byte[] fileBytes)
        //{
        //    // TODO: Integrate ClamAV or AV API
        //    return Task.FromResult(true);
        //}
    }
}
