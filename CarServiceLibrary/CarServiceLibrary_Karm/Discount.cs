using System;
using System.Collections.Generic;
using System.Text;
using CarServiceLibrary_Karm.Interfaces;

namespace CarServiceLibrary_Karm
{
    public class Discount : IDiscount
    {
        public List<Customer> VIPCustomers { get; set; }

        public decimal GetDiscount(decimal totalPrice, Customer customer)
        {
            var discount = 0m;

            if (totalPrice >= 100)
            { 
                discount += 5; 
            }

            foreach (Customer goldenCustomer in VIPCustomers)
            {
                if (goldenCustomer.Name.Equals(customer.Name) && goldenCustomer.SurName.Equals(customer.SurName))
                {
                    discount += 10;
                }
            }

            return totalPrice * discount / 100;

        }
    }
}
