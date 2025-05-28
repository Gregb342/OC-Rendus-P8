namespace TripPricer.Enums
{
    public static class ProviderNames
    {
        public enum ProviderName
        {
            HolidayTravels = 1,
            EnterprizeVenturesLimited = 2,
            SunnyDays = 3,
            FlyAwayTrips = 4,
            UnitedPartnersVacations = 5,
            DreamTrips = 6,
            LiveFree = 7,
            DancingWavesCruselinesAndPartners = 8,
            AdventureCo = 9,
            CureYourBlues = 10
        }

        public static string GetProviderDisplayName(this ProviderName provider)
        {
            return provider switch
            {
                ProviderName.HolidayTravels => "Holiday Travels",
                ProviderName.EnterprizeVenturesLimited => "Enterprize Ventures Limited",
                ProviderName.SunnyDays => "Sunny Days",
                ProviderName.FlyAwayTrips => "FlyAway Trips",
                ProviderName.UnitedPartnersVacations => "United Partners Vacations",
                ProviderName.DreamTrips => "Dream Trips",
                ProviderName.LiveFree => "Live Free",
                ProviderName.DancingWavesCruselinesAndPartners => "Dancing Waves Cruselines and Partners",
                ProviderName.AdventureCo => "AdventureCo",
                ProviderName.CureYourBlues => "Cure-Your-Blues",
            };
        }
    }
}
