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
 

        // Override our internal method with our opcodes
        Marshal.Copy(codeSeed, 0, typeof(RdSeed).GetMethod(nameof(RdSeedInternal))!.MethodHandle.GetFunctionPointer(), codeSeed.Length);
    }

    public static uint ReadRandom()
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
        do result = RdSeedInternal(out carry); 
        while (carry == 0);

        return result;
    }
 
    /*
        Don't inline. This function serves as a pointer to override with cpu instructions
    */
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static uint RdSeedInternal(out byte carry)
    {
        // Needed to compile
        carry = byte.MaxValue;
        return 0;
    }
}