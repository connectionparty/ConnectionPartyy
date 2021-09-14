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
            //builder.HasMany(c => c.Resposta).WithOne(c => c.).OnDelete(DeleteBehavior.Cascade);


            builder.Property(c => c.Texto).IsUnicode(false).HasMaxLength(140).IsRequired();

            builder.Property(c => c.Likes).IsRequired();
            builder.Property(c => c.Dislikes).IsRequired();


        }
    }
}
