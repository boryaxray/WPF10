using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF8_PRACT.ValidationRules
{
    public class DateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Дата обязательна для заполнения");

            if (value is DateTime date)
            {
                if (date > DateTime.Now)
                    return new ValidationResult(false, "Дата не может быть в будущем");

                if (date < DateTime.Now.AddYears(-150))
                    return new ValidationResult(false, "Дата слишком старая");
            }

            return ValidationResult.ValidResult;
        }
    }
}
