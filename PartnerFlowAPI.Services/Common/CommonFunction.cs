using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using PartnerFlowAPI.Database.Entities;
using FGLI_SharedLibrary.Core.Common.ApplicationExceptions;
using SelectPdf;
using FGLI_SharedLibrary.Core.Common.Utilities;


namespace PartnerFlowAPI.Services.Common
{
    public static class CommonFunctions
    {
        public static string ValidPanOrEmpty(string pan)
        {
            if (string.IsNullOrWhiteSpace(pan))
                return "";

            // Regex: 5 letters + 4 digits + 1 letter
            string pattern = @"^[A-Z]{5}[0-9]{4}[A-Z]{1}$";
            if (Regex.IsMatch(pan, pattern))
                return pan;
            else
                return "";
        }
        public static string ConvertDate(string inputDate)
        {
            // Check if input is null or empty
            if (string.IsNullOrWhiteSpace(inputDate))
            {
                return null;
            }

            DateTime parsedDate;

            // Try to parse in DD/MM/YYYY format first
            if (DateTime.TryParseExact(inputDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                // Return the original date if it is already in the desired format
                return parsedDate.ToString("dd/MM/yyyy");
            }
            // Try to parse in MM/YYYY format and convert it to 01/MM/YYYY
            else if (DateTime.TryParseExact(inputDate, "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                // Set the day to 1 and return in dd/MM/yyyy format
                return new DateTime(parsedDate.Year, parsedDate.Month, 1).ToString("dd/MM/yyyy");
            }
            else
            {
                // Return null or an error message if the format is not recognized
                return null;
            }
        }
        public static TimeSpan? ConvertStringToTimeSpan(string? timeSpanString)
        {
            if (string.IsNullOrEmpty(timeSpanString))
            {
                return null;
            }

            if (TimeSpan.TryParse(timeSpanString, out TimeSpan result))
            {
                return result;
            }

            return null; // Return null if parsing fails
        }

        public static void GetHashAndTimestamp(string apiSecret, out string hash, out long timestamp)
        {
            timestamp = GetTimestamp();
            hash = GetHash(apiSecret + "|" + timestamp);
        }

        private static long GetTimestamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        private static string GetHash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string? ConvertTimeSpanToString(TimeSpan? timeSpan)
        {
            return timeSpan?.ToString();
        }
        public static string GetUploadedFilePath(string Approot, string Foldername, string applicationno, string DocumentType, string Filename)
        {
            string filepath = Path.Combine(Approot, "Upload");
            filepath = Path.Combine(filepath, applicationno);
            filepath = Path.Combine(filepath, DocumentType);
            filepath = Path.Combine(filepath, Filename);
            return filepath;
        }
        public static string GetSubTypeName(List<AppDocumentType> subtyplist, int IntSubTypeID)
        {
            return subtyplist.Where(c => c.IntDocumentTypeID == IntSubTypeID).Select(s => s.VcDocumentTypeName).FirstOrDefault()!;
        }

        public static string RemovePrefix(this string inputString)
        {
            string result = inputString;
            List<string> prefixes = new List<string>() { "vc", "bit", "dt", "bt", "int", "ch", "dc", "ft" };
            foreach (string prefix in prefixes)
            {
                if (inputString.StartsWith(prefix))
                {
                    result = inputString.Substring(prefix.Length);
                    break;
                }
            }
            return result;
        }

        public static async Task<string> ReadJsonFile(IFormFile file)
        {
            string json = string.Empty;
            using (var streamReader = new StreamReader(file.OpenReadStream()))
            {
                var jsonString = await streamReader.ReadToEndAsync();
                json = jsonString;
            }
            return json;
        }

        public static int CalculateAge(string dob)
        {
            // Parse the date of birth from the input string
            if (DateTime.TryParseExact(dob, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthDate))
            {
                // Calculate the age
                DateTime currentDate = DateTime.Now;
                int age = currentDate.Year - birthDate.Year;

                // Adjust age if the birthday hasn't occurred yet this year
                if (currentDate.Month < birthDate.Month || (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day))
                {
                    age--;
                }

                return age;
            }
            else
            {
                // Handle invalid date format
                throw new ApiException("Invalid date of birth format. Please use dd/MM/yyyy.");
            }
        }
        public static bool IsMinor(string dob)
        {
            // Parse the date of birth from the input string
            if (DateTime.TryParseExact(dob, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthDate))
            {
                // Calculate the age
                DateTime currentDate = DateTime.Now;
                int age = currentDate.Year - birthDate.Year;

                // Adjust age if the birthday hasn't occurred yet this year
                if (currentDate.Month < birthDate.Month || (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day))
                {
                    age--;
                }

                // Check if the age is less than 18
                return age < 18;
            }
            else
            {
                // Handle invalid date format
                return false;
            }
        }
        public static bool IsMinor(DateTime? dob)
        {
            if (dob.HasValue)
            {
                // Calculate the age
                DateTime currentDate = DateTime.Now;
                int age = currentDate.Year - dob.Value.Year;

                // Adjust age if the birthday hasn't occurred yet this year
                if (currentDate.Month < dob.Value.Month || (currentDate.Month == dob.Value.Month && currentDate.Day < dob.Value.Day))
                {
                    age--;
                }

                // Check if the age is less than 18
                return age < 18;
            }
            else
            {
                // Handle null value
                return false;
            }
        }


        public static string GetOTP(int ComplexType, int OTPLength)
        {
            string OTP = "";
            string allowedChars = "1,2,3,4,5,6,7,8,9,0";

            if (ComplexType == 1 || ComplexType == 2)
                allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";

            if (ComplexType == 2)
                allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string IDString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < OTPLength; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                OTP = IDString;
            }
            return OTP;
        }
        public static string GetDisplayNameFromEnumValue<TEnum>(int enumValue) where TEnum : Enum
        {
            var enumType = typeof(TEnum);
            var memberInfo = enumType.GetMember(Enum.GetName(enumType, enumValue));

            if (memberInfo.Length > 0)
            {
                var displayAttribute = (DisplayAttribute)Attribute.GetCustomAttribute(memberInfo[0], typeof(DisplayAttribute));

                if (displayAttribute != null)
                {
                    return displayAttribute.Name;
                }
            }

            // Default to the string representation of the enum value if no display name is found
            return enumValue.ToString();
        }

        public static string ReadHTM(string path, string filename)
        {
            string filePath = Path.Combine(path, "PDFTemplate", filename);

            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            else
            {
                throw new KeyNotFoundException($"PDF template not found: {filePath}");
            }
        }
        public static byte[] GetFileBytes(string htmlContent)
        {
            try
            {
                // Instantiate the html to pdf converter
                HtmlToPdf converter = new HtmlToPdf
                {
                    Options = {
                    MarginTop = 25,
                    PdfPageSize = PdfPageSize.A4,
                    PdfPageOrientation = PdfPageOrientation.Portrait
                }
                };

                // Convert HTML string to PDF document
                PdfDocument doc = converter.ConvertHtmlString(htmlContent);

                // Save the document to a memory stream
                using (var ms = new MemoryStream())
                {
                    doc.Save(ms);
                    doc.Close();

                    // Return the byte array from the memory stream
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while converting HTML to PDF: {ex.Message}");
                // You can choose to throw the exception or return an empty byte array based on your requirement
                throw;
            }
        }

        public static MemoryStream GetFileMemoryStream(string htmlContent)
        {
            try
            {
                // Instantiate the html to pdf converter
                HtmlToPdf converter = new HtmlToPdf
                {
                    Options = {
                MarginTop = 25,
                PdfPageSize = PdfPageSize.A4,
                PdfPageOrientation = PdfPageOrientation.Portrait
            }
                };

                // Convert HTML string to PDF document
                PdfDocument doc = converter.ConvertHtmlString(htmlContent);

                // Save the document to a memory stream
                var ms = new MemoryStream();
                doc.Save(ms);
                doc.Close();

                // Reset the memory stream position to the beginning
                ms.Position = 0;

                // Return the memory stream
                return ms;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while converting HTML to PDF: {ex.Message}");
                // You can choose to throw the exception or return a null or empty memory stream based on your requirement
                throw;
            }
        }

        public static Dictionary<string, object> FlattenJson(string json)
        {
            var jsonObject = JObject.Parse(json);
            var flatJson = new Dictionary<string, object>();
            FlattenToken(jsonObject, parentKey: "", flatJson);
            return flatJson;
        }

        public static string GetValidFileName(DateTime dateTime, string applicationNumber, string fileName)
        {
            // Format date and time portion: ddMMyyyyHHmmss
            string dateTimePart = dateTime.ToString("ddMMyyyyHHmmss");

            // Combine parts to create the valid file name
            string validFileName = $"{applicationNumber}_{dateTimePart}_{SharedFunction.ConvertFilename(fileName)}";

            return validFileName;
        }
        public static string GetUWValidFileName(DateTime dateTime, string applicationNumber, string fileName, string code)
        {
            // Format date and time portion: ddMMyyyyHHmmss
            string dateTimePart = dateTime.ToString("ddMMyyyyHHmmss");

            // Combine parts to create the valid file name
            string validFileName = $"New_Business_{applicationNumber}_{dateTimePart}_{code}_{SharedFunction.ConvertFilename(fileName)}";

            return validFileName;
        }
        private static void FlattenToken(JToken token, string parentKey, Dictionary<string, object> flatJson)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    foreach (var prop in token.Children<JProperty>())
                    {
                        var propName = string.IsNullOrEmpty(parentKey) ? prop.Name : $"{parentKey}.{prop.Name}";
                        FlattenToken(prop.Value, propName, flatJson);
                    }
                    break;
                case JTokenType.Array:
                    int index = 0;
                    foreach (var value in token.Children())
                    {
                        FlattenToken(value, $"{parentKey}[{index}]", flatJson);
                        index++;
                    }
                    break;
                default:
                    flatJson[parentKey] = token.ToObject<object>();
                    break;
            }
        }

