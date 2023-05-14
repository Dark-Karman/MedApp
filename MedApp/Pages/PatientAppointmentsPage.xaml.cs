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

namespace MedApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PatientAppointmentsPage.xaml
    /// </summary>
    public partial class PatientAppointmentsPage : Page
    {
        private readonly Users _userSession;

        public PatientAppointmentsPage()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
        }

        public PatientAppointmentsPage(Users userSession) : this()
        {
            _userSession = userSession;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAppointmentsList();
        }

        public void UpdateAppointmentsList()
        {
            if (_userSession != null)
            {
                var patientSessionId = Conection.entities.Patients.FirstOrDefault(f => f.UserId == _userSession.Id).Id;
                AppointmentsLv.ItemsSource = Conection.entities.Appointments.Where(i => i.PatientId == patientSessionId).ToList();
            }
        }

        private void AppointmentsLv_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Получаем выбранного пользователя из ListView
            Appointments selectedAppointment = AppointmentsLv.SelectedItem as Appointments;

            // Если пользователь выбран, открываем новое окно
            if (selectedAppointment != null)
            {
                // Создаем новое окно и передаем выбранный элемент в его конструктор
                AppointmentDetailsWindow newWindow = new AppointmentDetailsWindow(selectedAppointment);
                newWindow.ShowDialog();
                UpdateAppointmentsList();
            }
        }
    }
}
