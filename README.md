[![](https://img.shields.io/nuget/v/soenneker.validators.gmail.exists.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.validators.gmail.exists/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.validators.gmail.exists/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.validators.gmail.exists/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.validators.gmail.exists.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.validators.gmail.exists/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.validators.gmail.exists/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.validators.gmail.exists/actions/workflows/codeql.yml)

# Soenneker.Validators.Gmail.Exists

A validation module checking for Gmail account existence.

## Install

```bash
dotnet add package Soenneker.Validators.Gmail.Exists
```

## Quick start

```csharp
using Soenneker.Validators.Gmail.Exists.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddGmailExistsValidatorAsSingleton();
```

Adds `IGmailExistsValidator` as a singleton service.

## What you get

- `IGmailExistsValidator` — A validation module checking for Gmail account existence.
- `GmailExistsValidatorRegistrar` — A validation module checking for Gmail account existence.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IGmailExistsValidator.EmailExists(email, cancellationToken)` | Checks whether the mailbox exists with the target email provider. | true if the mailbox exists; false if it does not; null when the provider cannot determine the result. |
| `IGmailExistsValidator.EmailExistsWithoutLimit(email, cancellationToken)` | Checks whether the mailbox exists without applying the validator rate limit. | true if the mailbox exists; false if it does not; null when the provider cannot determine the result. |
| `GmailExistsValidatorRegistrar.AddGmailExistsValidatorAsSingleton(services)` | Adds `IGmailExistsValidator` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `GmailExistsValidatorRegistrar.AddGmailExistsValidatorAsScoped(services)` | Adds `IGmailExistsValidator` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
- Dispose instances you own when their scope ends so held resources can be released.
