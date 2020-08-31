using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CarServiceLibrary_Karm.Test
{
    [TestFixture]
    public class TestCarServiceTest
    {
        [OneTimeSetUp]
        public void Setup()
        {




        }

        [Test]
        public void CheckExist_CorrectDataProvided_TrueResultReturned()
        {
            // Arrange
            var VwPassat5Parts = new List<CarPart>()
            {
                new CarPart { Name = "VW ADR 1.8P", Category = "Engine", Type = "Petrol" },
                new CarPart { Name = "18565R15", Category = "Wheels", Type = "Steel" },
                new CarPart { Name = "RedCherry", Category = "Body", Type = "Sedan" },
                new CarPart { Name = "MKPP5", Category = "Transmission", Type = "Mechanical" }
            };


            var VwPassat5 = new Car() { Model = "Vokswagen Passat B5", VIN = "MG245110H901", Parts = VwPassat5Parts };

            var AlexKarm = new Customer() { Name = "Alexey", SurName = "Karmilchyk" };

            var ServiceListTestCarService = new List<Service>()
            {
                new Service() { Description = "Oil change in a petrol engine", Price = 70, ServiceCategory = "Engine", ServiceType = "Petrol" },
                new Service() { Description = "Disc polishing", Price = 10, ServiceCategory = "Wheels", ServiceType = "Steel" },
                new Service() { Description = "Painting the whole car body", Price = 500, ServiceCategory = "Body", ServiceType = "Sedan" },
                new Service() { Description = "Transmission repair", Price = 120, ServiceCategory = "Transmission", ServiceType = "Machine" },
                new Service() { Description = "Transmission repair", Price = 100, ServiceCategory = "Transmission", ServiceType = "Mechanical" }
            };

            var OrderNumOne = new WorkOrder()
            {
                OrderCar = VwPassat5,
                ChosenServiceList = new List<Service>() { ServiceListTestCarService[0], ServiceListTestCarService[1], ServiceListTestCarService[3] },
                OrderCustomer = (ICustomer)AlexKarm
            };

            // Act


            // Assert


            Assert.Pass();
        }
    }
}