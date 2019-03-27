using System;
using System.Collections.Generic;
using System.Linq;

namespace Pocket.Common.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var set = new HashSet<int> { 1, 2, 3, 4, 5 };
            
            set.ExceptWith(new [] { 1, 2, 3, 6 });
            
            Console.WriteLine(set.ToList().AsString());
        }
    }
}