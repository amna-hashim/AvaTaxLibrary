using Avalara.AvaTax.Adapter.TaxService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaTaxLibrary
{
    internal class TaxServiceWrapper
    {
        public TaxSvc GetTaxSvcInstance(bool inProduction)
        {
            TaxSvc taxSvc = new TaxSvc();
            taxSvc.Configuration.Url = Properties.Settings.Default.ServiceUrl;
            taxSvc.Configuration.ViaUrl = Properties.Settings.Default.ServiceUrl;
            taxSvc.Profile.Client = Properties.Settings.Default.Client;
            taxSvc.Configuration.Security.UserName = Properties.Settings.Default.UserName;
            taxSvc.Configuration.Security.Password = Properties.Settings.Default.Password;
            taxSvc.Profile.Name = inProduction ? "Production" : "Development";

            return taxSvc;
        }
    }
}
