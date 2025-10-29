using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF8_PRACT
{
    public static class ThemeHelper
    {
        private static readonly string[] _themePaths = {
            "Styles/Colors/DefaultColors.xaml",
            "Styles/Colors/DarkTheme.xaml"
        };

        public static string CurrentTheme
        {
            get => Properties.Settings.Default.ThemePath ?? _themePaths[0];
            set
            {
                Properties.Settings.Default.ThemePath = value;
                Properties.Settings.Default.Save();
            }
        }

        public static void ApplyTheme(string themeName)
        {
            try
            {
                var app = Application.Current;
                if (app == null) return;

                // Очищаем текущие ресурсы цветов
                var dictionaries = app.Resources.MergedDictionaries;
                for (int i = dictionaries.Count - 1; i >= 0; i--)
                {
                    var dict = dictionaries[i];
                    if (dict.Source?.ToString().Contains("Styles/Colors") == true)
                    {
                        dictionaries.RemoveAt(i);
                    }
                }

                // Добавляем новую тему
                var newTheme = new ResourceDictionary();
                if (themeName == "Dark")
                {
                    newTheme.Source = new Uri("/Styles/Colors/DarkTheme.xaml", UriKind.Relative);
                }
                else
                {
                    newTheme.Source = new Uri("/Styles/Colors/DefaultColors.xaml", UriKind.Relative);
                }

                dictionaries.Add(newTheme);

                // Сохраняем настройку
                Properties.Settings.Default.ThemePath = themeName;
                Properties.Settings.Default.Save();

                // Принудительно обновляем все окна и страницы
                UpdateAllWindows();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка смены темы: {ex.Message}");
            }
        }

        private static void UpdateAllWindows()
        {
            var app = Application.Current;
            if (app == null) return;

            foreach (Window window in app.Windows)
            {
                // Обновляем фон окна
                if (app.Resources["WindowBackground"] is System.Windows.Media.Brush backgroundBrush)
                {
                    window.Background = backgroundBrush;
                }

                // Рекурсивно обновляем все элементы управления
                UpdateControlBackground(window);
            }
        }

        private static void UpdateControlBackground(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is Control control)
                {
                    // Обновляем фон для основных контейнеров
                    if (control is Panel || control is Page)
                    {
                        var backgroundBrush = Application.Current.Resources["WindowBackground"] as System.Windows.Media.Brush;
                        if (backgroundBrush != null)
                        {
                            control.Background = backgroundBrush;
                        }
                    }
                }

                // Рекурсивно обходим детей
                UpdateControlBackground(child);
            }
        }

        public static void ApplySavedTheme()
        {
            var savedTheme = Properties.Settings.Default.ThemePath;
            if (!string.IsNullOrEmpty(savedTheme))
            {
                ApplyTheme(savedTheme);
            }
            else
            {
                ApplyTheme("Light");
            }
        }

        public static void ToggleTheme()
        {
            var currentTheme = Properties.Settings.Default.ThemePath;
            if (currentTheme == "Dark")
            {
                ApplyTheme("Light");
            }
            else
            {
                ApplyTheme("Dark");
            }
        }
    }
}
