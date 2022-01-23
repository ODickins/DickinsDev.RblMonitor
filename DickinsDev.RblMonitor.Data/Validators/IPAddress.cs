using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DickinsDev.RblMonitor.Data.Validators
{
    public class IPAddress : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            System.Net.IPAddress __address;
            if (System.Net.IPAddress.TryParse((string)value, out __address))
                return true;
            return false;
        }
        public override string FormatErrorMessage(string name)
        {
            return $"Field '{name}' is not a valid IP Address.";
        }
    }
}
