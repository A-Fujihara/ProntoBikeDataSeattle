using CsvHelper;
using System.Globalization;

var filePath = @"D:\ProntoData\trip.csv";

var loader = new CsvErrorHandler();
var result = loader.LoadTrips(filePath);

Console.WriteLine($"\nLoaded {result.SuccessfulTrips.Count} trips");
Console.WriteLine($"Errors: {result.Errors.Count}");

if (result.Errors.Count > 0)
{
    Console.WriteLine("\nAll errors:");
    foreach (var error in result.Errors)
    {
        Console.WriteLine($"  Row {error.RowNumber}: {error.Message}");
    }
}

if (result.SuccessfulTrips.Count > 0)
{
    var firstTrip = result.SuccessfulTrips[0];
    Console.WriteLine($"\nFirst trip: {firstTrip.TripId}, {firstTrip.FromStationName} → {firstTrip.ToStationName}");
    Console.WriteLine($"Start: {firstTrip.StartTime}");
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();