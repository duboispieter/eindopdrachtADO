using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Videotheek.ValidationRules
{
    public class GenreValidatie : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ((value == null) || String.IsNullOrEmpty(value.ToString()))
                return new ValidationResult(false, "Selecteer een genre.");
            else
                return ValidationResult.ValidResult;
        }
    }
}
