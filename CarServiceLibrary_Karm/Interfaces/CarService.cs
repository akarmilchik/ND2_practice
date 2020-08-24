using System.Collections.Generic;

namespace CarServiceLibrary_Karm
{
    public interface CarService
    {
        List<Service> ServiceList { get; set; }
        void CalcOrderPrice();
    }
}
