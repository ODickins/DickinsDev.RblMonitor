using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DickinsDev.RblMonitor.Data.Functions
{
    public class Static
    {
        public static Models.DnsCheckResult CheckIP(string IPAddress, Models.Nameserver[] NameServers, Models.DNSBL[] DNSBLs)
        {
            // Confirm the IP Address given to the function is valid
            System.Net.IPAddress __address;
            if (!System.Net.IPAddress.TryParse(IPAddress, out __address))
                throw new ApplicationException($"Invalid IP Address: {IPAddress}");

            // Flip the IP Address given so that we can check a zone
            string[] __parts = __address.ToString().Split(".");
            Array.Reverse(__parts);

            // Get a list of all name servers from the input array where they are active
            List<DnsClient.NameServer> DnsNameServers = new List<DnsClient.NameServer>();
            foreach (var ns in NameServers)
                if (ns.isActive)
                    DnsNameServers.Add(new DnsClient.NameServer(System.Net.IPAddress.Parse(ns.IPAddress)));


            // Setup DNS Lookup Client
            DnsClient.LookupClient lookupClient = new DnsClient.LookupClient(DnsNameServers.ToArray());


            Models.DnsCheckResult dnsCheckResult = new Models.DnsCheckResult();

            // For each DNSBL where the BL is Active
            foreach (var bl in DNSBLs.Where(b => b.isActive))
            {
                try
                {
                    var response = lookupClient.Query(String.Format("{0}.{1}.{2}.{3}."+ bl.ZoneName, __parts), DnsClient.QueryType.A);
                    if (response.Answers.Count > 0)
                    {
                        string answer = string.Empty;
                        foreach (var ans in response.Answers)
                        {
                            answer += $"{response.Answers[0].ToString()},";
                        }
                        dnsCheckResult.RBLChecks.Add(new Models.RBLCheck()
                        {
                            isClean = false,
                            Response = answer,
                            RBL = bl
                        });
                    }
                    else
                    {
                        dnsCheckResult.RBLChecks.Add(new Models.RBLCheck()
                        {
                            isClean = true,
                            Response = "No Record.",
                            RBL = bl
                        });
                    }
                }
                catch (Exception ex)
                {
                    dnsCheckResult.RBLChecks.Add(new Models.RBLCheck()
                    {
                        isClean = true,
                        RBL = bl,
                        Error = ex.Message
                    });
                }
            }

            return dnsCheckResult;

        }
    }
}
