using Wetware.ReadRandom;
using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class CpuTests
{
    readonly ITestOutputHelper _testOutputHelper;
    public CpuTests(ITestOutputHelper testOutputHelper) => _testOutputHelper = testOutputHelper;

    [Fact]
    public void ReadRandomSeed_ReturnsNumberAbove0()
    {
        var random = Cpu.ReadRandomSeed();

        Assert.True(random > 0);
        
        _testOutputHelper.WriteLine($"RDSEED={random}");
    }

    [Fact]
    public void ReadRandom_ReturnsNumberAbove0()
    {
        var random = Cpu.ReadRandom();

        Assert.True(random > 0);
        
        _testOutputHelper.WriteLine($"RDRAND={random}");
    }
}