namespace Web.Application.Admins.Queries.DeleteUser
{
    public class DeleteUserbyEmailQueryValidator : AbstractValidator<DeleteUserbyEmailQuery>
    {
        public DeleteUserbyEmailQueryValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty()
                .NotNull();




        }

    }
}
