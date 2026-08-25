namespace PartnerFlowAPI.Database.Enums
{
    public enum ApplicationStatus
    {
        BI = 0, // BI Generated
        New = 1,//appplication is new
        DataEntry,//data entry is pending 2
        DataEntryCompleted,// data entry complete (confirmed) 3
        Payment,//payment is done 4 
        Documentupload,//application is at doc upload 5 
        SCR,// application is at scr 6 
        Submitted,//scr done 7 
        Dedupe, //8
        Recipting, //9
        QC, //10
        Resubmission,// resubmistion 11
        ResubmissionCompleted,// resubmistion completed 12
        DataUpdate, //13
        UW, //14
        UWResubmission, //15
        UWResubmissionCompleted, //16
        PolicyIssued, //17
        Reject //18
    }
    public static class ApplicationStatusExtensions
    {
        private static readonly Dictionary<ApplicationStatus, string> StatusDescriptions = new()
    {
        { ApplicationStatus.BI, "BI Generated" },
        { ApplicationStatus.New, "E-App Number Generated" },
        { ApplicationStatus.DataEntry, "Data Entry" },
        { ApplicationStatus.DataEntryCompleted, "Data Entry Completed" },
        { ApplicationStatus.Payment, "Payment" },
        { ApplicationStatus.Documentupload, "Document Uploaded" },
        { ApplicationStatus.SCR, "SCR" },
        { ApplicationStatus.Submitted, "Submitted" },
        { ApplicationStatus.Dedupe, "Dedupe" },
        { ApplicationStatus.Recipting, "Recipting" },
        { ApplicationStatus.QC, "QC" },
        { ApplicationStatus.DataUpdate, "Data Update" },
        { ApplicationStatus.Resubmission, "Resubmission" },
        { ApplicationStatus.ResubmissionCompleted, "Resubmission Completed" },
        { ApplicationStatus.UW, "UW" },
        { ApplicationStatus.PolicyIssued, "Policy Issued" },
        { ApplicationStatus.Reject, "Rejected" },
        { ApplicationStatus.UWResubmission, "Underwriter Followup Code Generated" },
        { ApplicationStatus.UWResubmissionCompleted, "Underwriter Followup Completed" },
    };

        public static string GetDescription(this ApplicationStatus status)
        {
            return StatusDescriptions.TryGetValue(status, out var description)
                ? description
                : "Unknown Status";
        }

    }

}
