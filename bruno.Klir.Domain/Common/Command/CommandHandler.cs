using bruno.Klir.Domain.Models;

namespace bruno.Klir.Domain.Common.Command
{
    public abstract class CommandHandler
    {
        protected CommandHandler()
        {
        }

        protected void AddError(string mensagem)
        {
            //TODO: Implement ValidationResult
        }

        protected async Task UnitOfWork(IUnitOfWork uow)
        {
            await uow.Commit();
        }
    }
}