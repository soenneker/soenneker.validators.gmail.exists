using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Soenneker.Extensions.String;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.HttpClientCache.Abstract;
using Soenneker.Utils.RateLimiting.Executor;
using Soenneker.Utils.RateLimiting.Factory.Abstract;
using Soenneker.Validators.Gmail.Exists.Abstract;

namespace Soenneker.Validators.Gmail.Exists;

///<inheritdoc cref="IGmailExistsValidator"/>
public class GmailExistsValidator : Validator.Validator, IGmailExistsValidator
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IRateLimitingFactory _rateLimitingFactory;

    private readonly TimeSpan _interval;

    public GmailExistsValidator(IHttpClientCache httpClientCache, ILogger<GmailExistsValidator> logger, IRateLimitingFactory rateLimitingFactory, IConfiguration configuration) : base(logger)
    {
        _httpClientCache = httpClientCache;
        _rateLimitingFactory = rateLimitingFactory;

        const string key = $"{nameof(GmailExistsValidator)}:IntervalMs";

        var intervalMs = configuration.GetValue<int?>(key);

        if (intervalMs != null)
            _interval = TimeSpan.FromMilliseconds(intervalMs.Value);
        else
        {
            Logger.LogDebug("{key} config was not set, defaulting to 3000ms rate limiting interval", key);
            _interval = TimeSpan.FromMilliseconds(3000);
        }
    }

    public async ValueTask<bool?> EmailExists(string email, CancellationToken cancellationToken = default)
    {
        RateLimitingExecutor rateLimiter = await _rateLimitingFactory.Get(nameof(GmailExistsValidator), _interval, cancellationToken).NoSync();

        return await rateLimiter.Execute(ct => EmailExistsWithoutLimit(email, ct), cancellationToken).NoSync();
    }

    public async ValueTask<bool?> EmailExistsWithoutLimit(string email, CancellationToken cancellationToken = default)
    {
        Logger.LogDebug("Checking if Gmail account ({email}) exists...", email);

        var url = $"https://calendar.google.com/calendar/ical/{email}/public/basic.ics";

        HttpClient client = await _httpClientCache.Get(nameof(GmailExistsValidator), cancellationToken: cancellationToken).NoSync();

        try
        {
            HttpResponseMessage response = await client.GetAsync(url, cancellationToken).NoSync();

            if (response.Headers.Contains("x-frame-options"))
            {
                IEnumerable<string> frameOptions = response.Headers.GetValues("x-frame-options");

                foreach (string option in frameOptions)
                {
                    if (option.EqualsIgnoreCase("SAMEORIGIN"))
                    {
                        Logger.LogDebug("Gmail account ({email}) exists", email);
                        return true;
                    }
                }
            }
        }
        catch (HttpRequestException e)
        {
            Logger.LogWarning(e, "An error occurred with {email}. {message}", email, e.Message);
            return null;
        }

        Logger.LogDebug("Gmail account ({email}) does NOT exist", email);
        return false;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _httpClientCache.RemoveSync(nameof(GmailExistsValidator));
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        return _httpClientCache.Remove(nameof(GmailExistsValidator));
    }
}