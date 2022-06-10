using System.Text;

namespace Seek.Core;

public class CarFileProcessor
{
    private readonly CarFileReader _carFileReader;
    private readonly CarCounter _carCounter;

    public CarFileProcessor(CarFileReader carFileReader, CarCounter carCounter)
    {
        _carFileReader = carFileReader;
        _carCounter = carCounter;
    }

    public string Process(Stream stream)
    {
        var carTimeStamps = _carFileReader.GetTimeStamps(stream);
        var result = _carCounter.Process(carTimeStamps);
        var output = $@"Total amount of cars: {result.TotalNumberOfCars}

Cars By Day:
{ToResultString(result.NumberOfCarsByDay)}

Top three by half hour:
{ToResultString(result.TopThreeHalfHours)}

1.5 hour with least amount of cars
{ToResultString(result.LeastContiguousOneHalfHourPeriod)}
";
        
        return output;
    }

    private string ToResultString((DateTime, int)? result)
    {
        if (!result.HasValue) return "None found";
        return $"{result.Value.Item1:yyyy-MM-ddTHH:mm:ss} {result.Value.Item2}";
    }

    private string ToResultString(Dictionary<DateTime, int> results)
    {
        var builder = new StringBuilder();
        foreach (var result in results.OrderBy(x => x.Key))
        {
            builder.Append($"{Environment.NewLine}{result.Key:yyyy-MM-dd} {result.Value}");
        }

        return builder.ToString();
    }

    private string ToResultString(IEnumerable<(DateTime, int)> results)
    {
        var builder = new StringBuilder();
        foreach (var result in results)
        {
            builder.Append($"{Environment.NewLine}{result.Item1:yyyy-MM-ddTHH:mm:ss} {result.Item2}");
        }

        return builder.ToString();
    }
}