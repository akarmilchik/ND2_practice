using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceLibrary_Karm.Interfaces
{
    public interface IDiscount
    {
        public decimal GetDiscount(decimal totalSum, Customer customer, List<Customer> VIPCustomers);

    }
}
