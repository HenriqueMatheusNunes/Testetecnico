using GestaoFornecedores.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoFornecedores.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Coleção de fornecedores no banco
        public DbSet<Fornecedor> Fornecedores { get; set; }
    }
}
