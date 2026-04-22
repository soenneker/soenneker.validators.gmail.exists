using Soenneker.Validators.Gmail.Exists.Abstract;
using Soenneker.Tests.HostedUnit;


namespace Soenneker.Validators.Gmail.Exists.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class GmailExistsValidatorTests : HostedUnitTest
{
    private readonly IGmailExistsValidator _util;

    public GmailExistsValidatorTests(Host host) : base(host)
    {
        _util = Resolve<IGmailExistsValidator>(true);
    }

    [Test]
    public void Default()
    {

    }
}
