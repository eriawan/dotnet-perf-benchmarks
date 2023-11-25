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
    public class AnonPipeStream
    {
        #region Benchmarks: PipeStream
        private readonly CancellationTokenSource _cts = new();
        private readonly byte[] _buffer = new byte[1];
        private AnonymousPipeServerStream _server;
        private AnonymousPipeClientStream _client;

        [GlobalSetup]
        public void Setup()
        {
            _server = new AnonymousPipeServerStream(PipeDirection.Out);
            _client = new AnonymousPipeClientStream(PipeDirection.In, _server.ClientSafePipeHandle);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _server.Dispose();
            _client.Dispose();
        }

        //[Benchmark(OperationsPerInvoke = 100_000)]
        [Benchmark(OperationsPerInvoke = 1_000)]
        public async Task ReadWriteAsync()
        {
            for (int i = 0; i < 100_000; i++)
            {
                ValueTask<int> read = _client.ReadAsync(_buffer, _cts.Token);
                await _server.WriteAsync(_buffer, _cts.Token);
                await read;
            }
        }
        #endregion
    }
}
