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
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public string LoginText { get; set; }
        public string PasswordText { get; set; }
        public Auth()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(loginTextBox) || Validation.GetHasError(passwordTextBox))
            {
                return;
            }

            if (!int.TryParse(LoginText, out int doctorId))
            {
                return;
            }

            var doctor = Doctor.LoadFromFile(doctorId);
            if (doctor == null || doctor.Password != PasswordText)
            {
                return;
            }

            NavigationService.Navigate(new MainPage(doctor));
        }
    }
}
