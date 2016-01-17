using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Videotheek.ValidationRules
{
    public class PositiefOfNulIngevuld : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int getal;

            if (value == null || value.ToString() == String.Empty)
                return new ValidationResult(false, "Getal moet ingevuld zijn.");
            if(!int.TryParse((string)value, out getal))
            {
                return new ValidationResult(false, "Voer een geldig getal in.");
            }
            if (getal < 0)
                return new ValidationResult(false, "Getal moet 0 of meer zijn.");


            return ValidationResult.ValidResult;
        }
    }
}
