using System.Collections.Generic;

namespace CarServiceLibrary_Karm
{
    public interface ICarRepairService<T1, T2, T3, T4, T5> where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class
    {
        bool Check(T1 Model1, T2 Model2, T3 Model3, T4 Model4, T5 Model5);

    }
}
