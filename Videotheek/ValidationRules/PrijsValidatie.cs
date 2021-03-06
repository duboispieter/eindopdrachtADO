﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Videotheek.ValidationRules
{
    public class PrijsValidatie : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal getal;
            NumberStyles style = NumberStyles.Currency;
            if (value == null || value.ToString() == String.Empty)
                return new ValidationResult(false, "Getal moet ingevuld zijn.");
            if (!decimal.TryParse((string)value, style,cultureInfo, out getal))
            {
                return new ValidationResult(false, "Voer een geldig getal in.");
            }
            if (getal <= 0)
                return new ValidationResult(false, "Getal moet meer dan 0 zijn.");

               
            return ValidationResult.ValidResult;
        }
    }
}
