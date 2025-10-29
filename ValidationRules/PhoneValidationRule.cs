using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF8_PRACT.ValidationRules
{
    public class PhoneValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(input))
                return new ValidationResult(false, "Номер телефона обязателен");

            var cleanPhone = new string(input.Where(char.IsDigit).ToArray());

            if (cleanPhone.Length < 10 || cleanPhone.Length > 11)
                return new ValidationResult(false, "Номер телефона должен содержать 10-11 цифр");

            return ValidationResult.ValidResult;
        }
    }
}
