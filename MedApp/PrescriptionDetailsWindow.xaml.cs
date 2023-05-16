using MedApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MedApp
{
    /// <summary>
    /// Логика взаимодействия для PrescriptionDetailsWindow.xaml
    /// </summary>
    public partial class PrescriptionDetailsWindow : Window
    {
        public PrescriptionDetailsWindow(Prescriptions selectedPrescription)
        {
            InitializeComponent();
            var selectedDoctor = Conection.entities.Doctors.FirstOrDefault(i => i.Id == selectedPrescription.DoctorId);
            firstNameDoctorTb.Text = Conection.entities.Users.FirstOrDefault(i => i.Id == selectedDoctor.UserId).FirstName;
            secondNameDoctorTb.Text = Conection.entities.Users.FirstOrDefault(i => i.Id == selectedDoctor.UserId).LastName;

            var selectedPatient = Conection.entities.Patients.FirstOrDefault(i => i.Id == selectedPrescription.PatientId);
            firstNamePatientTb.Text = Conection.entities.Users.FirstOrDefault(i => i.Id == selectedPatient.UserId).FirstName;
            secondNamePatientTb.Text = Conection.entities.Users.FirstOrDefault(i => i.Id == selectedPatient.UserId).LastName;
        }
    }
}
