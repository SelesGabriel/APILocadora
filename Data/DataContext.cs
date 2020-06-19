using Locadora.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace Locadora.Data
{
    public class DataContext : DbContext
    {
        readonly DbContextOptions options;
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            this.options = options;
        }


        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
    }
}
