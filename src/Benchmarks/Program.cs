using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Pocket.Common.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<StringParts>();
        }

        public class StringParts
        {
            private static readonly StringBuilder _sb = new StringBuilder();
            
            private const string A = "Oscar";
            private const string B = "Fingal";
            private const string C = "O'Flahertie";
            private const string D = "Wills";
            private const string E = "Wilde";

            [Benchmark] public string Two_Concat() => A + B;
            [Benchmark] public string Two_StringParts() => A.With(B);
            
            [Benchmark] public string Three_Concat() => A + B + C;
            [Benchmark] public string Three_StringParts() => A.With(B).With(C);
            
            [Benchmark] public string Four_Concat() => A + B + C + D;
            [Benchmark] public string Four_StringParts() => A.With(B).With(C).With(D);
            
            [Benchmark] public string Five_Concat() => A + B + C + D + E;
            [Benchmark] public string Five_StringParts() => A.With(B).With(C).With(D).With(E);
            [Benchmark]
            public string Five_StringBuilder() => new StringBuilder().Append(A).Append(B).Append(C).Append(D).Append(E).ToString();
            [Benchmark]
            public string Five_StringBuilderCached()
            {
                _sb.Clear();

                return _sb.Append(A).Append(B).Append(C).Append(D).Append(E).ToString();
            }
        }
    }
}