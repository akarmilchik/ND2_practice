using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceLibrary_Karm
{
    public interface IService
    {
        string Description { get; set; }

        decimal Price { get; set; }

    }
}
