using MediatR;
using Newsletter.Application.Shared;
using Newsletter.Core.Repositories;

namespace Newsletter.Application.User;

public record GetAllUsersQuery() : IRequest<IResult<List<UserListModel>>>;
public record UserListModel(Guid Id, string Email, string FirstName, string LastName);

public class GetAllUsers : IRequestHandler<GetAllUsersQuery, IResult<List<UserListModel>>>
{
    private readonly Users _Users;

    public GetAllUsers(Users Users)
    {
        _Users = Users;
    }
    public async Task<IResult<List<UserListModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var Users = await _Users.GetAll();

        var mappedUsers = Users
            .Select(x => new UserListModel(x.Id, x.Email, x.FirstName, x.LastName))
            .ToList();

        return Result.Success(mappedUsers);
    }
}
