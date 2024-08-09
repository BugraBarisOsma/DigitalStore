
using DigitalStore.Data.Interfaces;

namespace DigitalStore.Data.UnitOfWork;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;
    Task SaveChangesAsync();
}