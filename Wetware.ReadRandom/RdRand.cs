using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Wetware.ReadRandom;

public static class RdRand
{
    static RdRand()
    {
        var opCode = new byte[] { 0x0f, 0xc7, 0xf0, // rdrand eax
            0x0f, 0x92, 0x01, // setc byte ptr [rcx]
            0xc3 }; // ret

        Marshal.Copy(opCode, 0, typeof(RdRand).GetMethod(nameof(RdRandInternal))!.MethodHandle.GetFunctionPointer(), opCode.Length);
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
        return 32;
    }
}