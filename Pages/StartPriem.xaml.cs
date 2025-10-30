using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using WPF8_PRACT.User;

namespace WPF8_PRACT.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartPriem.xaml
    /// </summary>
    public partial class StartPriem : Page
    {
        public Pacient CurrentPacient { get; set; }
        public Doctor CurrentDoctor { get; set; }
        public AppointmentStory NewAppointment { get; set; } = new AppointmentStory();
        public ObservableCollection<Pacient> Pacients { get; set; } = new ObservableCollection<Pacient>();

        public StartPriem(Pacient pacient, Doctor doctor)
        {
            InitializeComponent();
            CurrentPacient = pacient;
            CurrentDoctor = doctor;
            DataContext = this;
        }

        private void AddPriems_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(datePicker) ||
                Validation.GetHasError(diagnosisTextBox) ||
                Validation.GetHasError(recommendationsTextBox))
            {
                return;
            }

            NewAppointment.DoctorId = CurrentDoctor.DoctorId;
            CurrentPacient.AppointmentStories.Add(NewAppointment);
            CurrentPacient.SaveToFile();
            NavigationService.GoBack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
