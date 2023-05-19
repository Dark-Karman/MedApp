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
    /// Логика взаимодействия для LabTestPage.xaml
    /// </summary>
    public partial class LabTestPage : Page
    {
        public LabTestPage(Users userSesion)
        {
            InitializeComponent();
            var patientSessionId = Conection.entities.Patients.FirstOrDefault(f => f.UserId == userSesion.Id).Id;
            labTestLv.ItemsSource = Conection.entities.PatientLabTests.Where(i => i.PatientId == patientSessionId).ToList();
        }

        private void labTestLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = labTestLv.SelectedItem as PatientLabTests;

            if (selectedItem != null)
            {
                descriptionTb.Text = Conection.entities.LabTests.FirstOrDefault(i => i.Id == selectedItem.LabTestId).Description;
                noteTb.Text = selectedItem.Note;
            }
            else
            {
                descriptionTb.Text = "";
                noteTb.Text = "";
            }
        }
    }
}
