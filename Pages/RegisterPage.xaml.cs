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

namespace WPF8_PRACT.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public Doctor NewDoctor { get; set; } = new Doctor();

        public RegisterPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            nameTextBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            lastNameTextBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            middleNameTextBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            passwordTextBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            specializationTextBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            if (Validation.GetHasError(nameTextBox) ||
               Validation.GetHasError(lastNameTextBox) ||
               Validation.GetHasError(passwordTextBox) ||
               Validation.GetHasError(specializationTextBox))
            {
                return;
            }

            NewDoctor.DoctorId = Doctor.GenerateDoctorId();
            NewDoctor.SaveToFile();
            NavigationService.Navigate(new MainPage(NewDoctor));
        }
    }
}


