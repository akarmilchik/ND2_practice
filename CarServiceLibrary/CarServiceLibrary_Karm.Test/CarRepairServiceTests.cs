using CarServiceLibrary_Karm.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CarServiceLibrary_Karm.Test
{
    [TestFixture]
    public class CarRepairServiceTests
    {
        [SetUp]
        public void Setup()
        {

        }

        class StubDiscount : IDiscount
        {
            public decimal GetDiscount(decimal totalSum, Customer customer, List<Customer> VIPCustomers)
            {
                return 0;
            }
        }

        [Test]
        public void GetOrderPrice_CorrectListProvided_CorrectTotalReturned()
        {
            // Arrange
            var VwPassat5Parts = new List<CarPart>()
            {
                new CarPart { Name = "VW ADR 1.8P", Category = "Engine", Type = "Petrol" },
                new CarPart { Name = "18565R15", Category = "Wheels", Type = "Steel" },
                new CarPart { Name = "Red Cherry", Category = "Body", Type = "Sedan" },
                new CarPart { Name = "MKPP 5", Category = "Transmission", Type = "Mechanical" }
            };

            var operationsList = new List<IOperation>()
            {
                new Operation() { Description = "Oil change in a petrol engine", Price = 70, OperationCategory = "Engine", OperationType = "Petrol" },
                new Operation() { Description = "Disc polishing", Price = 10, OperationCategory = "Wheels", OperationType = "Steel" },
                new Operation() { Description = "Painting the whole car body", Price = 500, OperationCategory = "Body", OperationType = "Sedan" },
                new Operation() { Description = "Transmission repair", Price = 120, OperationCategory = "Transmission", OperationType = "Machine" },
                new Operation() { Description = "Transmission repair", Price = 100, OperationCategory = "Transmission", OperationType = "Mechanical" }
            };
            var VwPassat5 = new Car { Model = "Volkswagen Passat B5", VIN = "MG245110H901", Parts = VwPassat5Parts };

            var AlexKarm = new Customer { Name = "Alexey", SurName = "Karmilchyk" };

            var order = new WorkOrder { OrderCar = VwPassat5, OrderCustomer = AlexKarm, ChosenServiceList = new List<IOperation> { operationsList[0], operationsList[2], operationsList[4] } };

            var discount = new StubDiscount();

            

            var carService = new CarRepairService { Name = "BestCar Service", Operations = operationsList, Discount = discount };

            // Act
            var price = carService.GetOrderPrice(order);

            // Assert
            Assert.AreEqual(670m, price);
        }

        [Test]
        public void GetOrderPrice_IncorrectListProvided_CorrectTotalReturned_CarParts()
        {
            // Arrange
            var VwPassat5Parts = new List<CarPart>()
            {
                new CarPart { Name = "VW ADR 1.8P", Category = "Engine", Type = "Diesel" },
                new CarPart { Name = "18565R15", Category = "Wheels", Type = "Steel" },
                new CarPart { Name = "Red Cherry", Category = "Body", Type = "Sedan" },
                new CarPart { Name = "MKPP 5", Category = "Transmission", Type = "Mechanical" }
            };

            var operationsList = new List<IOperation>()
            {
                new Operation() { Description = "Oil change in a petrol engine", Price = 70, OperationCategory = "Engine", OperationType = "Petrol" },
                new Operation() { Description = "Disc polishing", Price = 10, OperationCategory = "Wheels", OperationType = "Steel" },
                new Operation() { Description = "Painting the whole car body", Price = 500, OperationCategory = "Body", OperationType = "Sedan" },
                new Operation() { Description = "Transmission repair", Price = 120, OperationCategory = "Transmission", OperationType = "Machine" },
                new Operation() { Description = "Transmission repair", Price = 100, OperationCategory = "Transmission", OperationType = "Mechanical" }
            };
            var VwPassat5 = new Car { Model = "Volkswagen Passat B5", VIN = "MG245110H901", Parts = VwPassat5Parts };

            var AlexKarm = new Customer { Name = "Alexey", SurName = "Karmilchyk" };

            var order = new WorkOrder { OrderCar = VwPassat5, OrderCustomer = AlexKarm, ChosenServiceList = new List<IOperation> { operationsList[0], operationsList[2], operationsList[4] } };
           
            var discount = new StubDiscount();

            var carService = new CarRepairService { Name = "BestCar Service", Operations = operationsList, Discount = discount };

            // Act
            var price = carService.GetOrderPrice(order);

            // Assert
            Assert.AreEqual(0m, price);
        }

        [Test]
        public void GetOrderPrice_IncorrectListProvided_CorrectTotalReturned_ChosenOperations()
        {
            // Arrange
            var VwPassat5Parts = new List<CarPart>()
            {
                new CarPart { Name = "VW ADR 1.8P", Category = "Engine", Type = "Diesel" },
                new CarPart { Name = "18565R15", Category = "Wheels", Type = "Steel" },
                new CarPart { Name = "Red Cherry", Category = "Body", Type = "Sedan" },
                new CarPart { Name = "MKPP 5", Category = "Transmission", Type = "Mechanical" }
            };

            var operationsList = new List<IOperation>()
            {
                new Operation() { Description = "Oil change in a petrol engine", Price = 70, OperationCategory = "Engine", OperationType = "Petrol" },
                new Operation() { Description = "Disc polishing", Price = 10, OperationCategory = "Wheels", OperationType = "Steel" },
                new Operation() { Description = "Painting the whole car body", Price = 500, OperationCategory = "Body", OperationType = "Sedan" },
                new Operation() { Description = "Transmission repair", Price = 120, OperationCategory = "Transmission", OperationType = "Machine" },
                new Operation() { Description = "Transmission repair", Price = 100, OperationCategory = "Transmission", OperationType = "Mechanical" }
            };
            var VwPassat5 = new Car { Model = "Volkswagen Passat B5", VIN = "MG245110H901", Parts = VwPassat5Parts };

            var AlexKarm = new Customer { Name = "Alexey", SurName = "Karmilchyk" };

            var order = new WorkOrder { OrderCar = VwPassat5, OrderCustomer = AlexKarm, ChosenServiceList = new List<IOperation> { operationsList[0], operationsList[2], operationsList[3] } };

            var discount = new StubDiscount();

            var carService = new CarRepairService { Name = "BestCar Service", Operations = operationsList, Discount = discount };

            // Act
            var price = carService.GetOrderPrice(order);

            // Assert
            Assert.AreEqual(0m, price);
        }


        [Test]
        public void GetOrderPrice_CorrectListProvided_CorrectTotalReturned_WithDefaultDiscount()
        {
            // Arrange
            var VwPassat5Parts = new List<CarPart>()
            {
                new CarPart { Name = "VW ADR 1.8P", Category = "Engine", Type = "Petrol" },
                new CarPart { Name = "18565R15", Category = "Wheels", Type = "Steel" },
                new CarPart { Name = "Red Cherry", Category = "Body", Type = "Sedan" },
                new CarPart { Name = "MKPP 5", Category = "Transmission", Type = "Mechanical" }
            };

            var operationsList = new List<IOperation>()
            {
                new Operation() { Description = "Oil change in a petrol engine", Price = 70, OperationCategory = "Engine", OperationType = "Petrol" },
                new Operation() { Description = "Disc polishing", Price = 10, OperationCategory = "Wheels", OperationType = "Steel" },
                new Operation() { Description = "Painting the whole car body", Price = 500, OperationCategory = "Body", OperationType = "Sedan" },
                new Operation() { Description = "Transmission repair", Price = 120, OperationCategory = "Transmission", OperationType = "Machine" },
                new Operation() { Description = "Transmission repair", Price = 100, OperationCategory = "Transmission", OperationType = "Mechanical" }
            };

            var VwPassat5 = new Car { Model = "Volkswagen Passat B5", VIN = "MG245110H901", Parts = VwPassat5Parts };

            var AlexKarm = new Customer { Name = "Alexey", SurName = "Karmilchyk", DiscountStatus = "GOLD", DiscountValue = 10 };

            var order = new WorkOrder { OrderCar = VwPassat5, OrderCustomer = AlexKarm, ChosenServiceList = new List<IOperation> { operationsList[0], operationsList[2], operationsList[4] } };

            var VIPcutomers = new List<Customer> { AlexKarm };

            var discount = new DefaultDiscount();

            var carService = new CarRepairService { Name = "BestCar Service", Operations = operationsList, Discount = discount, VIPCustomers = VIPcutomers };

            // Act
            var price = carService.GetOrderPrice(order);

            // Assert
            Assert.AreEqual(569.5m, price);
        }

        
        [Test]
        public void GetTotal_CorrectListProvided_CorrectTotalReturned_Mock()
        {
            // Arrange
            var VwPassat5Parts = new List<CarPart>()
            {
                new CarPart { Name = "VW ADR 1.8P", Category = "Engine", Type = "Petrol" },
                new CarPart { Name = "18565R15", Category = "Wheels", Type = "Steel" },
                new CarPart { Name = "Red Cherry", Category = "Body", Type = "Sedan" },
                new CarPart { Name = "MKPP 5", Category = "Transmission", Type = "Mechanical" }
            };

            var VwPassat5 = new Car() { Model = "Vokswagen Passat B5", VIN = "MG245110H901", Parts = VwPassat5Parts };

            var customerGOLD = new Customer() { Name = "Alexey", SurName = "Karmilchyk", DiscountStatus = "GOLD", DiscountValue = 10 };
            var customerGuest = new Customer() { Name = "Vasya", SurName = "Pupkin", DiscountStatus = "Guest", DiscountValue = 1 };

            var VIPCustomers = new List<Customer>() { customerGOLD };

            var operationsList = new List<IOperation>()
            {
                new Operation() { Description = "Oil change in a petrol engine", Price = 70, OperationCategory = "Engine", OperationType = "Petrol" },
                new Operation() { Description = "Disc polishing", Price = 10, OperationCategory = "Wheels", OperationType = "Steel" },
                new Operation() { Description = "Painting the whole car body", Price = 500, OperationCategory = "Body", OperationType = "Sedan" },
                new Operation() { Description = "Transmission repair", Price = 120, OperationCategory = "Transmission", OperationType = "Machine" },
                new Operation() { Description = "Transmission repair", Price = 100, OperationCategory = "Transmission", OperationType = "Mechanical" }
            };

            var Mock = new Mock<IDiscount>();

            Mock.Setup(p => p.GetDiscount(It.IsAny<decimal>(), It.IsAny<Customer>(), It.IsAny<List<Customer>>())).Returns(0);
            Mock.Setup(p => p.GetDiscount(It.Is<decimal>(d => d > 200), It.IsAny<Customer>(), It.IsAny<List<Customer>>())).Returns(6);
            Mock.Setup(p => p.GetDiscount(It.Is<decimal>(d => d > 200), customerGOLD, VIPCustomers)).Returns(15);



            // var mock = new Mock<ICarRepairService<CarRepairService>>();

            //  mock.Setup(m => m.CheckExist(It.IsAny<WorkOrder>)).Returns(false);

            // Act


            // Assert

        }

        
    }
}