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
    
    public partial class PatientLabTests
    {
        public int Id { get; set; }
        public Nullable<int> PatientId { get; set; }
        public Nullable<int> DoctorId { get; set; }
        public Nullable<int> LabTestId { get; set; }
        public System.DateTime TestDate { get; set; }
        public string Result { get; set; }
        public string Note { get; set; }
        public int StatusId { get; set; }
    
        public virtual Doctors Doctors { get; set; }
        public virtual LabTests LabTests { get; set; }
        public virtual Patients Patients { get; set; }
        public virtual Status Status { get; set; }
    }
}
