using TripPricer.Enums;
using TripPricer.Helpers;

namespace TripPricer;

public class TripPricer
{
    public List<Provider> GetPrice(string apiKey, Guid attractionId, int adults, int children, int nightsStay, int rewardsPoints)
    {
        List<Provider> providers = new List<Provider>();

        var rng = new Random();
        var shuffledProviders = Enum
            .GetValues<ProviderNames.ProviderName>()
            .OrderBy(_ => rng.Next())
            .ToList();

        // Sleep to simulate some latency
        Thread.Sleep(ThreadLocalRandom.Current.Next(1, 50));

        foreach (var providerName in shuffledProviders)
        {
            int multiple = ThreadLocalRandom.Current.Next(100, 700);
            double childrenDiscount = children / 3.0;
            double price = multiple * adults + multiple * childrenDiscount * nightsStay + 0.99 - rewardsPoints;

            if (price < 0.0)
            {
                price = 0.0;
            }

            string provider = ProviderNames.GetProviderDisplayName(providerName);

            providers.Add(new Provider(attractionId, provider, price));
        }
        return providers;
    }
}
