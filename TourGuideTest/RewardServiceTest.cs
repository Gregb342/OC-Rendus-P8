using GpsUtil.Location;
using TourGuide.Users;

namespace TourGuideTest;

public class RewardServiceTest : IClassFixture<DependencyFixture>
{
    private readonly DependencyFixture _fixture;

    public RewardServiceTest(DependencyFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void UserGetRewards()
    {
        // Arrange
        _fixture.Initialize(0);
        var user = new User(Guid.NewGuid(), "jon", "000", "jon@tourGuide.com");
        var attraction = _fixture.GpsUtil.GetAttractions().First();
        user.AddToVisitedLocations(new VisitedLocation(user.UserId, attraction, DateTime.Now));

        // Act
        _fixture.TourGuideService.TrackUserLocation(user);
        var userRewards = user.UserRewards;
        _fixture.TourGuideService.Tracker.StopTracking();

        // Assert
        Assert.True(userRewards.Count == 1);
    }

    [Fact]
    public void IsWithinAttractionProximity()
    {
        // Arrange
        var attraction = _fixture.GpsUtil.GetAttractions().First();

        // Act & Assert
        Assert.True(_fixture.RewardsService.IsWithinAttractionProximity(attraction, attraction));
    }

    [Fact(Skip = ("Needs fixed - can throw InvalidOperationException"))]
    public void NearAllAttractions()
    {
        // Arrange
        _fixture.Initialize(1);
        _fixture.RewardsService.SetProximityBuffer(int.MaxValue);

        // Act
        var user = _fixture.TourGuideService.GetAllUsers().First();
        _fixture.RewardsService.CalculateRewards(user);
        var userRewards = _fixture.TourGuideService.GetUserRewards(user);
        _fixture.TourGuideService.Tracker.StopTracking();

        // Assert
        Assert.Equal(_fixture.GpsUtil.GetAttractions().Count, userRewards.Count);
    }

}
