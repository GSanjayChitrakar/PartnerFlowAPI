using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourNamespace.Entities
{
    public class tblPF_MinorDetails
    {
        [Key]
        public int MinorFormId { get; set; }

        [StringLength(50)]
        public string? vcApplicationNumber { get; set; }

        [StringLength(100)]
        public string? vcLAStudingInClass { get; set; }

        [StringLength(200)]
        public string? vcNameOfSchool { get; set; }

        public bool btAnyPhysicalProblem { get; set; }
        [StringLength(200)]
        public string? vcPhysicalProblemDesc { get; set; }

        public bool btAnyMedicationRegular { get; set; }
        [StringLength(200)]
        public string? vcAnyMedicationRegularDesc { get; set; }

        public bool btEpilepsyConvulsions { get; set; }
        [StringLength(200)]
        public string? vcEpilepsyConvulsionsDesc { get; set; }

        public bool btHeartLung { get; set; }
        [StringLength(200)]
        public string? vcHeartLungDesc { get; set; }

        public bool btDiabetes { get; set; }
        [StringLength(200)]
        public string? vcDiabetesDesc { get; set; }

        public bool btEczema { get; set; }
        [StringLength(200)]
        public string? vcEczemaDesc { get; set; }

        public bool btEatingdisorders { get; set; }
        [StringLength(200)]
        public string? vcEatingdisordersDesc { get; set; }

        public bool btWhoopingcough { get; set; }
        [StringLength(200)]
        public string? vcWhoopingcoughDesc { get; set; }

        public bool btGlandularfever { get; set; }
        [StringLength(200)]
        public string? vcGlandularfeverDesc { get; set; }

        public bool btEarinfection { get; set; }
        [StringLength(200)]
        public string? vcEarinfectionDesc { get; set; }

        public bool btMeaslesMumps { get; set; }
        [StringLength(200)]
        public string? vcMeaslesMumpsDesc { get; set; }

        public bool btConvulsions { get; set; }
        [StringLength(200)]
        public string? vcConvulsionsDesc { get; set; }

        public bool btChickenpox { get; set; }
        [StringLength(200)]
        public string? vcChickenpoxDesc { get; set; }

        public bool btScarletFever { get; set; }
        [StringLength(200)]
        public string? vcScarletFeverDesc { get; set; }

        public bool btBronchitisAsthma { get; set; }
        [StringLength(200)]
        public string? vcBronchitisAsthmaDesc { get; set; }

        public bool btHearingProblem { get; set; }
        [StringLength(200)]
        public string? vcHearingProblemDesc { get; set; }

        [StringLength(200)]
        public string? vcProvideDetailsOfAboveSelected { get; set; }

        [StringLength(200)]
        public string? vcRelevantInformatiom { get; set; }

        [StringLength(1000)]
        public string? vcDetailsOfVaccination { get; set; }

        public int? intAssureType { get; set; }

        [StringLength(100)]
        public string? vcLastAccessIP { get; set; }

        [StringLength(100)]
        public string? vcCreatedBy { get; set; }

        public DateTime? dtCreateDate { get; set; }

        [StringLength(100)]
        public string? vcModifiedBy { get; set; }

        public DateTime? dtModifiedDate { get; set; }

        public DateTime? dtDeletedDate { get; set; }

        public bool? bitIsDeleted { get; set; }

        [StringLength(200)]
        public string? vcotherVaccination { get; set; }
    }
}
