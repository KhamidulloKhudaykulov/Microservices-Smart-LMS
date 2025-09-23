namespace PaymentService.Application.Common.Rules;

public interface IRule<TCommand>
{
    Task<Result> CheckAsync(TCommand command, CancellationToken cancellationToken = default);
    IRule<TCommand> Then(IRule<TCommand> next);
}