namespace AvaTaxLibrary.API
{
    using System;
    using System.Configuration;
    using Avalara.AvaTax.Adapter;
    using Avalara.AvaTax.Adapter.AddressService;
    using Avalara.AvaTax.Adapter.TaxService;

    public class GetTax
    {
        public static GetTaxRequest BuildGetTaxRequest(CustomerOrder order)
        {
            GetTaxRequest getTaxRequest = new GetTaxRequest();

            // Document Level Parameters
            // Required Request Parameters
            getTaxRequest.CustomerCode = Properties.Settings.Default.Client;
            getTaxRequest.DocDate = DateTime.Now;
            //// getTaxRequest.Lines is also required, and is presented later in this file.

            // Best Practice Request Parameters
            //getTaxRequest.CompanyCode = Properties.Settings.Default.Client;
            getTaxRequest.DocCode = order.OCN;
            //getTaxRequest.DetailLevel = DetailLevel.Tax;
            //getTaxRequest.Commit = false;
            getTaxRequest.DocType = DocumentType.SalesInvoice;

            // Situational Request Parameters
            // getTaxRequest.BusinessIdentificationNo = "234243";
            // getTaxRequest.CustomerUsageType = "G";
            // getTaxRequest.ExemptionNo = "12345";
            // getTaxRequest.Discount = 50;
            // getTaxRequest.LocationCode = "01";

            // Optional Request Parameters
            //getTaxRequest.PurchaseOrderNo = "PO123456";
            //getTaxRequest.ReferenceCode = "ref123456";
            //getTaxRequest.PosLaneCode = "09";
            //getTaxRequest.CurrencyCode = "USD";
            //getTaxRequest.ExchangeRate = (decimal)1.0;
            //getTaxRequest.ExchangeRateEffDate = DateTime.Parse("2013-01-01");
            //getTaxRequest.SalespersonCode = "Bill Sales";


            // Address Data
            Address address1 = new Address();
            address1.Line1 = Properties.Settings.Default.CompanyAddressLine1;
            //address2.line2 = "suite 100";
            //address2.line3 = "attn accounts payable";
            address1.City = Properties.Settings.Default.CompanyCity;
            address1.Region = Properties.Settings.Default.CompanyState;
            address1.Country = Properties.Settings.Default.CompanyCountry;
            address1.PostalCode = Properties.Settings.Default.CompanyPostalCode;

            Address address2 = new Address();
            //address1.AddressCode = "01";
            address2.Line1 = order.AddressLine1;
            address2.Line2 = order.AddressLine2;
            address2.City = order.City;
            address2.Region = order.State;
            address2.PostalCode = order.PostalCode;
            address2.Country = order.Country;
            //address1.Latitude = Convert.ToDecimal(40.601567);
            //address1.Longitude = Convert.ToDecimal(-105.066062);

            //Address address3 = new Address();
            //address3.Latitude = "47.627935";
            //address3.Longitude = "-122.51702";

            // Line Data
            // Required Parameters
            Line line1 = new Line();
            line1.No = "01";
            //line1.ItemCode = "N543";
            line1.Qty = order.Quantity;
            line1.Amount = order.TotalAmount;
            line1.OriginAddress = address1;
            line1.DestinationAddress = address2;

            // Best Practice Request Parameters
            //line1.Description = "Red Size 7 Widget";
            //line1.TaxCode = "NT";

            // Situational Request Parameters
            // line1.CustomerUsageType = "L";
            // line1.ExemptionNo = "12345";
            // line1.Discounted = true;
            // line1.TaxIncluded = true;
            // line1.TaxOverride.TaxOverrideType = TaxOverrideType.TaxDate;
            // line1.TaxOverride.Reason = "Adjustment for return";
            // line1.TaxOverride.TaxDate = DateTime.Parse("2013-07-01");
            // line1.TaxOverride.TaxAmount = 0;

            // Optional Request Parameters
            //line1.Ref1 = "ref123";
            //line1.Ref2 = "ref456";
            getTaxRequest.Lines.Add(line1);

            //Line line2 = new Line();
            //line2.No = "02";
            //line2.ItemCode = "T345";
            //line2.Qty = 3;
            //line2.Amount = 150;
            //line2.OriginAddress = address1;
            //line2.DestinationAddress = address3;
            //line2.Description = "Size 10 Green Running Shoe";
            //line2.TaxCode = "PC030147";
            //getTaxRequest.Lines.Add(line2);

            //Line line3 = new Line();
            //line3.No = "02-FR";
            //line3.ItemCode = "FREIGHT";
            //line3.Qty = 1;
            //line3.Amount = 15;
            //line3.OriginAddress = address1;
            //line3.DestinationAddress = address3;
            //line3.Description = "Shipping Charge";
            //line3.TaxCode = "FR";
            //getTaxRequest.Lines.Add(line3);

            return getTaxRequest;
        }
      
        public static GetTaxResult Execute(CustomerOrder order, out string  summary)
        {
            summary = "";
            TaxServiceWrapper taxSvcWrapper = new TaxServiceWrapper();
            TaxSvc taxSvc = taxSvcWrapper.GetTaxSvcInstance(order.InProduction);

            PostTaxRequest postTaxRequest = new PostTaxRequest();

            GetTaxRequest getTaxRequest = BuildGetTaxRequest(order);

            GetTaxResult getTaxResult = taxSvc.GetTax(getTaxRequest);

            if (!getTaxResult.ResultCode.Equals(SeverityLevel.Success))
            {
                foreach (Message message in getTaxResult.Messages)
                {
                    summary = message.Summary;
                }
            }

            return getTaxResult;
        }
    }
}
