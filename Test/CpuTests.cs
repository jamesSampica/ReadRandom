using System;
using System.Diagnostics;
using Wetware.ReadRandom;
using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class CpuTests
{
    readonly ITestOutputHelper _testOutputHelper;
    public CpuTests(ITestOutputHelper testOutputHelper) => _testOutputHelper = testOutputHelper;

    [Fact]
    public void RunRand() => _testOutputHelper.WriteLine($"RDRAND={Cpu.Rand()}");

/*     [Fact]
    public void RunRandV2() => _testOutputHelper.WriteLine($"RDRAND={Cpu.RandV2()}"); */
}