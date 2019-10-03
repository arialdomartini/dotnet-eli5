using System;


namespace Console2_472
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Started");
            var result = Library2_472_newcsproj.WcfCall.InvokeIt();

            Console.WriteLine($"Called WCF code: result={result}");
        }
    }
}