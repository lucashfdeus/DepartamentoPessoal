using LH.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;
using System.Xml;

namespace LH.Date.Context
{
    public class AppRhContext : DbContext
    {
        public AppRhContext(DbContextOptions options) : base(options) { }

        public DbSet<RegistroPonto> RegistroPontos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        //Registro dos Mappings
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*Alterar comportamento padrão quando esquecer de mapear as classes e criar com varchar*/
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.Relational().ColumnType = "varchar(100)";

            /*Essa configuração será para registar todos os Mappings*/
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppRhContext).Assembly);

            /*Desabilitar o uso do DELETE ON CASCADE*/
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship
            .DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

    }
}
