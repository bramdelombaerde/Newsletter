using MediatR;
using Newsletter.Application.Shared;
using Newsletter.Core.Repositories;

namespace Newsletter.Application.User
{
    public record RemoveSubscriptionCommand(Guid UserId, Guid TitelId) : IRequest<IResult<RemoveSubscriptionResponse>>;
    public record RemoveSubscriptionResponse();
    public class RemoveSubscription : IRequestHandler<RemoveSubscriptionCommand, IResult<RemoveSubscriptionResponse>>
    {
        private readonly Users _users;
        private readonly Titels _titels;

        public RemoveSubscription(Users users, Titels titels)
        {
            _users = users;
            _titels = titels;
        }
        public async Task<IResult<RemoveSubscriptionResponse>> Handle(RemoveSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var user = await _users.GetById(request.UserId);
            var titel = await _titels.GetById(request.TitelId);

            if (user == null) return Result.NotFound<RemoveSubscriptionResponse>($"UserId '{request.UserId}' not found");
            if (titel == null) return Result.NotFound<RemoveSubscriptionResponse>($"TitelId '{request.TitelId}' not found");

            try
            {
                user.RemoveSubscription(request.TitelId);
            }
            catch (Exception ex)
            {
                return Result.BusinessRuleError<RemoveSubscriptionResponse>(ex.Message);
            }

            await _users.SaveChangesAsync();
            return Result.Success(new RemoveSubscriptionResponse());
        }
    }
}
