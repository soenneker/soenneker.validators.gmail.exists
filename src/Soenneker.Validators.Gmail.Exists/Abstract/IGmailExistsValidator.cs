using Soenneker.Validators.Validator.Abstract;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Soenneker.Validators.Gmail.Exists.Abstract;

/// <summary>
/// A validation module checking for Gmail account existence
/// </summary>
public interface IGmailExistsValidator : IValidator, IDisposable, IAsyncDisposable
{
    ValueTask<bool?> EmailExists(string email, CancellationToken cancellationToken = default);

    ValueTask<bool?> EmailExistsWithoutLimit(string email, CancellationToken cancellationToken = default);
}