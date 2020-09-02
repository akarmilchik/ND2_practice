using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceLibrary_Karm.Interfaces
{
    public interface IDiscount
    {
        public List<Customer> VIPCustomers { get; set; }
        public decimal GetDiscount(decimal totalSum, Customer customer);

    }
}
