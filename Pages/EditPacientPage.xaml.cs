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

namespace WPF8_PRACT.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditPacientPage.xaml
    /// </summary>
    public partial class EditPacientPage : Page
    {
        public ObservableCollection<Pacient> PacientList { get; set; }
        public Pacient CurrentPacient { get; set; }
        public Pacient OriginalPacient { get; set; }

        public EditPacientPage(ObservableCollection<Pacient> pacientList, Pacient pacient)
        {
            InitializeComponent();
            PacientList = pacientList;
            OriginalPacient = pacient;
            CurrentPacient = new Pacient
            {
                PacientId = pacient.PacientId,
                Name = pacient.Name,
                LastName = pacient.LastName,
                MiddleName = pacient.MiddleName,
                Birthday = pacient.Birthday,
                PhoneNumber = pacient.PhoneNumber,
                AppointmentStories = pacient.AppointmentStories

            };
            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(nameTextBox) ||
                Validation.GetHasError(lastNameTextBox) ||
                Validation.GetHasError(phoneTextBox))
            {
               
                return;
            }

            OriginalPacient.Name = CurrentPacient.Name;
            OriginalPacient.LastName = CurrentPacient.LastName;
            OriginalPacient.MiddleName = CurrentPacient.MiddleName;
            OriginalPacient.Birthday = CurrentPacient.Birthday;
            OriginalPacient.PhoneNumber = CurrentPacient.PhoneNumber;

            OriginalPacient.SaveToFile();
            NavigationService.GoBack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
