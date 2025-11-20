using Domain.Repositories;

namespace Domain.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        Task<bool> Commit(CancellationToken cancellationToken);
    }
}
