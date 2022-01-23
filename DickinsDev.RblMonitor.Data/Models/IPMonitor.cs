using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DickinsDev.RblMonitor.Data.Models
{
    public class IPMonitor
    {
        [Key]
        public Guid Guid { get; set; }

        [Required]
        [MaxLength(24)]
        [Display(Name = "Friendly Name")]
        public String IPName { get; set; }

        [Required]
        [Validators.IPAddress]
        [Display(Name = "IP Address")]
        public String IPAddress { get; set; }

        [Required]
        [Display(Name = "Monitor Active")]
        public bool isActive { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastCheck { get; set; }

        [Required]
        public int CheckInterval { get; set; } = 24;

        public bool CheckRequired
        {
            get
            {
                if (LastCheck.AddHours(CheckInterval) < DateTime.UtcNow)
                    return true;
                return false;
            }
        }

        [Required]
        [Display(Name = "Clean")]
        public bool isClean { get; set; } = true;


    }
}
