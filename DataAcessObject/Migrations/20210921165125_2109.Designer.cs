﻿// <auto-generated />
using System;
using DataAcessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAcessObject.Migrations
{
    [DbContext(typeof(ConnectionPartyDBContext))]
    [Migration("20210921165125_2109")]
    partial class _2109
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domains.Comentario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ComentarioID")
                        .HasColumnType("int");

                    b.Property<int>("Dislikes")
                        .HasColumnType("int");

                    b.Property<int>("EventoID")
                        .HasColumnType("int");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasMaxLength(140)
                        .IsUnicode(false)
                        .HasColumnType("varchar(140)");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ComentarioID");

                    b.HasIndex("EventoID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("Domains.Evento", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)");

                    b.Property<int>("Dislikes")
                        .HasColumnType("int");

                    b.Property<bool>("EhPublico")
                        .HasColumnType("bit");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("IdadeMinima")
                        .HasColumnType("int");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(70)
                        .IsUnicode(false)
                        .HasColumnType("varchar(70)");

                    b.Property<bool>("PrecisaApresentaDocumento")
                        .HasColumnType("bit");

                    b.Property<int>("QtdMaximaPessoas")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.Property<double?>("Valor")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("Domains.Tags", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("ID");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Domains.Usuario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCadastro")
                        .IsUnicode(false)
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .IsUnicode(false)
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Genero")
                        .IsUnicode(false)
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(70)
                        .IsUnicode(false)
                        .HasColumnType("varchar(70)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Telefone")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("EventoTags", b =>
                {
                    b.Property<int>("EventosID")
                        .HasColumnType("int");

                    b.Property<int>("TagsID")
                        .HasColumnType("int");

                    b.HasKey("EventosID", "TagsID");

                    b.HasIndex("TagsID");

                    b.ToTable("EventoTags");
                });

            modelBuilder.Entity("EventoUsuario", b =>
                {
                    b.Property<int>("EventoID")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.HasKey("EventoID", "UsuarioID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("EventoUsuario");
                });

            modelBuilder.Entity("Domains.Comentario", b =>
                {
                    b.HasOne("Domains.Comentario", null)
                        .WithMany("Resposta")
                        .HasForeignKey("ComentarioID");

                    b.HasOne("Domains.Evento", "Evento")
                        .WithMany("Comentarios")
                        .HasForeignKey("EventoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domains.Usuario", "Usuario")
                        .WithMany("Comentarios")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Evento");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domains.Evento", b =>
                {
                    b.HasOne("Domains.Usuario", "Organizador")
                        .WithMany("EventosCriados")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizador");
                });

            modelBuilder.Entity("EventoTags", b =>
                {
                    b.HasOne("Domains.Evento", null)
                        .WithMany()
                        .HasForeignKey("EventosID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domains.Tags", null)
                        .WithMany()
                        .HasForeignKey("TagsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventoUsuario", b =>
                {
                    b.HasOne("Domains.Evento", null)
                        .WithMany()
                        .HasForeignKey("EventoID")
                        .HasConstraintName("FK_EventoUsuario_EventoID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domains.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuarioID")
                        .HasConstraintName("FK_EventoUsuario_ParticipanteID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Domains.Comentario", b =>
                {
                    b.Navigation("Resposta");
                });

            modelBuilder.Entity("Domains.Evento", b =>
                {
                    b.Navigation("Comentarios");
                });

            modelBuilder.Entity("Domains.Usuario", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("EventosCriados");
                });
#pragma warning restore 612, 618
        }
    }
}
