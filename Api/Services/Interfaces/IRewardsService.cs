﻿using GpsUtil.Location;
using TourGuide.Users;

namespace TourGuide.Services.Interfaces
{
    public interface IRewardsService
    {
        Task CalculateRewards(User user);
        double GetDistance(Locations loc1, Locations loc2);
        bool IsWithinRewardRange(Attraction attraction, Locations location);
        void SetDefaultProximityBuffer();
        void SetProximityBuffer(int proximityBuffer);
        int GetRewardPoints(Attraction attraction, User user);
    }
}