﻿using GpsUtil.Location;
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
    public async Task UserGetRewards()
    {
        _fixture.Initialize(0);
        User user = new User(Guid.NewGuid(), "jon", "000", "jon@tourGuide.com");
        List<Attraction> attractions = await _fixture.GpsUtil.GetAttractions();
        user.AddToVisitedLocations(new VisitedLocation(user.UserId, attractions.First(), DateTime.Now));
        await _fixture.TourGuideService.TrackUserLocation(user);
        List<UserReward> userRewards = user.UserRewards;
        _fixture.TourGuideService.Tracker.StopTracking();
        Assert.True(userRewards.Count == 1);
    }

    [Fact]
    public async Task IsWithinAttractionProximity()
    {
        List<Attraction> attraction = await _fixture.GpsUtil.GetAttractions();
        Assert.True(_fixture.RewardsService.IsWithinRewardRange(attraction.First(), attraction.First()));
    }

    [Fact]
    public async Task NearAllAttractions()
    {
        _fixture.Initialize(1);
        _fixture.RewardsService.SetProximityBuffer(int.MaxValue);

        User user = _fixture.TourGuideService.GetAllUsers().First();
        await _fixture.RewardsService.CalculateRewards(user);
        List<UserReward> userRewards = _fixture.TourGuideService.GetUserRewards(user);
        _fixture.TourGuideService.Tracker.StopTracking();
        List<Attraction> attractions = await _fixture.GpsUtil.GetAttractions();

        Assert.Equal(attractions.Count, userRewards.Count);
    }

}
