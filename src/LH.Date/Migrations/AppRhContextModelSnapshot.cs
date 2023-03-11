﻿// <auto-generated />
using System;
using LH.Date.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LH.Date.Migrations
{
    [DbContext(typeof(AppRhContext))]
    partial class AppRhContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LH.Business.Models.Departamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("AnoVigencia");

                    b.Property<string>("MesVigencia")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NomeDepartamento")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<decimal>("TotalDescontos");

                    b.Property<decimal>("TotalExtras");

                    b.Property<decimal>("TotalPagar");

                    b.HasKey("Id");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("LH.Business.Models.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("FornecedorId");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId")
                        .IsUnique();

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("LH.Business.Models.Fornecedor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("TipoFornecedor");

                    b.HasKey("Id");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("LH.Business.Models.Funcionario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Codigo");

                    b.Property<Guid>("DepartamentoId");

                    b.Property<int>("DiasExtras");

                    b.Property<int>("DiasFalta");

                    b.Property<int>("DiasTrabalhados");

                    b.Property<decimal>("HorasDebito");

                    b.Property<decimal>("HorasExtras");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("TotalReceber");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("Funcionario");
                });

            modelBuilder.Entity("LH.Business.Models.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<DateTime>("DataCadastro");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<Guid>("FornecedorId");

                    b.Property<string>("Imagem")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("LH.Business.Models.RegistroPonto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan>("Almoco");

                    b.Property<int>("Codigo");

                    b.Property<DateTime>("Data");

                    b.Property<DateTime>("Entrada");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("Saida");

                    b.Property<decimal>("ValorHora");

                    b.HasKey("Id");

                    b.ToTable("RegistroPontos");
                });

            modelBuilder.Entity("LH.Business.Models.Endereco", b =>
                {
                    b.HasOne("LH.Business.Models.Fornecedor", "Fornecedor")
                        .WithOne("Endereco")
                        .HasForeignKey("LH.Business.Models.Endereco", "FornecedorId");
                });

            modelBuilder.Entity("LH.Business.Models.Funcionario", b =>
                {
                    b.HasOne("LH.Business.Models.Departamento", "Departamento")
                        .WithMany("Funcionarios")
                        .HasForeignKey("DepartamentoId");
                });

            modelBuilder.Entity("LH.Business.Models.Produto", b =>
                {
                    b.HasOne("LH.Business.Models.Fornecedor", "Fornecedor")
                        .WithMany("Produtos")
                        .HasForeignKey("FornecedorId");
                });
#pragma warning restore 612, 618
        }
    }
}
