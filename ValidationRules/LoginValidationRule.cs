using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF8_PRACT.ValidationRules
{
    public class LoginValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(input))
                return new ValidationResult(false, "Логин обязателен для заполнения");

            if (!input.All(char.IsDigit))
                return new ValidationResult(false, "Логин должен содержать только цифры");

            if (input.Length < 1)
                return new ValidationResult(false, "Логин слишком короткий");

            return ValidationResult.ValidResult;
        }
    }
}
