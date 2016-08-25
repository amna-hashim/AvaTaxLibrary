namespace AvaTaxLibrary.API
{
    using System;
    using System.Configuration;
    using Avalara.AvaTax.Adapter;
    using Avalara.AvaTax.Adapter.TaxService;

    public class PostTax
    {
        public static PostTaxResult Execute(CustomerOrder order, Avalara.AvaTax.Adapter.TaxService.GetTaxResult getTaxResult, out string summary)
        {
            summary = "";
            TaxServiceWrapper taxSvcWrapper = new TaxServiceWrapper();
            TaxSvc taxSvc = taxSvcWrapper.GetTaxSvcInstance(order.InProduction);

            PostTaxRequest postTaxRequest = new PostTaxRequest();

            // Required Request Parameters
            postTaxRequest.CompanyCode = Properties.Settings.Default.CompanyCode;
            postTaxRequest.DocType = DocumentType.SalesInvoice;
            postTaxRequest.DocCode = getTaxResult.DocCode;
            postTaxRequest.Commit = order.IsCommit;
            postTaxRequest.DocDate = getTaxResult.DocDate;
            postTaxRequest.TotalTax = order.TotalTax;
            postTaxRequest.TotalAmount = order.TotalAmount;

            // Optional Request Parameters
            postTaxRequest.NewDocCode = order.OCN;

            PostTaxResult postTaxResult = taxSvc.PostTax(postTaxRequest);

            if (!postTaxResult.ResultCode.Equals(SeverityLevel.Success))
            {
                foreach (Message message in postTaxResult.Messages)
                {
                    summary = message.Summary;
                }
            }

            return postTaxResult;
        }
    }
}
