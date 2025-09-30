using NUnit.Framework;

namespace Test.Tests
{
    [TestFixture]
    public class IRideMatchingServiceTest
    {




        ////private Mock<IUnitOfWork> _mockUnitOfWork;
        //private Mock<IUnitOfWork> _mockUnitOfWork;
        //private Mock<UserManager<User>> _mockUserManager;
        //private Mock<IMemoryCache> _mockCache;
        //private Mock<ILocationService> _mockLocationService;
        //private Mock<ITripCostService> _mockTripCostService;
        //private Mock<ILogger<RideMatchingService>> _mockLogger;
        //private RideMatchingService _rideMatchingService;

        //[SetUp]
        //public void Setup()
        //{
        //    _mockUnitOfWork = new Mock<IUnitOfWork>();
        //    _mockUserManager = new Mock<UserManager<User>>();
        //    _mockCache = new Mock<IMemoryCache>();
        //    _mockLocationService = new Mock<ILocationService>();
        //    _mockTripCostService = new Mock<ITripCostService>();
        //    _mockLogger = new Mock<ILogger<RideMatchingService>>();

        //    _rideMatchingService = new RideMatchingService(
        //        _mockUnitOfWork.Object,
        //        _mockUserManager.Object,
        //        _mockCache.Object,
        //        _mockLocationService.Object,
        //        _mockTripCostService.Object,
        //        _mockLogger.Object
        //    );
        //}




        //[Test]
        //public async Task MatchRidesAsync_RiderWithMatchingDriver_ReturnsRideDto()
        //{
        //    // Arrange
        //    var rider = new User { Id = "rider1", UserName = "rider" };
        //    var driver = new User
        //    {
        //        Id = "driver1",
        //        UserName = "driver",
        //        VehiclesDriven = new List<Vehicle> { new Vehicle { Model = "Car", Capacity = 4 } }
        //    };

        //    _mockUnitOfWork.Setup(x => x.UserRepository.GetByIdAsync("rider1")).ReturnsAsync(rider);
        //    _mockUserManager.Setup(x => x.GetRolesAsync(rider)).ReturnsAsync(new List<string> { "Rider" });
        //    _mockUnitOfWork.Setup(x => x.UserRepository.GetAllAsync()).ReturnsAsync(new List<User> { driver });
        //    _mockUserManager.Setup(x => x.GetRolesAsync(driver)).ReturnsAsync(new List<string> { "Driver" });

        //    var coord = new GeoCoordinate(30, 30);
        //    _mockLocationService.Setup(x => x.GetLatLng(It.IsAny<string>()))
        //             .ReturnsAsync((string address) => (coord.Latitude, coord.Longitude));


        //    _mockCache.Setup(c => c.TryGetValue("Driver_driver1", out It.Ref<object>.IsAny))
        //              .Callback((object key, out object value) =>
        //              {
        //                  value = new DriverLocation
        //                  {
        //                      Start = new GeoCoordinate(30, 30),
        //                      Destination = new GeoCoordinate(31, 31),
        //                      ETA = "15 min",
        //                      Route = "via XYZ"
        //                  };
        //              })
        //              .Returns(true);

        //    _mockLocationService.Setup(x => x.GetDistanceInKm(It.IsAny<string>(), It.IsAny<string>()))
        //                        .ReturnsAsync(10);

        //    _mockTripCostService.Setup(x => x.CalculateTripCost(10, 1, 5, 0))
        //                        .Returns(new TripCostResult
        //                        {
        //                            TotalCost = 100,
        //                            CostPerPassenger = 25,
        //                            DriverEarning = 80,
        //                            PlatformFee = 20
        //                        });

        //    _mockUnitOfWork.Setup(x => x.TripRepository.GetAllWithCareria(It.IsAny<Expression<Func<Trip, bool>>>(), null, default))
        //                   .ReturnsAsync(Enumerable.Empty<Trip>());

        //    // Act
        //    var result = await _rideMatchingService.MatchRidesAsync("rider1", "LocA", "LocB");

        //    // Assert
        //    Assert.That(result, Is.Not.Null);
        //    Assert.That(result.Count, Is.EqualTo(1));
        //    Assert.That(result[0].DriverId, Is.EqualTo("driver1"));
        //}



    }


}

