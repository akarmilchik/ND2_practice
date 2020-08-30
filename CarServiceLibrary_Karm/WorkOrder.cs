using System.Collections.Generic;

namespace CarServiceLibrary_Karm
{
    public class WorkOrder
    {
        public Car OrderCar;

        public ICustomer OrderCustomer;

        public List<Service> ChosenServiceList;

    }
}
