using MediatR;
using PaymentService.Application.UseCases.Payments.Contracts;
using PaymentService.Domain.Repositories;
using PaymentService.Domain.Specifications;

namespace PaymentService.Application.UseCases.Payments.Queries;

public record GetPaymentsByAccountIdCommand(
    Guid AccountId,
    int PageNumber,
    int PageSize) : IRequest<Result<List<PaymentResponseDto>>>;

public class GetPaymentsByAccountIdCommandHandler(
    IPaymentRepository _paymentRepository)
    : IRequestHandler<GetPaymentsByAccountIdCommand, Result<List<PaymentResponseDto>>>
{
    public async Task<Result<List<PaymentResponseDto>>> Handle(GetPaymentsByAccountIdCommand request, CancellationToken cancellationToken)
    {
        var specification = new PaymentByAccountSpecification(
            request.AccountId, 
            request.PageNumber, 
            request.PageSize);

        var payments = await _paymentRepository.ListAsync(specification);

        var response = payments.Select(p => new PaymentResponseDto
        {
            AccountId = p.AccountId,
            UserId = p.UserId,
            CourseId = p.CourseId,
            Amount = p.Amount,
            ForMonths = p.ForMonths,
            PaymentDate = p.PaymentDate,
            PaymentMethod = p.PaymentMethod.ToString()
        }).ToList();

        return Result.Success(response);
    }
}