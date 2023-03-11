using LH.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LH.Date.Mappings
{
    public class DepartamentoMapping : IEntityTypeConfiguration<Departamento>
    {
        //FLUENT API
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.NomeDepartamento)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.MesVigencia)
                .IsRequired();

            builder.Property(p => p.AnoVigencia)
                .IsRequired();

            builder.Property(p => p.TotalPagar)
                .IsRequired();

            builder.Property(p => p.TotalDescontos)
                .IsRequired();

            builder.Property(p => p.TotalExtras)
                 .IsRequired();

            // 1 : N => Departamento : Funcionarios
            builder.HasMany(f => f.Funcionarios)
                .WithOne(p => p.Departamento)
                .HasForeignKey(p => p.DepartamentoId);

            builder.ToTable("Departamentos");


        }
    }
}
