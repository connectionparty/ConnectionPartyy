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
    public class TagMapConfig : IEntityTypeConfiguration<Tags>
    {
        public void Configure(EntityTypeBuilder<Tags> builder)
        {
            builder.Property(c => c.Nome).IsUnicode(false).HasMaxLength(20).IsRequired();
            builder.HasIndex(c => c.Nome).IsUnique();

            builder.HasMany(c => c.Usuarios).WithMany(c => c.Tags);
            builder.HasMany(c => c.Eventos).WithMany(c => c.Tags);
        }
    }
}
