using System.Reflection;
using Seek;
using Seek.Core;


try
{
    var fileProcessor = new CarFileProcessor(new CarFileReader(), new CarCounter());
    
    var fileName = Environment.GetCommandLineArgs()[1];
    using var fileStream = File.Open(fileName, FileMode.Open);

    var result = fileProcessor.Process(fileStream);

    Console.WriteLine(result);
}
catch (Exception e)
{
    Console.WriteLine("Something went wrong");
    Console.WriteLine(e);
}