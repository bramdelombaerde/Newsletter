using MediatR;
using Newsletter.Application.Shared;
using Newsletter.Core.Repositories;

namespace Newsletter.Application.Newsletter
{
    public record ArchiveNewsletterCommand(Guid NewsletterId) : IRequest<IResult<ArchiveNewsletterResponse>>;
    public record ArchiveNewsletterResponse(Guid Id);

    public class ArchiveNewsletterHandler : IRequestHandler<ArchiveNewsletterCommand, IResult<ArchiveNewsletterResponse>>
    {
        private readonly Newsletters _newsletters;

        public ArchiveNewsletterHandler(
            Newsletters newsletters)
        {
            _newsletters = newsletters;
        }
        public async Task<IResult<ArchiveNewsletterResponse>> Handle(ArchiveNewsletterCommand request, CancellationToken cancellationToken)
        {
            var newsletter = await _newsletters.GetById(request.NewsletterId);
            if (newsletter == null) return Result.NotFound<ArchiveNewsletterResponse>($"NewsletterId '{request.NewsletterId}' not found");

            newsletter.Archive();

            await _newsletters.SaveChangesAsync(cancellationToken);

            return Result.Success(new ArchiveNewsletterResponse(newsletter.Id));
        }
    }
}
