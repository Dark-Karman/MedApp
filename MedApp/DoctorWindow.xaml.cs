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
using MedApp.DoctorsPages;
using MedApp.Model;

namespace MedApp
{
    /// <summary>
    /// Логика взаимодействия для DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        public DoctorWindow(Users userSesion)
        {
            InitializeComponent();
            MessageBox.Show($"Здравствуйте, {userSesion.FirstName} {userSesion.LastName}!", "Приветствие", MessageBoxButton.OK, MessageBoxImage.Information);

            appointmentsFrame.Content = new AppointmentsPage(userSesion);
            diseaseFrame.Content = new DiseasePage(userSesion);
            prscriptionFrame.Content = new PrescriptionPage(userSesion);
            labFrame.Content = new LabPage(userSesion);
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
