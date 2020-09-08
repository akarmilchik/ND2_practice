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
        private CarRepairService carService;
        private CarRepairService carServiceDefDiscountVIPCustomers;
        private WorkOrder order;
        private WorkOrder orderIncorrectParts;
        private WorkOrder orderIncorrectChosenOperations;

        [OneTimeSetUp]
        public void Setup()
        {
            var VwPassat5Parts = new List<CarPart>()
            {
                new CarPart { Name = "VW ADR 1.8P", Category = "Engine", Type = "Petrol" },
                new CarPart { Name = "18565R15", Category = "Wheels", Type = "Steel" },
                new CarPart { Name = "Red Cherry", Category = "Body", Type = "Sedan" },
                new CarPart { Name = "MKPP 5", Category = "Transmission", Type = "Mechanical" }
            };

            var VwPassat5PartsIncorrect = new List<CarPart>()
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
            var VwPassat5IncorrectParts = new Car { Model = "Volkswagen Passat B5", VIN = "MG245110H901", Parts = VwPassat5PartsIncorrect };

            var AlexKarm = new Customer { Name = "Alexey", SurName = "Karmilchyk" };
            var customerGOLD = new Customer() { Name = "Alexey", SurName = "Karmilchyk", DiscountStatus = "GOLD" };
            var customerGuest = new Customer() { Name = "Vasya", SurName = "Pupkin", DiscountStatus = "Guest" };

            var VIPcutomers = new List<Customer> { AlexKarm };
            var GOLDCustomers = new List<Customer>() { customerGOLD };

            order = new WorkOrder { OrderCar = VwPassat5, OrderCustomer = AlexKarm, ChosenServiceList = new List<IOperation> { operationsList[0], operationsList[2], operationsList[4] } };
            orderIncorrectParts = new WorkOrder { OrderCar = VwPassat5IncorrectParts, OrderCustomer = AlexKarm, ChosenServiceList = new List<IOperation> { operationsList[0], operationsList[2], operationsList[4] } };
            orderIncorrectChosenOperations = new WorkOrder { OrderCar = VwPassat5, OrderCustomer = AlexKarm, ChosenServiceList = new List<IOperation> { operationsList[0], operationsList[2], operationsList[3] } };
        
            var discount = new StubDiscount();
            var discountDefault = new DefaultDiscount();

            carService = new CarRepairService { Name = "BestCar Service", Operations = operationsList, Discount = discount };
            carServiceDefDiscountVIPCustomers = new CarRepairService { Name = "BestCar Service", Operations = operationsList, Discount = discount, VIPCustomers = VIPcutomers };
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
            // Act
            var price = carService.GetOrderPrice(order);

            // Assert
            Assert.AreEqual(670m, price);
        }

        [Test]
        public void GetOrderPrice_IncorrectListProvided_CorrectTotalReturned_CarParts()
        {
            // Act
            var price = carService.GetOrderPrice(orderIncorrectParts);

            // Assert
            Assert.AreEqual(0m, price);
        }

        [Test]
        public void GetOrderPrice_IncorrectListProvided_CorrectTotalReturned_ChosenOperations()
        {
            // Act
            var price = carService.GetOrderPrice(orderIncorrectChosenOperations);

            // Assert
            Assert.AreEqual(0m, price);
        }

        [Test]
        public void GetOrderPrice_CorrectListProvided_CorrectTotalReturned_WithDefaultDiscount()
        {
            // Act
            var price = carServiceDefDiscountVIPCustomers.GetOrderPrice(order);

            // Assert
            Assert.AreEqual(569.5m, price);
        }

        [Test]
        public void GetOrderPrice_CorrectListProvided_CorrectTotalReturned_Mock()
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

            var customerGOLD = new Customer() { Name = "Alexey", SurName = "Karmilchyk", DiscountStatus = "GOLD"};
            var customerGuest = new Customer() { Name = "Vasya", SurName = "Pupkin", DiscountStatus = "Guest"};

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
            Mock.Setup(p => p.GetDiscount(It.Is<decimal>(d => d > 200), It.IsAny<Customer>(), It.IsAny<List<Customer>>())).Returns(5);
            Mock.Setup(p => p.GetDiscount(It.Is<decimal>(d => d > 200), customerGuest, VIPCustomers)).Returns(5);
            Mock.Setup(p => p.GetDiscount(It.Is<decimal>(d => d > 200), customerGOLD, VIPCustomers)).Returns(15);

            var carService = new CarRepairService { Name = "BestCar Service", Operations = operationsList, Discount = Mock.Object, VIPCustomers = VIPCustomers };

            var workOrder = new WorkOrder { OrderCar = VwPassat5, OrderCustomer = customerGuest, ChosenServiceList = new List<IOperation> { operationsList[1] } };
            var workOrderFive = new WorkOrder { OrderCar = VwPassat5, OrderCustomer = customerGuest, ChosenServiceList = new List<IOperation> { operationsList[0], operationsList[2] } };
            var workOrderFifteen = new WorkOrder { OrderCar = VwPassat5, OrderCustomer = customerGOLD, ChosenServiceList = new List<IOperation> { operationsList[0], operationsList[2], operationsList[4] } };

            // Act
            var zeroDiscount = carService.GetOrderPrice(workOrder);
            var fiveDiscount = carService.GetOrderPrice(workOrderFive);
            var fifteenDiscount = carService.GetOrderPrice(workOrderFifteen);

            // Assert
            Assert.AreEqual(10, zeroDiscount);
            Assert.AreEqual(565, fiveDiscount);
            Assert.AreEqual(655, fifteenDiscount);

            Mock.Verify(d => d.GetDiscount(It.IsAny<decimal>(), It.IsAny<Customer>(), It.IsAny<List<Customer>>()), Times.Exactly(3));
        }

        [Test]
        public void 

    }
}