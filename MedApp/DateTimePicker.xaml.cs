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
    public partial class DateTimePicker : UserControl
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(DateTime?), typeof(DateTimePicker),
            new FrameworkPropertyMetadata(default(DateTime?), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

        public DateTimePicker()
        {
            InitializeComponent();
            DatePicker.SelectedDateChanged += (s, e) => UpdateValue();
            TimePicker.ValueChanged += (s, e) => UpdateValue();
        }

        public DateTime? Value
        {
            get { return (DateTime?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var picker = (DateTimePicker)d;
            picker.DatePicker.SelectedDate = ((DateTime?)e.NewValue)?.Date;
            picker.TimePicker.Value = (DateTime?)e.NewValue;
        }

        private void UpdateValue()
        {
            Value = DatePicker.SelectedDate?.Date + TimePicker.Value?.TimeOfDay;
        }
    }
}
