using System.Runtime.InteropServices;

namespace Zolian;

internal static class Win32
{
    [DllImport("Kernel32")]
    public static extern void AllocConsole();
}
