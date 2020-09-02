using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceLibrary_Karm.Interfaces
{
    public interface IOperation
    {
        string OperationType { get; set; }

        string OperationCategory { get; set; }

        string Description { get; set; }

        string Name { get; set; }

        decimal Price { get; set; }

    }
}
