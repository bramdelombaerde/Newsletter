using MediatR;
using Newsletter.Application.Shared;
using Newsletter.Core.Repositories;

namespace Newsletter.Application.User
{
    public record CreateSubscriptionCommand(Guid UserId, Guid TitelId) : IRequest<IResult<CreateSubscriptionResponse>>;
    public record CreateSubscriptionResponse(Guid Id);
    public class CreateSubscription : IRequestHandler<CreateSubscriptionCommand, IResult<CreateSubscriptionResponse>>
    {
        private readonly Users _users;
        private readonly Titels _titels;

        public CreateSubscription(Users users, Titels titels)
        {
            _users = users;
            _titels = titels;
        }
        public async Task<IResult<CreateSubscriptionResponse>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var user = await _users.GetById(request.UserId);
            var titel = await _titels.GetById(request.TitelId);

            if (user == null) return Result.NotFound<CreateSubscriptionResponse>($"UserId '{request.UserId}' not found");
            if (titel == null) return Result.NotFound<CreateSubscriptionResponse>($"TitelId '{request.TitelId}' not found");

            var subscription = new Domain.Subscription() { Titel = titel };

            try
            {
                user.AddSubscription(subscription);
            }
            catch (Exception ex)
            {
                return Result.BusinessRuleError<CreateSubscriptionResponse>(ex.Message);
            }

            await _users.SaveChangesAsync();
            return Result.Success(new CreateSubscriptionResponse(subscription.Id));
        }
    }
}
