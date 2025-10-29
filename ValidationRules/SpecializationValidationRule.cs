using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF8_PRACT.ValidationRules
{
    public class SpecializationValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(input))
                return new ValidationResult(false, "Специализация обязательна для заполнения");

            if (!Regex.IsMatch(input, @"^[а-яА-ЯёЁa-zA-Z\s\-]+$"))
                return new ValidationResult(false, "Специализация может содержать только буквы, пробелы и дефисы");

            if (input.Length < 3)
                return new ValidationResult(false, "Специализация должна содержать минимум 3 символа");

            return ValidationResult.ValidResult;
        }
    }
}
