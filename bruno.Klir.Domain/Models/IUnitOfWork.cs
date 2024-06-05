using System.Threading.Tasks;

namespace bruno.Klir.Domain.Models
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}