namespace Pocket.Benchmarks
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Benchmark.OfAssembly().Execute();
        }

        [Sample(times: 10000)]
        public class TestSample
        {
            [Run]
            public int SumRun() => 2 + 2;

            [Run]
            public int MultiplicationRun() => 2 * 2;
        }
    }
}