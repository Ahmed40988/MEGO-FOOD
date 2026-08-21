using Web.Application.Common.Interfaces;
using Web.Infrastructure.Common.Persistence.Data;

namespace Web.Infrastructure.Addresses.Persistence
{
    public class AdressesRepository(AppDbContext dbContext) : IAdressesRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task AddAddressAsync(UserAddress Entity, CancellationToken cancellationToken)
        {
            await _dbContext.UserAddresses.AddAsync(Entity);
        }

        public async Task<BaseAddress> UserAdressExist(string UserId,double Lat,double Lng,CancellationToken cancellationToken)
        {
            var existing = await _dbContext.UserAddresses.FirstOrDefaultAsync(x =>
                    x.UserId == UserId &&
                    Math.Abs(x.Latitude - Lat) < 0.0001 &&
                    Math.Abs(x.Longitude - Lng) < 0.0001);

            return existing;
        } 
    }

}
