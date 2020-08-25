using System.Collections.Generic;

namespace CarServiceLibrary_Karm
{
    public interface CarRepairService
    {
        List<Service> ServiceList { get; set; }
        void CalcOrderPrice();
    }
}
