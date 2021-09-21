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
    public class EventoMapConfig : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.Property(c => c.Nome).IsUnicode(false).HasMaxLength(70).IsRequired();

            builder.Property(c => c.Descricao).IsUnicode(false).HasMaxLength(300).IsRequired();

            builder.Property(c => c.DataInicio).IsRequired();
            builder.Property(c => c.DataFim).IsRequired();

            builder.Property(c => c.IdadeMinima).IsRequired();

            builder.Property(c => c.Endereco).IsUnicode(false).HasMaxLength(100).IsRequired();

            builder.Property(c => c.Likes).IsRequired();
            builder.Property(c => c.Dislikes).IsRequired();

            builder.Property(c => c.QtdMaximaPessoas).IsRequired();

            builder.HasMany(c => c.Tags).WithMany(c => c.Eventos);

            builder.Property(c => c.EhPublico).IsRequired();

        }
    }
}
