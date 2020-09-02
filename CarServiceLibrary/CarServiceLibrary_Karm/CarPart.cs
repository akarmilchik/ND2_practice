namespace CarServiceLibrary_Karm
{
    public class CarPart
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }

        public CarPart(string name, string type, string category)
        {
            Name = name;
            Type = type;
            Category = category;
        }
    }
}
