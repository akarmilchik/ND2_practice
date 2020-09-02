namespace CarServiceLibrary_Karm
{
    public class Customer
    {
        public string Name { get; set; }

        public string SurName { get; set; }

        public Customer(string name, string surName)
        {
            Name = name;
            SurName = surName;
        }

    }
}
