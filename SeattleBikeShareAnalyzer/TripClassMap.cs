using CsvHelper.Configuration;
using SeattleBikeShareAnalyzer;

public class TripClassMap : ClassMap<Trip>
{
    public TripClassMap()
    {
        Map(m => m.TripId).Name("trip_id");
        Map(m => m.StartTime).Name("starttime");
        Map(m => m.StopTime).Name("stoptime");
        Map(m => m.BikeId).Name("bikeid");
        Map(m => m.TripDuration).Name("tripduration");
        Map(m => m.FromStationName).Name("from_station_name");
        Map(m => m.ToStationName).Name("to_station_name");
        Map(m => m.FromStationId).Name("from_station_id");
        Map(m => m.ToStationId).Name("to_station_id");
        Map(m => m.UserType).Name("usertype");
        Map(m => m.Gender).Name("gender").Optional();
        Map(m => m.BirthYear).Name("birthyear").Optional();
    }
}