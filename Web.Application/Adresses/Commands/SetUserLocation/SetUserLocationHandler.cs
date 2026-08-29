using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Adresses.AddressDTO;
using Web.Domain.Addresses;

namespace Web.Application.Adresses.Commands.SetUserLocation
{
    public class SetUserLocationHandler(ILogger<SetUserLocationHandler> logger,IUnitOfWork unitOfWork,IReverseGeocodingService reverseGeocodingService
        ,IAdressesRepository adressesRepository) : IRequestHandler<SetUserLocationCommand, ErrorOr<SetUserLocationResponse>>
    {
        private readonly ILogger<SetUserLocationHandler> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IReverseGeocodingService _reverseGeocodingService = reverseGeocodingService;
        private readonly IAdressesRepository _adressesRepository = adressesRepository;

        public async Task<ErrorOr<SetUserLocationResponse>> Handle(SetUserLocationCommand request, CancellationToken cancellationToken)
        {
            string? address = null;

            try
            {
                address = await _reverseGeocodingService
                    .GetAddressAsync(request.Lat, request.Lng);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to get address for lat: {Lat}, lng: {Lng}",
                    request.Lat, request.Lng);
            }

            var existingAddress = await _adressesRepository
                .UserAdressExist(request.UserId,
                                 request.Lat,
                                 request.Lng,
                                 cancellationToken);

            if (existingAddress != null)
            {
                if (!string.IsNullOrWhiteSpace(address))
                {
                    existingAddress.UpdateCoordinates(request.Lat, request.Lng, address);
                }

                await _unitOfWork.CommitChangesAsync();

                return new SetUserLocationResponse(
                    existingAddress.Id,
                    existingAddress.Address
                );
            }

            var entity = new UserAddress(
                request.UserId,
                request.Lat,
                request.Lng,
                address
            );

            await _adressesRepository
                .AddAddressAsync(entity, cancellationToken);

            await _unitOfWork.CommitChangesAsync();

            return new SetUserLocationResponse(
                entity.Id,
                entity.Address
                );
        }
    }
}
