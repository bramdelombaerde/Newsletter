using MediatR;
using Newsletter.Application.Shared;
using Newsletter.Core.Repositories;

namespace Newsletter.Application.Titel;

public record GetAllTitelsQuery() : IRequest<IResult<List<TitelListModel>>>;
public record TitelListModel(Guid Id, string ShortName, string Name);

public class GetAllTitels : IRequestHandler<GetAllTitelsQuery, IResult<List<TitelListModel>>>
{
    private readonly Titels _titels;

    public GetAllTitels(Titels titels)
    {
        _titels = titels;
    }
    public async Task<IResult<List<TitelListModel>>> Handle(GetAllTitelsQuery request, CancellationToken cancellationToken)
    {
        var titels = await _titels.GetAll();

        var mappedTitels = titels
            .Select(x => new TitelListModel(x.Id, x.ShortName, x.Name))
            .ToList();

        return Result.Success(mappedTitels);
    }
}
