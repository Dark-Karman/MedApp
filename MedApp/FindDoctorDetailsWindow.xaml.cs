using MedApp.Model;
using MedApp.Pages;
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
    /// Логика взаимодействия для FindDoctorDetailsWindow.xaml
    /// </summary>
    public partial class FindDoctorDetailsWindow : Window
    {
        private readonly Doctors _selectedDoctor;
        private readonly Users _userSession;
        private readonly PatientAppointmentsPage _patientAppointmentsPage;

        public FindDoctorDetailsWindow(Doctors selectedDoctor, Users userSession, PatientAppointmentsPage patientAppointmentsPage)
        {
            InitializeComponent();

            _selectedDoctor = selectedDoctor;
            _userSession = userSession;
            _patientAppointmentsPage = patientAppointmentsPage;
        }

        private void AddAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDateTime = AppointmentDateTimePicker.Value;

            if (selectedDateTime.HasValue)
            {
                Appointments newAppointment = new Appointments();

                newAppointment.DoctorId = _selectedDoctor.Id;
                newAppointment.PatientId = Conection.entities.Patients.FirstOrDefault(i => i.UserId == _userSession.Id).Id;
                newAppointment.AppointmentDate = selectedDateTime.Value;
                newAppointment.StatusId = 1;

                Conection.entities.Appointments.Add(newAppointment);
                Conection.entities.SaveChanges();
                MessageBox.Show("Прием успешно добавлен");
                this.Close();
                _patientAppointmentsPage.UpdateAppointmentsList();
            }
            else
            {
                MessageBox.Show("Выберите дату и время");
            }
        }
    }

}
