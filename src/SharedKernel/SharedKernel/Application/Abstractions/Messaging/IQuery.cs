using MediatR;

namespace SharedKernel.Application.Abstractions.Messaging;

public interface IQuery<TReponse> : IRequest<Result<TReponse>>
{
}