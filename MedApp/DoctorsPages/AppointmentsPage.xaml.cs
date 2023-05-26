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
    /// Логика взаимодействия для AppointmentsPage.xaml
    /// </summary>
    public partial class AppointmentsPage : Page
    {
        private readonly Users _userSession;
        public AppointmentsPage()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
        }

        public AppointmentsPage(Users usersSesion) : this()
        {
            _userSession = usersSesion;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAppointmentsList();
        }

        public void UpdateAppointmentsList()
        {
            if (_userSession != null)
            {
                var docSessionId = Conection.entities.Doctors.FirstOrDefault(
                    f => f.UserId == _userSession.Id).Id;
                AppointmentsLv.ItemsSource = Conection.entities.Appointments.Where(
                    i => i.DoctorId == docSessionId).ToList();
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
                AppointmentDetailsWindow newWindow = new AppointmentDetailsWindow(selectedAppointment,
                    _userSession);
                newWindow.ShowDialog();
                UpdateAppointmentsList();
            }
        }
    }
}
