namespace AvaTaxCalcSOAP
{
    using System;
    using DGSAvaTaxLibrary;
    using DGSAvaTaxLibrary.API;
    using Avalara.AvaTax.Adapter.TaxService;

    public class Program
    {
        public static void Main()
        {
            try
            {
                string summary;
                #region Report Tax
                CustomerOrder order = new CustomerOrder();
                order.AddressLine1 = "631 LUPINE DR";
                order.City = "FORT COLLINS";
                order.Country = "US";
                order.State = "CO";
                order.PostalCode = "80524";
                order.InProduction = false;
                order.IsCommit = false;
                order.OCN = "041025304MS";
                order.Quantity = 1;
                order.TotalAmount = 66.98m;
                order.TotalTax = 4.96m;

                GetTaxResult getTaxResult = GetTax.Execute(order, out summary);
                PostTaxResult postTaxResult = PostTax.Execute(order, getTaxResult, out summary);
                #endregion

                #region same day cancellation or when shipment charges is filed
                CustomerOrder cancel = new CustomerOrder();
                cancel.InProduction = false;
                cancel.OCN = "041025304MS";
                CancelTaxResult cancelTaxResult = CancelTax.Execute(cancel.InProduction, cancel.OCN, out summary);
                #endregion

                #region harware refund
                CustomerOrder refund = new CustomerOrder();
                refund.AddressLine1 = "631 LUPINE DR";
                refund.City = "FORT COLLINS";
                refund.Country = "US";
                refund.State = "CO";
                refund.PostalCode = "80524";
                refund.InProduction = false;
                refund.IsCommit = false;
                refund.OCN = "041025304MS";
                refund.Quantity = 1;
                refund.TotalAmount = 54.99m; // hardware cost
                refund.TotalTax = 4.06m; // tax portion of hardware cost

                AdjustTaxResult adjustTaxResult = AdjustTaxTest.Execute(refund, out summary);
                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine("An Exception Occured: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Done");
                Console.ReadLine();
            }
        }
    }
}
