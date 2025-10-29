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
    /// Логика взаимодействия для AddPagePacient.xaml
    /// </summary>
    public partial class AddPagePacient : Page
    {
        public ObservableCollection<Pacient> PacientList { get; set; }
        public Pacient CurrentPacient { get; set; } = new Pacient();

        public AddPagePacient(ObservableCollection<Pacient> pacientList)
        {
            InitializeComponent();
            PacientList = pacientList;
            DataContext = this;
        }

        private void AddPacientButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(nameTextBox) ||
                Validation.GetHasError(lastNameTextBox) ||
                Validation.GetHasError(phoneTextBox) ||
                Validation.GetHasError(birthdayPicker))
            {
                // Ошибки уже показаны через ItemsControl
                return;
            }

            CurrentPacient.PacientId = Pacient.GeneratePacientId();
            PacientList.Add(CurrentPacient);
            CurrentPacient.SaveToFile();
            NavigationService.GoBack();
        }
    }
}
