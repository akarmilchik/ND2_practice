using System.Collections.Generic;

namespace CarServiceLibrary_Karm
{
    public class Car
    {
        public string Model { get; set; }

        public string VIN { get; set; }

        public List<CarParts> Parts;
            
    }
}
