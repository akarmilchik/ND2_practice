using CarServiceLibrary_Karm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarServiceLibrary_Karm
{
    public class CarRepairService : ICarRepairService
    {
        public IDiscount Discount { get; set; }

        public string Name { get; set; }

        public List<IOperation> Operations { get; set; }

        public List<Customer> VIPCustomers = new List<Customer>();
       
        public decimal GetOrderPrice(WorkOrder workOrder)
        {
            var price = 0m;

            if (CheckTransportForContentPart(workOrder))
            {
                var approachOperations = GetApproachOperations(workOrder);

                price = approachOperations.Sum(n => n.Price);
            }

            return price - Discount.GetDiscount(price, workOrder.OrderCustomer, VIPCustomers);
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
                    if ((operation.Description.Equals(workOrder.ChosenServiceList[i].Description)) && 
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
