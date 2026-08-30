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
    /// <summary>
    /// Applies a Google public-calendar response-header heuristic through the shared rate limiter.
    /// </summary>
    /// <param name="email">Email address to validate or query.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns><see langword="true"/> when the response matches the heuristic, <see langword="false"/> when it does not, or <see langword="null"/> for an HTTP transport failure.</returns>
    ValueTask<bool?> EmailExists(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Applies the Google public-calendar response-header heuristic without using the validator rate limiter.
    /// </summary>
    /// <param name="email">Email address to validate or query.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns><see langword="true"/> when the response matches the heuristic, <see langword="false"/> when it does not, or <see langword="null"/> for an HTTP transport failure.</returns>
    ValueTask<bool?> EmailExistsWithoutLimit(string email, CancellationToken cancellationToken = default);
}
