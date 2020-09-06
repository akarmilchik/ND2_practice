using System;
using System.Collections.Generic;
using System.Text;
using CarServiceLibrary_Karm.Interfaces;

namespace CarServiceLibrary_Karm
{
    public class DefaultDiscount : IDiscount
    {
        public decimal GetDiscount(decimal totalPrice, Customer customer, List<Customer> VIPCustomers)
        {
            var discount = 0m;

            if (totalPrice >= 200)
            { 
                discount += 5; 
            }

            foreach (Customer customerVIP in VIPCustomers)
            {
                if (customerVIP.Name.Equals(customer.Name) && customerVIP.SurName.Equals(customer.SurName))
                {
                    discount += 10;
                }
            }

            return totalPrice * discount / 100;

        }

    }
}
