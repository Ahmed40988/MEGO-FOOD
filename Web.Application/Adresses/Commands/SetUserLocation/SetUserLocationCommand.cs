using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Adresses.AddressDTO;

namespace Web.Application.Adresses.Commands.SetUserLocation
{
    public record SetUserLocationCommand(
        string UserId,
        double Lat,
        double Lng
    ) : IRequest<ErrorOr<SetUserLocationResponse>>;
}