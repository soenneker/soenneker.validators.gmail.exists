[![](https://img.shields.io/nuget/v/soenneker.validators.gmail.exists.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.validators.gmail.exists/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.validators.gmail.exists/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.validators.gmail.exists/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.validators.gmail.exists.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.validators.gmail.exists/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.validators.gmail.exists/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.validators.gmail.exists/actions/workflows/codeql.yml)

# Soenneker.Validators.Gmail.Exists

Applies a Google Calendar response-header heuristic that may indicate whether a Gmail address has an account.

## Install

```bash
dotnet add package Soenneker.Validators.Gmail.Exists
```

## Registration

```csharp
using Soenneker.Validators.Gmail.Exists.Registrars;
using Microsoft.Extensions.DependencyInjection;

services.AddGmailExistsValidatorAsSingleton();
```

Scoped registration is also available. Both registrations reuse singleton HTTP-client-cache and rate-limiter-factory services. Disposing a scoped validator does not evict or dispose the shared named HTTP client.

## Configure request spacing

```json
{
  "GmailExistsValidator": {
    "IntervalMs": 3000
  }
}
```

The default interval is 3,000 milliseconds. `EmailExists` routes calls through a shared named rate limiter using this interval.

## Check an address

```csharp
using Soenneker.Validators.Gmail.Exists.Abstract;

bool? result = await validator.EmailExists(
    "person@gmail.com",
    cancellationToken);
```

The address is escaped as one URL path component and sent to Google's public Calendar ICS endpoint. The response is considered a positive match only when an `X-Frame-Options` header value equals `SAMEORIGIN`, ignoring case.

Results are:

- `true`: the response contained the expected header value;
- `false`: the request completed but the response did not contain that value;
- `null`: `HttpClient` raised `HttpRequestException`.

Cancellation propagates as `OperationCanceledException`. The method does not validate email syntax or require a Gmail domain before sending the request.

`EmailExistsWithoutLimit` performs the same request without the local rate limiter. It does not bypass Google's limits and should be reserved for callers that already coordinate request pacing.

## Reliability and privacy

This is an undocumented response heuristic, not an account-verification API. Google can change the endpoint or headers at any time, and positive or negative results may be wrong. Do not use it as proof of mailbox ownership, as an authentication factor, or as the sole reason to accept or reject a user. Verification email remains the reliable ownership check.

The queried address is disclosed to Google in the request URL. It is no longer included in this validator's logs, but it may still appear in upstream HTTP infrastructure or Google's logs. Ensure that use is compatible with your privacy requirements and provider terms.
