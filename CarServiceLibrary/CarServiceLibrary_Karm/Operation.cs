using CarServiceLibrary_Karm.Interfaces;

namespace CarServiceLibrary_Karm
{
    public class Operation : IOperation
    {

        public string OperationType { get; set; }

        public string OperationCategory { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
