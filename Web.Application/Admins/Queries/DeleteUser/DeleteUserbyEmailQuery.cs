namespace Web.Application.Admins.Queries.DeleteUser
{
    public record DeleteUserbyEmailQuery(string Adminid, string Email) : IRequest<ErrorOr<Deleted>>;
}
