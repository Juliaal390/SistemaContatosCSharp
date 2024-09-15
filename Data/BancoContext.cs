using CrudMVC.Data.Map;
using CrudMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
namespace CrudMVC.Data
{
    public class BancoContext : DbContext
    {
        //Construtor
        public BancoContext(DbContextOptions<BancoContext> options) : base(options){
            
        }

        //Criar tabelas
        public DbSet<ContatoModel> Contatos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
