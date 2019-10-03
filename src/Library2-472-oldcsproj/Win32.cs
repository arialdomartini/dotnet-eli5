using System.Runtime.InteropServices;

namespace Library2_472_oldcsproj
{
    public class Win32
    {
        [DllImport("kernel32.dll")]
        internal static extern uint GetLastError();

        public static string OnlyWin32()
        {
            var lastError = GetLastError();
            return $"Last error: {lastError.ToString()}";
        }

    }
}