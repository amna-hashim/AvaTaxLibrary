namespace AvaTaxLibrary.API
{
    using System;
    using System.Configuration;
    using Avalara.AvaTax.Adapter;
    using Avalara.AvaTax.Adapter.AddressService;
    using Avalara.AvaTax.Adapter.TaxService;

    public class AdjustTaxTest
    {
        public static AdjustTaxResult Execute(CustomerOrder refundOrder, out string  summary)
        {
            summary = "";
            TaxServiceWrapper taxSvcWrapper = new TaxServiceWrapper();
            TaxSvc taxSvc = taxSvcWrapper.GetTaxSvcInstance(refundOrder.InProduction);

            AdjustTaxRequest adjustTaxRequest = new AdjustTaxRequest();

            GetTaxRequest getTaxRequest = GetTax.BuildGetTaxRequest(refundOrder);
            getTaxRequest.TaxOverride.TaxOverrideType = TaxOverrideType.TaxAmount;
            getTaxRequest.TaxOverride.Reason = "Adjustment for router return";
            //getTaxRequest.TaxOverride.TaxDate = DateTime.Parse("2013-07-01");
            getTaxRequest.TaxOverride.TaxAmount = refundOrder.TotalTax;
            getTaxRequest.ServiceMode = ServiceMode.Automatic;
        
            adjustTaxRequest.GetTaxRequest = getTaxRequest;
            adjustTaxRequest.AdjustmentReason = 5;
            adjustTaxRequest.AdjustmentDescription = "Tax adjusted based on router refund";

            AdjustTaxResult adjustTaxResult = taxSvc.AdjustTax(adjustTaxRequest);

            if (!adjustTaxResult.ResultCode.Equals(SeverityLevel.Success))
            {
                foreach (Message message in adjustTaxResult.Messages)
                {
                    summary = message.Summary;
                }
            }

            return adjustTaxResult;
        }
    }
}
