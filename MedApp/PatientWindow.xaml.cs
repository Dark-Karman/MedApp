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
    /// Логика взаимодействия для PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        public PatientWindow(Users userSesion)
        {
            InitializeComponent();
            PatientAppointmentsPage patientAppointmentsPage = new PatientAppointmentsPage();

            profFrame.Content = new PatientProfilePage(userSesion);
            appointmentsFrame.Content = new PatientAppointmentsPage(userSesion);
            findDoctorFrame.Content = new FindDoctorPage(userSesion, patientAppointmentsPage);
            diseasesFrame.Content = new DiseasesPage(userSesion);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Закрываем текущее окно и открываем окно авторизации
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
