using SeattleBikeShareAnalyzer;

public class LoadResult
{
    public List<Trip> SuccessfulTrips { get; } = new();
    public List<LoadError> Errors { get; } = new();

    public void AddError(string errorType, int rowNumber, string message)
    {
        Errors.Add(new LoadError
        {
            ErrorType = errorType,
            RowNumber = rowNumber,
            Message = message
        });
    }
}