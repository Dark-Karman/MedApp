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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedApp.DoctorsPages
{
    /// <summary>
    /// Логика взаимодействия для PrescriptionPage.xaml
    /// </summary>
    public partial class PrescriptionPage : Page
    {
        private readonly Users _userSession;
        public PrescriptionPage()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
        }

        public PrescriptionPage(Users userSesion) : this()
        {
            _userSession = userSesion;
        }

        public void UpdateAppointmentsList()
        {
            if (_userSession != null)
            {
                var docSessionId = Conection.entities.Doctors.FirstOrDefault(f => f.UserId == _userSession.Id).Id;
                prescriptionLv.ItemsSource = Conection.entities.Appointments.Where(i => i.DoctorId == docSessionId & i.StatusId == 3).ToList();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAppointmentsList();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedPatientAppointment = prescriptionLv.SelectedItem as Appointments;

            // Если запись о приеме пациента не была выбрана, то не происходит добавление
            if (selectedPatientAppointment == null)
            {
                MessageBox.Show("Пожалуйста, выберите прием пациента");
                return;
            }

            // Если дата начала или окончания не выбрана, то не происходит добавление
            if (startDP.SelectedDate == null || endDP.SelectedDate == null)
            {
                MessageBox.Show("Пожалуйста, выберите даты начала и окончания");
                return;
            }

            if (medNameTb.Text == "" || dozTb.Text == "")
            {
                MessageBox.Show("Пожалуйста, введите название препарата и дозировку");  
                return;
            }

            // Создание новой записи 
            Prescriptions prescriptions = new Prescriptions()
            {
                DoctorId = Conection.entities.Doctors.FirstOrDefault(i => i.UserId == _userSession.Id).Id,
                PatientId = selectedPatientAppointment.PatientId,
                Medication = medNameTb.Text,
                Dosage = dozTb.Text,
                StartDate = startDP.SelectedDate.Value, // Используйте .Value для получения DateTime из Nullable<DateTime>
                EndDate = endDP.SelectedDate.Value
            };

            // Добавление новой записи в таблицу Prescriptions
            Conection.entities.Prescriptions.Add(prescriptions);
            Conection.entities.SaveChanges();

            MessageBox.Show("Рецепт успешно добавлен");

            startDP.Text = "";
            endDP.Text = "";
            medNameTb.Text = "";
            dozTb.Text = "";
        }
    }
}
