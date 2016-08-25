namespace AvaTaxLibrary.API
{
    using System;
    using System.Configuration;
    using Avalara.AvaTax.Adapter;
    using Avalara.AvaTax.Adapter.TaxService;

    public class CancelTax
    {
        public static CancelTaxResult Execute(bool inProduction, string strOCN, out string  summary)
        {
            summary = "";
            TaxServiceWrapper taxSvcWrapper = new TaxServiceWrapper();
            TaxSvc taxSvc = taxSvcWrapper.GetTaxSvcInstance(inProduction);

            CancelTaxRequest cancelTaxRequest = new CancelTaxRequest();

            // Required Request Parameters
            cancelTaxRequest.CompanyCode = Properties.Settings.Default.CompanyCode;
            cancelTaxRequest.DocType = DocumentType.SalesInvoice;
            cancelTaxRequest.DocCode = strOCN;
            cancelTaxRequest.CancelCode = CancelCode.DocVoided;

            CancelTaxResult cancelTaxResult = taxSvc.CancelTax(cancelTaxRequest);

            if (!cancelTaxResult.ResultCode.Equals(SeverityLevel.Success))
            {
                foreach (Message message in cancelTaxResult.Messages)
                {
                    summary = message.Summary;
                }
            }

            return cancelTaxResult;
        }
    }
}
