using System.Text.Json.Serialization;

namespace PartnerFlowAPI.Services.Features.DocumentRequired.Model
{
    public class DocumentModel
    {
        public string ApplicationNumber { get; set; }
        public List<DocList> Documents { get; set; }
    }
    public class DocList
    {
        public int docrequirementid { get; set; }
        public int AssureType { get; set; }
        public int DocumentTypeId { get; set; }
        public int? SubType { get; set; }
        public string? DocumentName { get; set; }
        public string? DMSName { get; set; }
        public string? DocumentType { get; set; }
        public string? Comment { get; set; }
        public string? DocComment { get; set; }
        public string? FileUrl { get; set; }
        public string? DMSFileUrl { get; set; }
        public string? Reason { get; set; }
        public int Row { get; set; }
        public bool? Resubmit { get; set; }
        public int Status { get; set; }

        public int? UWrequirementID { get; set; }
        public string? UWfollowupcode { get; set; }
        public bool AutoGenrated { get; set; }
        [JsonIgnore]
        public int? ResubmissionStatus { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsForEditApp { get; set; }
    }

}
