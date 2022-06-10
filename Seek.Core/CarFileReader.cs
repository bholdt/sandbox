namespace Seek.Core;

public class CarFileReader
{
    public IEnumerable<CarTimeStamp> GetTimeStamps(Stream stream)
    {
        using StreamReader reader = new StreamReader(stream);
        var result = reader.ReadToEnd();
        var lines = result.Split("\n");
        return lines.Select(MapToTimeStamp).ToList();
    }

    private CarTimeStamp MapToTimeStamp(string line)
    {
        var parts = line.Split(" ");
        var date = DateTime.Parse(parts[0]);
        var numberOfCars = Int32.Parse(parts[1]);

        return new CarTimeStamp(date, numberOfCars);
    }
}