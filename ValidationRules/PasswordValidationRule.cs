using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF8_PRACT.ValidationRules
{
    public class PasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString();

            if (string.IsNullOrEmpty(input))
                return new ValidationResult(false, "Пароль обязателен для заполнения");

            if (input.Length < 6)
                return new ValidationResult(false, "Пароль должен содержать минимум 6 символов");

            return ValidationResult.ValidResult;
        }
    }
}
