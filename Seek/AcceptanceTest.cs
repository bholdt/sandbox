using System.Reflection;
using NUnit.Framework;
using Seek.Core;

namespace Seek;

public class AcceptanceTest
{
    [Test]
    public void CorrectOutputWhenGivenInputFile()
    {
        var result = ProcessExampleFile();

        Assert.AreEqual(@"Total amount of cars: 398

Cars By Day:

2021-12-01 179
2021-12-05 81
2021-12-08 134
2021-12-09 4

Top three by half hour:

2021-12-01T07:30:00 46
2021-12-01T08:00:00 42
2021-12-08T18:00:00 33

1.5 hour with least amount of cars
2021-12-01T05:00:00 31
", result);
    }

    private string ProcessExampleFile()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "Seek.cartimestamps.txt";
        using var fileStream = assembly.GetManifestResourceStream(resourceName);

        var fileProcessor = new CarFileProcessor(new CarFileReader(), new CarCounter());
        if (fileStream != null) return fileProcessor.Process(fileStream);

        return "Could not find file in test";
    }
}