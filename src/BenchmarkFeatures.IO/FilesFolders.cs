using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkFeatures.IO
{

    [SimpleJob(RuntimeMoniker.Net60, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class FilesFolders
    {

        #region Benchmarks: Path.GetTempFileName()
        private readonly List<string> _files = new();

        // NOTE: The performance of this benchmark is highly influenced by what's currently in your temp directory.
        [Benchmark]
        public void GetTempFileName()
        {
            for (int i = 0; i < 1000; i++) _files.Add(Path.GetTempFileName());
        }

        [IterationCleanup]
        public void FileDeleteCleanup()
        {
            foreach (string path in _files) File.Delete(path);
            _files.Clear();
        }
        #endregion
    }
}
