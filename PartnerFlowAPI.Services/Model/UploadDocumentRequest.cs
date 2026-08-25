using PartnerFlowAPI.Database.Enums;

namespace ProposalFromService.Application.Model
{
    public class UploadDocumentRequest
    {
        public string ApplicationNumber { get; set; }
        public AssureType AssureType { get; set; }
        public string? FileData { get; set; }
        public string? FileUrl { get; set; }
        public string? DmsUrl { get; set; }
        public string FileName { get; set; }
        public int? DocumentTypeId { get; set; }
        public int? SubTypeId { get; set; }
        public string? DocumentComment { get; set; }
        public bool? IsResubmited { get; set; } = false;
        public int RowId { get; set; } = 1;
        public int? UWrequirementID { get; set; }
        public string? UWfollowupcode { get; set; }
        public bool? IsForEditApp { get; set; }
    }
}
