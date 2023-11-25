using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCollectionBenchmark
{
    [MemoryDiagnoser]
    public class CollectionMethodsBenchmark
    {
        // Variable to store a simple collection of int
        private readonly List<int> dataIntegers;

        public CollectionMethodsBenchmark()
        {
            dataIntegers = Enumerable.Range(1, 100).ToList();
        }

        #region Where and Single Operations and Single only operations

        [Benchmark]
        public void OpSingleDefault_AfterWhere()
        {
            for (int i = 0; i < 1000; i++)
            {
                int? result = dataIntegers.Where(num => (num % 89) == 0).SingleOrDefault();
            }
        }

        [Benchmark]
        public void OpSingleDefault_NoWhere()
        {
            for (int i = 0; i < 1000; i++)
            {
                int? result = dataIntegers.SingleOrDefault(num => (num % 89) == 0);
            }
        }

        #endregion

        #region Where and First Operations and First only operations

        [Benchmark]
        public void OpFirstDefault_AfterWhere()
        {
            for (int i = 0; i < 1000; i++)
            {
                int? result = dataIntegers.Where(num => (num % 89) == 0).FirstOrDefault();
            }
        }

        [Benchmark]
        public void OpFirstDefault_NoWhere()
        {
            for (int i = 0; i < 1000; i++)
            {
                int? result = dataIntegers.FirstOrDefault(num => (num % 89) == 0);
            }
        }

        #endregion

        #region Using List Find method

        [Benchmark]
        public void ListFind()
        {
            for (int i = 0; i < 1000; i++)
            {
                int? result = dataIntegers.Find(num => (num % 89) == 0);
            }
        }
        #endregion
    }
}
