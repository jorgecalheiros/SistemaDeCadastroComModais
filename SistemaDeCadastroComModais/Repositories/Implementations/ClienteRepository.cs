using SistemaDeCadastroComModais.Data;
using SistemaDeCadastroComModais.Models;
using SistemaDeCadastroComModais.Repositories.Contracts;

namespace SistemaDeCadastroComModais.Repositories.Implementations
{
    public class ClienteRepository : IClienteRepository
    {

        private readonly DatabaseContext _databaseContext;

        public ClienteRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ClienteModel Add(ClienteModel entity)
        {
            _databaseContext.Clientes.Add(entity);
            _databaseContext.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            ClienteModel cliente = Get(id);
            if (cliente != null) { 
                _databaseContext.Clientes.Remove(cliente);
                _databaseContext.SaveChanges();
                return true;
            }
            return false;
        }

        public ClienteModel? Get(int id)
        {
            return _databaseContext.Clientes.FirstOrDefault(c => c.Id == id);
        }

        public List<ClienteModel> GetAll()
        {
            return _databaseContext.Clientes.ToList();
        }

        public ClienteModel Update(ClienteModel entity, int id)
        {
            ClienteModel? cliente = Get(id);
            if(cliente != null)
            {
                cliente.Name = entity.Name;
                cliente.Email = entity.Email;
                cliente.Telefone = entity.Telefone;
                _databaseContext.Clientes.Update(cliente);
                _databaseContext.SaveChanges();
                return cliente;
            }
            throw new Exception("Update error");
        }
    }
}
