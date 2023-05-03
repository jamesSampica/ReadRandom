using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Wetware.ReadRandom;

public static class RdRand
{
    static RdRand()
    {
        var opCodes = new byte[] { 0x0f, 0xc7, 0xf0, // rdrand eax
            0x0f, 0x92, 0x01, // setc byte ptr [rcx]
            0xc3 }; // ret

        Marshal.Copy(opCodes, 0, typeof(RdRand).GetMethod(nameof(RdRandInternal))!.MethodHandle.GetFunctionPointer(), opCodes.Length);
    }
 
    public static uint ReadRandom()
    {
        uint res;
        byte status;
        do { res = RdRandInternal(out status); }
        while (status == 0);
        return res;
    }
 
    [MethodImpl(MethodImplOptions.NoInlining)]
    static uint RdRandInternal(out byte status)
    {
        status = byte.MaxValue;
        return uint.MaxValue;
    }
}