        public static string ReplaceHtmlPlaceholdersVersionold(string htmlTemplate, Dictionary<string, object> DataKeyValue)
        {
            // Replace placeholders in the HTML template
            return DataKeyValue.Aggregate(htmlTemplate, (current, kvp) =>
                System.Text.RegularExpressions.Regex.Replace(current, $@"\b{System.Text.RegularExpressions.Regex.Escape(kvp.Key)}\b", kvp.Value?.ToString() ?? string.Empty));

            // return htmlTemplate;
        }
        public static string ReplaceHtmlPlaceholders(string htmlTemplate, Dictionary<string, object> DataKeyValue)
        {
            // Replace placeholders in the HTML template
            foreach (var kvp in DataKeyValue.Where(c => c.Value != null))
            {
                string placeholder = $"[{kvp.Key}]";
                string replacement = kvp.Value.ToString()!;
                htmlTemplate = htmlTemplate.Replace(placeholder, replacement, StringComparison.CurrentCulture);
            }

            return htmlTemplate;
        }
        public static string? ReplaceGenderCheck(string? gender)
        {
            if (!string.IsNullOrEmpty(gender))
            {

                if (gender == "M") return gender.Replace("M", "true");
                if (gender == "F") return gender.Replace("F", "true");
                if (gender == "T") return gender.Replace("T", "true");
                return "false";
            }
            return string.Empty;
        }
        public static string ExtractBaseUrl(string originalUrl)
        {
            int questionMarkIndex = originalUrl.IndexOf('?');
            if (questionMarkIndex != -1)
            {
                return originalUrl.Substring(0, questionMarkIndex);
            }
            return originalUrl;
        }
        public static string GetFileName(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            }

