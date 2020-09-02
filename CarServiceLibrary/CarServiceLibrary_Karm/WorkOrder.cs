using System.Collections.Generic;

namespace CarServiceLibrary_Karm
{
    public class WorkOrder
    {
        public Car OrderCar;

        public Customer OrderCustomer;

        public List<Operation> ChosenServiceList;

    }
}
