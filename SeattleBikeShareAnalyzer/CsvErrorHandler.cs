using SeattleBikeShareAnalyzer;
using System.Globalization;

public class CsvErrorHandler
{
    private readonly string _logPath;

    public CsvErrorHandler(string logPath = "data_load_errors.log")
    {
        _logPath = logPath;
    }

    public LoadResult LoadTrips(string filePath)
    {
        Console.WriteLine($"Starting to load from: {filePath}");
        var result = new LoadResult();

        if (!File.Exists(filePath))
        {
            result.AddError("CRITICAL", 0, $"File not found: {filePath}");
            return result;
        }

        try
        {
            var lines = File.ReadAllLines(filePath);
            var rowNumber = 0;
            var headerParsed = false;

            foreach (var line in lines)
            {
                rowNumber++;

                if (rowNumber % 50000 == 0)
                    Console.WriteLine($"Processing row {rowNumber}...");

                try
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Skip duplicate headers
                    if (line.StartsWith("trip_id"))
                    {
                        if (headerParsed)
                        {
                            result.AddError("SKIPPED", rowNumber, "Duplicate header row");
                        }
                        headerParsed = true;
                        continue;
                    }

                    // Try to parse the line as a trip
                    var trip = ParseTripFromCsvLine(line);
                    if (trip != null)
                    {
                        result.SuccessfulTrips.Add(trip);
                    }
                    else
                    {
                        result.AddError("PARSE", rowNumber, "Could not parse trip data");
                    }
                }
                catch (Exception ex)
                {
                    result.AddError("UNKNOWN", rowNumber, ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            result.AddError("CRITICAL", 0, $"Failed to read file: {ex.Message}");
        }

        LogResults(result);
        return result;
    }

    private Trip ParseTripFromCsvLine(string line)
    {
        try
        {
            var fields = ParseCsvLine(line);

            if (fields.Count < 12)
                return null;

            return new Trip
            {
                TripId = int.Parse(fields[0]),
                StartTime = DateTime.ParseExact(fields[1], "M/d/yyyy H:mm", CultureInfo.InvariantCulture),
                StopTime = DateTime.ParseExact(fields[2], "M/d/yyyy H:mm", CultureInfo.InvariantCulture),
                BikeId = fields[3],
                TripDuration = double.Parse(fields[4]),
                FromStationName = fields[5],
                ToStationName = fields[6],
                FromStationId = fields[7],
                ToStationId = fields[8],
                UserType = fields[9],
                Gender = fields.Count > 10 ? fields[10] : null,
                BirthYear = fields.Count > 11 && int.TryParse(fields[11], out var year) ? year : null
            };
        }
        catch
        {
            return null;
        }
    }

    private List<string> ParseCsvLine(string line)
    {
        var fields = new List<string>();
        var current = "";
        var inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            var c = line[i];

            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(current.Trim('"'));
                current = "";
            }
            else
            {
                current += c;
            }
        }

        fields.Add(current.Trim('"'));
        return fields;
    }

    private void LogResults(LoadResult result)
    {
        var skipped = result.Errors.Count(e => e.ErrorType == "SKIPPED");
        var parseErrors = result.Errors.Count(e => e.ErrorType == "PARSE");
        var otherErrors = result.Errors.Count(e => e.ErrorType == "UNKNOWN");

        var summary = $"\n--- Data Load Report ---\n" +
                      $"Successfully loaded: {result.SuccessfulTrips.Count}\n" +
                      $"Total errors: {result.Errors.Count}\n" +
                      $"  - Skipped rows: {skipped}\n" +
                      $"  - Parse errors: {parseErrors}\n" +
                      $"  - Other errors: {otherErrors}";

        Console.WriteLine(summary);

        try
        {
            using (var writer = new StreamWriter(_logPath, append: true))
            {
                writer.WriteLine($"=== Load Report: {DateTime.Now} ===");
                writer.WriteLine(summary);

                if (result.Errors.Count > 0 && result.Errors.Count <= 20)
                {
                    writer.WriteLine("\nErrors:");
                    foreach (var error in result.Errors)
                        writer.WriteLine($"  Row {error.RowNumber} [{error.ErrorType}]: {error.Message}");
                }

                writer.WriteLine();
            }
        }
        catch { }
    }
}