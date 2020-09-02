using System.Collections.Generic;

namespace CarServiceLibrary_Karm
{
    public class WorkOrder
    {
        public Car OrderCar;

        public Customer OrderCustomer;

        public List<Operation> ChosenServiceList;

        public WorkOrder(Car orderCar, Customer orderCustomer, List<Operation> chosenServiceList)
        {
            OrderCar = orderCar;
            OrderCustomer = orderCustomer;
            ChosenServiceList = chosenServiceList;
        }

    }
}
