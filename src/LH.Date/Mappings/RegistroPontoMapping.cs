using LH.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LH.Date.Mappings
{
    public class RegistroProntoMapping : IEntityTypeConfiguration<RegistroPonto>
    {
        //FLUENT API
        public void Configure(EntityTypeBuilder<RegistroPonto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.ValorHora)
               .IsRequired();


            builder.ToTable("RegistroPontos");
        }
    }
}
