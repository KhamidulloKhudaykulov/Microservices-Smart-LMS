using MediatR;
using PostService.Domain.Repositories;

namespace PostService.Application.UseCases.Posts.Queries;

public record GetAllPostsQuery : IRequest<Result>;

public class GetAllPostsQueryHandler(
    IPostRepository _postRepository) 
    : IRequestHandler<GetAllPostsQuery, Result>
{
    public async Task<Result> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _postRepository.SelectAllAsync();

        return Result.Success(posts);
    }
}
