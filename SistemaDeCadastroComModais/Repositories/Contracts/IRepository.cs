using Microsoft.EntityFrameworkCore;

namespace SistemaDeCadastroComModais.Repositories.Contracts
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T? Get(int id);
        T Add(T entity);
        T Update(T entity, int id);
        bool Delete(int id);
    }
}
