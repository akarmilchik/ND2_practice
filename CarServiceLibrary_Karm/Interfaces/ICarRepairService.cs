﻿using System.Collections.Generic;

namespace CarServiceLibrary_Karm
{
    public interface ICarRepairService<T> where T : class
    {
        bool Check(T Model);

    }
}