using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeFutureWebApplication.Models
{
    public class Customer
    {
        public Customer()
        {
            ProductsDistributed = new List<string>();
        }

        public string Name { get; set; }
        public int Zipcode { get; set; }
        public int HouseholdSize { get; set; }
        public string Email { get; set; }
        public List<string> ProductsDistributed { get; set; }


    }
}
