﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MedAppDBEntities : DbContext
    {
        public MedAppDBEntities()
            : base("name=MedAppDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<Diseases> Diseases { get; set; }
        public virtual DbSet<Doctors> Doctors { get; set; }
        public virtual DbSet<LabTests> LabTests { get; set; }
        public virtual DbSet<PatientDiseases> PatientDiseases { get; set; }
        public virtual DbSet<PatientLabTests> PatientLabTests { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }
        public virtual DbSet<Prescriptions> Prescriptions { get; set; }
        public virtual DbSet<Specialization> Specialization { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
