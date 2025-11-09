using System;
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
            get => Properties.Settings.Default.ThemePath ?? "Light";
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

                // Удаляем только наши цветовые словари
                var dictionaries = app.Resources.MergedDictionaries;
                var colorDictionariesToRemove = new System.Collections.Generic.List<ResourceDictionary>();

                foreach (var dict in dictionaries)
                {
                    if (dict.Source != null)
                    {
                        var sourceString = dict.Source.ToString();
                        if (sourceString.Contains("Styles/Colors/DefaultColors.xaml") ||
                            sourceString.Contains("Styles/Colors/DarkTheme.xaml"))
                        {
                            colorDictionariesToRemove.Add(dict);
                        }
                    }
                }

                foreach (var dictToRemove in colorDictionariesToRemove)
                {
                    dictionaries.Remove(dictToRemove);
                }

                // Добавляем новую тему
                var newTheme = new ResourceDictionary();
                if (themeName == "Dark")
                {
                    newTheme.Source = new Uri("pack://application:,,,/Styles/Colors/DarkTheme.xaml", UriKind.Absolute);
                }
                else
                {
                    newTheme.Source = new Uri("pack://application:,,,/Styles/Colors/DefaultColors.xaml", UriKind.Absolute);
                }

                dictionaries.Add(newTheme);

                // Сохраняем настройку
                CurrentTheme = themeName;

                // Обновляем все окна
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
                UpdateWindowResources(window);
            }
        }

        private static void UpdateWindowResources(Window window)
        {
            if (window == null) return;

            // Обновляем фон окна
            var backgroundBrush = Application.Current.Resources["WindowBackground"] as Brush;
            if (backgroundBrush != null)
            {
                window.Background = backgroundBrush;
            }

            // Рекурсивно обновляем все элементы
            UpdateVisualTree(window);
        }

        private static void UpdateVisualTree(DependencyObject parent)
        {
            if (parent == null) return;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is FrameworkElement element)
                {
                    // Принудительно обновляем стили
                    element.InvalidateVisual();
                    element.InvalidateArrange();
                    element.InvalidateMeasure();
                }

                UpdateVisualTree(child);
            }
        }

        public static void ApplySavedTheme()
        {
            var savedTheme = CurrentTheme;
            ApplyTheme(savedTheme);
        }

        public static void ToggleTheme()
        {
            var currentTheme = CurrentTheme;
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