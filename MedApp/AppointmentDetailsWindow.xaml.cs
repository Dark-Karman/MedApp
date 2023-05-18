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
    /// Логика взаимодействия для AppointmentDetailsWindow.xaml
    /// </summary>
    public partial class AppointmentDetailsWindow : Window
    {
        public AppointmentDetailsWindow(Appointments selectedAppointments, Users userSesion)
        {
            InitializeComponent();
            SelectedAppointments = selectedAppointments;
            if (userSesion.Role == "Patient")
            {
                completeBtn.Visibility = Visibility.Collapsed;
            }
            else completeBtn.Visibility = Visibility.Visible;
        }


        private Appointments _selectedAppointments;

        public Appointments SelectedAppointments
        {
            get { return _selectedAppointments; }
            private set { _selectedAppointments = value; }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateAppointmentStatus(2);
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateAppointmentStatus(1);
        }

        private void UpdateAppointmentDateTime()
        {
            DateTime? selectedDateTime = AppointmentDateTimePicker.Value;

            if (selectedDateTime.HasValue)
            {
                Appointments editAppointments = Conection.entities.Appointments.FirstOrDefault(a => a.Id == SelectedAppointments.Id);
                if (editAppointments != null)
                {
                    editAppointments.AppointmentDate = selectedDateTime.Value;
                    Conection.entities.SaveChanges();
                }

                MessageBox.Show("Дата и время приёма изменены");
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите дату и время");
            }
        }


        private void UpdateAppointmentStatus(int statusId)
        {
            Appointments editAppointments = Conection.entities.Appointments.FirstOrDefault(a => a.Id == SelectedAppointments.Id);
            if (editAppointments != null)
            {
                editAppointments.StatusId = statusId;
                Conection.entities.SaveChanges();
            }

            this.Close();
        }

        private void SaveDateTimeBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateAppointmentDateTime();
        }

        private void completeBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateAppointmentStatus(3);
        }
    }
}
