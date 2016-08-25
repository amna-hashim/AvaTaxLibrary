using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaTaxLibrary
{
    /// <summary>
    /// it will contain all details of the customer and order.
    /// </summary>
    public class CustomerOrder
    {
        public bool InProduction { get; set; }
        public bool IsCommit { get; set; } // boolean to commit transaction in avalara, default value is false
        public int Quantity { get; set; } // total quantity of router
        public decimal TotalTax { get; set; } // total sales tax
        public decimal TotalAmount { get; set; } // total sales amount before tax
        public string OCN { get; set; }  //router OCN

        public string AddressLine1 { get; set; } // customer address line 1
        public string AddressLine2 { get; set; } // customer address line 2
        public string City { get; set; } // customer city
        public string State { get; set; } // customer state
        public string PostalCode { get; set; } // customer postal code
        public string Country { get; set; } // customer country
    }
}
