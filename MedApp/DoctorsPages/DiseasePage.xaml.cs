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
    /// Логика взаимодействия для DiseasePage.xaml
    /// </summary>
    public partial class DiseasePage : Page
    {
        private readonly Users _userSession;
        public DiseasePage()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
            disNameComB.ItemsSource = Conection.entities.Diseases.ToList();
        }

        public DiseasePage(Users userSesion) : this()
        {
            _userSession = userSesion;
        }

        public void UpdateAppointmentsList()
        {
            if (_userSession != null)
            {
                var docSessionId = Conection.entities.Doctors.FirstOrDefault(f => f.UserId == _userSession.Id).Id;
                diseaseLv.ItemsSource = Conection.entities.Appointments.Where(i => i.DoctorId == docSessionId & i.StatusId == 3).ToList();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAppointmentsList();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранного заболевания из ComboBox
            var selectedDisease = disNameComB.SelectedItem as Diseases;

            // Если заболевание не было выбрано, то не происходит добавление
            if (selectedDisease == null)
            {
                MessageBox.Show("Пожалуйста, выберите заболевание");
                return;
            }

            var selectedPatientAppointment = diseaseLv.SelectedItem as Appointments;

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

            // Создание новой записи о заболевании для пациента
            PatientDiseases patientDisease = new PatientDiseases()
            {
                DiseaseId = selectedDisease.Id,
                PatientId = selectedPatientAppointment.PatientId,
                StartDate = startDP.SelectedDate.Value, // Используйте .Value для получения DateTime из Nullable<DateTime>
                EndDate = endDP.SelectedDate.Value
            };

            // Добавление новой записи в таблицу PatientDiseases
            Conection.entities.PatientDiseases.Add(patientDisease);
            Conection.entities.SaveChanges();

            MessageBox.Show("Заболевание успешно добавлено");
            disNameComB.Text = "";
            startDP.Text = "";
            endDP.Text = "";
        }
    }
}
