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
    /// Логика взаимодействия для LabPage.xaml
    /// </summary>
    public partial class LabPage : Page
    {
        private readonly Users _userSession;
        public LabPage()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
            labComB.ItemsSource = Conection.entities.LabTests.ToList();
        }

        public LabPage(Users userSesion) : this()
        {
            _userSession = userSesion;
        }

        public void UpdateAppointmentsList()
        {
            if (_userSession != null)
            {
                var docSessionId = Conection.entities.Doctors.FirstOrDefault(f => f.UserId == _userSession.Id).Id;
                labLv.ItemsSource = Conection.entities.Appointments.Where(i => i.DoctorId == docSessionId & i.StatusId == 3).ToList();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAppointmentsList();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedLabTest = labComB.SelectedItem as LabTests;

            // Если заболевание не было выбрано, то не происходит добавление
            if (selectedLabTest == null)
            {
                MessageBox.Show("Пожалуйста, выберите лабораторное тестирование");
                return;
            }

            var selectedPatientAppointment = labLv.SelectedItem as Appointments;

            // Если запись о приеме пациента не была выбрана, то не происходит добавление
            if (selectedPatientAppointment == null)
            {
                MessageBox.Show("Пожалуйста, выберите пациента");
                return;
            }

            // Если дата начала или окончания не выбрана, то не происходит добавление
            if (startDP.SelectedDate == null)
            {
                MessageBox.Show("Пожалуйста, выберите дату");
                return;
            }

            // Создание новой записи о заболевании для пациента
            PatientLabTests labTest = new PatientLabTests()
            {
                PatientId = selectedPatientAppointment.PatientId,
                LabTestId = selectedLabTest.Id,
                TestDate = startDP.SelectedDate.Value,
                StatusId = 1
            };

            // Добавление новой записи в таблицу PatientDiseases
            Conection.entities.PatientLabTests.Add(labTest);
            Conection.entities.SaveChanges();

            MessageBox.Show("Лабораторное тестирование успешно назначено пациенту");
            labComB.Text = "";
            startDP.Text = "";
        }

        private void goToEditMod_Click(object sender, RoutedEventArgs e)
        {
            addGridPreset.Visibility = Visibility.Collapsed;
            editGridPreset.Visibility = Visibility.Visible;
        }

        private void goToAddMod_Click(object sender, RoutedEventArgs e)
        {
            addGridPreset.Visibility = Visibility.Visible;
            editGridPreset.Visibility = Visibility.Collapsed;
        }
    }
}
