using Soenneker.Validators.Gmail.Exists.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Validators.Gmail.Exists.Tests;

[Collection("Collection")]
public class GmailExistsValidatorTests : FixturedUnitTest
{
    private readonly IGmailExistsValidator _util;

    public GmailExistsValidatorTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IGmailExistsValidator>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
