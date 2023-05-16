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
    /// Логика взаимодействия для PrescriptionsPage.xaml
    /// </summary>
    public partial class PrescriptionsPage : Page
    {
        public PrescriptionsPage(Users userSesion)
        {
            InitializeComponent();
            var patientSessionId = Conection.entities.Patients.FirstOrDefault(f => f.UserId == userSesion.Id).Id;
            prescriptionLv.ItemsSource = Conection.entities.Prescriptions.Where(i => i.PatientId == patientSessionId).ToList();
        }

        private void prescriptionLv_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Получаем выбранного пользователя из ListView
            Prescriptions selectedPrescription = prescriptionLv.SelectedItem as Prescriptions;

            // Если пользователь выбран, открываем новое окно
            if (selectedPrescription != null)
            {
                // Создаем новое окно и передаем выбранный элемент в его конструктор
                PrescriptionDetailsWindow newWindow = new PrescriptionDetailsWindow(selectedPrescription);
                newWindow.ShowDialog();
            }
        }
    }
}
