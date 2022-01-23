using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DickinsDev.RblMonitor.WebUI.Controllers
{
    public class ApiController : Controller
    {
        private readonly Data.DataContext _context;
        private readonly Utilities.Smtp.ISmtpClient _smtpClient;
        public ApiController(Data.DataContext context, Utilities.Smtp.ISmtpClient smtpClient)
        {
            _context = context;
            _smtpClient = smtpClient;
        }

        public async Task<IActionResult> CheckAll()
        {
            var allMonitors = await _context.IPMonitors.ToListAsync();
            foreach (var monitor in allMonitors.Where(m => m.isActive && m.CheckRequired))
            {
                var check = Data.Functions.Static.CheckIP(monitor.IPAddress, _context.Nameservers.ToArray(), _context.DNSBLs.ToArray());
                if (check.isClean != monitor.isClean)
                    await SendAlert(monitor.IPAddress, check);

                monitor.isClean = check.isClean;
                monitor.LastCheck = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();

            return new StatusCodeResult(201);
        }

        public async Task<int> SendAlert(string IPAddress, Data.Models.DnsCheckResult dnsCheckResult)
        {
            Utilities.Smtp.SmtpMessage smtpMessage = new Utilities.Smtp.SmtpMessage();
            _smtpClient.OverrideFromAddress("RBL Monitor", "RBLMonitor@dickins.dev");

            foreach (var user in _context.EmailTargets.Where(m => m.isActive))
                smtpMessage.ToEmail.Add(user.EmailAddress);

            

            smtpMessage.Subject = $"IP Address '{IPAddress}' status has changed.";
            smtpMessage.Body = $"The IP Address '{IPAddress}' has changed state.\r\n" +
                $"Is the address clean? {dnsCheckResult.isClean}\r\n" +
                $"How many RBL's does the address appear on: {dnsCheckResult.RBLChecks.Count}";
            try
            {

                await _smtpClient.SendEmailAsync(smtpMessage);
            }
            catch { return 1; }
            return 0;
        }
    }
}
