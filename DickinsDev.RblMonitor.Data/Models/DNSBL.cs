using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DickinsDev.RblMonitor.Data.Models
{
    public class DNSBL
    {
        [Key]
        public Guid Guid { get; set; }

        [Required]
        [MaxLength(24)]
        [Display(Name = "Friendly Name")]
        public String RblName { get; set; }

        [Required]
        [Display(Name ="Zone Name")]
        public string ZoneName { get; set; }
    
        [Required]
        [Display(Name ="List Active")]
        public bool isActive { get; set; }
    }
}
