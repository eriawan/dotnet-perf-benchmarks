using BenchmarkDotNet.Running;

namespace BenchmarkFeatures.IO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<FilesFolders>();
            BenchmarkRunner.Run<AnonPipeStream>();
            Console.ReadLine();
        }
    }
}
