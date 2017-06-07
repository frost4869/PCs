using PC.DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PC.Utils
{
    class NewPcValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var inputPcName = Convert.ToString(value);

            using (var db = new PCEntities())
            {
                try
                {
                    if (db.Pcs.Any(q => q.PC_Name.ToLower().Equals(inputPcName.ToLower())))
                    {
                        return new ValidationResult(false, "Pc Name is already existed !");
                    }
                    else
                    {
                        return ValidationResult.ValidResult;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
