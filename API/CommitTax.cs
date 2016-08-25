namespace AvaTaxLibrary.API
{
    using System;
    using System.Configuration;
    using Avalara.AvaTax.Adapter;
    using Avalara.AvaTax.Adapter.AddressService;
    using Avalara.AvaTax.Adapter.TaxService;

    public class CommitTax
    {
        public static CommitTaxResult Execute(bool inProduction, string strOCN, out string summary)
        {
            summary = "";
            TaxServiceWrapper taxSvcWrapper = new TaxServiceWrapper();
            TaxSvc taxSvc = taxSvcWrapper.GetTaxSvcInstance(inProduction);

            CommitTaxRequest commitTaxRequest = new CommitTaxRequest();

            // Required Parameters
            commitTaxRequest.DocCode = strOCN;
            commitTaxRequest.DocType = DocumentType.SalesInvoice;
            commitTaxRequest.CompanyCode = Properties.Settings.Default.CompanyCode;

            // Optional Parameters
            //commitTaxRequest.NewDocCode = "INV001";

            CommitTaxResult commitTaxResult = taxSvc.CommitTax(commitTaxRequest);

            if (!commitTaxResult.ResultCode.Equals(SeverityLevel.Success))
            {
                foreach (Message message in commitTaxResult.Messages)
                {
                    summary = message.Summary;
                }
            }

            return commitTaxResult;
        }
    }
}
