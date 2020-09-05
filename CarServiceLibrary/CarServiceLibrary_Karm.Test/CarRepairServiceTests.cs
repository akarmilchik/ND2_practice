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
            public List<Customer> VIPCustomers { get; set; }

            public decimal GetDiscount(decimal totalSum, Customer customer)
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
                new CarPart("VW ADR 1.8P","Petrol","Engine"),
                new CarPart("18565R15","Steel", "Wheels"),
                new CarPart("Red Cherry", "Sedan", "Body"),
                new CarPart("MKPP 5","Mechanical","Transmission")
            };

            var operationsList = new List<IOperation>()
            {
                new Operation("Petrol", "Engine","Oil change in a petrol engine", 70, "Change oil petrol"),
                new Operation("Steel", "Wheels", "Disc polishing", 10, "Disc polishing"),
                new Operation("Sedan", "Body", "Painting the whole car body", 500, "Painting whole body"),
                new Operation("Machine", "Transmission", "Transmission repair", 120, "Transmission repair"),
                new Operation("Mechanical", "Transmission", "Transmission repair", 100, "Transmission repair")
            };
            var VwPassat5 = new Car("Vokswagen Passat B5", "MG245110H901", VwPassat5Parts);

            var AlexKarm = new Customer("Alexey", "Karmilchyk");

            var order = new WorkOrder(VwPassat5, AlexKarm, new List<IOperation> { operationsList[0], operationsList[2], operationsList[4]});

            var discount = new StubDiscount();

            var carService = new CarRepairService("BestCar Service", operationsList, discount);

            // Act
            var price = carService.GetOrderPrice(order);

            // Assert
            Assert.AreEqual(670m, price);
        }
        /*
        [Test]
        public void CheckAll_WorkOrderProvided_TrueResultReturned()
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

            var AlexKarm = new Customer() { Name = "Alexey", SurName = "Karmilchyk" };

            var ServiceListTestCarService = new List<Operation>()
            {
                new Operation() { Description = "Oil change in a petrol engine", Price = 70, OperationCategory = "Engine", OperationType = "Petrol" },
                new Operation() { Description = "Disc polishing", Price = 10, OperationCategory = "Wheels", OperationType = "Steel" },
                new Operation() { Description = "Painting the whole car body", Price = 500, OperationCategory = "Body", OperationType = "Sedan" },
                new Operation() { Description = "Transmission repair", Price = 120, OperationCategory = "Transmission", OperationType = "Machine" },
                new Operation() { Description = "Transmission repair", Price = 100, OperationCategory = "Transmission", OperationType = "Mechanical" }
            };

            var OrderNumOne = new WorkOrder()
            {
                OrderCar = VwPassat5,
                ChosenServiceList = new List<Operation>() { ServiceListTestCarService[0], ServiceListTestCarService[1], ServiceListTestCarService[3] },
                OrderCustomer = AlexKarm
            };

            var STO = new CarRepairService();

            // Act
            var validCheckExist = STO.CheckExist(OrderNumOne);
            var validCheckPrice = STO.CheckPrice(OrderNumOne);
            var validCheckParts = STO.CheckParts(OrderNumOne);

            // Assert
            Assert.AreEqual(true, validCheckExist);
            Assert.AreEqual(true, validCheckPrice);
            Assert.AreEqual(true, validCheckParts);
        }

        [Test]
        public void CheckAllMoq_WorkOrderProvided_TrueResultReturned()
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

            var AlexKarm = new Customer() { Name = "Alexey", SurName = "Karmilchyk" };

            var ServiceListTestCarService = new List<Operation>()
            {
                new Operation() { Description = "Oil change in a petrol engine", Price = 70, OperationCategory = "Engine", OperationType = "Petrol" },
                new Operation() { Description = "Disc polishing", Price = 10, OperationCategory = "Wheels", OperationType = "Steel" },
                new Operation() { Description = "Painting the whole car body", Price = 500, OperationCategory = "Body", OperationType = "Sedan" },
                new Operation() { Description = "Transmission repair", Price = 120, OperationCategory = "Transmission", OperationType = "Machine" },
                new Operation() { Description = "Transmission repair", Price = 100, OperationCategory = "Transmission", OperationType = "Mechanical" }
            };

            var OrderNumOne = new WorkOrder()
            {
                OrderCar = VwPassat5,
                ChosenServiceList = new List<Operation>() { ServiceListTestCarService[0], ServiceListTestCarService[1], ServiceListTestCarService[3] },
                OrderCustomer = AlexKarm
            };

            var STO = new CarRepairService();

            var mock = new Mock<ICarRepairService<CarRepairService>>();

          //  mock.Setup(m => m.CheckExist(It.IsAny<WorkOrder>)).Returns(false);

            // Act
            var validCheckExist = STO.CheckExist(OrderNumOne);
            var validCheckPrice = STO.CheckPrice(OrderNumOne);
            var validCheckParts = STO.CheckParts(OrderNumOne);

            // Assert
            Assert.AreEqual(true, validCheckExist);
            Assert.AreEqual(true, validCheckPrice);
            Assert.AreEqual(true, validCheckParts);
        }

        */
    }
}