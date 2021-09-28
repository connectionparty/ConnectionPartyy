using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessObject.Mappings
{
    class ComentarioMapConfig : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Comentario> builder)
        {
            builder.HasOne(c => c.Usuario).WithMany(c => c.Comentarios).OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.Texto).IsUnicode(false).HasMaxLength(240).IsRequired();

            builder.Property(c => c.Likes).IsRequired();
            builder.Property(c => c.Dislikes).IsRequired();

            builder.HasOne(c => c.Evento).WithMany(c => c.Comentarios);
        }
    }
}
