using System.Collections.Generic;

namespace CarServiceLibrary_Karm.Interfaces
{
    public interface ICarRepairService
    {
        public string Name { get; set; }

        public List<IOperation> Operations { get; set; }

        public decimal GetOrderPrice(WorkOrder workOrder);

    }
}
