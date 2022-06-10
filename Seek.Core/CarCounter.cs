using Seek.Core;

namespace Seek;

public class CarCounter
{
    private int _totalCars;
    private readonly Dictionary<DateTime, int> _dayCount = new();
    private readonly Dictionary<DateTime, int> _halfHourCount = new();
    public CountResultDto Process(IEnumerable<CarTimeStamp> carTimeStamp)
    {
        foreach (var timestamp in carTimeStamp)
        {
            _totalCars += timestamp.NumberOfCars;
            AddToDayCount(timestamp);
            AddToHalfHourCount(timestamp);
        }

        return new CountResultDto()
        {
            TotalNumberOfCars = _totalCars,
            NumberOfCarsByDay = _dayCount,
            TopThreeHalfHours = GetTopThreeHalfHours(),
            LeastContiguousOneHalfHourPeriod = GetLeastContiguousPeriod()
        };
    }

    private IEnumerable<(DateTime, int)> GetTopThreeHalfHours()
    {
        return _halfHourCount
            .OrderByDescending(x => x.Value)
            .Take(3)
            .Select(x => (x.Key, x.Value))
            .ToArray();
    }

    private (DateTime, int) GetLeastContiguousPeriod()
    {
        var least = int.MaxValue;
        var date = DateTime.MaxValue;

        var totals = _halfHourCount.OrderBy(x => x.Key).ToArray();

        for (var i = 0; i < totals.Length - 2; i++)
        {
            if ((totals[i + 2].Key - totals[i].Key).TotalHours <= 1.5)
            {
                var temp = totals[i].Value + totals[i + 1].Value + totals[i + 2].Value;
                if (temp < least)
                {
                    least = temp;
                    date = totals[i].Key;
                }
            }
        }

        return (date, least);
    }

    private void AddToHalfHourCount(CarTimeStamp timestamp)
    {
        var date = RoundToClosestHalfHourDate(timestamp.Date);
        if (_halfHourCount.ContainsKey(date))
        {
            _halfHourCount[date] += timestamp.NumberOfCars;
        }
        else
        {
            _halfHourCount.Add(date, timestamp.NumberOfCars);
        }
    }
    
    private int RoundToClosestHalfHour(int number)
    {
        return (number / 30) *30;
    }
    
    private DateTime RoundToClosestHalfHourDate(DateTime value)
    {
        return new DateTime(value.Year, value.Month, value.Day, value.Hour, RoundToClosestHalfHour(value.Minute), 0);
    }

    private void AddToDayCount(CarTimeStamp timestamp)
    {
        var date = timestamp.Date.Date;
        if (_dayCount.ContainsKey(date))
        {
            _dayCount[date] += timestamp.NumberOfCars;
        }
        else
        {
            _dayCount.Add(date, timestamp.NumberOfCars);
        }
    }
}


