using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DickinsDev.RblMonitor.WebUI.Models.Home
{
    public class QuickCheckModel
    {
        public string IPAddress { get; set; }
        public bool isClean => DnsCheckResult.isClean;
        public Data.Models.DnsCheckResult DnsCheckResult { get; set; }
    }
}
