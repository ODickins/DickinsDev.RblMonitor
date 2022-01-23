using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DickinsDev.RblMonitor.Data.Models
{
    public class Nameserver
    {
        [Key]
        public Guid Guid { get; set; }

        [Required]
        [MaxLength(24)]
        [Display(Name = "Friendly Name")]
        public String ServerName { get; set; }

        [Required]
        [Validators.IPAddress]
        [Display(Name = "IP Address")]
        public String IPAddress { get; set; }

        [Required]
        [Display(Name = "Server Active")]
        public bool isActive { get; set; }


    }
}
