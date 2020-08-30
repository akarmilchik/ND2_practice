using System.Collections.Generic;

namespace CarServiceLibrary_Karm
{
    public class Car : ICar
    {
        public string Model { get; set; }

        public string VIN { get; set; }

        public List<CarPart> Parts;
            
    }
}
