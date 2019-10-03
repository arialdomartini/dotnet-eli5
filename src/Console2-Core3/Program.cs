using System;

namespace Console2_Core3
{
    class Program
    {
        static void Main(string[] args)
        {
            var lastError1 = Library2_472_oldcsproj.Win32.OnlyWin32();

            // this works at runtime
            var lastError2 = Library2_472_newcsproj.Win32.OnlyWin32();
            Console.WriteLine(lastError1 + lastError2);
        }
    }
}