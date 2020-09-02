namespace CarServiceLibrary_Karm
{
    public class Service : IService
    {

        public string ServiceType { get; set; }

        public string ServiceCategory { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
