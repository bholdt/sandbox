namespace Seek.Core;

public class CarTimeStamp : IEquatable<CarTimeStamp>
{
    public DateTime Date { get; }
    public int NumberOfCars { get; }

    public CarTimeStamp(DateTime date, int numberOfCars)
    {
        Date = date;
        NumberOfCars = numberOfCars;
    }

    public bool Equals(CarTimeStamp? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Date.Equals(other.Date) && NumberOfCars == other.NumberOfCars;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Date, NumberOfCars);
    }
}