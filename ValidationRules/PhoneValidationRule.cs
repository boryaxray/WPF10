using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace WPF8_PRACT.ValidationRules
{
    public class PhoneValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;

            if (string.IsNullOrWhiteSpace(input))
                return new ValidationResult(false, "Номер телефона обязателен");

            string cleanPhone = new string(input.Where(char.IsDigit).ToArray());

            if (cleanPhone.Length != input.Trim().Length)
                return new ValidationResult(false, "Номер телефона должен содержать только цифры");

            if (cleanPhone.Length < 10 || cleanPhone.Length > 11)
                return new ValidationResult(false, "Номер телефона должен содержать 10-11 цифр");

            if (!long.TryParse(cleanPhone, out long phoneNumber))
                return new ValidationResult(false, "Некорректный формат номера телефона");

           
            return ValidationResult.ValidResult;
        }
    }
}