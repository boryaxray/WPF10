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
    public class NameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(input))
                return new ValidationResult(false, "Поле обязательно для заполнения");

            if (input.Length < 2)
                return new ValidationResult(false, "Должно содержать минимум 2 символа");

            if (!Regex.IsMatch(input, @"^[а-яА-ЯёЁa-zA-Z\-]+$"))
                return new ValidationResult(false, "Может содержать только буквы и дефис");

            return ValidationResult.ValidResult;
        }
    }
}
