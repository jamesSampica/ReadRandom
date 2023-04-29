using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Wetware.ReadRandom;

public static class RdSeed
{
    static RdSeed()
    {
        var codeSeed = new byte[] { 0x0f, 0xc7, 0xf8, // rdseed eax
            //0x67, 0x0f, 0x92, 0x01, // setc byte ptr [ecx]
            0x0f, 0x92, 0x01, // setc byte ptr [rcx]
            0xc3 }; // ret

        Marshal.Copy(codeSeed, 0, typeof(RdSeed).GetMethod(nameof(RdSeedInternal))!.MethodHandle.GetFunctionPointer(), codeSeed.Length);
    }

    public static uint ReadRandom()
    {
        uint res;
        byte status;
        do { res = RdSeedInternal(out status); }
        while (status == 0);
        return res;
    }
 
    [MethodImpl(MethodImplOptions.NoInlining)]
    static uint RdSeedInternal(out byte status)
    {
        status = byte.MaxValue;
        return 32;
    }
}