            // Extract the file name from the URL
            var uri = new Uri(url);
            var fileName = System.IO.Path.GetFileName(uri.LocalPath.Split(".")[0]);

            return fileName;
        }

        public static string GetFileExtension(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            }

            // Extract the file extension from the URL
            var uri = new Uri(url);
            var fileExtension = System.IO.Path.GetExtension(uri.LocalPath.Split(".")[1]);

            return fileExtension;
        }
        public static string GetEnumValueFromDisplayName<TEnum>(string displayName) where TEnum : Enum
        {
            var enumType = typeof(TEnum);

            foreach (var field in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();

                if (displayAttribute != null && displayAttribute.Name == displayName)
                {
                    var enumValue = (int)field.GetValue(null);
                    return enumValue.ToString();
                }
            }

            // Default to null if no matching display name is found
            return null;
        }
        public static int ConvertNullableDecimalToInt(decimal? value)
        {
            if (!value.HasValue) return 0;

            // Assuming you want to truncate the decimal part
            return (int)value.Value;


        }

        //public static byte[] GetFileBytes(string filestring)
        //{
        //    byte[] pdfbytes = null;
        //    // Instantiate the html to pdf converter
        //    HtmlToPdf converter = new HtmlToPdf();
        //    converter.Options.MarginTop = 25;
        //    converter.Options.PdfPageSize = PdfPageSize.A4;
        //    converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

        //    PdfDocument doc = converter.ConvertHtmlString(filestring);
        //    using (var ms = new MemoryStream())
        //    {
        //        doc.Save(ms);
        //        pdfbytes = ms.ToArray();
        //    }
        //    return pdfbytes;
        //}


        //public static byte[] GetFileBytes(string filestring)
        //{
        //    byte[] pdfbytes = null;

        //    // Set the path to 'libwkhtmltox.dll'

        //    var globalSettings = new GlobalSettings
        //    {
        //        ColorMode = ColorMode.Color,
        //        Orientation = Orientation.Portrait,
        //        PaperSize = PaperKind.A4,
        //        Margins = new MarginSettings { Top = 25 },
        //    };

        //    var objectSettings = new ObjectSettings
        //    {
        //        PagesCount = true,
        //        HtmlContent = filestring,
        //        WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/css", "styles.css") },
        //    };

        //    var converter = new SynchronizedConverter(new PdfTools());

        //    pdfbytes = converter.Convert(new HtmlToPdfDocument()
        //    {
        //        GlobalSettings = globalSettings,
        //        Objects = { objectSettings }
        //    });

        //    return pdfbytes;
        //}

        public static string GetPartitionKey(HttpContext httpContext)
        {
            var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var apiKey = httpContext.Request.Headers["X-ClientId"].FirstOrDefault() ?? "anonymous";
            var endpoint = httpContext.Request.Path.ToString();
            return $"{ip}:{apiKey}:{endpoint}";
        }
    }
}
