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
    
    public partial class PatientDiseases
    {
        public int Id { get; set; }
        public Nullable<int> PatientId { get; set; }
        public Nullable<int> DiseaseId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
    
        public virtual Diseases Diseases { get; set; }
        public virtual Patients Patients { get; set; }
    }
}