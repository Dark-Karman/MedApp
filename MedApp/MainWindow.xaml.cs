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

namespace MedApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = Conection.entities.Users.FirstOrDefault(u => u.Email == emailTextBox.Text && u.PasswordHash == passwordBox.Password);

            if (user != null)
            {
                if (user.Role == "Doctor")
                {
                    // Открываем главное окно для врача
                    var userSesion = user;
                    DoctorWindow doctorWindow = new DoctorWindow(userSesion);
                    doctorWindow.Show();
                }
                else if (user.Role == "Patient")
                {
                    // Открываем главное окно для пациента
                    var userSesion = user;
                    PatientWindow patientWindow = new PatientWindow(userSesion);
                    patientWindow.Show();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильный адрес электронной почты или пароль.", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            //PatientWindow patientWindow = new PatientWindow();
            //patientWindow.Show();
            Close();
        }
    }
}
    

