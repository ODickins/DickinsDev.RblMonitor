using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DickinsDev.RblMonitor.Data.Models
{
    public class DnsCheckResult
    {
        public bool isClean
        {
            get
            {
                return !this.RBLChecks.Any(c => c.isClean == false);
            }
        }
        public List<RBLCheck> RBLChecks { get; set; } = new List<RBLCheck>();
    }
    public class RBLCheck
    {
        public bool isClean { get; set; }
        public Models.DNSBL RBL { get; set; }
        public string Response { get; set; }
        public string Error { get; set; }
    }
}
