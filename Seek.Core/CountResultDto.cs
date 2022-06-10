namespace Seek.Core;

public class CountResultDto
{
    public int TotalNumberOfCars { get; set; }
    public Dictionary<DateTime, int> NumberOfCarsByDay { get; set; } = new();
    public IEnumerable<(DateTime, int)> TopThreeHalfHours { get; set; } = Array.Empty<(DateTime, int)>();
    public (DateTime, int)? LeastContiguousOneHalfHourPeriod { get; set; }
}