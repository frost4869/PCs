using PC.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace PC.Utils
{
    class PcValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            PcViewModel pc = (value as BindingGroup).Items[0] as PcViewModel;

            var validateResult = ValidateInput(pc);
            if (!validateResult.IsValidated)
            {
                return new ValidationResult(validateResult.IsValidated, validateResult.ValidateMessage);
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }

        public static Validation ValidateInput(PcViewModel model)
        {
            var check = new Validation();
            if (model != null)
            {
                if (!String.IsNullOrEmpty(model.PC_Name) && !String.IsNullOrEmpty(model.Type) && !String.IsNullOrEmpty(model.OS) && !String.IsNullOrEmpty(model.PB) && !String.IsNullOrEmpty(model.Office_Located) && (!String.IsNullOrEmpty(model.MAC) || !String.IsNullOrEmpty(model.MAC2)))
                {
                    var addMacReg1 = "^[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}$";
                    var addMacReg2 = "^[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}$";

                    var regex1 = new Regex(addMacReg1);
                    var regex2 = new Regex(addMacReg2);

                    if (!String.IsNullOrEmpty(model.MAC) && !String.IsNullOrEmpty(model.MAC2))
                    {
                        if ((regex1.IsMatch(model.MAC) || regex2.IsMatch(model.MAC)) && (regex1.IsMatch(model.MAC2) || regex2.IsMatch(model.MAC2)))
                        {
                            check.IsValidated = true;
                        }
                        else
                        {
                            check.ValidateMessage = "MAC or MAC2 format is NOT a valid mac address format (##:##:##:##:##:##)";
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(model.MAC))
                        {
                            if (regex1.IsMatch(model.MAC) || regex2.IsMatch(model.MAC))
                            {
                                check.IsValidated = true;
                            }
                            else
                            {
                                check.ValidateMessage = "MAC format is NOT a valid mac address format (##:##:##:##:##:##)";
                            }
                        }


                        if (!String.IsNullOrEmpty(model.MAC2))
                        {
                            if (regex1.IsMatch(model.MAC2) || regex2.IsMatch(model.MAC2))
                            {
                                check.IsValidated = true;
                            }
                            else
                            {
                                check.ValidateMessage += "MAC2 format is NOT a valid mac address format (##:##:##:##:##:##)";
                            }
                        }
                    }
                }
                else
                {
                    check.ValidateMessage += "Fields with [ * ] mark must not be empty";
                }
            }

            return check;
        }

    }

    public class Validation
    {
        public bool IsValidated { get; set; } = false;
        public string ValidateMessage { get; set; }
    }
}
