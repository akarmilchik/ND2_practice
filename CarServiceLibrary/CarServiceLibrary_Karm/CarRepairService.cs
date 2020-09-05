﻿using CarServiceLibrary_Karm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CarServiceLibrary_Karm
{
    public class CarRepairService : ICarRepairService
    {
        private bool isValid = true;

        public IDiscount Discount { get; private set; }

        public string Name { get; set; }

        public List<IOperation> Operations { get; set; }

        public CarRepairService(string name, List<IOperation> operations, IDiscount discount)
        {
            Name = name;
            Operations = operations;
            Discount = discount;

        }


        private bool CheckExist(WorkOrder workOrder)
        {            
            if (workOrder == null)
            {
                isValid = false;
            }

            return isValid;
        }

        private bool CheckPrice(WorkOrder workOrder)
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
        private bool CheckParts(WorkOrder workOrder)
        {
            var carPart = workOrder.OrderCar.Parts.AsEnumerable().Select(v => v.Type == "Electrical" && v.Category == "Engine").FirstOrDefault();

            if (carPart == true)
            {
                foreach (var itemService in workOrder.ChosenServiceList)
                {
                    if ((itemService.OperationCategory == "Engine") && (itemService.OperationType == "Petrol" || itemService.OperationType == "Diesel"))
                        isValid = false;
                }


            }

            carPart = workOrder.OrderCar.Parts.AsEnumerable().Select(v => ((v.Type == "Diesel" || v.Type == "Petrol") && (v.Category == "Engine"))).FirstOrDefault();

            if (carPart == true)
            {
                foreach (var itemService in workOrder.ChosenServiceList)
                {
                    if (itemService.OperationCategory == "Engine" && itemService.OperationType == "Electrical")
                        isValid = false;
                }


            }

            return isValid;
        }
        public decimal GetOrderPrice(WorkOrder workOrder)
        {
            var price = 0m;

            if (CheckTransportForContentPart(workOrder))
            {
                var approachOperations = GetApproachOperations(workOrder);

                price = approachOperations.Sum(n => n.Price);
            }

            return price;
        }

        private bool CheckTransportForContentPart(WorkOrder workOrder)
        {
            var checkRes = true;

            for (int i = 0; i < workOrder.ChosenServiceList.Count; i++)
            {
                if (!(workOrder.OrderCar.Parts.Any(k => k.Category.Equals(workOrder.ChosenServiceList[i].OperationCategory))) ||
                    !(workOrder.OrderCar.Parts.Any(k => k.Type.Equals(workOrder.ChosenServiceList[i].OperationType))))
                {
                    checkRes =  false;
                    break;
                }
            }
            return checkRes;
        }

        private List<IOperation> GetApproachOperations(WorkOrder workOrder)
        {
            var result = new List<IOperation>() { };

            foreach (IOperation operation in Operations)
            {
                for (int i = 0; i < workOrder.ChosenServiceList.Count; i++)
                {
                    if ((operation.Name.Equals(workOrder.ChosenServiceList[i].Name)) && 
                        (operation.OperationCategory == workOrder.ChosenServiceList[i].OperationCategory) && 
                        (operation.OperationType == workOrder.ChosenServiceList[i].OperationType))
                    { 
                        result.Add(operation); 
                    }
                }
            }

            return result;
        }

    }
    
}
