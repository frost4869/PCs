using MahApps.Metro.Controls;
using PC.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
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
            var metroWindow = (Application.Current.MainWindow as MetroWindow);

            if (!validateResult.IsValidated)
            {
                metroWindow.FindChild<Button>("BtnUpdate").IsEnabled = false;
                metroWindow.FindChild<ToggleSwitch>("btn_toggle_edit").IsEnabled = false;

                return new ValidationResult(validateResult.IsValidated, validateResult.ValidateMessage);
            }
            else
            {
                metroWindow.FindChild<Button>("BtnUpdate").IsEnabled = true;
                metroWindow.FindChild<ToggleSwitch>("btn_toggle_edit").IsEnabled = true;
                return ValidationResult.ValidResult;
            }
        }

        public static Validation ValidateInput(PcViewModel model)
        {
            var check = new Validation();
            if (model != null)
            {
                if (String.IsNullOrEmpty(model.PC_Name))
                {
                    check.ValidateMessage += "PC Name must not be empty.\n";
                    check.IsValidated = false;
                }
                if (String.IsNullOrEmpty(model.Type))
                {
                    check.ValidateMessage += "Type must not be empty.\n";
                    check.IsValidated = false;
                }
                if (String.IsNullOrEmpty(model.OS))
                {
                    check.IsValidated = false;
                    check.ValidateMessage += "OS must not be empty.\n";
                }
                if (String.IsNullOrEmpty(model.PB))
                {
                    check.IsValidated = false;
                    check.ValidateMessage += "PB must not be empty.\n";
                }
                if (String.IsNullOrEmpty(model.Office_Located))
                {
                    check.IsValidated = false;
                    check.ValidateMessage += "Office Located must not be empty.\n";
                }
                var addMacReg1 = "^[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}$";
                var addMacReg2 = "^[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}$";

                var regex1 = new Regex(addMacReg1);
                var regex2 = new Regex(addMacReg2);

                if (String.IsNullOrEmpty(model.MAC) && String.IsNullOrEmpty(model.MAC2))
                {
                    check.IsValidated = false;
                    check.ValidateMessage += "Atleast one MAC Address must be entered.\n";
                }
                if (!String.IsNullOrEmpty(model.MAC))
                {
                    if (!regex1.IsMatch(model.MAC) && !regex2.IsMatch(model.MAC))
                    {
                        check.IsValidated = false;
                        check.ValidateMessage += "MAC format is NOT a valid mac address format (##:##:##:##:##:##)\n";
                    }
                }
                if (!String.IsNullOrEmpty(model.MAC2))
                {
                    if (!regex1.IsMatch(model.MAC2) && !regex2.IsMatch(model.MAC2))
                    {
                        check.IsValidated = false;
                        check.ValidateMessage += "MAC2 format is NOT a valid mac address format (##:##:##:##:##:##)\n";
                    }
                }
            }

            return check;
        }

    }

    public class Validation
    {
        public bool IsValidated { get; set; } = true;
        public string ValidateMessage { get; set; }
    }
}
