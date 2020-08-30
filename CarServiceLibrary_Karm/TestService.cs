using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CarServiceLibrary_Karm
{
    public class TestService : ICarRepairService<WorkOrder>
    {

        public bool Check(WorkOrder workOrder)
        {

            bool isValid = true;

            if (workOrder.OrderCar == null || workOrder.OrderCustomer == null || workOrder.ChosenServiceList.Count == 0 || workOrder.OrderCar.Parts.Count == 0)
            {
                Console.WriteLine("Some data was not entered");
                isValid = false;
            }

            foreach (var itemService in workOrder.ChosenServiceList)
            {
                if (itemService.Price == 0)
                {
                    Console.WriteLine("The price for the service {0} is not specified", itemService.Description);
                    isValid = false;
                }
            }

            var carPart = workOrder.OrderCar.Parts.AsEnumerable().Select(v => v.Type == "Electrical" && v.Category == "Engine").FirstOrDefault();

            if (carPart == true)
            {
                foreach (var itemService in workOrder.ChosenServiceList)
                {
                    if ((itemService.ServiceCategory == "Engine") && (itemService.ServiceType == "Petrol" || itemService.ServiceType == "Diesel"))
                        isValid = false;
                }


            }

            carPart = workOrder.OrderCar.Parts.AsEnumerable().Select(v => ((v.Type == "Diesel" || v.Type == "Petrol") && (v.Category == "Engine"))).FirstOrDefault();

            if (carPart == true)
            {
                foreach (var itemService in workOrder.ChosenServiceList)
                {
                    if (itemService.ServiceCategory == "Engine" && itemService.ServiceType == "Electrical")
                        isValid = false;
                }


            }


            return isValid;

        }
    }
}
