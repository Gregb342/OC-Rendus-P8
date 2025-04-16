using GpsUtil.Location;
using TourGuide.Users;
using TripPricer;

namespace TourGuideTest
{
    public class TourGuideServiceTour : IClassFixture<DependencyFixture>
    {
        private readonly DependencyFixture _fixture;

        public TourGuideServiceTour(DependencyFixture fixture)
        {
            _fixture = fixture;
        }

        public void Dispose()
        {
            _fixture.Cleanup();
        }

        [Fact]
        public void GetUserLocation()
        {
            // Arrange
            _fixture.Initialize(0);
            var user = new User(Guid.NewGuid(), "jon", "000", "jon@tourGuide.com");

            // Act
            var visitedLocation = _fixture.TourGuideService.TrackUserLocation(user);
            _fixture.TourGuideService.Tracker.StopTracking();

            // assert
            Assert.Equal(user.UserId, visitedLocation.UserId);
        }

        [Fact]
        public void AddUser()
        {
            // Arrange
            _fixture.Initialize(0);
            var user = new User(Guid.NewGuid(), "jon", "000", "jon@tourGuide.com");
            var user2 = new User(Guid.NewGuid(), "jon2", "000", "jon2@tourGuide.com");

            // Act
            _fixture.TourGuideService.AddUser(user);
            _fixture.TourGuideService.AddUser(user2);

            var retrievedUser = _fixture.TourGuideService.GetUser(user.UserName);
            var retrievedUser2 = _fixture.TourGuideService.GetUser(user2.UserName);

            _fixture.TourGuideService.Tracker.StopTracking();

            // Assert
            Assert.Equal(user, retrievedUser);
            Assert.Equal(user2, retrievedUser2);
        }

        [Fact]
        public void GetAllUsers()
        {
            // Arrange
            _fixture.Initialize(0);
            var user = new User(Guid.NewGuid(), "jon", "000", "jon@tourGuide.com");
            var user2 = new User(Guid.NewGuid(), "jon2", "000", "jon2@tourGuide.com");

            // Act
            _fixture.TourGuideService.AddUser(user);
            _fixture.TourGuideService.AddUser(user2);

            List<User> allUsers = _fixture.TourGuideService.GetAllUsers();

            _fixture.TourGuideService.Tracker.StopTracking();

            // Assert
            Assert.Contains(user, allUsers);
            Assert.Contains(user2, allUsers);
        }

        [Fact]
        public void TrackUser()
        {
            // Arrange
            _fixture.Initialize();
            var user = new User(Guid.NewGuid(), "jon", "000", "jon@tourGuide.com");
            var visitedLocation = _fixture.TourGuideService.TrackUserLocation(user);

            // Act
            _fixture.TourGuideService.Tracker.StopTracking();

            // Assert
            Assert.Equal(user.UserId, visitedLocation.UserId);
        }

        [Fact]
        public void GetNearbyAttractions()
        {
            // Arrange
            _fixture.Initialize(0);
            var user = new User(Guid.NewGuid(), "jon", "000", "jon@tourGuide.com");
            var visitedLocation = _fixture.TourGuideService.TrackUserLocation(user);

            // Act
            List<Attraction> attractions = _fixture.TourGuideService.GetNearByAttractions(visitedLocation);

            _fixture.TourGuideService.Tracker.StopTracking();

            // Assert
            Assert.Equal(5, attractions.Count);
        }

        [Fact]
        public void GetTripDeals()
        {
            // Arrange
            _fixture.Initialize(0);
            var user = new User(Guid.NewGuid(), "jon", "000", "jon@tourGuide.com");
            List<Provider> providers = _fixture.TourGuideService.GetTripDeals(user);

            // Act
            _fixture.TourGuideService.Tracker.StopTracking();

            // Assert
            Assert.Equal(10, providers.Count);
        }
    }
}
