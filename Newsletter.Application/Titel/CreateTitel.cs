using MediatR;
using Newsletter.Application.Shared;
using Newsletter.Core.Repositories;

namespace Newsletter.Application.Titel
{
    public record CreateTitelCommand(string Name, string ShortName) : IRequest<IResult<CreateTitelResponse>>;
    public record CreateTitelResponse(Guid Id, string ShortName);

    public class CreateTitelHandler : IRequestHandler<CreateTitelCommand, IResult<CreateTitelResponse>>
    {
        private readonly Titels _titels;

        public CreateTitelHandler(Titels titels)
        {
            _titels = titels;
        }
        public async Task<IResult<CreateTitelResponse>> Handle(CreateTitelCommand request, CancellationToken cancellationToken)
        {
            if (await _titels.DoesTitelAlreadyExist(request.ShortName))
                return Result.BusinessRuleError<CreateTitelResponse>("This title already exists");

            var titel = new Domain.Titel(request.ShortName, request.Name);
            await _titels.Create(titel);
            await _titels.SaveChangesAsync(cancellationToken);

            return Result.Success(new CreateTitelResponse(titel.Id, titel.ShortName));
        }
    }
}
