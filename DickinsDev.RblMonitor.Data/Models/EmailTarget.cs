using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DickinsDev.RblMonitor.Data.Models
{
    public class EmailTarget
    {
        [Key]
        public Guid Guid { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="Email Address")]
        public String EmailAddress { get; set; }

        [Required]
        [Display(Name ="Send Alerts")]
        public bool isActive { get; set; }
    }
}
