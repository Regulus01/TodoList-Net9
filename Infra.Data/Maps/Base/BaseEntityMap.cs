using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Maps.Base;

public class BaseEntityMap<TDomain> : IEntityTypeConfiguration<TDomain> where TDomain : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TDomain> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
               .HasColumnName("Id")
               .IsRequired()
               .ValueGeneratedOnAdd();
        
        builder.Property(x => x.CreationDate)
               .HasColumnName("CreationDate")
               .IsRequired();

        builder.Property(x => x.UpdateDate)
               .HasColumnName("UpdateDate")
               .IsRequired();
    }
}