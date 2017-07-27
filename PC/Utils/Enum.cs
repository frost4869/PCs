using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC.Utils
{
    public enum Filters
    {
        [Display(Name = "Pc Name")]
        Pc_Name,
        [Display(Name = "Phòng Ban")]
        PB,
        [Display(Name = "Nhân Viên")]
        NV,
        [Display(Name = "Mac Address 1")]
        MAC,
        [Display(Name = "Mac Address 2")]
        MAC2,
        [Display(Name = "IP Address")]
        IP,
        [Display(Name = "Office Location")]
        Location,
        [Display(Name = "Service Tag")]
        Service_Tag
    }

    public enum Types
    {
        PC, 
        Laptop,
        DELL
    }
}
