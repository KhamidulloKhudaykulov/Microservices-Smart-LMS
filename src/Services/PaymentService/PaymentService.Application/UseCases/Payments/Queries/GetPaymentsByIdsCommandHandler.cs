using MediatR;
using PaymentService.Application.UseCases.Payments.Contracts;
using PaymentService.Domain.Repositories;
using PaymentService.Domain.Specifications;

namespace PaymentService.Application.UseCases.Payments.Queries;

public record GetPaymentsByIdsCommand(
    Guid AccountId,
    List<Guid> PaymentIds) : IRequest<Result<List<PaymentResponseDto>>>;

public class GetPaymentsByIdsCommandHandler(
    IPaymentRepository _paymentRepository)
    : IRequestHandler<GetPaymentsByIdsCommand, Result<List<PaymentResponseDto>>>
{
    public async Task<Result<List<PaymentResponseDto>>> Handle(GetPaymentsByIdsCommand request, CancellationToken cancellationToken)
    {
        var specification = new PaymentByIdsSpecification(request.AccountId, request.PaymentIds);
        var payments = await _paymentRepository.ListAsync(specification);
    }
}