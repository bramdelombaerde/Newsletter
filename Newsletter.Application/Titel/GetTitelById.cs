using MediatR;
using Newsletter.Application.Shared;
using Newsletter.Core.Repositories;

namespace Newsletter.Application.Titel;

public record GeTitelByIdQuery(Guid Id) : IRequest<IResult<TitelDetailModel>>;
public record TitelDetailModel(Guid Id, string ShortName, string Name);

public class GetTitelById : IRequestHandler<GeTitelByIdQuery, IResult<TitelDetailModel>>
{
    private readonly Titels _titels;

    public GetTitelById(Titels titels)
    {
        _titels = titels;
    }
    public async Task<IResult<TitelDetailModel>> Handle(GeTitelByIdQuery request, CancellationToken cancellationToken)
    {
        var titel = await _titels.GetById(request.Id);

        var mappedTitel = new TitelDetailModel(titel.Id, titel.ShortName, titel.Name);

        return Result.Success(mappedTitel);
    }
}
