using System.Collections.Generic;
using CarServiceLibrary_Karm.Interfaces;

namespace CarServiceLibrary_Karm
{
    public class WorkOrder
    {
        public Car OrderCar;

        public Customer OrderCustomer;

        public List<IOperation> ChosenServiceList;
    }
}
