using System.Collections.Generic;

namespace CarServiceLibrary_Karm
{
    interface ICarRepairService<T> where T : class
    {
        public string Name { get; set; }

        bool CheckExist(T Model);

    }
}
