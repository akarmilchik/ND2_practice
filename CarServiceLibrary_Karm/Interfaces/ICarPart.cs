using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceLibrary_Karm
{
    public interface ICarPart
    {
        string Name { get; set; }

        string Type { get; set; }

        string Category { get; set; }

    }
}
