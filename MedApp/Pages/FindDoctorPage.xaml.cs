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
    /// Логика взаимодействия для FindDoctorPage.xaml
    /// </summary>
    public partial class FindDoctorPage : Page
    {
        private readonly PatientAppointmentsPage _patientAppointmentsPage;

        public FindDoctorPage()
        {
            InitializeComponent();
            findDoctorLv.ItemsSource = Conection.entities.Doctors.ToList();
        }
        public FindDoctorPage(Users usersSesion, PatientAppointmentsPage patientAppointmentsPage) : this()
        {
            UserSession = usersSesion;
            _patientAppointmentsPage = patientAppointmentsPage;
        }

        private Users _userSession;
        public Users UserSession
        {
            get { return _userSession; }
            set
            {
                _userSession = value;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDoctorList();
        }

        private void UpdateDoctorList()
        {
            string searchText = SearchTextBox.Text;
            findDoctorLv.ItemsSource = Conection.entities.Doctors
                .Where(d => d.Users.FirstName.Contains(searchText) || d.Users.LastName.Contains(searchText) || d.Specialization.name.Contains(searchText))
                .ToList();
        }

        private void findDoctorLv_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Получаем выбранного врача из ListView
            Doctors selectedAppointments = findDoctorLv.SelectedItem as Doctors;

            // Если врач выбран, открываем новое окно
            if (selectedAppointments != null)
            {
                // Создаем новое окно и передаем выбранный элемент в его конструктор
                FindDoctorDetailsWindow newWindow = new FindDoctorDetailsWindow(selectedAppointments, _userSession, _patientAppointmentsPage);

                newWindow.ShowDialog();
            }
        }
    }
}
