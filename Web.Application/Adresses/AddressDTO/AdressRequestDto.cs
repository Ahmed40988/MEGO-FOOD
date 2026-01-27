using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Adresses.AddressDTO
{
    public record AddressRequestDto
 (
     string Street,
     string City,
     string? State,
     string? PostalCode,
     string Country
 );

}
