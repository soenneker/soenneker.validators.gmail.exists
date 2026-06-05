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
    /// Executes the email exists operation.
    /// </summary>
    /// <param name="email">The email address.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<bool?> EmailExists(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the email exists without limit operation.
    /// </summary>
    /// <param name="email">The email address.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<bool?> EmailExistsWithoutLimit(string email, CancellationToken cancellationToken = default);
}