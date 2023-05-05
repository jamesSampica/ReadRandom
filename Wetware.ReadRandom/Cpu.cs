using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Wetware.ReadRandom;

public static class Cpu
{
    static Cpu()
    {
        var rdSeedOps = new byte[] { 
            0x0f, 0xc7, 0xf8, // rdseed eax
            0x0f, 0x92, 0x01, // setc byte ptr [rcx]
            0xc3              // ret
        };

        var rdRandOps = new byte[] { 
            0x0f, 0xc7, 0xf0, // rdrand eax
            0x0f, 0x92, 0x01, // setc byte ptr [rcx]
            0xc3              // ret
        };

        // Override our internal method with our opcodes
        Marshal.Copy(rdSeedOps, 0, typeof(Cpu).GetMethod(nameof(RdSeedInternal))!.MethodHandle.GetFunctionPointer(), rdSeedOps.Length);
        Marshal.Copy(rdRandOps, 0, typeof(Cpu).GetMethod(nameof(RdRandInternal))!.MethodHandle.GetFunctionPointer(), rdRandOps.Length);
    }

    
    /// <summary>Reads a random seed value from the CPU's deterministic random bit generator (DRBG).</summary>
    public static uint ReadRandomSeed() => ReadRandomInternal(RdSeedInternal);

    /// <summary>Reads a random value from the CPU's enhanced, nondeterministic random number generator (ENRNG).</summary>
    public static uint ReadRandom() => ReadRandomInternal(RdRandInternal);

    delegate uint ReadRandomDelegate(out byte carry);
    static uint ReadRandomInternal(ReadRandomDelegate readRandom)
    {
        uint result;

        /*
            The Carry Flag indicates whether a random value is available at the time the instruction is executed. 
            CF=1 indicates that the data in the destination is valid. Otherwise CF=0 and the data in the destination 
            operand will be returned as zeros for the specified width. All other flags are forced to 0 in either situation. 
            Software must check the state of CF=1 for determining if a valid random seed value has been returned, 
            otherwise it is expected to loop and retry execution of RDSEED.
        */
        byte carry;
        do result = readRandom(out carry); 
        while (carry == 0);

        return result;
    }

    /// <summary>Internal instructions for RdSeed. Not to be called directly. Use <see cref="ReadRandomSeed"/> instead.</summary>
    [MethodImpl(MethodImplOptions.NoInlining)] // Don't inline. This function serves as a pointer to override with cpu opcodes
    public static uint RdSeedInternal(out byte carry)
    {
        // Needed to compile
        carry = byte.MaxValue;
        return 0;
    }

    /// <summary>Internal instructions for RdRand. Not to be called directly. Use <see cref="ReadRandom"/> instead.</summary>
    [MethodImpl(MethodImplOptions.NoInlining)] // Don't inline. This function serves as a pointer to override with cpu opcodes
    public static uint RdRandInternal(out byte carry)
    {
        // Needed to compile
        carry = byte.MaxValue;
        return 0;
    }
}