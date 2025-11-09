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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public Doctor CurrentDoctor { get; set; }
        public ObservableCollection<Pacient> Pacients { get; set; } = new ObservableCollection<Pacient>();
        public Pacient SelectedPacient { get; set; }

        public MainPage(Doctor doctor)
        {
            InitializeComponent();
            CurrentDoctor = doctor;
            LoadPacients();
            DataContext = this;
        }

        private void LoadPacients()
        {
            var files = System.IO.Directory.GetFiles("Pacients", "P_*.json");
            foreach (var file in files)
            {
                var pacientId = System.IO.Path.GetFileNameWithoutExtension(file).Substring(2);
                var pacient = Pacient.LoadFromFile(pacientId);
                Pacients.Add(pacient);
            }
        }

        private void AddPacient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPagePacient(Pacients));
        }

        private void StartPriem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPriem(SelectedPacient, CurrentDoctor));
        }

        private void EditPacient_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPacient != null)
            {
                NavigationService.Navigate(new EditPacientPage(Pacients, SelectedPacient));
            }
            else MessageBox.Show("Выберите пациента для редактирования");

            
        }
        private void DeletePacient_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPacient != null)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить пациента {SelectedPacient.LastName} {SelectedPacient.Name}?",
                                           "Подтверждение удаления",
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Удаляем файл пациента
                    string fileName = $"P_{SelectedPacient.PacientId}.json";
                    string filePath = System.IO.Path.Combine("Pacients", fileName);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    // Удаляем из коллекции
                    Pacients.Remove(SelectedPacient);

                  
                }
            }
            else
            {
                MessageBox.Show("Выберите пациента для удаления");
            }
        }
    }
}
