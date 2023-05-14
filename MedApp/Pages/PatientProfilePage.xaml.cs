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
    /// Логика взаимодействия для PatientProfilePage.xaml
    /// </summary>
    public partial class PatientProfilePage : Page
    {
        public PatientProfilePage()
        {
            InitializeComponent();
        }

        public PatientProfilePage(Users userSession) : this()
        {
            UserSession = userSession;
        }

        private Users _userSession;
        public Users UserSession
        {
            get { return _userSession; }
            set
            {
                _userSession = value;
                MessageBox.Show($"Здравствуйте, {_userSession.FirstName} {_userSession.LastName}!", "Приветствие", MessageBoxButton.OK, MessageBoxImage.Information);
                TextWritter();
            }
        }

        void TextWritter()
        {
            FirstNameTextBlock.Text = _userSession.FirstName;
            LastNameTextBlock.Text = _userSession.LastName;
            EmailTextBlock.Text = _userSession.Email;
            PhoneNumberTextBlock.Text = _userSession.PhoneNumber;
            var pationInfo = Conection.entities.Patients.FirstOrDefault(i => i.UserId == _userSession.Id);
            BirthDateTextBlock.Text = pationInfo.BirthDate.HasValue ? pationInfo.BirthDate.Value.ToString("dd.MM.yyyy") : "Не указано";
            GenderTextBlock.Text = pationInfo.Gender;
            AddressTextBlock.Text = pationInfo.Address;
        }

        void UnlockTextBox()
        {
            FirstNameTextBlock.IsEnabled = true;
            LastNameTextBlock.IsEnabled = true;
            EmailTextBlock.IsEnabled = true;
            PhoneNumberTextBlock.IsEnabled = true;
            BirthDateTextBlock.IsEnabled = true;
            GenderTextBlock.IsEnabled = true;
            AddressTextBlock.IsEnabled = true;
            saveProfBtn.Visibility = Visibility.Visible;
            cancelSaveBtn.Visibility = Visibility.Visible;
        }

        void LockTextBox()
        {
            FirstNameTextBlock.IsEnabled = false;
            LastNameTextBlock.IsEnabled = false;
            EmailTextBlock.IsEnabled = false;
            PhoneNumberTextBlock.IsEnabled = false;
            BirthDateTextBlock.IsEnabled = false;
            GenderTextBlock.IsEnabled = false;
            AddressTextBlock.IsEnabled = false;
            saveProfBtn.Visibility = Visibility.Collapsed;
            cancelSaveBtn.Visibility = Visibility.Collapsed;

            TextWritter();
        }

        private void editProfBtn_Click(object sender, RoutedEventArgs e)
        {
            UnlockTextBox();
        }

        private void saveProfBtn_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текущего пользователя/пациента из базы данных
            Users editUser = Conection.entities.Users.FirstOrDefault(u => u.Id == _userSession.Id);
            Patients editPatients = Conection.entities.Patients.FirstOrDefault(p => p.UserId == _userSession.Id);

            // Обновляем данные пользователя/пациента
            if (editUser != null && editPatients != null)
            {
                editUser.FirstName = FirstNameTextBlock.Text;
                editUser.LastName = LastNameTextBlock.Text;
                editUser.Email = EmailTextBlock.Text;
                editUser.PhoneNumber = PhoneNumberTextBlock.Text;
                editPatients.BirthDate = (DateTime)BirthDateTextBlock.SelectedDate;
                editPatients.Gender = GenderTextBlock.Text;
                editPatients.Address = AddressTextBlock.Text;

                Conection.entities.SaveChanges();
                TextWritter();
                LockTextBox();
            }
        }

        private void cancelSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            LockTextBox();
        }
    }


}
