using System;
using System.Collections.Generic;
using System.Text;

namespace DickinsDev.Utilities.Smtp
{
    public class SmtpConfiguration
    {
        public string Hostname { get; set; } = "localhost";
        public int Port { get; set; } = 25;

        public bool UseSsl { get; set; } = false;
        public bool AllowUntrustedCertificates { get; set; } = false;
        public bool RequireAuthentication { get; set; } = false;

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
