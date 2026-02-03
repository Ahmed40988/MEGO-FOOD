namespace Web.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitChangesAsync();

    }
}
