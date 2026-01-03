using CsvHelper.Configuration.Attributes;
using System.Globalization;

namespace SeattleBikeShareAnalyzer;

public class Trip
{
    [Name("trip_id")]
    public int TripId { get; set; }

    [Name("starttime")]
    public DateTime StartTime { get; set; }

    [Name("stoptime")]
    public DateTime StopTime { get; set; }

    [Name("bikeid")]
    public string BikeId { get; set; }

    [Name("tripduration")]
    public double TripDuration { get; set; }

    [Name("from_station_name")]
    public string FromStationName { get; set; }

    [Name("to_station_name")]
    public string ToStationName { get; set; }

    [Name("from_station_id")]
    public string FromStationId { get; set; }

    [Name("to_station_id")]
    public string ToStationId { get; set; }

    [Name("usertype")]
    public string UserType { get; set; }

    [Name("gender")]
    public string Gender { get; set; }

    [Name("birthyear")]
    public int? BirthYear { get; set; }
}