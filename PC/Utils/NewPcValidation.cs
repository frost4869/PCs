using PC.DataAccess;
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
    class NewPcValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var inputValue = Convert.ToString(value).Trim();

            using (var db = new PCEntities())
            {
                try
                {
                    if (db.Pcs.Any(q => q.PC_Name.ToLower().Equals(inputValue.ToLower()) && q.Active == true))
                    {
                        return new ValidationResult(false, "Pc Name is already existed !");
                    }
                    else if (!string.IsNullOrEmpty(inputValue) && db.Pcs.Any(q => q.ServiceTag.ToLower().Equals(inputValue.ToLower()) && q.Active == true))
                    {
                        return new ValidationResult(false, "Service Tag is already existed !");
                    }
                    else
                    {
                        return ValidationResult.ValidResult;
                    }
                }
                catch (Exception ex)
                {
                    Util.WriteLog(ex.Message + "\n" + ex.StackTrace);
                    Util.ShowMessageBoxAsync("Error", ex.Message);
                    throw;
                }
            }
        }
    }
}
