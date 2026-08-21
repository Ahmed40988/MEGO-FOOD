using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Admins.Commands
{
    public class SeedDataCommandHandler
        : IRequestHandler<SeedDataCommand, ErrorOr<Success>>
    {
        private readonly ISeedRepository _seedRepository;

        public SeedDataCommandHandler(ISeedRepository seedRepository)
        {
            _seedRepository = seedRepository;
        }

        public async Task<ErrorOr<Success>> Handle(
            SeedDataCommand request,
            CancellationToken cancellationToken)
        {
            await _seedRepository.SeedAsync(request.UserId, cancellationToken);

            return Result.Success;
        }
    }
}
