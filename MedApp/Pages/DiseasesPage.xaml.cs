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
    /// Логика взаимодействия для DiseasesPage.xaml
    /// </summary>
    public partial class DiseasesPage : Page
    {
        public DiseasesPage(Users userSesion)
        {
            InitializeComponent();
            var patientSessionId = Conection.entities.Patients.FirstOrDefault(f => f.UserId == userSesion.Id).Id;
            diseasesLv.ItemsSource = Conection.entities.PatientDiseases.Where(i => i.PatientId == patientSessionId).ToList();
        }

        private void diseasesLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            var selectedItem = diseasesLv.SelectedItem as PatientDiseases;

            if (selectedItem != null)
            {
                descriptionTb.Text = Conection.entities.Diseases.FirstOrDefault(i=> i.Id == selectedItem.DiseaseId).Description; 
            }
            else
            {
                descriptionTb.Text = "";
            }
        }
    }
}
