using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DickinsDev.RblMonitor.WebUI.Models.Home
{
    public class IndexModel
    {
        [DickinsDev.RblMonitor.Data.Validators.IPAddress]
        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }
    }
}
