using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PC.Utils
{
    class InputMacValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var inputMac = Convert.ToString(value);

            var addMacReg1 = "^[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}$";
            var addMacReg2 = "^[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}$";

            var regex1 = new Regex(addMacReg1);
            var regex2 = new Regex(addMacReg2);

            if (!regex1.IsMatch(inputMac) && !regex2.IsMatch(inputMac))
            {
                return new ValidationResult(false, "Mac Address format is not valid");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
