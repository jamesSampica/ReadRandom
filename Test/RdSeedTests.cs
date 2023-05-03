using Wetware.ReadRandom;
using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class RdSeedTests
{
    //readonly RdSeed _sut = new();
    readonly ITestOutputHelper _testOutputHelper;
    public RdSeedTests(ITestOutputHelper testOutputHelper) => _testOutputHelper = testOutputHelper;

    [Fact]
    public void ReadsANumber()
    {
        var random = RdSeed.ReadRandom();

        Assert.True(random > 0);
        
        _testOutputHelper.WriteLine($"RDSEED={random}");
    } 
}