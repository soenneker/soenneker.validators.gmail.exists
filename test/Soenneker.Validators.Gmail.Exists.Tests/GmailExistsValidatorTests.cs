using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Validators.Gmail.Exists.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Validators.Gmail.Exists.Tests;

[Collection("Collection")]
public class GmailExistsValidatorTests : FixturedUnitTest
{
    private readonly IGmailExistsValidator _validator;

    public GmailExistsValidatorTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _validator = Resolve<IGmailExistsValidator>(true);
    }
}
