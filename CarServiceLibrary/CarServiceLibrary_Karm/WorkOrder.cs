using System.Collections.Generic;
using CarServiceLibrary_Karm.Interfaces;

namespace CarServiceLibrary_Karm
{
    public class WorkOrder
    {
        public Car OrderCar;

        public Customer OrderCustomer;

        public List<IOperation> ChosenServiceList;

        public WorkOrder(Car orderCar, Customer orderCustomer, List<IOperation> chosenServiceList)
        {
            OrderCar = orderCar;
            OrderCustomer = orderCustomer;
            ChosenServiceList = chosenServiceList;
        }

    }
}
