//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MedApp.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Prescriptions
    {
        public int Id { get; set; }
        public Nullable<int> DoctorId { get; set; }
        public Nullable<int> PatientId { get; set; }
        public string Medication { get; set; }
        public string Dosage { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
    
        public virtual Doctors Doctors { get; set; }
        public virtual Patients Patients { get; set; }
    }
}
