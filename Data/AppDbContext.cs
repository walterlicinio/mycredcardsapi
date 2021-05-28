using Microsoft.EntityFrameworkCore;
using myCredCardsAPI.Data.Models;

namespace myCredCardsAPI.Data
{
    public class AppDbContext : DbContext
    {
        //Construtor da Aplicação
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //Nomes das Tabelas a serem criadas
        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}
