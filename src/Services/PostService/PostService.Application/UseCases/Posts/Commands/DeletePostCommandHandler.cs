using MediatR;
using PostService.Domain.Repositories;

namespace PostService.Application.UseCases.Posts.Commands;

public record DeletePostCommand(Guid PostId, Guid UserId) : IRequest<Result>;

public class DeletePostCommandHandler(
    IPostRepository _postRepository,
    IUnitOfWork _unitOfWork)
    : IRequestHandler<DeletePostCommand, Result>
{
    public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.SelectByIdAsync(request.PostId);

        if (post is null)
        {
            return Result.Failure(new Error(
                code: "Post.NotFound",
                message: $"Post with ID={request.PostId} was not found"));
        }

        // Optional: check ownership (only post owner can delete)
        if (post.UserId != request.UserId)
        {
            return Result.Failure(new Error(
                code: "Post.Unauthorized",
                message: "You are not allowed to delete this post"));
        }

        await _postRepository.DeleteAsync(post);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
