using FluentValidation;

namespace ProposalFromService.Application.Model
{
    public class UploadDocumentRequestValidator : AbstractValidator<UploadDocumentRequest>
    {
        public UploadDocumentRequestValidator()
        {
            //RuleFor(request => request.FileData)
            //    .NotNull().WithMessage("File cannot be null.")
            //    .NotEmpty().WithMessage("File cannot be empty.");


            List<int> allowedTypes = new List<int> { 2, 5, 7, 43, 44, 541, 542, 40, 41 };

            RuleFor(request => request.ApplicationNumber)
                .NotEmpty().WithMessage("ApplicationNumber is required.")
                .MaximumLength(255).WithMessage("ApplicationNumber cannot exceed 255 characters.");

            RuleFor(request => request.DocumentTypeId)
                .GreaterThan(0).WithMessage("DocumentTypeId must be greater than 0.");

            //Add condition for Age, Address and Identity Proof for Subtype ID
            RuleFor(request => request.SubTypeId)
                    .GreaterThan(0)
                    .WithMessage("Sub-TypeId is required when Document TypeId is for Age, Address or Identity Proof.")
                    .When(request => request.DocumentTypeId.HasValue && allowedTypes.Contains(request.DocumentTypeId.Value));

            //Add confition for AssureType to be 1,2 or 3
            RuleFor(request => request.AssureType)
                .NotEmpty()
                .IsInEnum().WithMessage("Invalid Assure Type");


            //RuleFor(request => request.DocumentCommetn)
            //    .MaximumLength(500).WithMessage("DocumentComment cannot exceed 500 characters.");
        }
    }
    public class UploadDocumentRequestListValidator : AbstractValidator<List<UploadDocumentRequest>>
    {
        public UploadDocumentRequestListValidator()
        {
            RuleForEach(r => r).SetValidator(new UploadDocumentRequestValidator());
        }
    }
}
