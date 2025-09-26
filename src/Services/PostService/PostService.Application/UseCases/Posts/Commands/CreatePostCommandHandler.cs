using MediatR;
using PostService.Application.Interfaces.Clients;
using PostService.Domain.Entities;
using PostService.Domain.Repositories;

namespace PostService.Application.UseCases.Posts.Commands;

public record CreatePostCommand(
    Guid userId,
    string Title,
    string Body) : IRequest<Result>;

public class CreatePostCommandHandler(
    IPostRepository _postRepository,
    IUnitOfWork _unitOfWork,
    IUserServiceClient _userServiceClient)
    : IRequestHandler<CreatePostCommand, Result>
{
    public async Task<Result> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        if (!await _userServiceClient.VerifyExistUserAsync(request.userId))
        {
            return Result.Failure(new Error(
                code: "User.NotFound",
                message: $"This user with ID={request.userId} is not found"));
        }

        var post = Post.Create(Guid.NewGuid(), request.Title, request.Body, request.userId).Value;

        await _postRepository.InsertAsync(post);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}