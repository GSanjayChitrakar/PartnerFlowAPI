namespace PartnerFlowAPI.Services.Features.DocumentRequired.Model
{
    public class DocumnetTypeModel
    {
        public string ApplicationType { get; set; }
        public List<DocTypeMode>? DocumentType { get; set; }
    }
    public class DocTypeMode
    {
        public int? TypeId { get; set; }
        public int? CommonTypeId { get; set; }
        public string? Name { get; set; }
        public bool IsMultiple { get; set; }
        public List<SubTypeModel>? SubType { get; set; }

    }
    public class SubTypeModel
    {
        public int? SubTypeId { get; set; }
        public string? Name { get; set; }
    }
}
