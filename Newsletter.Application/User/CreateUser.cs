using MediatR;
using Newsletter.Application.Shared;
using Newsletter.Core.Repositories;

namespace Newsletter.Application.User
{
    public record CreateUserCommand(string Email, string FirstName, string LastName) : IRequest<IResult<CreateUserResponse>>;
    public record CreateUserResponse(Guid Id, string Email);

    public class CreateUserHandler : IRequestHandler<CreateUserCommand, IResult<CreateUserResponse>>
    {
        private readonly Users _users;

        public CreateUserHandler(Users users)
        {
            _users = users;
        }
        public async Task<IResult<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _users.DoesUserAlreadyExist(request.Email))
                return Result.BusinessRuleError<CreateUserResponse>("This user already exists");

            var user = new Domain.User(request.Email, request.FirstName, request.LastName);
            await _users.Create(user);
            await _users.SaveChangesAsync(cancellationToken);

            return Result.Success(new CreateUserResponse(user.Id, user.Email));

        }
    }
}
