using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessObject.Mappings
{
    public class UsuarioMapConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(c => c.Nome).IsUnicode(false).HasMaxLength(70).IsRequired();

            builder.Property(c => c.UserName).IsUnicode(false).HasMaxLength(20).IsRequired();
            builder.HasIndex(c => c.UserName).IsUnique();

            builder.Property(c => c.Email).IsUnicode(false).HasMaxLength(100).IsRequired();
            builder.HasIndex(c => c.Email).IsUnique();

            builder.Property(c => c.Telefone).IsUnicode(false).HasMaxLength(15).IsRequired();
            builder.HasIndex(c => c.Telefone).IsUnique();

            builder.Property(c => c.Senha).IsUnicode(false).HasMaxLength(20).IsRequired();

            builder.Property(c => c.DataNascimento).IsRequired();

            builder.Property(c => c.Genero).IsRequired();

            builder.Property(c => c.Bairro).IsUnicode(false).HasMaxLength(40).IsRequired();
            builder.Property(c => c.Rua).IsUnicode(false).HasMaxLength(60).IsRequired();
            builder.Property(c => c.Numero).IsUnicode(false).HasMaxLength(5).IsRequired();
            builder.Property(c => c.Complemento).IsUnicode(false).HasMaxLength(60);

            builder.Property(c => c.DataCadastro).IsRequired();

            builder.HasMany(c => c.EventosCriados).WithOne(c => c.Organizador);
            builder.HasMany(c => c.EventosParticipados).WithMany(c => c.Participantes).UsingEntity<Dictionary<string, object>>("EventoUsuario",
              j => j.HasOne<Evento>().WithMany().HasForeignKey("EventoID").HasConstraintName("FK_EventoUsuario_EventoID").OnDelete(DeleteBehavior.NoAction),
              j => j.HasOne<Usuario>().WithMany().HasForeignKey("UsuarioID").HasConstraintName("FK_EventoUsuario_ParticipanteID").OnDelete(DeleteBehavior.NoAction));
        }
    }
}
