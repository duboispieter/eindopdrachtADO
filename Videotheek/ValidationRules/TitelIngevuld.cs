using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Videotheek.ValidationRules
{
    public class TitelIngevuld : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
                if ((string)value == "" || (string)value == String.Empty || value == null)
                return new ValidationResult(false, "Waarde moet ingevuld zijn.");
            else return ValidationResult.ValidResult;
        }
    }
}
