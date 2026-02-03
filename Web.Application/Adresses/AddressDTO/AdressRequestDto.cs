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
