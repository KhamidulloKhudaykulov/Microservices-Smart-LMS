namespace Integration.Infrastucture.Decorators;

public abstract class BaseDecorator<TService>
    where TService : class
{
    protected readonly TService _inner;

    protected BaseDecorator(TService inner)
    {
        _inner = inner ?? throw new ArgumentNullException(nameof(inner));
    }
}
