using MediatR;
using Newsletter.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsletter.Application.Titel
{
    public record CreateTitelCommand(string Name, string ShortName) : IRequest<CreatedTitelResponse>;
    public record CreatedTitelResponse(Guid Id, string ShortName);

    public class CreateTitelHandler : IRequestHandler<CreateTitelCommand, CreatedTitelResponse>
    {
        private readonly Titels _titels;

        public CreateTitelHandler(Titels titels)
        {
            _titels = titels;
        }
        public async Task<CreatedTitelResponse> Handle(CreateTitelCommand request, CancellationToken cancellationToken)
        {
            var titel = new Domain.Titel(request.ShortName, request.Name);
            await _titels.Create(titel);
            await _titels.SaveChangesAsync();

            return new CreatedTitelResponse(titel.Id, titel.ShortName);
        }
    }
}
