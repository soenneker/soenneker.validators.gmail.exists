using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Utils.HttpClientCache.Registrar;
using Soenneker.Utils.RateLimiting.Factory.Registrars;
using Soenneker.Validators.Gmail.Exists.Abstract;

namespace Soenneker.Validators.Gmail.Exists.Registrars;

/// <summary>
/// A validation module checking for Gmail account existence
/// </summary>
public static class GmailExistsValidatorRegistrar
{
    /// <summary>
    /// Adds <see cref="IGmailExistsValidator"/> as a singleton service. <para/>
    /// </summary>
    public static void AddGmailExistsValidatorAsSingleton(this IServiceCollection services)
    {
        services.AddRateLimitingFactoryAsSingleton();
        services.AddHttpClientCache();
        services.TryAddSingleton<IGmailExistsValidator, GmailExistsValidator>();
    }

    /// <summary>
    /// Adds <see cref="IGmailExistsValidator"/> as a scoped service. <para/>
    /// </summary>
    public static void AddGmailExistsValidatorAsScoped(this IServiceCollection services)
    {
        services.AddRateLimitingFactoryAsSingleton();
        services.AddHttpClientCache();
        services.TryAddScoped<IGmailExistsValidator, GmailExistsValidator>();
    }
}
