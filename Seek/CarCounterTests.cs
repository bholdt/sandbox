using System;
using NUnit.Framework;
using Seek.Core;

namespace Seek;

public class Tests
{
    [Test]
    public void TotalCarsSeen()
    {
        var carCounter = new CarCounter();

        var result = carCounter.Process(new[]
        {
            new CarTimeStamp(DateTime.UtcNow, 1),
            new CarTimeStamp(DateTime.UtcNow, 3)
        });

        Assert.AreEqual(result.TotalNumberOfCars, 4);
    }

    // A sequence of lines where each line contains a date (in yyyy-mm-dd format) and the
    // number of cars seen on that day (eg. 2016-11-23 289) for all days listed in the input file
    [Test]
    public void TotalCarsByDay()
    {
        var carCounter = new CarCounter();

        var result = carCounter.Process(new[] {
                new CarTimeStamp(DateTime.Parse("2010-01-01 09:10"), 1),
                new CarTimeStamp(DateTime.Parse("2010-01-01 10:10"), 1),
                new CarTimeStamp(DateTime.Parse("2010-01-02 08:00"), 3),
        });

        Assert.AreEqual(result.NumberOfCarsByDay[DateTime.Parse("2010-01-01")], 2);
        Assert.AreEqual(result.NumberOfCarsByDay[DateTime.Parse("2010-01-02")], 3);
    }
    
    // The top 3 half hours with most cars, in the same format as the input file
    [Test]
    public void TopThreeCarForHalfHour()
    {
        var carCounter = new CarCounter();

        var result = carCounter.Process(new[]
        {
                new CarTimeStamp(DateTime.Parse("2010-01-01 09:10"), 1),
                new CarTimeStamp(DateTime.Parse("2010-01-01 09:20"), 10),
                new CarTimeStamp(DateTime.Parse("2010-01-01 10:10"), 1),
                new CarTimeStamp(DateTime.Parse("2010-01-01 10:30"), 20),
                new CarTimeStamp(DateTime.Parse("2010-01-01 10:59"), 20),
                new CarTimeStamp(DateTime.Parse("2010-01-03 9:30"), 20),
        });

        Assert.AreEqual(result.TopThreeHalfHours, new[]
        {
            (DateTime.Parse("2010-01-01 10:30"), 40),
            (DateTime.Parse("2010-01-03 09:30"), 20),
            (DateTime.Parse("2010-01-01 09:00"), 11),
        });
    }

    // The 1.5 hour period with least cars (i.e. 3 contiguous half hour records)
    [Test]
    public void HalfHourPeriodWithLeastCars()
    {
        var carCounter = new CarCounter();

        var result = carCounter.Process(new[]
        {
                new CarTimeStamp(DateTime.Parse("2010-01-01 09:00"), 1),
                new CarTimeStamp(DateTime.Parse("2010-01-01 09:15"), 10),
                new CarTimeStamp(DateTime.Parse("2010-01-01 09:30"), 1),
                new CarTimeStamp(DateTime.Parse("2010-01-01 09:45"), 20),
                new CarTimeStamp(DateTime.Parse("2010-01-01 10:00"), 20),
                new CarTimeStamp(DateTime.Parse("2010-01-01 10:15"), 0),
                new CarTimeStamp(DateTime.Parse("2010-01-01 10:30"), 1),
                new CarTimeStamp(DateTime.Parse("2010-01-02 11:00"), 0),
                new CarTimeStamp(DateTime.Parse("2010-01-02 11:15"), 0),
                new CarTimeStamp(DateTime.Parse("2010-01-02 11:30"), 1),
                new CarTimeStamp(DateTime.Parse("2010-01-02 11:45"), 2),
                new CarTimeStamp(DateTime.Parse("2010-01-02 12:00"), 10),
                new CarTimeStamp(DateTime.Parse("2022-01-02 12:30"), 0),
        });

        Assert.AreEqual(result.LeastContiguousOneHalfHourPeriod, (DateTime.Parse("2010-01-02 11:00"), 13));
        
    }
}