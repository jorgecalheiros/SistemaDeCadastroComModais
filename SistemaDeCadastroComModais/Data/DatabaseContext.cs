using Microsoft.EntityFrameworkCore;
using SistemaDeCadastroComModais.Models;

namespace SistemaDeCadastroComModais.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {
        }

        public DbSet<ClienteModel> Clientes { get; set; }
    }
}
