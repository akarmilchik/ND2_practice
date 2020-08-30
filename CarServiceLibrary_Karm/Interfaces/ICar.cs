using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceLibrary_Karm
{
    public interface ICar
    {
        string Model { get; set; }

        string VIN { get; set; }

        List<CarPart> Parts;

    }
}
