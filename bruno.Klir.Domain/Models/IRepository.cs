using System.Data.Common;

namespace bruno.Klir.Domain.Models
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; set; }
    }
}
