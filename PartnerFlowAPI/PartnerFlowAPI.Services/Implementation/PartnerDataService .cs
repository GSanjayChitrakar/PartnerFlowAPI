using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using PartnerFlowAPI.Database.Context;
using PartnerFlowAPI.Database.Entities;
using PartnerFlowAPI.Domain.Entities;
using PartnerFlowAPI.Models.Dtos;
using PartnerFlowAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using YourNamespace.Domain.Entities;
using YourNamespace.Entities;

namespace PartnerFlowAPI.Services.Implementation
{
    public class PartnerDataService : IPartnerDataService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public PartnerDataService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ValidationResultModel> ProcessPartnerDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            // 1️⃣ Get allowed fields for this partner
            var allowedFields = await _context.PartnerSections
                .Where(ps => ps.PartnerId == partnerId)
                .Join(
                    _context.SectionFields.Include(sf => sf.Field),
                    ps => ps.SectionId,
                    sf => sf.SectionId,
                    (ps, sf) => sf.Field
                )
                .Where(f => !f.IsDeleted)
                .Select(f => new FieldDefinition
                {
                    FieldName = f.FieldName,
                    DataType = f.DataType,
                    Length = f.Length
                })
                .Distinct()
                .ToListAsync();

            var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

            // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
            var normalizedPayload = payload.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
            );

            // 3️⃣ Detect if nested JSON exists
            bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

            JObject dataObject = hasNestedObject
                ? FlattenJson(JObject.FromObject(normalizedPayload))
                : JObject.FromObject(normalizedPayload);

            // 4️⃣ Validate each field
            foreach (var prop in dataObject.Properties())
            {
                var key = prop.Name.ToLower();
                var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                if (!fieldDict.ContainsKey(key))
                {
                    invalidFields.Add($"{prop.Name} (Field Not Configured)");
                    continue;
                }

                var field = fieldDict[key];

                // Validate datatype
                if (!IsValidDataType(value, field.DataType))
                {
                    invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                    continue;
                }

                // Validate length
                if (field.Length.HasValue && value?.Length > field.Length)
                {
                    invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                }
            }

            result.InvalidFields = invalidFields;
            result.Success = invalidFields.Count == 0;

            if (result.Success)
            {
                var entity = new tblPartnerSuitability
                {
                    //check nul in all the apis wherever green line is visible
                    GdSuitabilityId = Guid.NewGuid(),
                    VcJourneyId = GetValueIgnoreCase(dataObject, "journeyID"),
                    VcSalutation = GetValueIgnoreCase(dataObject, "salutation"),
                    VcLeadID = GetValueIgnoreCase(dataObject, "leadID"),
                    VcName = GetValueIgnoreCase(dataObject, "LAName"),
                    VcFirstName = GetValueIgnoreCase(dataObject, "LAFirstName"),
                    VcMiddleName = GetValueIgnoreCase(dataObject, "LAMiddleName"),
                    VcLastName = GetValueIgnoreCase(dataObject, "LALastName"),
                    VcDOB = GetValueIgnoreCase(dataObject, "laDOB"),
                    DtDOB = ParseDateNullable(GetValueIgnoreCase(dataObject, "laDOB")),
                    IntNationality = ParseInt(GetValueIgnoreCase(dataObject, "Nationality")) ?? 0,
                    ChGender = (GetValueIgnoreCase(dataObject, "gender") ?? string.Empty).FirstOrDefault(),
                    VcMobile = GetValueIgnoreCase(dataObject, "laMobile"),
                    VcInternationalMobile = GetValueIgnoreCase(dataObject, "LAInternationalMobile"),
                    VcEmail = GetValueIgnoreCase(dataObject, "laEmail"),
                    BtIsSmoker = ParseBool(GetValueIgnoreCase(dataObject, "isLASmoker")) ?? false,
                    VcLifeStage = GetValueIgnoreCase(dataObject, "lifeStage"),
                    VcRiskProfile = GetValueIgnoreCase(dataObject, "proposerRiskProfile"),
                    DcAnnualIncome = ParseDecimal(GetValueIgnoreCase(dataObject, "proposerAnnualIncome")),
                    IntExistingInsurance = ParseInt(GetValueIgnoreCase(dataObject, "existingInsurance")),
                    DcExistingSumAssured = ParseDecimal(GetValueIgnoreCase(dataObject, "existingSumAssured")),
                    IntLifeGoals = ParseInt(GetValueIgnoreCase(dataObject, "lifeGoals")),
                    IntPolicyTerm = ParseInt(GetValueIgnoreCase(dataObject, "policyTerm")) ?? 0,
                    DcGoalCurrentValue = ParseDecimal(GetValueIgnoreCase(dataObject, "goalCurrentValue")),
                    DcTimeToAchieveGoal = ParseDecimal(GetValueIgnoreCase(dataObject, "timeToAchieveGoal")),
                    DcGoalFutureValue = ParseDecimal(GetValueIgnoreCase(dataObject, "goalFutureValue")),
                    VcProductCategory = GetValueIgnoreCase(dataObject, "productCategory"),
                    VcScheme = GetValueIgnoreCase(dataObject, "scheme"),
                    VcLastAccessIP = "0.0.0.0", // need to get properip
                    VcCreatedBy = partnerName ?? "system", // partnername
                    DtCreateDate = DateTime.Now, // everywhere
                    BitIsDeleted = false
                };

                _context.partnerSuitabilities.Add(entity);
                await _context.SaveChangesAsync();
            }

            return result;
        }



        public async Task<ValidationResultModel> AgentDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            // 1️⃣ Get allowed fields for this partner
            var allowedFields = await _context.PartnerSections
                .Where(ps => ps.PartnerId == partnerId)
                .Join(
                    _context.SectionFields.Include(sf => sf.Field),
                    ps => ps.SectionId,
                    sf => sf.SectionId,
                    (ps, sf) => sf.Field
                )
                .Where(f => !f.IsDeleted)
                .Select(f => new FieldDefinition
                {
                    FieldName = f.FieldName,
                    DataType = f.DataType,
                    Length = f.Length
                })
                .Distinct()
                .ToListAsync();

            var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

            // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
            var normalizedPayload = payload.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
            );

            // 3️⃣ Detect if nested JSON exists
            bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

            JObject dataObject = hasNestedObject
                ? FlattenJson(JObject.FromObject(normalizedPayload))
                : JObject.FromObject(normalizedPayload);

            // 4️⃣ Validate each field
            foreach (var prop in dataObject.Properties())
            {
                var key = prop.Name.ToLower();
                var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                if (!fieldDict.ContainsKey(key))
                {
                    invalidFields.Add($"{prop.Name} (Field Not Configured)");
                    continue;
                }

                var field = fieldDict[key];

                // Validate datatype
                if (!IsValidDataType(value, field.DataType))
                {
                    invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                    continue;
                }

                // Validate length
                if (field.Length.HasValue && value?.Length > field.Length)
                {
                    invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                }
            }

            result.InvalidFields = invalidFields;
            result.Success = invalidFields.Count == 0;

            var partnerData = await _context.tblPartnerDatas
                                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                                    .Select(p => new { p.ApplicationNumber })
                                    .FirstOrDefaultAsync();

            if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
            {
                result.Success = false;
                result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                return result;
            }

            if (result.Success)
            {
                tblPF_AgentDetails? entity = null;
                try
                {
                    entity = new tblPF_AgentDetails
                    {
                        VcApplicationNumber = partnerData.ApplicationNumber,
                        AgentFirstName = GetValueIgnoreCase(dataObject, "AgentFirstName"),
                        AgentMiddleName = GetValueIgnoreCase(dataObject, "AgentMiddleName"),
                        AgentLastName = GetValueIgnoreCase(dataObject, "AgentLastName"),
                        AgentCode = GetValueIgnoreCase(dataObject, "AgentCode"),
                        ChannelCode = GetValueIgnoreCase(dataObject, "ChannelCode"),
                        MobileNumber = GetValueIgnoreCase(dataObject, "MobileNumber"),
                        BranchName = GetValueIgnoreCase(dataObject, "BranchName"),
                        BranchCode = GetValueIgnoreCase(dataObject, "BranchCode"),
                        DesignationCode = GetValueIgnoreCase(dataObject, "DesignationCode"),
                        DesignationDescription = GetValueIgnoreCase(dataObject, "DesignationDescription"),
                        EmailAddress = GetValueIgnoreCase(dataObject, "EmailAddress"),
                        MasterAgencyCode = GetValueIgnoreCase(dataObject, "MasterAgencyCode"),
                        Title = GetValueIgnoreCase(dataObject, "Title"),
                        VcLastAccessIp = GetValueIgnoreCase(dataObject, "VcLastAccessIp"),
                        DtCreateDate = DateTime.Now,
                        VcCreatedBy = partnerName ?? "system",
                        BitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "BitIsDeleted")) ?? false

                    };

                    _context.tblPF_AgentDetails.Add(entity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    // 🧩 Extract detailed SQL-level info
                    var sqlEx = ex.InnerException ?? ex;
                    string message = sqlEx.Message;

                    // 🔍 Log every property value to help find the culprit
                    var entityValues = string.Join(Environment.NewLine,
                        _context.Entry(entity).CurrentValues.Properties
                            .Select(p => $"{p.Name}: {_context.Entry(entity).CurrentValues[p] ?? "NULL"}"));

                    result.Success = false;
                    result.InvalidFields.Add("Database Error while saving Agent Details");
                    result.ErrorMessage = $"Error: {message}\n\nField Values:\n{entityValues}";

                    // Optional: log to file or console for debugging
                    Console.WriteLine("❌ Save Error: " + message);
                    Console.WriteLine("⚠️ Entity Values:\n" + entityValues);
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.InvalidFields.Add("Unexpected error while saving data");
                    result.ErrorMessage = ex.InnerException?.Message ?? ex.Message;

                    Console.WriteLine("💥 General Exception: " + result.ErrorMessage);
                }
            }
            return result;
        }


        public async Task<ValidationResultModel> BankAccountDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            // 1️⃣ Get allowed fields for this partner
            var allowedFields = await _context.PartnerSections
                .Where(ps => ps.PartnerId == partnerId)
                .Join(
                    _context.SectionFields.Include(sf => sf.Field),
                    ps => ps.SectionId,
                    sf => sf.SectionId,
                    (ps, sf) => sf.Field
                )
                .Where(f => !f.IsDeleted)
                .Select(f => new FieldDefinition
                {
                    FieldName = f.FieldName,
                    DataType = f.DataType,
                    Length = f.Length
                })
                .Distinct()
                .ToListAsync();

            var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

            // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
            var normalizedPayload = payload.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
            );

            // 3️⃣ Detect if nested JSON exists
            bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

            JObject dataObject = hasNestedObject
                ? FlattenJson(JObject.FromObject(normalizedPayload))
                : JObject.FromObject(normalizedPayload);

            // 4️⃣ Validate each field
            foreach (var prop in dataObject.Properties())
            {
                var key = prop.Name.ToLower();
                var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                if (!fieldDict.ContainsKey(key))
                {
                    invalidFields.Add($"{prop.Name} (Field Not Configured)");
                    continue;
                }

                var field = fieldDict[key];

                // Validate datatype
                if (!IsValidDataType(value, field.DataType))
                {
                    invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                    continue;
                }

                // Validate length
                if (field.Length.HasValue && value?.Length > field.Length)
                {
                    invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                }
            }

            result.InvalidFields = invalidFields;
            result.Success = invalidFields.Count == 0;

            var partnerData = await _context.tblPartnerDatas
                                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                                    .Select(p => new { p.ApplicationNumber })
                                    .FirstOrDefaultAsync();

            if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
            {
                result.Success = false;
                result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                return result;
            }

            if (result.Success)
            {
                var entity = new tblPF_BankAccountData
                {
                    VcApplicationNumber = partnerData.ApplicationNumber,
                    IntAssureType = ParseInt(GetValueIgnoreCase(dataObject, "IntAssureType")) ?? 0,
                    VcRequestAccountName = GetValueIgnoreCase(dataObject, "VcRequestAccountName") ?? string.Empty,
                    VcRequestAccountNumber = GetValueIgnoreCase(dataObject, "VcRequestAccountNumber") ?? string.Empty,
                    VcRequestAccountIFSC = GetValueIgnoreCase(dataObject, "VcRequestAccountIFSC") ?? string.Empty,
                    VcResponseAccountName = GetValueIgnoreCase(dataObject, "VcResponseAccountName"),
                    VcResponseAccountNumber = GetValueIgnoreCase(dataObject, "VcResponseAccountNumber"),
                    VcResponseAccountIFSC = GetValueIgnoreCase(dataObject, "VcResponseAccountIFSC"),
                    VcResponseBankResponse = GetValueIgnoreCase(dataObject, "VcResponseBankResponse"),
                    BtResponseBankTxnStatus = ParseBool(GetValueIgnoreCase(dataObject, "BtResponseBankTxnStatus")),
                    VcResponseBankRRN = GetValueIgnoreCase(dataObject, "VcResponseBankRRN"),
                    VcResponseStatusCode = GetValueIgnoreCase(dataObject, "VcResponseStatusCode"),
                    BtResponseIsValid = ParseBool(GetValueIgnoreCase(dataObject, "BtResponseIsValid")),
                    VcResponseIdentifier = GetValueIgnoreCase(dataObject, "VcResponseIdentifier"),
                    FtResponseNameMatchScore = ParseDouble(GetValueIgnoreCase(dataObject, "FtResponseNameMatchScore")),
                    VcResponseValidity = GetValueIgnoreCase(dataObject, "VcResponseValidity"),
                    VcStatus = GetValueIgnoreCase(dataObject, "VcStatus"),
                    VcErrorDescription = GetValueIgnoreCase(dataObject, "VcErrorDescription"),
                    VcLastAccessIP = GetValueIgnoreCase(dataObject, "VcLastAccessIP") ?? "0.0.0.0",
                    VcCreatedBy = partnerName ?? "system",
                    DtCreateDate = DateTime.Now,
                    BitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "BitIsDeleted")) ?? false
                };

                _context.tblPF_BankAccountDatas.Add(entity);
                await _context.SaveChangesAsync();
            }

            return result;
        }


        public async Task<ValidationResultModel> CommunicationDetailstDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            // 1️⃣ Get allowed fields for this partner
            var allowedFields = await _context.PartnerSections
                .Where(ps => ps.PartnerId == partnerId)
                .Join(
                    _context.SectionFields.Include(sf => sf.Field),
                    ps => ps.SectionId,
                    sf => sf.SectionId,
                    (ps, sf) => sf.Field
                )
                .Where(f => !f.IsDeleted)
                .Select(f => new FieldDefinition
                {
                    FieldName = f.FieldName,
                    DataType = f.DataType,
                    Length = f.Length
                })
                .Distinct()
                .ToListAsync();

            var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

            // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
            var normalizedPayload = payload.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
            );

            // 3️⃣ Detect if nested JSON exists
            bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

            JObject dataObject = hasNestedObject
                ? FlattenJson(JObject.FromObject(normalizedPayload))
                : JObject.FromObject(normalizedPayload);

            // 4️⃣ Validate each field
            foreach (var prop in dataObject.Properties())
            {
                var key = prop.Name.ToLower();
                var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                if (!fieldDict.ContainsKey(key))
                {
                    invalidFields.Add($"{prop.Name} (Field Not Configured)");
                    continue;
                }

                var field = fieldDict[key];

                // Validate datatype
                if (!IsValidDataType(value, field.DataType))
                {
                    invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                    continue;
                }

                // Validate length
                if (field.Length.HasValue && value?.Length > field.Length)
                {
                    invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                }
            }

            result.InvalidFields = invalidFields;
            result.Success = invalidFields.Count == 0;

            var partnerData = await _context.tblPartnerDatas
                                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                                    .Select(p => new { p.ApplicationNumber })
                                    .FirstOrDefaultAsync();

            if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
            {
                result.Success = false;
                result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                return result;
            }

            if (result.Success)
            {
                var entity = new tblPF_CommunicationDetails
                {
                    VcApplicationNumber = partnerData.ApplicationNumber,
                    IntAssureType = ParseInt(GetValueIgnoreCase(dataObject, "IntAssureType")) ?? 0,

                    VcCAAddressLine1 = GetValueIgnoreCase(dataObject, "VcCAAddressLine1"),
                    VcCAAddressLine2 = GetValueIgnoreCase(dataObject, "VcCAAddressLine2"),
                    VcCAAddressLine3 = GetValueIgnoreCase(dataObject, "VcCAAddressLine3"),
                    VcCALandmark = GetValueIgnoreCase(dataObject, "VcCALandmark"),
                    VcCAPincode = GetValueIgnoreCase(dataObject, "VcCAPincode"),
                    VcCACity = GetValueIgnoreCase(dataObject, "VcCACity"),
                    VcCAState = GetValueIgnoreCase(dataObject, "VcCAState"),
                    VcCACountry = GetValueIgnoreCase(dataObject, "VcCACountry"),

                    BtIsPASameCA = ParseBool(GetValueIgnoreCase(dataObject, "BtIsPASameCA")) ?? false,

                    VcPAAddressLine1 = GetValueIgnoreCase(dataObject, "VcPAAddressLine1"),
                    VcPAAddressLine2 = GetValueIgnoreCase(dataObject, "VcPAAddressLine2"),
                    VcPAAddressLine3 = GetValueIgnoreCase(dataObject, "VcPAAddressLine3"),
                    VcPALandmark = GetValueIgnoreCase(dataObject, "VcPALandmark"),
                    VcPAPincode = GetValueIgnoreCase(dataObject, "VcPAPincode"),
                    VcPACity = GetValueIgnoreCase(dataObject, "VcPACity"),
                    VcPAState = GetValueIgnoreCase(dataObject, "VcPAState"),
                    VcPACountry = GetValueIgnoreCase(dataObject, "VcPACountry"),

                    VcMobileNumber = GetValueIgnoreCase(dataObject, "VcMobileNumber"),
                    VcAlternateNumber = GetValueIgnoreCase(dataObject, "VcAlternateNumber"),
                    VcWorkContactNumber = GetValueIgnoreCase(dataObject, "VcWorkContactNumber"),
                    VcInterNationalNumber = GetValueIgnoreCase(dataObject, "VcInterNationalNumber"),
                    VcEmailAddress = GetValueIgnoreCase(dataObject, "VcEmailAddress"),
                    BtIsEditable = ParseBool(GetValueIgnoreCase(dataObject, "BtIsEditable")) ?? true,

                    VcLastAccessIP = "0.0.0.0",
                    VcCreatedBy = partnerName ?? "system",
                    DtCreateDate = DateTime.Now,
                    BitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "BitIsDeleted")) ?? false,
                    BtKYCAddressUpdate = ParseBool(GetValueIgnoreCase(dataObject, "BtKYCAddressUpdate"))
                };

                _context.tblPF_CommunicationDetails.Add(entity);
                await _context.SaveChangesAsync();
            }

            return result;
        }


        public async Task<ValidationResultModel> EmploymentDetailstDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            try
            {
                // 1️⃣ Get allowed fields for this partner
                var allowedFields = await _context.PartnerSections
                    .Where(ps => ps.PartnerId == partnerId)
                    .Join(
                        _context.SectionFields.Include(sf => sf.Field),
                        ps => ps.SectionId,
                        sf => sf.SectionId,
                        (ps, sf) => sf.Field
                    )
                    .Where(f => !f.IsDeleted)
                    .Select(f => new FieldDefinition
                    {
                        FieldName = f.FieldName,
                        DataType = f.DataType,
                        Length = f.Length
                    })
                    .Distinct()
                    .ToListAsync();

                var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

                // 2️⃣ Normalize payload (convert any JsonElement values to .NET types)
                var normalizedPayload = payload.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
                );

                // 3️⃣ Detect if nested JSON exists
                bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);
                JObject dataObject = hasNestedObject
                    ? FlattenJson(JObject.FromObject(normalizedPayload))
                    : JObject.FromObject(normalizedPayload);

                // 4️⃣ Validate each field
                foreach (var prop in dataObject.Properties())
                {
                    var key = prop.Name.ToLower();
                    var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                    if (!fieldDict.ContainsKey(key))
                    {
                        invalidFields.Add($"{prop.Name} (Field Not Configured)");
                        continue;
                    }

                    var field = fieldDict[key];

                    // Validate datatype
                    if (!IsValidDataType(value, field.DataType))
                    {
                        invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                        continue;
                    }

                    // Validate length
                    if (field.Length.HasValue && value?.Length > field.Length)
                    {
                        invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                    }
                }

                result.InvalidFields = invalidFields;
                result.Success = invalidFields.Count == 0;

                var partnerData = await _context.tblPartnerDatas
                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                    .Select(p => new { p.ApplicationNumber })
                    .FirstOrDefaultAsync();

                if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
                {
                    result.Success = false;
                    result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                    return result;
                }

                if (result.Success)
                {
                    var entity = new tblPF_EmploymentDetails
                    {
                        vcApplicationNumber = partnerData.ApplicationNumber,
                        intAssureType = ParseInt(GetValueIgnoreCase(dataObject, "intAssureType")) ?? 0,
                        vcOccupation = GetValueIgnoreCase(dataObject, "vcOccupation"),
                        btHazardousEnvironment = ParseBool(GetValueIgnoreCase(dataObject, "btHazardousEnvironment")) ?? false,
                        vcWorkDomain = GetValueIgnoreCase(dataObject, "vcWorkDomain"),
                        vcFirmOrEmployerName = GetValueIgnoreCase(dataObject, "vcFirmOrEmployerName"),
                        vcNatureOfBusiness = GetValueIgnoreCase(dataObject, "vcNatureOfBusiness"),
                        vcNatureOfDuties = GetValueIgnoreCase(dataObject, "vcNatureOfDuties"),
                        vcDesignation = GetValueIgnoreCase(dataObject, "vcDesignation"),
                        dcAnnualIncome = ParseDecimal(GetValueIgnoreCase(dataObject, "dcAnnualIncome")),
                        vcBusinessAddress1 = GetValueIgnoreCase(dataObject, "vcBusinessAddress1"),
                        vcBusinessAddress2 = GetValueIgnoreCase(dataObject, "vcBusinessAddress2"),
                        vcBusinessAddress3 = GetValueIgnoreCase(dataObject, "vcBusinessAddress3"),
                        vcBusinessRoadName = GetValueIgnoreCase(dataObject, "vcBusinessRoadName"),
                        vcBusinessAddressLandmark = GetValueIgnoreCase(dataObject, "vcBusinessAddressLandmark"),
                        vcBusinessCity = GetValueIgnoreCase(dataObject, "vcBusinessCity"),
                        vcBusinessState = GetValueIgnoreCase(dataObject, "vcBusinessState"),
                        vcBusinessCountry = GetValueIgnoreCase(dataObject, "vcBusinessCountry"),
                        vcBusinessPincode = GetValueIgnoreCase(dataObject, "vcBusinessPincode"),
                        vcStudingInClass = GetValueIgnoreCase(dataObject, "vcStudingInClass"),
                        dcInsuranceCover = ParseDecimal(GetValueIgnoreCase(dataObject, "dcInsuranceCover")),
                        dcParentAnnualIncome = ParseDecimal(GetValueIgnoreCase(dataObject, "dcParentAnnualIncome")),
                        dcSiblingsInsuranceCover = ParseDecimal(GetValueIgnoreCase(dataObject, "dcSiblingsInsuranceCover")),
                        vcLastAccessIP = "0.0.0.0",
                        vcCreatedBy = partnerName ?? "system",
                        dtCreateDate = DateTime.Now,
                        bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "bitIsDeleted")) ?? false
                        
                    };

                    _context.tblPF_EmploymentDetails.Add(entity);

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.InvalidFields.Add("Database Save Error");

                        // Log the real cause to console or your logger
                        Console.WriteLine("❌ Error saving EmploymentDetails entity:");
                        Console.WriteLine("Message: " + ex.Message);
                        if (ex.InnerException != null)
                            Console.WriteLine("Inner Exception: " + ex.InnerException.Message);

                        // Optionally: capture details in result for debugging
                        result.ErrorMessage = ex.InnerException?.Message ?? ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                // Top-level safety catch for unexpected logic errors
                result.Success = false;
                result.InvalidFields.Add("Unexpected Error");
                result.ErrorMessage = ex.InnerException?.Message ?? ex.Message;

                Console.WriteLine("❌ Top-level error in EmploymentDetailstDataAsync:");
                Console.WriteLine("Message: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
            }

            return result;
        }


        public async Task<ValidationResultModel> FamilyDetailstDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            try
            {
                // 1️⃣ Get allowed fields for this partner
                var allowedFields = await _context.PartnerSections
                    .Where(ps => ps.PartnerId == partnerId)
                    .Join(
                        _context.SectionFields.Include(sf => sf.Field),
                        ps => ps.SectionId,
                        sf => sf.SectionId,
                        (ps, sf) => sf.Field
                    )
                    .Where(f => !f.IsDeleted)
                    .Select(f => new FieldDefinition
                    {
                        FieldName = f.FieldName,
                        DataType = f.DataType,
                        Length = f.Length
                    })
                    .Distinct()
                    .ToListAsync();

                var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

                // 2️⃣ Normalize payload
                var normalizedPayload = payload.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
                );

                // 3️⃣ Flatten if nested JSON exists
                bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);
                JObject dataObject = hasNestedObject
                    ? FlattenJson(JObject.FromObject(normalizedPayload))
                    : JObject.FromObject(normalizedPayload);

                // 4️⃣ Validate each field
                foreach (var prop in dataObject.Properties())
                {
                    var key = prop.Name.ToLower();
                    var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                    if (!fieldDict.ContainsKey(key))
                    {
                        invalidFields.Add($"{prop.Name} (Field Not Configured)");
                        continue;
                    }

                    var field = fieldDict[key];

                    if (!IsValidDataType(value, field.DataType))
                    {
                        invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                        continue;
                    }

                    if (field.Length.HasValue && value?.Length > field.Length)
                    {
                        invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                    }
                }

                result.InvalidFields = invalidFields;
                result.Success = invalidFields.Count == 0;

                var partnerData = await _context.tblPartnerDatas
                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                    .Select(p => new { p.ApplicationNumber })
                    .FirstOrDefaultAsync();

                if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
                {
                    result.Success = false;
                    result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                    return result;
                }

                if (result.Success)
                {
                    string currentField = string.Empty; // 🧭 to trace which property fails
                    try
                    {
                        var entity = new tblPF_FamilyDetails
                        {
                            vcApplicationNumber = partnerData.ApplicationNumber,
                            intAssureType = ParseInt(GetValueIgnoreCase(dataObject, currentField = "intAssureType")) ?? 0,
                            vcRelation = GetValueIgnoreCase(dataObject, currentField = "vcRelation"),
                            vcTitle = GetValueIgnoreCase(dataObject, currentField = "vcTitle"),
                            vcFirstName = GetValueIgnoreCase(dataObject, currentField = "vcFirstName") ?? string.Empty,
                            vcMiddleName = GetValueIgnoreCase(dataObject, currentField = "vcMiddleName"),
                            vcLastName = GetValueIgnoreCase(dataObject, currentField = "vcLastName") ?? string.Empty,
                            dtDOB = ParseDateNullable(GetValueIgnoreCase(dataObject, currentField = "dtDOB")),
                            chGender = GetValueIgnoreCase(dataObject, currentField = "chGender"),
                            dcAnnualIncome = ParseDecimal(GetValueIgnoreCase(dataObject, currentField = "dcAnnualIncome")),
                            vcMobileNumber = GetValueIgnoreCase(dataObject, currentField = "vcMobileNumber"),
                            intTotalLIfeSA = ParseInt(GetValueIgnoreCase(dataObject, currentField = "intTotalLIfeSA")),
                            intVitalStatus = ParseInt(GetValueIgnoreCase(dataObject, currentField = "intVitalStatus")),
                            vcCauseOfDeath = GetValueIgnoreCase(dataObject, currentField = "vcCauseOfDeath"),
                            intAgeAtDeath = ParseInt(GetValueIgnoreCase(dataObject, currentField = "intAgeAtDeath")),
                            vcHealthStatus = GetValueIgnoreCase(dataObject, currentField = "vcHealthStatus"),
                            vcOccupation = GetValueIgnoreCase(dataObject, currentField = "vcOccupation"),
                            btIsAppointee = ParseBool(GetValueIgnoreCase(dataObject, currentField = "btIsAppointee")),
                            vcNameOfAppointee = GetValueIgnoreCase(dataObject, currentField = "vcNameOfAppointee"),
                            vcAppointeeDob = ParseDateNullable(GetValueIgnoreCase(dataObject, currentField = "vcAppointeeDob")),
                            chAppointeeGender = GetValueIgnoreCase(dataObject, currentField = "chAppointeeGender"),
                            vcAppointeeContactNumber = GetValueIgnoreCase(dataObject, currentField = "vcAppointeeContactNumber"),
                            vcAppointeeCkycNumber = GetValueIgnoreCase(dataObject, currentField = "vcAppointeeCkycNumber"),
                            vcAppointeeAddress1 = GetValueIgnoreCase(dataObject, currentField = "vcAppointeeAddress1"),
                            vcAppointeeAddress2 = GetValueIgnoreCase(dataObject, currentField = "vcAppointeeAddress2"),
                            vcAppointeeLandmark = GetValueIgnoreCase(dataObject, currentField = "vcAppointeeLandmark"),
                            vcAppointeeCity = GetValueIgnoreCase(dataObject, currentField = "vcAppointeeCity"),
                            vcAppointeeState = GetValueIgnoreCase(dataObject, currentField = "vcAppointeeState"),
                            vcAppointeeCountry = GetValueIgnoreCase(dataObject, currentField = "vcAppointeeCountry"),
                            vcAppointeePincode = GetValueIgnoreCase(dataObject, currentField = "vcAppointeePincode"),
                            btAppointeeAddressSameAsNominee = ParseBool(GetValueIgnoreCase(dataObject, currentField = "btAppointeeAddressSameAsNominee")),
                            vcRelationshipWithNominee = GetValueIgnoreCase(dataObject, currentField = "vcRelationshipWithNominee"),
                            vcAppointeeSignature = GetValueIgnoreCase(dataObject, currentField = "vcAppointeeSignature"),
                            intAgeAtOnset = ParseInt(GetValueIgnoreCase(dataObject, currentField = "intAgeAtOnset")),
                            vcLivingOrDeceased = GetValueIgnoreCase(dataObject, currentField = "vcLivingOrDeceased"),
                            vcDiagnosis = GetValueIgnoreCase(dataObject, currentField = "vcDiagnosis"),
                            btIsNominee = ParseBool(GetValueIgnoreCase(dataObject, currentField = "btIsNominee")),
                            ftNomineePercentage = (double?)ParseDecimal(GetValueIgnoreCase(dataObject, currentField = "ftNomineePercentage")),
                            vcLastAccessIP = "0.0.0.0",
                            vcCreatedBy = partnerName ?? "system",
                            dtCreateDate = DateTime.Now,
                            bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, currentField = "bitIsDeleted")) ?? false
                        };

                        _context.tblPF_FamilyDetails.Add(entity);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.InvalidFields.Add($"❌ Error processing field: {currentField} → {ex.Message}");
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.InvalidFields.Add($"❌ General Error: {ex.Message}");
            }

            return result;
        }


        public async Task<ValidationResultModel> FATCADetailstDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            try
            {
                // 1️⃣ Get allowed fields for this partner
                var allowedFields = await _context.PartnerSections
                    .Where(ps => ps.PartnerId == partnerId)
                    .Join(
                        _context.SectionFields.Include(sf => sf.Field),
                        ps => ps.SectionId,
                        sf => sf.SectionId,
                        (ps, sf) => sf.Field
                    )
                    .Where(f => !f.IsDeleted)
                    .Select(f => new FieldDefinition
                    {
                        FieldName = f.FieldName,
                        DataType = f.DataType,
                        Length = f.Length
                    })
                    .Distinct()
                    .ToListAsync();

                var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

                // 2️⃣ Normalize payload
                var normalizedPayload = payload.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
                );

                // 3️⃣ Detect nested JSON
                bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

                JObject dataObject = hasNestedObject
                    ? FlattenJson(JObject.FromObject(normalizedPayload))
                    : JObject.FromObject(normalizedPayload);

                // 4️⃣ Validate each field
                foreach (var prop in dataObject.Properties())
                {
                    var key = prop.Name.ToLower();
                    var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                    if (!fieldDict.ContainsKey(key))
                    {
                        invalidFields.Add($"{prop.Name} (Field Not Configured)");
                        continue;
                    }

                    var field = fieldDict[key];

                    // Validate datatype
                    if (!IsValidDataType(value, field.DataType))
                    {
                        invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                        continue;
                    }

                    // Validate length
                    if (field.Length.HasValue && value?.Length > field.Length)
                    {
                        invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                    }
                }

                result.InvalidFields = invalidFields;
                result.Success = invalidFields.Count == 0;

                var partnerData = await _context.tblPartnerDatas
                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                    .Select(p => new { p.ApplicationNumber })
                    .FirstOrDefaultAsync();

                if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
                {
                    result.Success = false;
                    result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                    return result;
                }

                // 5️⃣ Save only if valid
                if (result.Success)
                {
                    var entity = new tblpf_FatcaDetails
                    {
                        vcApplicationNumber = partnerData.ApplicationNumber,
                        intAssureType = ParseInt(GetValueIgnoreCase(dataObject, "intAssureType")) ?? 0,
                        btFatca = ParseBool(GetValueIgnoreCase(dataObject, "btFatca")),
                        vcFatcaAddressJurisdiction = GetValueIgnoreCase(dataObject, "vcFatcaAddressJurisdiction"),
                        vcFatcaTaxIdentificationNumber = GetValueIgnoreCase(dataObject, "vcFatcaTaxIdentificationNumber"),
                        vcFatcaValidityOfDocumentaryEvidence = GetValueIgnoreCase(dataObject, "vcFatcaValidityOfDocumentaryEvidence"),
                        vcFatcaTaxResidencyCountry = GetValueIgnoreCase(dataObject, "vcFatcaTaxResidencyCountry"),
                        vcFatcaTINNumberIssuingCountry = GetValueIgnoreCase(dataObject, "vcFatcaTINNumberIssuingCountry"),
                        vcLastAccessIP = "0.0.0.0",
                        vcCreatedBy = partnerName ?? "system",
                        dtCreateDate = DateTime.Now,
                        bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "bitIsDeleted")) ?? false
                    };

                    _context.tblpf_FatcaDetails.Add(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.InvalidFields.Add($"Exception: {ex.Message}");

                // optional: log error
                Console.WriteLine($"[FATCADetailstDataAsync] Error: {ex}");
            }

            return result;
        }

        public async Task<ValidationResultModel> FinancialQuestionDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            try
            {
                // 1️⃣ Get allowed fields for this partner
                var allowedFields = await _context.PartnerSections
                    .Where(ps => ps.PartnerId == partnerId)
                    .Join(
                        _context.SectionFields.Include(sf => sf.Field),
                        ps => ps.SectionId,
                        sf => sf.SectionId,
                        (ps, sf) => sf.Field
                    )
                    .Where(f => !f.IsDeleted)
                    .Select(f => new FieldDefinition
                    {
                        FieldName = f.FieldName,
                        DataType = f.DataType,
                        Length = f.Length
                    })
                    .Distinct()
                    .ToListAsync();

                var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

                // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
                var normalizedPayload = payload.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
                );

                // 3️⃣ Detect if nested JSON exists
                bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

                JObject dataObject = hasNestedObject
                    ? FlattenJson(JObject.FromObject(normalizedPayload))
                    : JObject.FromObject(normalizedPayload);

                // 4️⃣ Validate each field
                foreach (var prop in dataObject.Properties())
                {
                    var key = prop.Name.ToLower();
                    var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                    if (!fieldDict.ContainsKey(key))
                    {
                        invalidFields.Add($"{prop.Name} (Field Not Configured)");
                        continue;
                    }

                    var field = fieldDict[key];

                    // Validate datatype
                    if (!IsValidDataType(value, field.DataType))
                    {
                        invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                        continue;
                    }

                    // Validate length
                    if (field.Length.HasValue && value?.Length > field.Length)
                    {
                        invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                    }
                }

                result.InvalidFields = invalidFields;
                result.Success = invalidFields.Count == 0;

                var partnerData = await _context.tblPartnerDatas
                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                    .Select(p => new { p.ApplicationNumber })
                    .FirstOrDefaultAsync();

                if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
                {
                    result.Success = false;
                    result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                    return result;
                }

                if (result.Success)
                {
                    tblpf_financialQuestion entity = null;

                    try
                    {
                        entity = new tblpf_financialQuestion
                        {
                            vcApplicationNumber = partnerData.ApplicationNumber,
                            intAssureType = ParseInt(GetValueIgnoreCase(dataObject, "intAssureType")),
                            vcpremiumFinancedBy = GetValueIgnoreCase(dataObject, "vcpremiumFinancedBy"),
                            vcoccupation = GetValueIgnoreCase(dataObject, "vcoccupation"),
                            vcannualIncome = GetValueIgnoreCase(dataObject, "vcannualIncome"),
                            vcincome = GetValueIgnoreCase(dataObject, "vcincome"),
                            btassessedForIncomeTax = ParseBool(GetValueIgnoreCase(dataObject, "btassessedForIncomeTax")) ?? false,
                            vcpanNumber = GetValueIgnoreCase(dataObject, "vcpanNumber"),
                            vcsourceOfIncome = GetValueIgnoreCase(dataObject, "vcsourceOfIncome"),
                            bthaveAgriculturalLand = ParseBool(GetValueIgnoreCase(dataObject, "bthaveAgriculturalLand")) ?? false,
                            vchaveAgriculturalLandDetails = GetValueIgnoreCase(dataObject, "vchaveAgriculturalLandDetails"),
                            vccashDepositesCertificates = GetValueIgnoreCase(dataObject, "vccashDepositesCertificates"),
                            vcnscUtiPpfPension = GetValueIgnoreCase(dataObject, "vcnscUtiPpfPension"),
                            vccapitalInvestment = GetValueIgnoreCase(dataObject, "vccapitalInvestment"),
                            vcimmovableProperties = GetValueIgnoreCase(dataObject, "vcimmovableProperties"),
                            vcotherInvestmentsSavings = GetValueIgnoreCase(dataObject, "vcotherInvestmentsSavings"),
                            vctotal = GetValueIgnoreCase(dataObject, "vctotal"),
                            vcnetWorth = GetValueIgnoreCase(dataObject, "vcnetWorth"),
                            vcotherLiabilities = GetValueIgnoreCase(dataObject, "vcotherLiabilities"),
                            vcliabilitiesTotal = GetValueIgnoreCase(dataObject, "vcliabilitiesTotal"),
                            vcloan = GetValueIgnoreCase(dataObject, "vcloan"),
                            vcotherDetails = GetValueIgnoreCase(dataObject, "vcotherDetails"),
                            btinsuranceFromOtherCompanines = ParseBool(GetValueIgnoreCase(dataObject, "btinsuranceFromOtherCompanines")) ?? false,
                            vcinsuranceCompanyName = GetValueIgnoreCase(dataObject, "vcinsuranceCompanyName"),
                            vcpolicyNumber = GetValueIgnoreCase(dataObject, "vcpolicyNumber"),
                            vcbasicSumAssured = GetValueIgnoreCase(dataObject, "vcbasicSumAssured"),
                            vcridersOpted = GetValueIgnoreCase(dataObject, "vcridersOpted"),
                            vcmedicalOrNonMedical = GetValueIgnoreCase(dataObject, "vcmedicalOrNonMedical"),
                            vcannualizedPremium = GetValueIgnoreCase(dataObject, "vcannualizedPremium"),
                            vcaccept = GetValueIgnoreCase(dataObject, "vcaccept"),
                            vcnameOfLa = GetValueIgnoreCase(dataObject, "vcnameOfLa"),
                            vcspecifyDetails = GetValueIgnoreCase(dataObject, "vcspecifyDetails"),
                            vcyear2425 = GetValueIgnoreCase(dataObject, "vcyear2425"),
                            vcyear2324 = GetValueIgnoreCase(dataObject, "vcyear2324"),
                            vcyear2223 = GetValueIgnoreCase(dataObject, "vcyear2223"),
                            vcAgriyear2425 = GetValueIgnoreCase(dataObject, "vcAgriyear2425"),
                            vcAgriyear2324 = GetValueIgnoreCase(dataObject, "vcAgriyear2324"),
                            vcAgriyear2223 = GetValueIgnoreCase(dataObject, "vcAgriyear2223"),
                            vcrentyear2425 = GetValueIgnoreCase(dataObject, "vcrentyear2425"),
                            vcrentyear2324 = GetValueIgnoreCase(dataObject, "vcrentyear2324"),
                            vcrentyear2223 = GetValueIgnoreCase(dataObject, "vcrentyear2223"),
                            vccapitalgainyear2425 = GetValueIgnoreCase(dataObject, "vccapitalgainyear2425"),
                            vccapitalgainyear2324 = GetValueIgnoreCase(dataObject, "vccapitalgainyear2324"),
                            vccapitalgainyear2223 = GetValueIgnoreCase(dataObject, "vccapitalgainyear2223"),
                            vclongcapitalgainyear2425 = GetValueIgnoreCase(dataObject, "vclongcapitalgainyear2425"),
                            vclongcapitalgainyear2324 = GetValueIgnoreCase(dataObject, "vclongcapitalgainyear2324"),
                            vclongcapitalgainyear2223 = GetValueIgnoreCase(dataObject, "vclongcapitalgainyear2223"),
                            vcinterestyear2425 = GetValueIgnoreCase(dataObject, "vcinterestyear2425"),
                            vcinterestyear2324 = GetValueIgnoreCase(dataObject, "vcinterestyear2324"),
                            vcinterestyear2223 = GetValueIgnoreCase(dataObject, "vcinterestyear2223"),
                            vcotheryear2425 = GetValueIgnoreCase(dataObject, "vcotheryear2425"),
                            vcotheryear2324 = GetValueIgnoreCase(dataObject, "vcotheryear2324"),
                            vcotheryear2223 = GetValueIgnoreCase(dataObject, "vcotheryear2223"),
                            vctotalanualyear2425 = GetValueIgnoreCase(dataObject, "vctotalanualyear2425"),
                            vctotalanualyear2324 = GetValueIgnoreCase(dataObject, "vctotalanualyear2324"),
                            vctotalanualyear2223 = GetValueIgnoreCase(dataObject, "vctotalanualyear2223"),

                            vcCreatedBy = partnerName ?? "system",
                            vcLastAccessIP = "0.0.0.0",
                            dtCreateDate = DateTime.Now,
                            bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "bitIsDeleted")) ?? false
                        };
                    }
                    catch (Exception fieldEx)
                    {
                        result.Success = false;
                        result.InvalidFields.Add($"Field Mapping Error: {fieldEx.Message}");
                        Console.WriteLine($"[FinancialQuestionDataAsync] Field Error: {fieldEx}");
                        return result;
                    }

                    try
                    {
                        _context.tblpf_FinancialQuestions.Add(entity);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception dbEx)
                    {
                        result.Success = false;
                        result.InvalidFields.Add($"Database Save Error: {dbEx.Message}");

                        // log full exception
                        Console.WriteLine($"[FinancialQuestionDataAsync] Save Error: {dbEx}");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.InvalidFields.Add($"General Exception: {ex.Message}");
                Console.WriteLine($"[FinancialQuestionDataAsync] Exception: {ex}");
            }

            return result;
        }


        public async Task<ValidationResultModel> Form60QuestionDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            try
            {
                // 1️⃣ Get allowed fields for this partner
                var allowedFields = await _context.PartnerSections
                    .Where(ps => ps.PartnerId == partnerId)
                    .Join(
                        _context.SectionFields.Include(sf => sf.Field),
                        ps => ps.SectionId,
                        sf => sf.SectionId,
                        (ps, sf) => sf.Field
                    )
                    .Where(f => !f.IsDeleted)
                    .Select(f => new FieldDefinition
                    {
                        FieldName = f.FieldName,
                        DataType = f.DataType,
                        Length = f.Length
                    })
                    .Distinct()
                    .ToListAsync();

                var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

                // 2️⃣ Normalize payload
                var normalizedPayload = payload.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
                );

                // 3️⃣ Flatten JSON if nested
                bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

                JObject dataObject = hasNestedObject
                    ? FlattenJson(JObject.FromObject(normalizedPayload))
                    : JObject.FromObject(normalizedPayload);

                // 4️⃣ Validate each field
                foreach (var prop in dataObject.Properties())
                {
                    var key = prop.Name.ToLower();
                    var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                    if (!fieldDict.ContainsKey(key))
                    {
                        invalidFields.Add($"{prop.Name} (Field Not Configured)");
                        continue;
                    }

                    var field = fieldDict[key];

                    // Validate datatype
                    if (!IsValidDataType(value, field.DataType))
                    {
                        invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                        continue;
                    }

                    // Validate length
                    if (field.Length.HasValue && value?.Length > field.Length)
                    {
                        invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                    }
                }

                result.InvalidFields = invalidFields;
                result.Success = invalidFields.Count == 0;

                // 5️⃣ Get Partner Application Number
                var partnerData = await _context.tblPartnerDatas
                                        .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                                        .Select(p => new { p.ApplicationNumber })
                                        .FirstOrDefaultAsync();

                if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
                {
                    result.Success = false;
                    result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                    return result;
                }

                // 6️⃣ If valid, save entity
                if (result.Success)
                {
                    var entity = new tblPF_Form60Questions
                    {
                        vcApplicationNumber = partnerData.ApplicationNumber,
                        intAssureType = ParseInt(GetValueIgnoreCase(dataObject, "intAssureType")) ?? 0,
                        btAppliedForPan = ParseBool(GetValueIgnoreCase(dataObject, "btAppliedForPan")),
                        vcIdentityDocument = GetValueIgnoreCase(dataObject, "vcIdentityDocument"),
                        vcIdentityDocumentCode = GetValueIgnoreCase(dataObject, "vcIdentityDocumentCode"),
                        vcIdentityDocumentNumber = GetValueIgnoreCase(dataObject, "vcIdentityDocumentNumber"),
                        vcDocumentAddressLine1 = GetValueIgnoreCase(dataObject, "vcDocumentAddressLine1"),
                        vcDocumentAddressLine2 = GetValueIgnoreCase(dataObject, "vcDocumentAddressLine2"),
                        vcDocumentCountry = GetValueIgnoreCase(dataObject, "vcDocumentCountry"),
                        vcDocumentState = GetValueIgnoreCase(dataObject, "vcDocumentState"),
                        vcDocumentCity = GetValueIgnoreCase(dataObject, "vcDocumentCity"),
                        vcDocumentPincode = GetValueIgnoreCase(dataObject, "vcDocumentPincode"),
                        vcSupportOfAddressDocument = GetValueIgnoreCase(dataObject, "vcSupportOfAddressDocument"),
                        vcSupportOfAddressDocumentCode = GetValueIgnoreCase(dataObject, "vcSupportOfAddressDocumentCode"),
                        vcSupportOfAddressIdentificationNumber = GetValueIgnoreCase(dataObject, "vcSupportOfAddressIdentificationNumber"),
                        vcIssuingDocumentAddressLine1 = GetValueIgnoreCase(dataObject, "vcIssuingDocumentAddressLine1"),
                        vcIssuingDocumentAddressLine2 = GetValueIgnoreCase(dataObject, "vcIssuingDocumentAddressLine2"),
                        vcIssuingDocumentCountry = GetValueIgnoreCase(dataObject, "vcIssuingDocumentCountry"),
                        vcIssuingDocumentState = GetValueIgnoreCase(dataObject, "vcIssuingDocumentState"),
                        vcIssuingDocumentCity = GetValueIgnoreCase(dataObject, "vcIssuingDocumentCity"),
                        vcIssuingDocumentPincode = GetValueIgnoreCase(dataObject, "vcIssuingDocumentPincode"),
                        vcNameOfDocumentIssuer = GetValueIgnoreCase(dataObject, "vcNameOfDocumentIssuer"),
                        vcSupportOfNameOfDocumentIssuer = GetValueIgnoreCase(dataObject, "vcSupportOfNameOfDocumentIssuer"),
                        vcAgriculturalIncome = GetValueIgnoreCase(dataObject, "vcAgriculturalIncome"),
                        vcOtherAgriculturalIncome = GetValueIgnoreCase(dataObject, "vcOtherAgriculturalIncome"),
                        dtDateOfPanApplication = ParseDateNullable(GetValueIgnoreCase(dataObject, "dtDateOfPanApplication")),
                        vcAcknowledgementNumber = GetValueIgnoreCase(dataObject, "vcAcknowledgementNumber"),

                        // System fields
                        vcCreatedBy = partnerName ?? "system",
                        vcLastAccessIP = "0.0.0.0",
                        dtCreateDate = DateTime.Now,
                        bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "bitIsDeleted")) ?? false
                    };

                    _context.tblPF_Form60Questions.Add(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.InvalidFields.Add($"Exception: {ex.Message}");
                // Optionally log the exception
                // _logger.LogError(ex, "Error in Form60QuestionDataAsync");
            }

            return result;
        }


        public async Task<ValidationResultModel> HealthConditionDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            // 1️⃣ Get allowed fields for this partner
            var allowedFields = await _context.PartnerSections
                .Where(ps => ps.PartnerId == partnerId)
                .Join(
                    _context.SectionFields.Include(sf => sf.Field),
                    ps => ps.SectionId,
                    sf => sf.SectionId,
                    (ps, sf) => sf.Field
                )
                .Where(f => !f.IsDeleted)
                .Select(f => new FieldDefinition
                {
                    FieldName = f.FieldName,
                    DataType = f.DataType,
                    Length = f.Length
                })
                .Distinct()
                .ToListAsync();

            var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

            // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
            var normalizedPayload = payload.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
            );

            // 3️⃣ Detect if nested JSON exists
            bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

            JObject dataObject = hasNestedObject
                ? FlattenJson(JObject.FromObject(normalizedPayload))
                : JObject.FromObject(normalizedPayload);

            // 4️⃣ Validate each field
            foreach (var prop in dataObject.Properties())
            {
                var key = prop.Name.ToLower();
                var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                if (!fieldDict.ContainsKey(key))
                {
                    invalidFields.Add($"{prop.Name} (Field Not Configured)");
                    continue;
                }

                var field = fieldDict[key];

                // Validate datatype
                if (!IsValidDataType(value, field.DataType))
                {
                    invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                    continue;
                }

                // Validate length
                if (field.Length.HasValue && value?.Length > field.Length)
                {
                    invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                }
            }

            result.InvalidFields = invalidFields;
            result.Success = invalidFields.Count == 0;

            var partnerData = await _context.tblPartnerDatas
                                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                                    .Select(p => new { p.ApplicationNumber })
                                    .FirstOrDefaultAsync();

            if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
            {
                result.Success = false;
                result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                return result;
            }

            if (result.Success)
            {
                var entity = new tblPF_HealthConditions
                {
                    vcApplicationNumber = partnerData.ApplicationNumber,
                    intAssureType = ParseInt(GetValueIgnoreCase(dataObject, "intAssureType")) ?? 0,
                    ftHeightInCms = ParseFloat(GetValueIgnoreCase(dataObject, "ftHeightInCms")),
                    ftHeightInInches = ParseFloat(GetValueIgnoreCase(dataObject, "ftHeightInInches")),
                    ftHeightInFeet = ParseFloat(GetValueIgnoreCase(dataObject, "ftHeightInFeet")),
                    ftWeightinKgs = ParseFloat(GetValueIgnoreCase(dataObject, "ftWeightinKgs")),
                    btWeightChange6M = ParseBool(GetValueIgnoreCase(dataObject, "btWeightChange6M")),
                    btWeightChange1Year = ParseBool(GetValueIgnoreCase(dataObject, "btWeightChange1Year")),
                    vcWeightGainOrLost = GetValueIgnoreCase(dataObject, "vcWeightGainOrLost"),
                    ftWeightGainOrLostInKgs = ParseFloat(GetValueIgnoreCase(dataObject, "ftWeightGainOrLostInKgs")),
                    vcCauseOfWeightIncrease = GetValueIgnoreCase(dataObject, "vcCauseOfWeightIncrease"),
                    btIsPregnent = ParseBool(GetValueIgnoreCase(dataObject, "btIsPregnent")),
                    intDurationOfPregnantInWeek = ParseInt(GetValueIgnoreCase(dataObject, "intDurationOfPregnantInWeek")),
                    btComplicationInPregnency = ParseBool(GetValueIgnoreCase(dataObject, "btComplicationInPregnency")),
                    btGynaecologicalProblem = ParseBool(GetValueIgnoreCase(dataObject, "btGynaecologicalProblem")),
                    vcGynaecologicalProblemDetail = GetValueIgnoreCase(dataObject, "vcGynaecologicalProblemDetail"),
                    dtDateOfLastDelivery = ParseDateNullable(GetValueIgnoreCase(dataObject, "dtDateOfLastDelivery")),
                    btGynaecologicalComplications = ParseBool(GetValueIgnoreCase(dataObject, "btGynaecologicalComplications")),
                    dtApproxDueDateOfDelivery = ParseDateNullable(GetValueIgnoreCase(dataObject, "dtApproxDueDateOfDelivery")),
                    btDiagnosedMedicalTreatment = ParseBool(GetValueIgnoreCase(dataObject, "btDiagnosedMedicalTreatment")),
                    btSpouseSufferingHivOrHepatitis = ParseBool(GetValueIgnoreCase(dataObject, "btSpouseSufferingHivOrHepatitis")),
                    vcSpouseSufferingHivOrHepatitisDetail = GetValueIgnoreCase(dataObject, "vcSpouseSufferingHivOrHepatitisDetail"),
                    dtDateOfFirstDiagnosisForHivOrHepatitis = ParseDateNullable(GetValueIgnoreCase(dataObject, "dtDateOfFirstDiagnosisForHivOrHepatitis")),
                    btLaTreatmentSame = ParseBool(GetValueIgnoreCase(dataObject, "btLaTreatmentSame")),
                    vcExactTreatmentMedicalDetail = GetValueIgnoreCase(dataObject, "vcExactTreatmentMedicalDetail"),
                    btIsFamilySufferAnyNeurologicalDisorder = ParseBool(GetValueIgnoreCase(dataObject, "btIsFamilySufferAnyNeurologicalDisorder")),
                    vcFamilySufferAnyNeurologicalDisorderDetail = GetValueIgnoreCase(dataObject, "vcFamilySufferAnyNeurologicalDisorderDetail"),
                    btIsAnyChestRelatedCheckups = ParseBool(GetValueIgnoreCase(dataObject, "btIsAnyChestRelatedCheckups")),
                    dtFirstDiagnosisOfAnyChestRelatedCheckupsDate = ParseDateNullable(GetValueIgnoreCase(dataObject, "dtFirstDiagnosisOfAnyChestRelatedCheckupsDate")),
                    vcDiagnosisOfAnyChestRelatedCheckupsDetail = GetValueIgnoreCase(dataObject, "vcDiagnosisOfAnyChestRelatedCheckupsDetail"),
                    vcDiagnosisOfAnyChestRelatedCheckupsOngoing = GetValueIgnoreCase(dataObject, "vcDiagnosisOfAnyChestRelatedCheckupsOngoing"),
                    btAsthmaOrTuberculosis = ParseBool(GetValueIgnoreCase(dataObject, "btAsthmaOrTuberculosis")),
                    btUlcerOrPencreatic = ParseBool(GetValueIgnoreCase(dataObject, "btUlcerOrPencreatic")),
                    btArthritisOrBone = ParseBool(GetValueIgnoreCase(dataObject, "btArthritisOrBone")),
                    vcArthritisDiagnosis = GetValueIgnoreCase(dataObject, "vcArthritisDiagnosis"),
                    vcAffectedjoints = GetValueIgnoreCase(dataObject, "vcAffectedjoints"),
                    dtDiagnosisDate = ParseDateNullable(GetValueIgnoreCase(dataObject, "dtDiagnosisDate")),
                    vcStillHaveSymptoms = ParseDateNullable(GetValueIgnoreCase(dataObject, "vcStillHaveSymptoms")),
                    btBloodDisorder = ParseBool(GetValueIgnoreCase(dataObject, "btBloodDisorder")),
                    btCancerOrTumour = ParseBool(GetValueIgnoreCase(dataObject, "btCancerOrTumour")),
                    btChestPainOrHeartAttack = ParseBool(GetValueIgnoreCase(dataObject, "btChestPainOrHeartAttack")),
                    btCongenitalOrHereditary = ParseBool(GetValueIgnoreCase(dataObject, "btCongenitalOrHereditary")),
                    btDiabetesOrsugarInUrine = ParseBool(GetValueIgnoreCase(dataObject, "btDiabetesOrsugarInUrine")),
                    btEyeNoseEarSkinDisorder = ParseBool(GetValueIgnoreCase(dataObject, "btEyeNoseEarSkinDisorder")),
                    btHypertension = ParseBool(GetValueIgnoreCase(dataObject, "btHypertension")),
                    btHivTesting = ParseBool(GetValueIgnoreCase(dataObject, "btHivTesting")),
                    btLiverGallbladderJaundice = ParseBool(GetValueIgnoreCase(dataObject, "btLiverGallbladderJaundice")),
                    btMentalHealthDisorders = ParseBool(GetValueIgnoreCase(dataObject, "btMentalHealthDisorders")),
                    btPhysicalImpairment = ParseBool(GetValueIgnoreCase(dataObject, "btPhysicalImpairment")),
                    btNeurologicalDisorders = ParseBool(GetValueIgnoreCase(dataObject, "btNeurologicalDisorders")),
                    btHormonalDisorders = ParseBool(GetValueIgnoreCase(dataObject, "btHormonalDisorders")),
                    btUrologicalReproductiveDisorders = ParseBool(GetValueIgnoreCase(dataObject, "btUrologicalReproductiveDisorders")),
                    btMedicalTreatmentHistoryLast5Years = ParseBool(GetValueIgnoreCase(dataObject, "btMedicalTreatmentHistoryLast5Years")),
                    btCirculatorySystemDisorder = ParseBool(GetValueIgnoreCase(dataObject, "btCirculatorySystemDisorder")),
                    btCurrentlySufferingOtherThanAboveDetails = ParseBool(GetValueIgnoreCase(dataObject, "btCurrentlySufferingOtherThanAboveDetails")),
                    btDigestiveDisorder = ParseBool(GetValueIgnoreCase(dataObject, "btDigestiveDisorder")),
                    btKidneyStoneDisorder = ParseBool(GetValueIgnoreCase(dataObject, "btKidneyStoneDisorder")),
                    btMedicalHistory = ParseBool(GetValueIgnoreCase(dataObject, "btMedicalHistory")),
                    btPsychiatricDisorder = ParseBool(GetValueIgnoreCase(dataObject, "btPsychiatricDisorder")),
                    btSpouseAdvisedTest = ParseBool(GetValueIgnoreCase(dataObject, "btSpouseAdvisedTest")),
                    btSurgeryOrInvetigations = ParseBool(GetValueIgnoreCase(dataObject, "btSurgeryOrInvetigations")),
                    btThyroidDisorder = ParseBool(GetValueIgnoreCase(dataObject, "btThyroidDisorder")),
                    vcInvestigationsTreatment = GetValueIgnoreCase(dataObject, "vcInvestigationsTreatment"),
                    vcTreatmentDetails = GetValueIgnoreCase(dataObject, "vcTreatmentDetails"),
                    vcLastAccessIP = GetValueIgnoreCase(dataObject, "vcLastAccessIP") ?? partnerName ?? "system",
                    vcCreatedBy = partnerName ?? "system",
                    dtCreateDate = DateTime.Now,
                    vcModifiedBy = GetValueIgnoreCase(dataObject, "vcModifiedBy"),
                    dtModifiedDate = ParseDateNullable(GetValueIgnoreCase(dataObject, "dtModifiedDate")),
                    dtDeletedDate = ParseDateNullable(GetValueIgnoreCase(dataObject, "dtDeletedDate")),
                    bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "bitIsDeleted")) ?? false,
                    GyneacologicalDiagnosis = ParseBool(GetValueIgnoreCase(dataObject, "GyneacologicalDiagnosis")),
                    HealthDisorder = ParseBool(GetValueIgnoreCase(dataObject, "HealthDisorder")),
                    HospitalizedOrUndergoneAnySurgery = ParseBool(GetValueIgnoreCase(dataObject, "HospitalizedOrUndergoneAnySurgery")),
                    AccidentInsuranceBeenDeclinedEver = ParseBool(GetValueIgnoreCase(dataObject, "AccidentInsuranceBeenDeclinedEver")),
                    AnyFamilyMemberUndergoneForCovid19Test = ParseBool(GetValueIgnoreCase(dataObject, "AnyFamilyMemberUndergoneForCovid19Test")),
                    HeridetoryDisorder = ParseBool(GetValueIgnoreCase(dataObject, "HeridetoryDisorder")),
                    ThroatSkinDisorder = ParseBool(GetValueIgnoreCase(dataObject, "ThroatSkinDisorder")),
                    HypertensionHighBloodPressure = ParseBool(GetValueIgnoreCase(dataObject, "HypertensionHighBloodPressure")),
                    HivAidsInfection = ParseBool(GetValueIgnoreCase(dataObject, "HivAidsInfection")),
                    NervousMentalDisorder = ParseBool(GetValueIgnoreCase(dataObject, "NervousMentalDisorder")),
                    PhysicalImpairmentDisabilityHandicap = ParseBool(GetValueIgnoreCase(dataObject, "PhysicalImpairmentDisabilityHandicap")),
                    BrainDisorderDetails = ParseBool(GetValueIgnoreCase(dataObject, "BrainDisorderDetails")),
                    ThyroidOrHormonalDisorder = ParseBool(GetValueIgnoreCase(dataObject, "ThyroidOrHormonalDisorder")),
                    ReproductiveOrganDisorder = ParseBool(GetValueIgnoreCase(dataObject, "ReproductiveOrganDisorder")),
                    UndergoneAnyTreatmentInLast5Years = ParseBool(GetValueIgnoreCase(dataObject, "UndergoneAnyTreatmentInLast5Years")),
                    MammographyBiopsyDetails = ParseBool(GetValueIgnoreCase(dataObject, "MammographyBiopsyDetails")),
                    PregnancyDetailsDisorder = ParseBool(GetValueIgnoreCase(dataObject, "PregnancyDetailsDisorder")),
                    CauseOfChange = GetValueIgnoreCase(dataObject, "CauseOfChange"),
                    DueDateOfDelivery = ParseDateNullable(GetValueIgnoreCase(dataObject, "DueDateOfDelivery")),
                    SpousePartnerHIVAIDS = ParseBool(GetValueIgnoreCase(dataObject, "SpousePartnerHIVAIDS")),
                    HaveUnderGoAnyTreatment = ParseBool(GetValueIgnoreCase(dataObject, "HaveUnderGoAnyTreatment")),
                    ProvideDetails = GetValueIgnoreCase(dataObject, "ProvideDetails"),
                    ProvidePregencyDetails = GetValueIgnoreCase(dataObject, "ProvidePregencyDetails"),
                    DurationInWeek = GetValueIgnoreCase(dataObject, "DurationInWeek"),
                    btAnyOtherIllness = ParseBool(GetValueIgnoreCase(dataObject, "btAnyOtherIllness")),
                    btHypertensionHeartattack = ParseBool(GetValueIgnoreCase(dataObject, "btHypertensionHeartattack")),
                    btParalysisStroke = ParseBool(GetValueIgnoreCase(dataObject, "btParalysisStroke")),
                    btMoreInformation = ParseBool(GetValueIgnoreCase(dataObject, "btMoreInformation")),
                    btBrainEyeEar = ParseBool(GetValueIgnoreCase(dataObject, "btBrainEyeEar"))
                };

                _context.tblPF_HealthConditions.Add(entity);
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<ValidationResultModel> HealthConditionDetailsDataAsync(
     int partnerId,
     Dictionary<string, object> payload,
     string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            try
            {
                // 1️⃣ Get allowed fields for this partner
                var allowedFields = await _context.PartnerSections
                    .Where(ps => ps.PartnerId == partnerId)
                    .Join(
                        _context.SectionFields.Include(sf => sf.Field),
                        ps => ps.SectionId,
                        sf => sf.SectionId,
                        (ps, sf) => sf.Field
                    )
                    .Where(f => !f.IsDeleted)
                    .Select(f => new FieldDefinition
                    {
                        FieldName = f.FieldName,
                        DataType = f.DataType,
                        Length = f.Length
                    })
                    .Distinct()
                    .ToListAsync();

                var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

                // 2️⃣ Normalize payload (handle JsonElement conversion)
                var normalizedPayload = payload.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
                );

                // 3️⃣ Detect nested JSON
                bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);
                JObject dataObject = hasNestedObject
                    ? FlattenJson(JObject.FromObject(normalizedPayload))
                    : JObject.FromObject(normalizedPayload);

                // 4️⃣ Validate each field
                foreach (var prop in dataObject.Properties())
                {
                    var key = prop.Name.ToLower();
                    var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                    if (!fieldDict.ContainsKey(key))
                    {
                        invalidFields.Add($"{prop.Name} (Field Not Configured)");
                        continue;
                    }

                    var field = fieldDict[key];

                    // Validate datatype
                    if (!IsValidDataType(value, field.DataType))
                    {
                        invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                        continue;
                    }

                    // Validate length
                    if (field.Length.HasValue && value?.Length > field.Length)
                    {
                        invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                    }
                }

                result.InvalidFields = invalidFields;
                result.Success = invalidFields.Count == 0;

                var partnerData = await _context.tblPartnerDatas
                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                    .Select(p => new { p.ApplicationNumber })
                    .FirstOrDefaultAsync();

                if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
                {
                    result.Success = false;
                    result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                    return result;
                }

                if (result.Success)
                {
                    try
                    {
                        var entity = new tblPF_HealthConditionsDetails
                        {
                            vcApplicationNumber = partnerData.ApplicationNumber,
                            intReflexQuestionTypeID = ParseInt(GetValueIgnoreCase(dataObject, "intReflexQuestionTypeID")),
                            vcParentFieldName = GetValueIgnoreCase(dataObject, "vcParentFieldName"),
                            vcNameOfIllness = GetValueIgnoreCase(dataObject, "vcNameOfIllness"),
                            dtFirstDiagnosis = ParseDate(GetValueIgnoreCase(dataObject, "dtFirstDiagnosis")),
                            vcTreatmentDetails = GetValueIgnoreCase(dataObject, "vcTreatmentDetails"),
                            vcCurrentStatus = GetValueIgnoreCase(dataObject, "vcCurrentStatus"),
                            btSameTreatment = ParseBool(GetValueIgnoreCase(dataObject, "btSameTreatment")),
                            btAnyCOmplecationConditions = ParseBool(GetValueIgnoreCase(dataObject, "btAnyCOmplecationConditions")),
                            vcFollowUpAdvise = GetValueIgnoreCase(dataObject, "vcFollowUpAdvise"),
                            vcAdditionalRemarks = GetValueIgnoreCase(dataObject, "vcAdditionalRemarks"),
                            vcMedicineDosageDetails = GetValueIgnoreCase(dataObject, "vcMedicineDosageDetails"),
                            vcnameOfTreatingDoctor = GetValueIgnoreCase(dataObject, "vcnameOfTreatingDoctor"),
                            vcCreatedBy = partnerName ?? "system",
                            vcLastAccessIP = "0.0.0.0",
                            dtCreateDate = DateTime.Now,
                            vcModifiedBy = GetValueIgnoreCase(dataObject, "vcModifiedBy"),
                            dtModifiedDate = ParseDate(GetValueIgnoreCase(dataObject, "dtModifiedDate")),
                            dtDeletedDate = ParseDate(GetValueIgnoreCase(dataObject, "dtDeletedDate")),
                            bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "bitIsDeleted")) ?? false,
                            btPregnancyDetailsDisorder = ParseBool(GetValueIgnoreCase(dataObject, "btPregnancyDetailsDisorder"))
                        };

                        _context.tblPF_HealthConditionsDetails.Add(entity);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.InvalidFields.Add($"Error while creating entity: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.InvalidFields.Add($"Unexpected Error: {ex.Message}");
            }

            return result;
        }


        public async Task<ValidationResultModel> InsuranceHistoryDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            try
            {
                // 1️⃣ Get allowed fields for this partner
                var allowedFields = await _context.PartnerSections
                    .Where(ps => ps.PartnerId == partnerId)
                    .Join(
                        _context.SectionFields.Include(sf => sf.Field),
                        ps => ps.SectionId,
                        sf => sf.SectionId,
                        (ps, sf) => sf.Field
                    )
                    .Where(f => !f.IsDeleted)
                    .Select(f => new FieldDefinition
                    {
                        FieldName = f.FieldName,
                        DataType = f.DataType,
                        Length = f.Length
                    })
                    .Distinct()
                    .ToListAsync();

                var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

                // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
                var normalizedPayload = payload.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
                );

                // 3️⃣ Detect if nested JSON exists
                bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

                JObject dataObject = hasNestedObject
                    ? FlattenJson(JObject.FromObject(normalizedPayload))
                    : JObject.FromObject(normalizedPayload);

                // 4️⃣ Validate each field
                foreach (var prop in dataObject.Properties())
                {
                    var key = prop.Name.ToLower();
                    var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                    if (!fieldDict.ContainsKey(key))
                    {
                        invalidFields.Add($"{prop.Name} (Field Not Configured)");
                        continue;
                    }

                    var field = fieldDict[key];

                    // Validate datatype
                    if (!IsValidDataType(value, field.DataType))
                    {
                        invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                        continue;
                    }

                    // Validate length
                    if (field.Length.HasValue && value?.Length > field.Length)
                    {
                        invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                    }
                }

                result.InvalidFields = invalidFields;
                result.Success = invalidFields.Count == 0;

                var partnerData = await _context.tblPartnerDatas
                                        .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                                        .Select(p => new { p.ApplicationNumber })
                                        .FirstOrDefaultAsync();

                if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
                {
                    result.Success = false;
                    result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                    return result;
                }

                if (result.Success)
                {
                    var entity = new tblPF_InsuranceHistory
                    {
                        vcApplicationNumber = partnerData.ApplicationNumber,
                        intAssureType = ParseInt(GetValueIgnoreCase(dataObject, "intAssureType")) ?? 0,
                        vcTitle = GetValueIgnoreCase(dataObject, "vcTitle"),
                        vcFirstName = GetValueIgnoreCase(dataObject, "vcFirstName"),
                        vcMiddleName = GetValueIgnoreCase(dataObject, "vcMiddleName"),
                        vcLastName = GetValueIgnoreCase(dataObject, "vcLastName"),
                        vcPolicyNumber = GetValueIgnoreCase(dataObject, "vcPolicyNumber"),
                        dtDateOfProposal = ParseDate(GetValueIgnoreCase(dataObject, "dtDateOfProposal")),
                        intTypeOfPolicy = ParseInt(GetValueIgnoreCase(dataObject, "intTypeOfPolicy")),
                        dcBaseSumAssured = ParseDecimal(GetValueIgnoreCase(dataObject, "dcBaseSumAssured")),
                        dcPremiumPerAnnum = ParseDecimal(GetValueIgnoreCase(dataObject, "dcPremiumPerAnnum")),
                        vcMonthOfInsurance = GetValueIgnoreCase(dataObject, "vcMonthOfInsurance"),
                        vcYearOfInsurance = GetValueIgnoreCase(dataObject, "vcYearOfInsurance"),
                        intCurrentStatus = ParseInt(GetValueIgnoreCase(dataObject, "intCurrentStatus")),
                        intAcceptanceTerms = ParseInt(GetValueIgnoreCase(dataObject, "intAcceptanceTerms")),
                        vcLastAccessIP = "0.0.0.0",
                        vcCreatedBy = partnerName ?? "system",
                        dtCreateDate = DateTime.Now
                    };

                    _context.tblPF_InsuranceHistories.Add(entity);

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException dbEx)
                    {
                        // Detect which column caused the null or constraint issue
                        var inner = dbEx.InnerException?.Message ?? dbEx.Message;
                        result.Success = false;
                        result.InvalidFields.Add($"Database error while saving InsuranceHistory: {inner}");

                        // Optional: attempt to identify column name from error message
                        var match = System.Text.RegularExpressions.Regex.Match(inner, @"column ['""]?(\w+)['""]?");
                        if (match.Success)
                        {
                            result.InvalidFields.Add($"⚠️ Likely Null/Invalid Column: {match.Groups[1].Value}");
                        }

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.InvalidFields.Add($"Unhandled Exception: {ex.Message}");
            }

            return result;
        }


        public async Task<ValidationResultModel> LifeStyleDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            // 1️⃣ Get allowed fields for this partner
            var allowedFields = await _context.PartnerSections
                .Where(ps => ps.PartnerId == partnerId)
                .Join(
                    _context.SectionFields.Include(sf => sf.Field),
                    ps => ps.SectionId,
                    sf => sf.SectionId,
                    (ps, sf) => sf.Field
                )
                .Where(f => !f.IsDeleted)
                .Select(f => new FieldDefinition
                {
                    FieldName = f.FieldName,
                    DataType = f.DataType,
                    Length = f.Length
                })
                .Distinct()
                .ToListAsync();

            var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

            // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
            var normalizedPayload = payload.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
            );

            // 3️⃣ Detect if nested JSON exists
            bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

            JObject dataObject = hasNestedObject
                ? FlattenJson(JObject.FromObject(normalizedPayload))
                : JObject.FromObject(normalizedPayload);

            // 4️⃣ Validate each field
            foreach (var prop in dataObject.Properties())
            {
                var key = prop.Name.ToLower();
                var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                if (!fieldDict.ContainsKey(key))
                {
                    invalidFields.Add($"{prop.Name} (Field Not Configured)");
                    continue;
                }

                var field = fieldDict[key];

                // Validate datatype
                if (!IsValidDataType(value, field.DataType))
                {
                    invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                    continue;
                }

                // Validate length
                if (field.Length.HasValue && value?.Length > field.Length)
                {
                    invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                }
            }

            result.InvalidFields = invalidFields;
            result.Success = invalidFields.Count == 0;

            var partnerData = await _context.tblPartnerDatas
                                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                                    .Select(p => new { p.ApplicationNumber })
                                    .FirstOrDefaultAsync();

            if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
            {
                result.Success = false;
                result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                return result;
            }

            if (result.Success)
            {
                var entity = new tblPF_LifeStyleDetails
                {
                    vcApplicationNumber = partnerData.ApplicationNumber,
                    intAssureType = ParseInt(GetValueIgnoreCase(dataObject, "intAssureType")) ?? 0,
                    btConsumeTobacco = GetValueIgnoreCase(dataObject, "btConsumeTobacco"),
                    intDurationTobaccoConsumption = ParseInt(GetValueIgnoreCase(dataObject, "intDurationTobaccoConsumption")),
                    vcTobaccoConsumptionWhatForm = GetValueIgnoreCase(dataObject, "vcTobaccoConsumptionWhatForm"),
                    dtTobaccoConsumptionStopOn = ParseDate(GetValueIgnoreCase(dataObject, "dtTobaccoConsumptionStopOn")),
                    dtCigarettesIfstoppedconsuming = ParseDate(GetValueIgnoreCase(dataObject, "dtCigarettesIfstoppedconsuming")),
                    intNumberPerDay = ParseInt(GetValueIgnoreCase(dataObject, "intNumberPerDay")),
                    intNumberOfMonths = ParseInt(GetValueIgnoreCase(dataObject, "intNumberOfMonths")),
                    btFormOfCigarettes = ParseBool(GetValueIgnoreCase(dataObject, "btFormOfCigarettes")),
                    intNumberOfCigarettesPerDay = ParseInt(GetValueIgnoreCase(dataObject, "intNumberOfCigarettesPerDay")),
                    intNumberOfCigarettesPerMonth = ParseInt(GetValueIgnoreCase(dataObject, "intNumberOfCigarettesPerMonth")),
                    btFormOfBidi = ParseBool(GetValueIgnoreCase(dataObject, "btFormOfBidi")),
                    intNumberOfBidiPerDay = ParseInt(GetValueIgnoreCase(dataObject, "intNumberOfBidiPerDay")),
                    intNumberOfBidiPerMonth = ParseInt(GetValueIgnoreCase(dataObject, "intNumberOfBidiPerMonth")),
                    vcBidiStoppedYear = GetValueIgnoreCase(dataObject, "vcBidiStoppedYear"),
                    vcBidiStoppedMonth = GetValueIgnoreCase(dataObject, "vcBidiStoppedMonth"),
                    btFormOfGutka = ParseBool(GetValueIgnoreCase(dataObject, "btFormOfGutka")),
                    intNumberOfGutkaPerDay = ParseInt(GetValueIgnoreCase(dataObject, "intNumberOfGutkaPerDay")),
                    intNumberOfGutkaPerMonths = ParseInt(GetValueIgnoreCase(dataObject, "intNumberOfGutkaPerMonths")),
                    vcGutkaStoppedYear = GetValueIgnoreCase(dataObject, "vcGutkaStoppedYear"),
                    vcGutkaStoppedMonths = GetValueIgnoreCase(dataObject, "vcGutkaStoppedMonths"),
                    btNeverConsume = ParseBool(GetValueIgnoreCase(dataObject, "btNeverConsume")),
                    btStoppedConsumption = ParseBool(GetValueIgnoreCase(dataObject, "btStoppedConsumption")),
                    vcStoppedOn = GetValueIgnoreCase(dataObject, "vcStoppedOn"),
                    btConsumeAlchohol = ParseBool(GetValueIgnoreCase(dataObject, "btConsumeAlchohol")),
                    intConsumeAlchoholFrequency = ParseInt(GetValueIgnoreCase(dataObject, "intConsumeAlchoholFrequency")),
                    btHardLiquorConsume = ParseBool(GetValueIgnoreCase(dataObject, "btHardLiquorConsume")),
                    intHardLiquorPerWeek = ParseInt(GetValueIgnoreCase(dataObject, "intHardLiquorPerWeek")),
                    intBeerBottlesPerWeek = ParseInt(GetValueIgnoreCase(dataObject, "intBeerBottlesPerWeek")),
                    intBeerBottlesPerMonths = ParseInt(GetValueIgnoreCase(dataObject, "intBeerBottlesPerMonths")),
                    vcBeerStoppedYears = GetValueIgnoreCase(dataObject, "vcBeerStoppedYears"),
                    vcBeerStoppedMonths = GetValueIgnoreCase(dataObject, "vcBeerStoppedMonths"),
                    btWineConsume = ParseBool(GetValueIgnoreCase(dataObject, "btWineConsume")),
                    intWineGlassPerWeek = ParseInt(GetValueIgnoreCase(dataObject, "intWineGlassPerWeek")),
                    intWineBottlesPerMonths = ParseInt(GetValueIgnoreCase(dataObject, "intWineBottlesPerMonths")),
                    vcWineStoppedYear = GetValueIgnoreCase(dataObject, "vcWineStoppedYear"),
                    vcWineStoppedMonths = GetValueIgnoreCase(dataObject, "vcWineStoppedMonths"),
                    btNarcoticOrDrug = ParseBool(GetValueIgnoreCase(dataObject, "btNarcoticOrDrug")),
                    btNarcoticOrDrugStopped = ParseBool(GetValueIgnoreCase(dataObject, "btNarcoticOrDrugStopped")),
                    intQuantityOfDrugConsumePerDay = ParseInt(GetValueIgnoreCase(dataObject, "intQuantityOfDrugConsumePerDay")),
                    btDrugNeverConsume = ParseBool(GetValueIgnoreCase(dataObject, "btDrugNeverConsume")),
                    btDrugStopConsumption = ParseBool(GetValueIgnoreCase(dataObject, "btDrugStopConsumption")),
                    vcDrugStopppedOn = GetValueIgnoreCase(dataObject, "vcDrugStopppedOn"),
                    btDrugMarijuanaConsumed = ParseBool(GetValueIgnoreCase(dataObject, "btDrugMarijuanaConsumed")),
                    intMarijuanaPerDay = ParseInt(GetValueIgnoreCase(dataObject, "intMarijuanaPerDay")),
                    intMarijuanaPerMonth = ParseInt(GetValueIgnoreCase(dataObject, "intMarijuanaPerMonth")),
                    vcMarijuanaStoppedYear = GetValueIgnoreCase(dataObject, "vcMarijuanaStoppedYear"),
                    vcMarijuanaStoppedMonths = GetValueIgnoreCase(dataObject, "vcMarijuanaStoppedMonths"),
                    btDrugCocaineConsumed = ParseBool(GetValueIgnoreCase(dataObject, "btDrugCocaineConsumed")),
                    intCocainePerDay = ParseInt(GetValueIgnoreCase(dataObject, "intCocainePerDay")),
                    intCocainePerMonth = ParseInt(GetValueIgnoreCase(dataObject, "intCocainePerMonth")),
                    vcCocaineStoppedYear = GetValueIgnoreCase(dataObject, "vcCocaineStoppedYear"),
                    vcCocaineStoppedMonths = GetValueIgnoreCase(dataObject, "vcCocaineStoppedMonths"),
                    btDrugAddictiveConsumed = ParseBool(GetValueIgnoreCase(dataObject, "btDrugAddictiveConsumed")),
                    vcNameOfDrug = GetValueIgnoreCase(dataObject, "vcNameOfDrug"),
                    dtDrugConsumptionStopOn = ParseDate(GetValueIgnoreCase(dataObject, "dtDrugConsumptionStopOn")),
                    btHazardousHobbiesSports = ParseBool(GetValueIgnoreCase(dataObject, "btHazardousHobbiesSports")),
                    vcHobbiesOrSportsRisk = GetValueIgnoreCase(dataObject, "vcHobbiesOrSportsRisk"),
                    intAddictiveDrugPerDay = ParseInt(GetValueIgnoreCase(dataObject, "intAddictiveDrugPerDay")),
                    intAddictiveDrugPerMonth = ParseInt(GetValueIgnoreCase(dataObject, "intAddictiveDrugPerMonth")),
                    vcAddictiveDrugStoppedYear = GetValueIgnoreCase(dataObject, "vcAddictiveDrugStoppedYear"),
                    vcAddictiveDrugStoppedMonths = GetValueIgnoreCase(dataObject, "vcAddictiveDrugStoppedMonths"),
                    btHazardousHobbies = ParseBool(GetValueIgnoreCase(dataObject, "btHazardousHobbies")),
                    vcHazardousHobbies = GetValueIgnoreCase(dataObject, "vcHazardousHobbies"),
                    intHazardousHobbies = ParseInt(GetValueIgnoreCase(dataObject, "intHazardousHobbies")),
                    vcOwnAsset = GetValueIgnoreCase(dataObject, "vcOwnAsset"),
                    btOutSideIndiaLast30Days = ParseBool(GetValueIgnoreCase(dataObject, "btOutSideIndiaLast30Days")),
                    btIsHospitalizedForCovid = ParseBool(GetValueIgnoreCase(dataObject, "btIsHospitalizedForCovid")),
                    dtDateOnHospitalizedForCovid = ParseDate(GetValueIgnoreCase(dataObject, "dtDateOnHospitalizedForCovid")),
                    dtDateOnReleaseFromHospitalForCovid = ParseDate(GetValueIgnoreCase(dataObject, "dtDateOnReleaseFromHospitalForCovid")),
                    btIcuRequirement = ParseBool(GetValueIgnoreCase(dataObject, "btIcuRequirement")),
                    btIscomplicationSuffered = ParseBool(GetValueIgnoreCase(dataObject, "btIscomplicationSuffered")),
                    vcComplicationDetails = GetValueIgnoreCase(dataObject, "vcComplicationDetails"),
                    vcOutSideIndiaReason = GetValueIgnoreCase(dataObject, "vcOutSideIndiaReason"),
                    VcWhatForm = GetValueIgnoreCase(dataObject, "VcWhatForm"),
                    intLiquorPegsPerFrequency = ParseInt(GetValueIgnoreCase(dataObject, "intLiquorPegsPerFrequency")),
                    intBeerPintPerFrequency = ParseInt(GetValueIgnoreCase(dataObject, "intBeerPintPerFrequency")),
                    intWineGlassesPerFrequency = ParseInt(GetValueIgnoreCase(dataObject, "intWineGlassesPerFrequency")),
                    btAlchoholWithdrawal = ParseBool(GetValueIgnoreCase(dataObject, "btAlchoholWithdrawal")),
                    vcHazardousReflexTypeIds = GetValueIgnoreCase(dataObject, "vcHazardousReflexTypeIds"),
                    bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "bitIsDeleted")) ?? false,
                    btCovid19 = ParseBool(GetValueIgnoreCase(dataObject, "btCovid19")),
                    btConsumeAlcohol = ParseBool(GetValueIgnoreCase(dataObject, "btConsumeAlcohol")),
                    vcConsumeNarcotics = GetValueIgnoreCase(dataObject, "vcConsumeNarcotics"),
                    intQuantityOfNarcotics = ParseInt(GetValueIgnoreCase(dataObject, "intQuantityOfNarcotics")),
                    dtAdmission = ParseDate(GetValueIgnoreCase(dataObject, "dtAdmission")),
                    dtDischarge = ParseDate(GetValueIgnoreCase(dataObject, "dtDischarge")),
                    btConsumeTobaccoAlchoholNarcoticSubstance = ParseBool(GetValueIgnoreCase(dataObject, "btConsumeTobaccoAlchoholNarcoticSubstance")),
                    btConsumeTobaccoAlchoholNarcoticSubstanceConsumptionStopped = ParseBool(GetValueIgnoreCase(dataObject, "btConsumeTobaccoAlchoholNarcoticSubstanceConsumptionStopped")),
                    vcDurationTobacco = GetValueIgnoreCase(dataObject, "vcDurationTobacco"),
                    vcDurationAlcohol = GetValueIgnoreCase(dataObject, "vcDurationAlcohol"),
                    vcDurationNarcotics = GetValueIgnoreCase(dataObject, "vcDurationNarcotics"),
                    vcDurationSinceStopped = GetValueIgnoreCase(dataObject, "vcDurationSinceStopped"),
                    vcChooseHabbitSubstance = GetValueIgnoreCase(dataObject, "vcChooseHabbitSubstance"),
                    vcReasonForDiscontinuation = GetValueIgnoreCase(dataObject, "vcReasonForDiscontinuation"),
                    intQuantityCigarCigarettesBeediPaan = ParseInt(GetValueIgnoreCase(dataObject, "intQuantityCigarCigarettesBeediPaan")),
                    intQuantityBeerWineHardLiquor = ParseInt(GetValueIgnoreCase(dataObject, "intQuantityBeerWineHardLiquor")),
                    intQuantityAnyNarcotics = ParseInt(GetValueIgnoreCase(dataObject, "intQuantityAnyNarcotics")),
                    vcLastAccessIP = "0.0.0.0",
                    vcCreatedBy = partnerName ?? "system",
                    dtCreateDate = DateTime.Now,
                    vcModifiedBy = null,
                    dtModifiedDate = null,
                    dtDeletedDate = null
                };
                _context.tblPF_LifeStyleDetails.Add(entity);
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<ValidationResultModel> MandateDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            // 1️⃣ Get allowed fields for this partner
            var allowedFields = await _context.PartnerSections
                .Where(ps => ps.PartnerId == partnerId)
                .Join(
                    _context.SectionFields.Include(sf => sf.Field),
                    ps => ps.SectionId,
                    sf => sf.SectionId,
                    (ps, sf) => sf.Field
                )
                .Where(f => !f.IsDeleted)
                .Select(f => new FieldDefinition
                {
                    FieldName = f.FieldName,
                    DataType = f.DataType,
                    Length = f.Length
                })
                .Distinct()
                .ToListAsync();

            var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

            // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
            var normalizedPayload = payload.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
            );

            // 3️⃣ Detect if nested JSON exists
            bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

            JObject dataObject = hasNestedObject
                ? FlattenJson(JObject.FromObject(normalizedPayload))
                : JObject.FromObject(normalizedPayload);

            // 4️⃣ Validate each field
            foreach (var prop in dataObject.Properties())
            {
                var key = prop.Name.ToLower();
                var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                if (!fieldDict.ContainsKey(key))
                {
                    invalidFields.Add($"{prop.Name} (Field Not Configured)");
                    continue;
                }

                var field = fieldDict[key];

                // Validate datatype
                if (!IsValidDataType(value, field.DataType))
                {
                    invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                    continue;
                }

                // Validate length
                if (field.Length.HasValue && value?.Length > field.Length)
                {
                    invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                }
            }

            result.InvalidFields = invalidFields;
            result.Success = invalidFields.Count == 0;

            var partnerData = await _context.tblPartnerDatas
                                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                                    .Select(p => new { p.ApplicationNumber })
                                    .FirstOrDefaultAsync();

            if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
            {
                result.Success = false;
                result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                return result;
            }

            if (result.Success)
            {
                var entity = new tblPF_MandateDetails
                {
                    vcApplicationNumber = partnerData.ApplicationNumber,
                    intPersonalID = ParseInt(GetValueIgnoreCase(dataObject, "intPersonalID")),
                    intAssureType = ParseInt(GetValueIgnoreCase(dataObject, "intAssureType")) ?? 0,
                    vcTransactionId = GetValueIgnoreCase(dataObject, "vcTransactionId"),
                    intPaymentStatus = ParseInt(GetValueIgnoreCase(dataObject, "intPaymentStatus")) ?? 0,
                    intMandateStatus = ParseInt(GetValueIgnoreCase(dataObject, "intMandateStatus")),
                    btIsMandateCase = ParseBool(GetValueIgnoreCase(dataObject, "btIsMandateCase")),
                    dtMandateStartDate = ParseDate(GetValueIgnoreCase(dataObject, "dtMandateStartDate")),
                    dtMandateEndDate = ParseDate(GetValueIgnoreCase(dataObject, "dtMandateEndDate")),
                    vcPaymentType = GetValueIgnoreCase(dataObject, "vcPaymentType"),
                    vcRenewalMode = GetValueIgnoreCase(dataObject, "vcRenewalMode"),
                    dcAmount = ParseDecimal(GetValueIgnoreCase(dataObject, "dcAmount")),
                    vcMandateOption = GetValueIgnoreCase(dataObject, "vcMandateOption"),
                    vcMandateLink = GetValueIgnoreCase(dataObject, "vcMandateLink"),
                    vcChequeDDNumber = GetValueIgnoreCase(dataObject, "vcChequeDDNumber"),
                    vcChequeDDDate = ParseDate(GetValueIgnoreCase(dataObject, "vcChequeDDDate")),
                    ftChequeDDAmount = ParseDecimal(GetValueIgnoreCase(dataObject, "ftChequeDDAmount")),
                    vcBankName = GetValueIgnoreCase(dataObject, "vcBankName"),
                    vcBankBranch = GetValueIgnoreCase(dataObject, "vcBankBranch"),
                    vcBankAccountType = GetValueIgnoreCase(dataObject, "vcBankAccountType"),
                    vcBankAccountNumber = GetValueIgnoreCase(dataObject, "vcBankAccountNumber"),
                    vcBankIFSCCode = GetValueIgnoreCase(dataObject, "vcBankIFSCCode"),
                    vcBankMICRCode = GetValueIgnoreCase(dataObject, "vcBankMICRCode"),
                    btIsAlreadyEInsurance = ParseBool(GetValueIgnoreCase(dataObject, "btIsAlreadyEInsurance")),
                    btOpenEInsuranceAccount = ParseBool(GetValueIgnoreCase(dataObject, "btOpenEInsuranceAccount")),
                    dcTotalInstalmentPremiumWithTaxes = ParseDecimal(GetValueIgnoreCase(dataObject, "dcTotalInstalmentPremiumWithTaxes")),
                    dtChequeDate = ParseDate(GetValueIgnoreCase(dataObject, "dtChequeDate")),
                    ftFixedIncomePayoutPercentage = ParseFloat(GetValueIgnoreCase(dataObject, "ftFixedIncomePayoutPercentage")),
                    ftLumpsumPayoutPercentage = ParseFloat(GetValueIgnoreCase(dataObject, "ftLumpsumPayoutPercentage")),
                    intFirstYearPremiumMode = ParseInt(GetValueIgnoreCase(dataObject, "intFirstYearPremiumMode")),
                    intPaymentFrequency = ParseInt(GetValueIgnoreCase(dataObject, "intPaymentFrequency")),
                    intPayoutFrequency = ParseInt(GetValueIgnoreCase(dataObject, "intPayoutFrequency")),
                    intPayoutOption = ParseInt(GetValueIgnoreCase(dataObject, "intPayoutOption")),
                    intPayoutTerm = ParseInt(GetValueIgnoreCase(dataObject, "intPayoutTerm")),
                    intRenewalPremiumMode = ParseInt(GetValueIgnoreCase(dataObject, "intRenewalPremiumMode")),
                    intRepository = ParseInt(GetValueIgnoreCase(dataObject, "intRepository")),
                    vcAccountNumber = GetValueIgnoreCase(dataObject, "vcAccountNumber"),
                    vcBranch = GetValueIgnoreCase(dataObject, "vcBranch"),
                    vcCardNumber = GetValueIgnoreCase(dataObject, "vcCardNumber"),
                    vcChequeNumber = GetValueIgnoreCase(dataObject, "vcChequeNumber"),
                    vcDemandDraft = GetValueIgnoreCase(dataObject, "vcDemandDraft"),
                    vcDrownOn = GetValueIgnoreCase(dataObject, "vcDrownOn"),
                    vcEInsuranceAccountNumber = GetValueIgnoreCase(dataObject, "vcEInsuranceAccountNumber"),
                    vcIfscCode = GetValueIgnoreCase(dataObject, "vcIfscCode"),
                    vcInsuranceRepositoryName = GetValueIgnoreCase(dataObject, "vcInsuranceRepositoryName"),
                    vcPaymentCardNetwork = GetValueIgnoreCase(dataObject, "vcPaymentCardNetwork"),
                    btIsRecieveEInsurance = ParseBool(GetValueIgnoreCase(dataObject, "btIsRecieveEInsurance")),
                    vcEmail = GetValueIgnoreCase(dataObject, "vcEmail"),
                    vcDemanddraftno = GetValueIgnoreCase(dataObject, "vcDemanddraftno"),
                    dtDemaddraftDate = ParseDate(GetValueIgnoreCase(dataObject, "dtDemaddraftDate")),
                    vcAgentTitle = GetValueIgnoreCase(dataObject, "vcAgentTitle"),
                    vcAgentFirstName = GetValueIgnoreCase(dataObject, "vcAgentFirstName"),
                    vcAgentMiddleName = GetValueIgnoreCase(dataObject, "vcAgentMiddleName"),
                    vcAgentLastName = GetValueIgnoreCase(dataObject, "vcAgentLastName"),
                    vcAgentPhoneNo = GetValueIgnoreCase(dataObject, "vcAgentPhoneNo"),
                    vcAgentEmailID = GetValueIgnoreCase(dataObject, "vcAgentEmailID"),
                    vcPolicyNumber = GetValueIgnoreCase(dataObject, "vcPolicyNumber"),
                    vcPGMandateNo = GetValueIgnoreCase(dataObject, "vcPGMandateNo"),
                    vcMandateMode = GetValueIgnoreCase(dataObject, "vcMandateMode"),
                    vcLifeAsiaMandateNumber = GetValueIgnoreCase(dataObject, "vcLifeAsiaMandateNumber"),
                    vcMandateStatus = GetValueIgnoreCase(dataObject, "vcMandateStatus"),
                    vcReferenceNumber = GetValueIgnoreCase(dataObject, "vcReferenceNumber"),
                    vcPGStatus = GetValueIgnoreCase(dataObject, "vcPGStatus"),
                    vcPGReferenceNo = GetValueIgnoreCase(dataObject, "vcPGReferenceNo"),
                    vcPGMsg = GetValueIgnoreCase(dataObject, "vcPGMsg"),
                    vcInternalBankId = GetValueIgnoreCase(dataObject, "vcInternalBankId"),
                    vcInternalTransactionID = GetValueIgnoreCase(dataObject, "vcInternalTransactionID"),
                    vcToken = GetValueIgnoreCase(dataObject, "vcToken"),
                    vcFactHouse = GetValueIgnoreCase(dataObject, "vcFactHouse"),
                    bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "bitIsDeleted")) ?? false,
                    vcLastAccessIP = "0.0.0.0",
                    vcCreatedBy = partnerName ?? "system",
                    dtCreateDate = DateTime.Now
                };
                _context.tblPF_MandateDetails.Add(entity);
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<ValidationResultModel> MinorDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            // 1️⃣ Get allowed fields for this partner
            var allowedFields = await _context.PartnerSections
                .Where(ps => ps.PartnerId == partnerId)
                .Join(
                    _context.SectionFields.Include(sf => sf.Field),
                    ps => ps.SectionId,
                    sf => sf.SectionId,
                    (ps, sf) => sf.Field
                )
                .Where(f => !f.IsDeleted)
                .Select(f => new FieldDefinition
                {
                    FieldName = f.FieldName,
                    DataType = f.DataType,
                    Length = f.Length
                })
                .Distinct()
                .ToListAsync();

            var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

            // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
            var normalizedPayload = payload.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
            );

            // 3️⃣ Detect if nested JSON exists
            bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

            JObject dataObject = hasNestedObject
                ? FlattenJson(JObject.FromObject(normalizedPayload))
                : JObject.FromObject(normalizedPayload);

            // 4️⃣ Validate each field
            foreach (var prop in dataObject.Properties())
            {
                var key = prop.Name.ToLower();
                var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                if (!fieldDict.ContainsKey(key))
                {
                    invalidFields.Add($"{prop.Name} (Field Not Configured)");
                    continue;
                }

                var field = fieldDict[key];

                // Validate datatype
                if (!IsValidDataType(value, field.DataType))
                {
                    invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                    continue;
                }

                // Validate length
                if (field.Length.HasValue && value?.Length > field.Length)
                {
                    invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                }
            }

            result.InvalidFields = invalidFields;
            result.Success = invalidFields.Count == 0;

            var partnerData = await _context.tblPartnerDatas
                                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                                    .Select(p => new { p.ApplicationNumber })
                                    .FirstOrDefaultAsync();

            if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
            {
                result.Success = false;
                result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                return result;
            }

            if (result.Success)
            {
                var entity = new tblPF_MinorDetails
                {
                    vcApplicationNumber = partnerData.ApplicationNumber,
                    vcLAStudingInClass = GetValueIgnoreCase(dataObject, "vcLAStudingInClass"),
                    vcNameOfSchool = GetValueIgnoreCase(dataObject, "vcNameOfSchool"),
                    btAnyPhysicalProblem = ParseBool(GetValueIgnoreCase(dataObject, "btAnyPhysicalProblem")) ?? false,
                    vcPhysicalProblemDesc = GetValueIgnoreCase(dataObject, "vcPhysicalProblemDesc"),
                    btAnyMedicationRegular = ParseBool(GetValueIgnoreCase(dataObject, "btAnyMedicationRegular")) ?? false,
                    vcAnyMedicationRegularDesc = GetValueIgnoreCase(dataObject, "vcAnyMedicationRegularDesc"),
                    btEpilepsyConvulsions = ParseBool(GetValueIgnoreCase(dataObject, "btEpilepsyConvulsions")) ?? false,
                    vcEpilepsyConvulsionsDesc = GetValueIgnoreCase(dataObject, "vcEpilepsyConvulsionsDesc"),
                    btHeartLung = ParseBool(GetValueIgnoreCase(dataObject, "btHeartLung")) ?? false,
                    vcHeartLungDesc = GetValueIgnoreCase(dataObject, "vcHeartLungDesc"),
                    btDiabetes = ParseBool(GetValueIgnoreCase(dataObject, "btDiabetes")) ?? false,
                    vcDiabetesDesc = GetValueIgnoreCase(dataObject, "vcDiabetesDesc"),
                    btEczema = ParseBool(GetValueIgnoreCase(dataObject, "btEczema")) ?? false,
                    vcEczemaDesc = GetValueIgnoreCase(dataObject, "vcEczemaDesc"),
                    btEatingdisorders = ParseBool(GetValueIgnoreCase(dataObject, "btEatingdisorders")) ?? false,
                    vcEatingdisordersDesc = GetValueIgnoreCase(dataObject, "vcEatingdisordersDesc"),
                    btWhoopingcough = ParseBool(GetValueIgnoreCase(dataObject, "btWhoopingcough")) ?? false,
                    vcWhoopingcoughDesc = GetValueIgnoreCase(dataObject, "vcWhoopingcoughDesc"),
                    btGlandularfever = ParseBool(GetValueIgnoreCase(dataObject, "btGlandularfever")) ?? false,
                    vcGlandularfeverDesc = GetValueIgnoreCase(dataObject, "vcGlandularfeverDesc"),
                    btEarinfection = ParseBool(GetValueIgnoreCase(dataObject, "btEarinfection")) ?? false,
                    vcEarinfectionDesc = GetValueIgnoreCase(dataObject, "vcEarinfectionDesc"),
                    btMeaslesMumps = ParseBool(GetValueIgnoreCase(dataObject, "btMeaslesMumps")) ?? false,
                    vcMeaslesMumpsDesc = GetValueIgnoreCase(dataObject, "vcMeaslesMumpsDesc"),
                    btConvulsions = ParseBool(GetValueIgnoreCase(dataObject, "btConvulsions")) ?? false,
                    vcConvulsionsDesc = GetValueIgnoreCase(dataObject, "vcConvulsionsDesc"),
                    btChickenpox = ParseBool(GetValueIgnoreCase(dataObject, "btChickenpox")) ?? false,
                    vcChickenpoxDesc = GetValueIgnoreCase(dataObject, "vcChickenpoxDesc"),
                    btScarletFever = ParseBool(GetValueIgnoreCase(dataObject, "btScarletFever")) ?? false,
                    vcScarletFeverDesc = GetValueIgnoreCase(dataObject, "vcScarletFeverDesc"),
                    btBronchitisAsthma = ParseBool(GetValueIgnoreCase(dataObject, "btBronchitisAsthma")) ?? false,
                    vcBronchitisAsthmaDesc = GetValueIgnoreCase(dataObject, "vcBronchitisAsthmaDesc"),
                    btHearingProblem = ParseBool(GetValueIgnoreCase(dataObject, "btHearingProblem")) ?? false,
                    vcHearingProblemDesc = GetValueIgnoreCase(dataObject, "vcHearingProblemDesc"),
                    vcProvideDetailsOfAboveSelected = GetValueIgnoreCase(dataObject, "vcProvideDetailsOfAboveSelected"),
                    vcRelevantInformatiom = GetValueIgnoreCase(dataObject, "vcRelevantInformatiom"),
                    vcDetailsOfVaccination = GetValueIgnoreCase(dataObject, "vcDetailsOfVaccination"),
                    intAssureType = ParseInt(GetValueIgnoreCase(dataObject, "intAssureType")),
                    vcotherVaccination = GetValueIgnoreCase(dataObject, "vcotherVaccination"),
                    vcLastAccessIP = "0.0.0.0",
                    vcCreatedBy = partnerName ?? "system",
                    dtCreateDate = DateTime.Now,
                    bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "bitIsDeleted")) ?? false
                };
                _context.tblPF_MinorDetails.Add(entity);
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<ValidationResultModel> NomineeDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            // 1️⃣ Get allowed fields for this partner
            var allowedFields = await _context.PartnerSections
                .Where(ps => ps.PartnerId == partnerId)
                .Join(
                    _context.SectionFields.Include(sf => sf.Field),
                    ps => ps.SectionId,
                    sf => sf.SectionId,
                    (ps, sf) => sf.Field
                )
                .Where(f => !f.IsDeleted)
                .Select(f => new FieldDefinition
                {
                    FieldName = f.FieldName,
                    DataType = f.DataType,
                    Length = f.Length
                })
                .Distinct()
                .ToListAsync();

            var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

            // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
            var normalizedPayload = payload.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
            );

            // 3️⃣ Detect if nested JSON exists
            bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

            JObject dataObject = hasNestedObject
                ? FlattenJson(JObject.FromObject(normalizedPayload))
                : JObject.FromObject(normalizedPayload);

            // 4️⃣ Validate each field
            foreach (var prop in dataObject.Properties())
            {
                var key = prop.Name.ToLower();
                var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                if (!fieldDict.ContainsKey(key))
                {
                    invalidFields.Add($"{prop.Name} (Field Not Configured)");
                    continue;
                }

                var field = fieldDict[key];

                // Validate datatype
                if (!IsValidDataType(value, field.DataType))
                {
                    invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                    continue;
                }

                // Validate length
                if (field.Length.HasValue && value?.Length > field.Length)
                {
                    invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                }
            }

            result.InvalidFields = invalidFields;
            result.Success = invalidFields.Count == 0;

            var partnerData = await _context.tblPartnerDatas
                                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                                    .Select(p => new { p.ApplicationNumber })
                                    .FirstOrDefaultAsync();

            if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
            {
                result.Success = false;
                result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                return result;
            }

            if (result.Success)
            {
                var entity = new tblPF_NomineeDetails
                {
                    intAssureType = ParseInt(GetValueIgnoreCase(dataObject, "intAssureType")),
                    intFamilyDetailsId = ParseInt(GetValueIgnoreCase(dataObject, "intFamilyDetailsId")),
                    vcRelation = GetValueIgnoreCase(dataObject, "vcRelation"),
                    vcApplicationNumber = partnerData.ApplicationNumber,
                    vcTitle = GetValueIgnoreCase(dataObject, "vcTitle"),
                    vcFirstName = GetValueIgnoreCase(dataObject, "vcFirstName") ?? string.Empty,
                    vcMiddleName = GetValueIgnoreCase(dataObject, "vcMiddleName"),
                    vcLastName = GetValueIgnoreCase(dataObject, "vcLastName"),
                    dtDOB = ParseDate(GetValueIgnoreCase(dataObject, "dtDOB")),
                    chGender = GetValueIgnoreCase(dataObject, "chGender"),
                    dcAnnualIncome = ParseDecimal(GetValueIgnoreCase(dataObject, "dcAnnualIncome")),
                    vcMobileNumber = GetValueIgnoreCase(dataObject, "vcMobileNumber"),
                    ftNomineePercentage = ParseDouble(GetValueIgnoreCase(dataObject, "ftNomineePercentage")),
                    vcOccupation = GetValueIgnoreCase(dataObject, "vcOccupation"),
                    btIsAppointee = ParseBool(GetValueIgnoreCase(dataObject, "btIsAppointee")),
                    vcAppointeeRelation = GetValueIgnoreCase(dataObject, "vcAppointeeRelation"),
                    vcAppointeeTile = GetValueIgnoreCase(dataObject, "vcAppointeeTile"),
                    vcAppointeeFirstName = GetValueIgnoreCase(dataObject, "vcAppointeeFirstName"),
                    vcAppointeeMiddleName = GetValueIgnoreCase(dataObject, "vcAppointeeMiddleName"),
                    vcAppointeeLastName = GetValueIgnoreCase(dataObject, "vcAppointeeLastName"),
                    vcAppointeeDob = ParseDate(GetValueIgnoreCase(dataObject, "vcAppointeeDob")),
                    chAppointeeGender = GetValueIgnoreCase(dataObject, "chAppointeeGender"),
                    vcAppointeeContactNumber = GetValueIgnoreCase(dataObject, "vcAppointeeContactNumber"),
                    vcAppointeeCkycNumber = GetValueIgnoreCase(dataObject, "vcAppointeeCkycNumber"),
                    vcAppointeeAddress1 = GetValueIgnoreCase(dataObject, "vcAppointeeAddress1"),
                    vcAppointeeAddress2 = GetValueIgnoreCase(dataObject, "vcAppointeeAddress2"),
                    vcAppointeeLandmark = GetValueIgnoreCase(dataObject, "vcAppointeeLandmark"),
                    vcAppointeeCity = GetValueIgnoreCase(dataObject, "vcAppointeeCity"),
                    vcAppointeeState = GetValueIgnoreCase(dataObject, "vcAppointeeState"),
                    vcAppointeeCountry = GetValueIgnoreCase(dataObject, "vcAppointeeCountry"),
                    vcAppointeePincode = GetValueIgnoreCase(dataObject, "vcAppointeePincode"),
                    btAppointeeAddressSameAsNominee = ParseBool(GetValueIgnoreCase(dataObject, "btAppointeeAddressSameAsNominee")),
                    vcRelationshipWithNominee = GetValueIgnoreCase(dataObject, "vcRelationshipWithNominee"),
                    vcAppointeeSignature = GetValueIgnoreCase(dataObject, "vcAppointeeSignature"),
                    intAgeAtOnset = ParseInt(GetValueIgnoreCase(dataObject, "intAgeAtOnset")),
                    vcLivingOrDeceased = GetValueIgnoreCase(dataObject, "vcLivingOrDeceased"),
                    vcDiagnosis = GetValueIgnoreCase(dataObject, "vcDiagnosis"),
                    vcBankAccountNumber = GetValueIgnoreCase(dataObject, "vcBankAccountNumber"),
                    vcIFSCCODE = GetValueIgnoreCase(dataObject, "vcIFSCCODE"),
                    vcBANKNAME = GetValueIgnoreCase(dataObject, "vcBANKNAME"),
                    vcBranchLocation = GetValueIgnoreCase(dataObject, "vcBranchLocation"),
                    vcAppointeeDedupeMode = GetValueIgnoreCase(dataObject, "vcAppointeeDedupeMode"),
                    vcNomineeDedupeMode = GetValueIgnoreCase(dataObject, "vcNomineeDedupeMode"),
                    vcLifeAsiaClientId = GetValueIgnoreCase(dataObject, "vcLifeAsiaClientId"),
                    dtClientIDGeneratedOn = ParseDate(GetValueIgnoreCase(dataObject, "dtClientIDGeneratedOn")),
                    vcAppointeeLAClientID = GetValueIgnoreCase(dataObject, "vcAppointeeLAClientID"),
                    dtAppointeeClientIDGeneratedOn = ParseDate(GetValueIgnoreCase(dataObject, "dtAppointeeClientIDGeneratedOn")),
                    intDeDupeStatus = ParseInt(GetValueIgnoreCase(dataObject, "intDeDupeStatus")) ?? 0,
                    intImageQCStatus = ParseInt(GetValueIgnoreCase(dataObject, "intImageQCStatus")) ?? 0,
                    intDocQCStatus = ParseInt(GetValueIgnoreCase(dataObject, "intDocQCStatus")) ?? 0,
                    intAppointeeDeDupeStatus = ParseInt(GetValueIgnoreCase(dataObject, "intAppointeeDeDupeStatus")) ?? 0,
                    intAppointeeImageQCStatus = ParseInt(GetValueIgnoreCase(dataObject, "intAppointeeImageQCStatus")) ?? 0,
                    intAppointeeDocQCStatus = ParseInt(GetValueIgnoreCase(dataObject, "intAppointeeDocQCStatus")) ?? 0,
                    vcLastAccessIP = "0.0.0.0",
                    vcCreatedBy = partnerName ?? "system",
                    dtCreateDate = DateTime.Now,
                    bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "bitIsDeleted")) ?? false
                };
                _context.tblPF_NomineeDetails.Add(entity);
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<ValidationResultModel> NRIDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            // 1️⃣ Get allowed fields for this partner
            var allowedFields = await _context.PartnerSections
                .Where(ps => ps.PartnerId == partnerId)
                .Join(
                    _context.SectionFields.Include(sf => sf.Field),
                    ps => ps.SectionId,
                    sf => sf.SectionId,
                    (ps, sf) => sf.Field
                )
                .Where(f => !f.IsDeleted)
                .Select(f => new FieldDefinition
                {
                    FieldName = f.FieldName,
                    DataType = f.DataType,
                    Length = f.Length
                })
                .Distinct()
                .ToListAsync();

            var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

            // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
            var normalizedPayload = payload.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
            );

            // 3️⃣ Detect if nested JSON exists
            bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

            JObject dataObject = hasNestedObject
                ? FlattenJson(JObject.FromObject(normalizedPayload))
                : JObject.FromObject(normalizedPayload);

            // 4️⃣ Validate each field
            foreach (var prop in dataObject.Properties())
            {
                var key = prop.Name.ToLower();
                var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                if (!fieldDict.ContainsKey(key))
                {
                    invalidFields.Add($"{prop.Name} (Field Not Configured)");
                    continue;
                }

                var field = fieldDict[key];

                // Validate datatype
                if (!IsValidDataType(value, field.DataType))
                {
                    invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                    continue;
                }

                // Validate length
                if (field.Length.HasValue && value?.Length > field.Length)
                {
                    invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                }
            }

            result.InvalidFields = invalidFields;
            result.Success = invalidFields.Count == 0;

            var partnerData = await _context.tblPartnerDatas
                                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                                    .Select(p => new { p.ApplicationNumber })
                                    .FirstOrDefaultAsync();

            if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
            {
                result.Success = false;
                result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                return result;
            }

            if (result.Success)
            {
                var entity = new tblPF_NRIDetails
                {
                    intAssureTypeID = ParseInt(GetValueIgnoreCase(dataObject, "intAssureTypeID")),
                    vcApplicationNumber = partnerData.ApplicationNumber,
                    vcTitle = GetValueIgnoreCase(dataObject, "vcTitle"),
                    vcFirstName = GetValueIgnoreCase(dataObject, "vcFirstName"),
                    vcMiddleName = GetValueIgnoreCase(dataObject, "vcMiddleName"),
                    vcLastName = GetValueIgnoreCase(dataObject, "vcLastName"),
                    vcNationality = GetValueIgnoreCase(dataObject, "vcNationality"),
                    vcCurrentResidenceCountry = GetValueIgnoreCase(dataObject, "vcCurrentResidenceCountry"),
                    vcCountryVisited = GetValueIgnoreCase(dataObject, "vcCountryVisited"),
                    dtDateOfLeaving = ParseDate(GetValueIgnoreCase(dataObject, "dtDateOfLeaving")),
                    intIntentedDuration = ParseInt(GetValueIgnoreCase(dataObject, "intIntentedDuration")),
                    vcAddressLine1 = GetValueIgnoreCase(dataObject, "vcAddressLine1"),
                    vcAddressLine2 = GetValueIgnoreCase(dataObject, "vcAddressLine2"),
                    vcCountry = GetValueIgnoreCase(dataObject, "vcCountry"),
                    vcState = GetValueIgnoreCase(dataObject, "vcState"),
                    vcCity = GetValueIgnoreCase(dataObject, "vcCity"),
                    vcPincode = GetValueIgnoreCase(dataObject, "vcPincode"),
                    vcPurposeofstaying = GetValueIgnoreCase(dataObject, "vcPurposeofstaying"),
                    vcPassportnumber = GetValueIgnoreCase(dataObject, "vcPassportnumber"),
                    vcDateofissue = GetValueIgnoreCase(dataObject, "vcDateofissue"),
                    dtPassportvalidity = ParseDate(GetValueIgnoreCase(dataObject, "dtPassportvalidity")),
                    vcRecententry = GetValueIgnoreCase(dataObject, "vcRecententry"),
                    vcCountryofresidence = GetValueIgnoreCase(dataObject, "vcCountryofresidence"),
                    dtResidencedate = ParseDate(GetValueIgnoreCase(dataObject, "dtResidencedate")),
                    vcResidentialstatusfortax = GetValueIgnoreCase(dataObject, "vcResidentialstatusfortax"),
                    vcPermanentAccount = GetValueIgnoreCase(dataObject, "vcPermanentAccount"),
                    btDoyouNriAccount = ParseBool(GetValueIgnoreCase(dataObject, "btDoyouNriAccount")),
                    vcBankname = GetValueIgnoreCase(dataObject, "vcBankname"),
                    vcBankAddressLine1 = GetValueIgnoreCase(dataObject, "vcBankAddressLine1"),
                    vcBankAddressLine2 = GetValueIgnoreCase(dataObject, "vcBankAddressLine2"),
                    vcBankcountry = GetValueIgnoreCase(dataObject, "vcBankcountry"),
                    vcBankstate = GetValueIgnoreCase(dataObject, "vcBankstate"),
                    vcBankcity = GetValueIgnoreCase(dataObject, "vcBankcity"),
                    vcBankpincode = GetValueIgnoreCase(dataObject, "vcBankpincode"),
                    vcTypeofaccount = GetValueIgnoreCase(dataObject, "vcTypeofaccount"),
                    vcBankaccountnumber = GetValueIgnoreCase(dataObject, "vcBankaccountnumber"),
                    vcDispatchedname = GetValueIgnoreCase(dataObject, "vcDispatchedname"),
                    vcDispatchaddress1 = GetValueIgnoreCase(dataObject, "vcDispatchaddress1"),
                    vcDispatchaddress2 = GetValueIgnoreCase(dataObject, "vcDispatchaddress2"),
                    vcDispatchcountry = GetValueIgnoreCase(dataObject, "vcDispatchcountry"),
                    vcDispatchstate = GetValueIgnoreCase(dataObject, "vcDispatchstate"),
                    vcDispatchcity = GetValueIgnoreCase(dataObject, "vcDispatchcity"),
                    vcDispatchpincode = GetValueIgnoreCase(dataObject, "vcDispatchpincode"),
                    vcPaymentmanner = GetValueIgnoreCase(dataObject, "vcPaymentmanner"),
                    vcEcsEnterBankName = GetValueIgnoreCase(dataObject, "vcEcsEnterBankName"),
                    vcLastAccessIP = "0.0.0.0",
                    vcCreatedBy = partnerName ?? "system",
                    dtCreateDate = DateTime.Now,
                    bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "bitIsDeleted")) ?? false
                };
                _context.tblPF_NRIDetails.Add(entity);
                await _context.SaveChangesAsync();
            }

            return result;
        }
        public async Task<ValidationResultModel> OtherInsuranceDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            // 1️⃣ Get allowed fields for this partner
            var allowedFields = await _context.PartnerSections
                .Where(ps => ps.PartnerId == partnerId)
                .Join(
                    _context.SectionFields.Include(sf => sf.Field),
                    ps => ps.SectionId,
                    sf => sf.SectionId,
                    (ps, sf) => sf.Field
                )
                .Where(f => !f.IsDeleted)
                .Select(f => new FieldDefinition
                {
                    FieldName = f.FieldName,
                    DataType = f.DataType,
                    Length = f.Length
                })
                .Distinct()
                .ToListAsync();

            var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

            // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
            var normalizedPayload = payload.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
            );

            // 3️⃣ Detect if nested JSON exists
            bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

            JObject dataObject = hasNestedObject
                ? FlattenJson(JObject.FromObject(normalizedPayload))
                : JObject.FromObject(normalizedPayload);

            // 4️⃣ Validate each field
            foreach (var prop in dataObject.Properties())
            {
                var key = prop.Name.ToLower();
                var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                if (!fieldDict.ContainsKey(key))
                {
                    invalidFields.Add($"{prop.Name} (Field Not Configured)");
                    continue;
                }

                var field = fieldDict[key];

                // Validate datatype
                if (!IsValidDataType(value, field.DataType))
                {
                    invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                    continue;
                }

                // Validate length
                if (field.Length.HasValue && value?.Length > field.Length)
                {
                    invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                }
            }

            result.InvalidFields = invalidFields;
            result.Success = invalidFields.Count == 0;

            var partnerData = await _context.tblPartnerDatas
                                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                                    .Select(p => new { p.ApplicationNumber })
                                    .FirstOrDefaultAsync();

            if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
            {
                result.Success = false;
                result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                return result;
            }

            if (result.Success)
            {
                var entity = new tblPF_OtherInsurances
                {
                    vcApplicationNumber = partnerData.ApplicationNumber,
                    intAssureType = ParseInt(GetValueIgnoreCase(dataObject, "intAssureType")) ?? 0,
                    vcInsurerNumber = GetValueIgnoreCase(dataObject, "vcInsurerNumber"),
                    vcToBeInsured = GetValueIgnoreCase(dataObject, "vcToBeInsured"),
                    vcDeathSumAssured = GetValueIgnoreCase(dataObject, "vcDeathSumAssured"),
                    vcPremiumPerAnnum = GetValueIgnoreCase(dataObject, "vcPremiumPerAnnum"),
                    dtMonthAndYearOfInssuance = ParseDate(GetValueIgnoreCase(dataObject, "dtMonthAndYearOfInssuance")),
                    vcAcceptanceTerm = GetValueIgnoreCase(dataObject, "vcAcceptanceTerm"),
                    vcCurrentStatus = GetValueIgnoreCase(dataObject, "vcCurrentStatus"),
                    vcPolicyOrAppliactionNumber = GetValueIgnoreCase(dataObject, "vcPolicyOrAppliactionNumber"),
                    vcStandardAcceptance = GetValueIgnoreCase(dataObject, "vcStandardAcceptance"),
                    dtProposalDate = ParseDate(GetValueIgnoreCase(dataObject, "dtProposalDate")),
                    vcTypeofpolicyterm = GetValueIgnoreCase(dataObject, "vcTypeofpolicyterm"),
                    vcLastAccessIP = "0.0.0.0",
                    vcCreatedBy = partnerName ?? "system",
                    dtCreateDate = DateTime.Now,
                    vcModifiedBy = null,
                    dtModifiedDate = null,
                    dtDeletedDate = null,
                    bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "bitIsDeleted")) ?? false

                };
                _context.tblPF_OtherInsurances.Add(entity);
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<ValidationResultModel> PartialWithdrawalDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            // 1️⃣ Get allowed fields for this partner
            var allowedFields = await _context.PartnerSections
                .Where(ps => ps.PartnerId == partnerId)
                .Join(
                    _context.SectionFields.Include(sf => sf.Field),
                    ps => ps.SectionId,
                    sf => sf.SectionId,
                    (ps, sf) => sf.Field
                )
                .Where(f => !f.IsDeleted)
                .Select(f => new FieldDefinition
                {
                    FieldName = f.FieldName,
                    DataType = f.DataType,
                    Length = f.Length
                })
                .Distinct()
                .ToListAsync();

            var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

            // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
            var normalizedPayload = payload.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
            );

            // 3️⃣ Detect if nested JSON exists
            bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

            JObject dataObject = hasNestedObject
                ? FlattenJson(JObject.FromObject(normalizedPayload))
                : JObject.FromObject(normalizedPayload);

            // 4️⃣ Validate each field
            foreach (var prop in dataObject.Properties())
            {
                var key = prop.Name.ToLower();
                var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                if (!fieldDict.ContainsKey(key))
                {
                    invalidFields.Add($"{prop.Name} (Field Not Configured)");
                    continue;
                }

                var field = fieldDict[key];

                // Validate datatype
                if (!IsValidDataType(value, field.DataType))
                {
                    invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                    continue;
                }

                // Validate length
                if (field.Length.HasValue && value?.Length > field.Length)
                {
                    invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                }
            }

            result.InvalidFields = invalidFields;
            result.Success = invalidFields.Count == 0;

            var partnerData = await _context.tblPartnerDatas
                                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                                    .Select(p => new { p.ApplicationNumber })
                                    .FirstOrDefaultAsync();

            if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
            {
                result.Success = false;
                result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                return result;
            }

            if (result.Success)
            {
                var entity = new tblpf_PartialWithdrawal
                {

                    vcApplicationNumber = partnerData.ApplicationNumber,
                    intAssureType = ParseInt(GetValueIgnoreCase(dataObject, "intAssureType")) ?? 0,
                    btIsSystematicWithdrawal = ParseBool(GetValueIgnoreCase(dataObject, "btIsSystematicWithdrawal")),
                    dtWithdrawalStartDate = ParseDate(GetValueIgnoreCase(dataObject, "dtWithdrawalStartDate")),
                    dcWithdrawalMonthlyAmount = ParseDecimal(GetValueIgnoreCase(dataObject, "dcWithdrawalMonthlyAmount")),
                    intWithdrawalNumber = ParseInt(GetValueIgnoreCase(dataObject, "intWithdrawalNumber")) ?? 0,
                    vcLastAccessIP = "0.0.0.0",
                    vcCreatedBy = partnerName ?? "system",
                    dtCreateDate = DateTime.Now,
                    vcModifiedBy = null,
                    dtModifiedDate = null,
                    dtDeletedDate = null,
                    bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "bitIsDeleted")) ?? false
                };
                _context.tblpf_PartialWithdrawals.Add(entity);
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<ValidationResultModel> PaymentDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            // 1️⃣ Get allowed fields for this partner
            var allowedFields = await _context.PartnerSections
                .Where(ps => ps.PartnerId == partnerId)
                .Join(
                    _context.SectionFields.Include(sf => sf.Field),
                    ps => ps.SectionId,
                    sf => sf.SectionId,
                    (ps, sf) => sf.Field
                )
                .Where(f => !f.IsDeleted)
                .Select(f => new FieldDefinition
                {
                    FieldName = f.FieldName,
                    DataType = f.DataType,
                    Length = f.Length
                })
                .Distinct()
                .ToListAsync();

            var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

            // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
            var normalizedPayload = payload.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
            );

            // 3️⃣ Detect if nested JSON exists
            bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

            JObject dataObject = hasNestedObject
                ? FlattenJson(JObject.FromObject(normalizedPayload))
                : JObject.FromObject(normalizedPayload);

            // 4️⃣ Validate each field
            foreach (var prop in dataObject.Properties())
            {
                var key = prop.Name.ToLower();
                var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                if (!fieldDict.ContainsKey(key))
                {
                    invalidFields.Add($"{prop.Name} (Field Not Configured)");
                    continue;
                }

                var field = fieldDict[key];

                // Validate datatype
                if (!IsValidDataType(value, field.DataType))
                {
                    invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                    continue;
                }

                // Validate length
                if (field.Length.HasValue && value?.Length > field.Length)
                {
                    invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                }
            }

            result.InvalidFields = invalidFields;
            result.Success = invalidFields.Count == 0;

            var partnerData = await _context.tblPartnerDatas
                                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                                    .Select(p => new { p.ApplicationNumber })
                                    .FirstOrDefaultAsync();

            if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
            {
                result.Success = false;
                result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                return result;
            }

            if (result.Success)
            {
                var entity = new tblPF_PaymentDetails
                {
                    vcApplicationNumber = partnerData.ApplicationNumber,
                    intPersonalID = ParseInt(GetValueIgnoreCase(dataObject, "intPersonalID")),
                    intAssureType = ParseInt(GetValueIgnoreCase(dataObject, "intAssureType")) ?? 0,
                    vcTransactionId = GetValueIgnoreCase(dataObject, "vcTransactionId"),
                    intPaymentStatus = ParseInt(GetValueIgnoreCase(dataObject, "intPaymentStatus")) ?? 0,
                    intMandateStatus = ParseInt(GetValueIgnoreCase(dataObject, "intMandateStatus")),
                    btIsMandateCase = ParseBool(GetValueIgnoreCase(dataObject, "btIsMandateCase")),
                    dtMandateStartDate = ParseDate(GetValueIgnoreCase(dataObject, "dtMandateStartDate")),
                    dtMandateEndDate = ParseDate(GetValueIgnoreCase(dataObject, "dtMandateEndDate")),
                    vcPaymentType = GetValueIgnoreCase(dataObject, "vcPaymentType"),
                    vcRenewalMode = GetValueIgnoreCase(dataObject, "vcRenewalMode"),
                    dcAmount = ParseDecimal(GetValueIgnoreCase(dataObject, "dcAmount")),
                    vcMandateOption = GetValueIgnoreCase(dataObject, "vcMandateOption"),
                    vcMandateLink = GetValueIgnoreCase(dataObject, "vcMandateLink"),
                    vcChequeDDNumber = GetValueIgnoreCase(dataObject, "vcChequeDDNumber"),
                    vcChequeDDDate = ParseDate(GetValueIgnoreCase(dataObject, "vcChequeDDDate")),
                    ftChequeDDAmount = ParseDecimal(GetValueIgnoreCase(dataObject, "ftChequeDDAmount")),
                    vcBankName = GetValueIgnoreCase(dataObject, "vcBankName"),
                    vcBankBranch = GetValueIgnoreCase(dataObject, "vcBankBranch"),
                    vcBankAccountType = GetValueIgnoreCase(dataObject, "vcBankAccountType"),
                    vcBankAccountNumber = GetValueIgnoreCase(dataObject, "vcBankAccountNumber"),
                    vcBankIFSCCode = GetValueIgnoreCase(dataObject, "vcBankIFSCCode"),
                    vcBankMICRCode = GetValueIgnoreCase(dataObject, "vcBankMICRCode"),
                    btIsAlreadyEInsurance = ParseBool(GetValueIgnoreCase(dataObject, "btIsAlreadyEInsurance")),
                    btOpenEInsuranceAccount = ParseBool(GetValueIgnoreCase(dataObject, "btOpenEInsuranceAccount")),
                    dcTotalInstalmentPremiumWithTaxes = ParseDecimal(GetValueIgnoreCase(dataObject, "dcTotalInstalmentPremiumWithTaxes")),
                    dtChequeDate = ParseDate(GetValueIgnoreCase(dataObject, "dtChequeDate")),
                    ftFixedIncomePayoutPercentage = ParseFloat(GetValueIgnoreCase(dataObject, "ftFixedIncomePayoutPercentage")),
                    ftLumpsumPayoutPercentage = ParseFloat(GetValueIgnoreCase(dataObject, "ftLumpsumPayoutPercentage")),
                    intFirstYearPremiumMode = ParseInt(GetValueIgnoreCase(dataObject, "intFirstYearPremiumMode")),
                    intPaymentFrequency = ParseInt(GetValueIgnoreCase(dataObject, "intPaymentFrequency")),
                    intPayoutFrequency = ParseInt(GetValueIgnoreCase(dataObject, "intPayoutFrequency")),
                    intPayoutOption = ParseInt(GetValueIgnoreCase(dataObject, "intPayoutOption")),
                    intPayoutTerm = ParseInt(GetValueIgnoreCase(dataObject, "intPayoutTerm")),
                    intRenewalPremiumMode = ParseInt(GetValueIgnoreCase(dataObject, "intRenewalPremiumMode")),
                    intRepository = ParseInt(GetValueIgnoreCase(dataObject, "intRepository")),
                    vcAccountNumber = GetValueIgnoreCase(dataObject, "vcAccountNumber"),
                    vcBranch = GetValueIgnoreCase(dataObject, "vcBranch"),
                    vcCardNumber = GetValueIgnoreCase(dataObject, "vcCardNumber"),
                    vcChequeNumber = GetValueIgnoreCase(dataObject, "vcChequeNumber"),
                    vcDemandDraft = GetValueIgnoreCase(dataObject, "vcDemandDraft"),
                    vcDrownOn = GetValueIgnoreCase(dataObject, "vcDrownOn"),
                    vcEInsuranceAccountNumber = GetValueIgnoreCase(dataObject, "vcEInsuranceAccountNumber"),
                    vcIfscCode = GetValueIgnoreCase(dataObject, "vcIfscCode"),
                    vcInsuranceRepositoryName = GetValueIgnoreCase(dataObject, "vcInsuranceRepositoryName"),
                    vcPaymentCardNetwork = GetValueIgnoreCase(dataObject, "vcPaymentCardNetwork"),
                    btIsRecieveEInsurance = ParseBool(GetValueIgnoreCase(dataObject, "btIsRecieveEInsurance")),
                    vcEmail = GetValueIgnoreCase(dataObject, "vcEmail"),
                    vcDemanddraftno = GetValueIgnoreCase(dataObject, "vcDemanddraftno"),
                    dtDemaddraftDate = ParseDate(GetValueIgnoreCase(dataObject, "dtDemaddraftDate")),
                    vcAgentTitle = GetValueIgnoreCase(dataObject, "vcAgentTitle"),
                    vcAgentFirstName = GetValueIgnoreCase(dataObject, "vcAgentFirstName"),
                    vcAgentMiddleName = GetValueIgnoreCase(dataObject, "vcAgentMiddleName"),
                    vcAgentLastName = GetValueIgnoreCase(dataObject, "vcAgentLastName"),
                    vcAgentPhoneNo = GetValueIgnoreCase(dataObject, "vcAgentPhoneNo"),
                    vcAgentEmailID = GetValueIgnoreCase(dataObject, "vcAgentEmailID"),
                    vcPolicyNumber = GetValueIgnoreCase(dataObject, "vcPolicyNumber"),
                    vcReferenceNumber = GetValueIgnoreCase(dataObject, "vcReferenceNumber"),
                    vcPGReferenceNo = GetValueIgnoreCase(dataObject, "vcPGReferenceNo"),
                    vcInternalTransactionID = GetValueIgnoreCase(dataObject, "vcInternalTransactionID"),
                    vcLastAccessIP = "0.0.0.0",
                    vcCreatedBy = partnerName ?? "system",
                    dtCreateDate = DateTime.Now,
                    vcModifiedBy = null,
                    dtModifiedDate = null,
                    dtDeletedDate = null,
                    bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "bitIsDeleted")) ?? false

                };
                _context.tblPF_PaymentDetails.Add(entity);
                await _context.SaveChangesAsync();
            }

            return result;
        }
        public async Task<ValidationResultModel> PersonalDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName)
        {
            var result = new ValidationResultModel();
            var invalidFields = new List<string>();

            // 1️⃣ Get allowed fields for this partner
            var allowedFields = await _context.PartnerSections
                .Where(ps => ps.PartnerId == partnerId)
                .Join(
                    _context.SectionFields.Include(sf => sf.Field),
                    ps => ps.SectionId,
                    sf => sf.SectionId,
                    (ps, sf) => sf.Field
                )
                .Where(f => !f.IsDeleted)
                .Select(f => new FieldDefinition
                {
                    FieldName = f.FieldName,
                    DataType = f.DataType,
                    Length = f.Length
                })
                .Distinct()
                .ToListAsync();

            var fieldDict = allowedFields.ToDictionary(f => f.FieldName.ToLower(), f => f);

            // 2️⃣ Normalize payload (convert any JsonElement values to normal .NET types)
            var normalizedPayload = payload.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value is JsonElement je ? GetJsonElementValue(je) : kvp.Value
            );

            // 3️⃣ Detect if nested JSON exists
            bool hasNestedObject = normalizedPayload.Values.Any(v => v is JObject || v is Dictionary<string, object>);

            JObject dataObject = hasNestedObject
                ? FlattenJson(JObject.FromObject(normalizedPayload))
                : JObject.FromObject(normalizedPayload);

            // 4️⃣ Validate each field
            foreach (var prop in dataObject.Properties())
            {
                var key = prop.Name.ToLower();
                var value = prop.Value?.Type == JTokenType.Null ? null : prop.Value?.ToString();

                if (!fieldDict.ContainsKey(key))
                {
                    invalidFields.Add($"{prop.Name} (Field Not Configured)");
                    continue;
                }

                var field = fieldDict[key];

                // Validate datatype
                if (!IsValidDataType(value, field.DataType))
                {
                    invalidFields.Add($"{prop.Name} (Invalid DataType: Expected {field.DataType})");
                    continue;
                }

                // Validate length
                if (field.Length.HasValue && value?.Length > field.Length)
                {
                    invalidFields.Add($"{prop.Name} (Length Exceeded: Max {field.Length})");
                }
            }

            result.InvalidFields = invalidFields;
            result.Success = invalidFields.Count == 0;

            var partnerData = await _context.tblPartnerDatas
                                    .Where(p => p.PartnerID == partnerId && !p.IsDeleted)
                                    .Select(p => new { p.ApplicationNumber })
                                    .FirstOrDefaultAsync();

            if (partnerData == null || string.IsNullOrEmpty(partnerData.ApplicationNumber))
            {
                result.Success = false;
                result.InvalidFields.Add("ApplicationNumber (Not Found for given PartnerId)");
                return result;
            }

            if (result.Success)
            {
                var entity = new tblPF_PersonalDetails
                {

                    vcApplicationNumber = partnerData.ApplicationNumber,
                    intAssureType = ParseInt(GetValueIgnoreCase(dataObject, "intAssureType")) ?? 0,
                    vcTitle = GetValueIgnoreCase(dataObject, "vcTitle"),
                    vcCompanyName = GetValueIgnoreCase(dataObject, "vcCompanyName"),
                    vcFirstName = GetValueIgnoreCase(dataObject, "vcFirstName"),
                    vcMiddleName = GetValueIgnoreCase(dataObject, "vcMiddleName"),
                    vcLastName = GetValueIgnoreCase(dataObject, "vcLastName"),
                    vcNameBeforeMarriageTitle = GetValueIgnoreCase(dataObject, "vcNameBeforeMarriageTitle"),
                    vcNameBeforeMarriageFirstName = GetValueIgnoreCase(dataObject, "vcNameBeforeMarriageFirstName"),
                    vcNameBeforeMarriageMiddleName = GetValueIgnoreCase(dataObject, "vcNameBeforeMarriageMiddleName"),
                    vcNameBeforeMarriageLastName = GetValueIgnoreCase(dataObject, "vcNameBeforeMarriageLastName"),
                    vcAdharNumber = GetValueIgnoreCase(dataObject, "vcAdharNumber"),
                    vcPANNumber = GetValueIgnoreCase(dataObject, "vcPANNumber"),
                    intMaritalStatus = ParseInt(GetValueIgnoreCase(dataObject, "intMaritalStatus")),
                    intNumberOfChildren = ParseInt(GetValueIgnoreCase(dataObject, "intNumberOfChildren")),
                    vcCKYCNumber = GetValueIgnoreCase(dataObject, "vcCKYCNumber"),
                    vcEducationQualification = GetValueIgnoreCase(dataObject, "vcEducationQualification"),
                    dcAnnualIncome = ParseDecimal(GetValueIgnoreCase(dataObject, "dcAnnualIncome")),
                    vcAUCustomerId = GetValueIgnoreCase(dataObject, "vcAUCustomerId"),
                    vcAUSavingsOrLoanAccountNumber = GetValueIgnoreCase(dataObject, "vcAUSavingsOrLoanAccountNumber"),
                    chGender = string.IsNullOrWhiteSpace(GetValueIgnoreCase(dataObject, "chGender"))
                ? (char?)null
                : GetValueIgnoreCase(dataObject, "chGender")![0],
                    dtDOB = ParseDate(GetValueIgnoreCase(dataObject, "dtDOB")),
                    vcCountryOfBirth = GetValueIgnoreCase(dataObject, "vcCountryOfBirth"),
                    intNationality = ParseInt(GetValueIgnoreCase(dataObject, "intNationality")),
                    btIsCriminalRecord = ParseBool(GetValueIgnoreCase(dataObject, "btIsCriminalRecord")),
                    vcCriminalCaseDescription = GetValueIgnoreCase(dataObject, "vcCriminalCaseDescription"),
                    btRelatedToPoliticalParty = ParseBool(GetValueIgnoreCase(dataObject, "btRelatedToPoliticalParty")),
                    vcPoliticalDescription = GetValueIgnoreCase(dataObject, "vcPoliticalDescription"),

                    vcRelationWithLA = GetValueIgnoreCase(dataObject, "vcRelationWithLA"),
                    vcInterNationalNumber = GetValueIgnoreCase(dataObject, "vcInterNationalNumber"),
                    vcCountryOfResidence = GetValueIgnoreCase(dataObject, "vcCountryOfResidence"),

                    btPanVerified = ParseBool(GetValueIgnoreCase(dataObject, "btPanVerified")),
                    btPanDOBMatch = ParseBool(GetValueIgnoreCase(dataObject, "btPanDOBMatch")),
                    btPanNameMatch = ParseBool(GetValueIgnoreCase(dataObject, "btPanNameMatch")),
                    vcPanDublicate = GetValueIgnoreCase(dataObject, "vcPanDublicate"),
                    vcRequestId = GetValueIgnoreCase(dataObject, "vcRequestId"),
                    btisPanVerify = ParseBool(GetValueIgnoreCase(dataObject, "btisPanVerify")),
                    intAMLStatus = ParseInt(GetValueIgnoreCase(dataObject, "intAMLStatus")),
                    vcCibilScore = GetValueIgnoreCase(dataObject, "vcCibilScore"),
                    vcIncomeEstimator = GetValueIgnoreCase(dataObject, "vcIncomeEstimator"),
                    vcIIBScore = GetValueIgnoreCase(dataObject, "vcIIBScore"),
                    vcDedupeMode = GetValueIgnoreCase(dataObject, "vcDedupeMode"),

                    vcLastAccessIP = "0.0.0.0",
                    vcCreatedBy = partnerName ?? "system",
                    dtCreateDate = DateTime.Now,
                    vcModifiedBy = null,
                    dtModifiedDate = null,
                    dtDeletedDate = null,
                    bitIsDeleted = ParseBool(GetValueIgnoreCase(dataObject, "bitIsDeleted")) ?? false,
                    intDeDupeStatus = 0,
                    intImageQCStatus = 0,
                    intDocQCStatus = 0,
                    SysStartTime = DateTime.Now,
                    SysEndTime = DateTime.Parse("9999-12-31 23:59:59")

                };
                _context.tblPF_PersonalDetails.Add(entity);
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<string> SubmitDataAsync(int partnerId, Dictionary<string, object> payload)
        {
            
            await Task.Delay(10);

         
            return "Data Inserted Successfully";
        }


        // ✅ Converts JsonElement to normal .NET values
        private object GetJsonElementValue(JsonElement element)
        {
            return element.ValueKind switch
            {
                JsonValueKind.String => element.GetString(),
                JsonValueKind.Number => element.TryGetInt64(out long l)
                    ? l
                    : element.TryGetDecimal(out decimal d)
                        ? d
                        : (object)element.GetRawText(),
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                JsonValueKind.Null => null,
                JsonValueKind.Object => JObject.Parse(element.GetRawText()),
                JsonValueKind.Array => JArray.Parse(element.GetRawText()),
                _ => element.GetRawText()
            };
        }


        private bool IsValidDataType(string value, string dataType)
        {
            if (value == null)
                return true; // Allow nulls — handled elsewhere

            switch (dataType.ToLower())
            {
                case "int":
                case "integer":
                    return int.TryParse(value, out _);

                case "decimal":
                    return decimal.TryParse(value, out _);

                case "double":
                    return double.TryParse(value, out _);

                case "bit":
                case "boolean":
                    return bool.TryParse(value, out _);

                case "datetime":
                    return DateTime.TryParse(value, out _);

                case "date":
                    // Validate only date part (ignore time)
                    return DateTime.TryParse(value, out var dt) && dt.TimeOfDay == TimeSpan.Zero;

                case "char":
                case "nchar":
                    return value.Length == 1;

                case "nvarchar":
                case "varchar":
                case "string":
                    return true;

                default:
                    return true; // Fallback: treat as valid for unknown types
            }
        }


        private JObject FlattenJson(JObject original)
        {
            var flat = new JObject();

            foreach (var prop in original.Properties())
            {
                if (prop.Value.Type == JTokenType.Object)
                {
                    var nested = (JObject)prop.Value;
                    foreach (var nestedProp in nested.Properties())
                        flat[nestedProp.Name] = nestedProp.Value;
                }
                else
                {
                    flat[prop.Name] = prop.Value;
                }
            }

            return flat;
        }

        private int? ParseInt(string value)
        {
            if (int.TryParse(value, out var v)) return v;
            return null;
        }

        private decimal? ParseDecimal(string value)
        {
            if (decimal.TryParse(value, out var v)) return v;
            return null;
        }

        private DateTime? ParseDateNullable(string value)
        {
            if (DateTime.TryParse(value, out var dt)) return dt;
            return null;
        }

        private static bool? ParseBool(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            value = value.Trim().ToLowerInvariant();

            return value switch
            {
                "true" or "1" or "yes" => true,
                "false" or "0" or "no" => false,
                _ => null
            };
        }


        private double? ParseDouble(string value)
        {
            if (double.TryParse(value, out var v)) return v;
            return null;
        }
        private float? ParseFloat(string value)
        {
            if (float.TryParse(value, out var v)) return v;
            return null;
        }

        private DateTime? ParseDate(object? value)
        {
            if (value == null) return null;
            if (DateTime.TryParse(value.ToString(), out DateTime result))
                return result;

            return null;
        }
        private static string? GetValueIgnoreCase(JObject obj, string propertyName)
        {
            var prop = obj.Properties()
                .FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
            return prop?.Value?.ToString();
        }
    }
}
