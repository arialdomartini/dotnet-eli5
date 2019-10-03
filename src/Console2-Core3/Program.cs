using System;

namespace Console2_Core3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started");
            var result = Library2_472_newcsproj.WcfCall.InvokeIt();

            Console.WriteLine($"Called WCF code: result={result}");
        }
    }
}