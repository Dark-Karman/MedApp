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
                editLabLv.ItemsSource = Conection.entities.PatientLabTests.Where(i => i.DoctorId == docSessionId).ToList();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAppointmentsList();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedLabTest = labComB.SelectedItem as LabTests;

            if (selectedLabTest == null)
            {
                MessageBox.Show("Пожалуйста, выберите лабораторное тестирование");
                return;
            }

            var selectedPatientAppointment = labLv.SelectedItem as Appointments;

            if (selectedPatientAppointment == null)
            {
                MessageBox.Show("Пожалуйста, выберите пациента");
                return;
            }

            if (startDP.Value == null)
            {
                MessageBox.Show("Пожалуйста, выберите дату и время");
                return;
            }

            if (startDP.Value.Value.TimeOfDay == TimeSpan.Zero)
            {
                MessageBox.Show("Пожалуйста, выберите время");
                return;
            }

            var docSessionId = Conection.entities.Doctors.FirstOrDefault(f => f.UserId == _userSession.Id).Id;
            PatientLabTests labTest = new PatientLabTests()
            {
                PatientId = selectedPatientAppointment.PatientId,
                LabTestId = selectedLabTest.Id,
                TestDate = startDP.Value.Value,
                DoctorId = docSessionId,
                StatusId = 1
            };

            Conection.entities.PatientLabTests.Add(labTest);
            Conection.entities.SaveChanges();

            MessageBox.Show("Лабораторное тестирование успешно назначено пациенту");
            labComB.Text = "";
            startDP.Value = null;
            UpdateAppointmentsList();
        }

        private void goToEditMod_Click(object sender, RoutedEventArgs e)
        {
            addGridPreset.Visibility = Visibility.Collapsed;
            editGridPreset.Visibility = Visibility.Visible;
            UpdateAppointmentsList();
        }

        private void goToAddMod_Click(object sender, RoutedEventArgs e)
        {
            addGridPreset.Visibility = Visibility.Visible;
            editGridPreset.Visibility = Visibility.Collapsed;
            UpdateAppointmentsList();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedResultObj = resultComB.SelectedItem as ComboBoxItem;
            if (selectedResultObj == null)
            {
                MessageBox.Show("Пожалуйста, выберите результат тестирование");
                return;
            }
            string selectedResult = selectedResultObj.Content.ToString();

            var selectedLabTest = editLabLv.SelectedItem as PatientLabTests;
            if (selectedLabTest == null)
            {
                MessageBox.Show("Пожалуйста, выберите пациента");
                return;
            }

            PatientLabTests editString = Conection.entities.PatientLabTests.FirstOrDefault(i => i.Id == selectedLabTest.Id);

            if (editString != null)
            {
                editString.Result = selectedResult;
                editString.StatusId = 3; 
                editString.Note = noteTb.Text;

                Conection.entities.SaveChanges();

                MessageBox.Show("Тест успешно обновлен");
                resultComB.Text = "";
                noteTb.Text = "";
                UpdateAppointmentsList();
            }
            else
            {
                MessageBox.Show("Выбранный тест не найден");
            }
        }
    }
}
