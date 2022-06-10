using NUnit.Framework;

namespace Seek;

public class HelperTests
{
    [Test]
    public void RoundToClosestHalfHourTests()
    {
        Assert.AreEqual(RoundToClosestHalfHour(0), 0);
        Assert.AreEqual(RoundToClosestHalfHour(15), 0);
        Assert.AreEqual(RoundToClosestHalfHour(29), 0);
        Assert.AreEqual(RoundToClosestHalfHour(30), 30);
        Assert.AreEqual(RoundToClosestHalfHour(59), 30);
    }


    private int RoundToClosestHalfHour(int number)
    {
        return (number / 30) *30;
    }
}