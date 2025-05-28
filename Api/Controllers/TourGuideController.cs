using GpsUtil.Location;
using Microsoft.AspNetCore.Mvc;
using TourGuide.Services;
using TourGuide.Services.Interfaces;
using TourGuide.Users;
using TripPricer;

namespace TourGuide.Controllers;

[ApiController]
[Route("[controller]")]
public class TourGuideController : ControllerBase
{
    private readonly ITourGuideService _tourGuideService;
    private readonly IRewardsService _rewardsService;

    public TourGuideController(ITourGuideService tourGuideService, IRewardsService rewardsService)
    {
        _tourGuideService = tourGuideService;
        _rewardsService = rewardsService;
    }

    [HttpGet("getLocation")]
    public async Task<ActionResult<VisitedLocation>> GetLocation([FromQuery] string userName)
    {
        VisitedLocation location = await _tourGuideService.GetUserLocation(GetUser(userName));
        return Ok(location);
    }

    [HttpGet("getNearbyAttractions")]
    public async Task<ActionResult<List<Attraction>>> GetNearbyAttractions([FromQuery] string userName)
    {
        List<object> list = new();
        try
        {
            User user = GetUser(userName);
            VisitedLocation visitedLocation = await _tourGuideService.GetUserLocation(user);
            List<Attraction> attractions = await _tourGuideService.GetNearByAttractions(visitedLocation);

            foreach (Attraction attraction in attractions)
            {
                list.Add(new
                {
                    Name = attraction.AttractionName,
                    attraction.Latitude,
                    attraction.Longitude,
                    Distance = _rewardsService.GetDistance(attraction, visitedLocation.Location),
                    Reward = _rewardsService.GetRewardPoints(attraction, user)
                });
            }
            return Ok(new
            {
                UserLatitude = visitedLocation.Location.Latitude,
                UserLongitude = visitedLocation.Location.Longitude,
                Attractions = list
            });
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpGet("getRewards")]
    public ActionResult<List<UserReward>> GetRewards([FromQuery] string userName)
    {
        List<UserReward> rewards = _tourGuideService.GetUserRewards(GetUser(userName));
        return Ok(rewards);
    }

    [HttpGet("getTripDeals")]
    public ActionResult<List<Provider>> GetTripDeals([FromQuery] string userName)
    {
        List<Provider> deals = _tourGuideService.GetTripDeals(GetUser(userName));
        return Ok(deals);
    }

    private User GetUser(string userName)
    {
        return _tourGuideService.GetUser(userName);
    }
}
