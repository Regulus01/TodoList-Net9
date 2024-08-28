using Domain.Entities;
using Infra.Data.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Maps;

public class TaskListMap : BaseEntityMap<TaskList>
{
    public override void Configure(EntityTypeBuilder<TaskList> builder)
    {
        base.Configure(builder);
        
        builder.Property(x => x.Title)
               .HasColumnName("Tal_Title")
               .HasMaxLength(50)
               .IsRequired();

        builder.ToTable("TaskLists", "ToDoList");
    }
}