using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Fortes.DAL;

namespace Fortes.DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fortes.Models.Categoria", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Fortes.Models.Despesa", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("CategoriaId");

                    b.Property<DateTime>("Data");

                    b.Property<string>("Observacao");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Fortes.Models.Receita", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("CategoriaId");

                    b.Property<DateTime>("Data");

                    b.Property<string>("Observacao");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Fortes.Models.Despesa", b =>
                {
                    b.HasOne("Fortes.Models.Categoria")
                        .WithMany()
                        .ForeignKey("CategoriaId");
                });

            modelBuilder.Entity("Fortes.Models.Receita", b =>
                {
                    b.HasOne("Fortes.Models.Categoria")
                        .WithMany()
                        .ForeignKey("CategoriaId");
                });
        }
    }
}
