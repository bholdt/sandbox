using System;
using System.IO;
using NUnit.Framework;
using Seek.Core;

namespace Seek;

public class FileReaderTest
{
    [Test]
    public void ConvertsLineToCarTimestamp()
    {
        var line = GenerateStreamFromString("2021-12-01T05:00:00 5\n2021-01-01T01:00:00 10");

        var reader = new CarFileReader();
        var timeStamps = reader.GetTimeStamps(line);
        
        Assert.AreEqual(new[]
        {
            new CarTimeStamp(new DateTime(2021,12,1,5,0,0), 5),
            new CarTimeStamp(new DateTime(2021,1,1,1,0,0), 10),
        }, timeStamps);
    }
    
    [Test]
    public void ThrowsException_WithIncorrectFormattedDate()
    {
        var line = GenerateStreamFromString("202a1-12-01TT05:00:00 5");

        var reader = new CarFileReader();
        Assert.Throws<FormatException>(() =>
        {
            reader.GetTimeStamps(line);
        });
        
    }

    private static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
    
}