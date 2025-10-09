namespace GradeModule.Application.Common.Rules;

public abstract class RuleBase<TCommand> : IRule<TCommand>
{
    private IRule<TCommand>? _next;
    public IRule<TCommand> Then(IRule<TCommand> next)
        => _next = next;
    public async Task<Result> CheckAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        var result = await HandleAsync(command, cancellationToken);
        if (!result.IsSuccess)
            return result;

        if (_next is not null)
            return await _next.CheckAsync(command, cancellationToken);

        return Result.Success();
    }

    protected abstract Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken);
}
