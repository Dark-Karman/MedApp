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
using System.Windows.Shapes;

namespace MedApp
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }


        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstNameTextBox.Text) ||
               string.IsNullOrWhiteSpace(lastNameTextBox.Text) ||
               string.IsNullOrWhiteSpace(emailTextBox.Text) ||
               string.IsNullOrWhiteSpace(phoneTextBox.Text) ||
               string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var existingUser = Conection.entities.Users.FirstOrDefault(u => u.Email == emailTextBox.Text);
            if (existingUser != null)
            {
                MessageBox.Show("Пользователь с таким адресом электронной почты уже зарегистрирован.", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Users newUser = new Users();
            Patients newPatients = new Patients();

            newUser.FirstName = firstNameTextBox.Text;
            newUser.LastName = lastNameTextBox.Text;
            newUser.Email = emailTextBox.Text;
            newUser.PhoneNumber = phoneTextBox.Text;
            newUser.PasswordHash = passwordBox.Password;
            newUser.Role = "Patient"; // По умолчанию регистрируем пациентов

            Conection.entities.Users.Add(newUser);
            Conection.entities.SaveChanges();

            newPatients.UserId = Conection.entities.Users.OrderByDescending(u => u.Id).FirstOrDefault().Id;
            Conection.entities.Patients.Add(newPatients);
            Conection.entities.SaveChanges();

            MessageBox.Show("Регистрация успешно завершена!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);

            // Возвращаемся к окну авторизации
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void rulesCb_Click(object sender, RoutedEventArgs e)
        {
            if (rulesCb.IsChecked == true)
            {
                registerButton.IsEnabled = true;
            }
            else
            {
                registerButton.IsEnabled = false;
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            RulesWindow rules = new RulesWindow();   
            rules.Show();
        }

    }
}
