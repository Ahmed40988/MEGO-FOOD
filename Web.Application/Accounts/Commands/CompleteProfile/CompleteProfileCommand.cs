using Web.Application.Adresses.AddressDTO;

namespace Web.Application.Accounts.Commands.CompleteProfile
{
    public record CompleteProfileCommand(string UserId, string FullName,
        string Phone,
        DateOnly DateOfBirth,
        AddressRequestDto Address,
        IFormFile Image) : IRequest<ErrorOr<Success>>;
}
