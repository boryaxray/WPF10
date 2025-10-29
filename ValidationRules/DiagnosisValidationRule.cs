using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF8_PRACT.ValidationRules
{
    public class DiagnosisValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(input))
                return new ValidationResult(false, "Диагноз обязателен для заполнения");

            if (input.Length < 3)
                return new ValidationResult(false, "Диагноз должен содержать минимум 3 символа");

            return ValidationResult.ValidResult;
        }
    }
}
