using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CarServiceLibrary_Karm
{
    public class TestCarService : ICarRepairService<WorkOrder>
    {
        bool isValid = true;
        bool CheckExist(WorkOrder workOrder)
        {            
            if (workOrder == null)
            {
                isValid = false;
            }

            return isValid;
        }

        bool CheckPrice(WorkOrder workOrder)
        {
            foreach (var itemService in workOrder.ChosenServiceList)
            {
                if (itemService.Price == 0)
                {
                    isValid = false;
                }
            }
            return isValid;
        }
        bool CheckParts(WorkOrder workOrder)
        {
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
