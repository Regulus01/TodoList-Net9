using Domain.Entities;
using Infra.Data.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Maps;

public class TaskItemMap : BaseEntityMap<TaskItem>
{
    public override void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Title)
               .HasMaxLength(80)
               .HasColumnName("Tai_Title")
               .IsRequired();
        
        builder.Property(x => x.Description)
               .HasColumnName("Tai_Description")
               .HasMaxLength(150);
        
        builder.Property(x => x.DueDate)
               .HasColumnName("Tai_DueDate");

        builder.Property(x => x.IsCompleted)
               .HasColumnName("Tai_IsCompleted")
               .IsRequired();
        
        builder.Property(x => x.TaskListId)
               .HasColumnName("Tai_TaskListId")
               .IsRequired();

        builder.HasOne(x => x.TaskList)
               .WithMany(x => x.TaskItems)
               .HasForeignKey(x => x.TaskListId);
        
        builder.ToTable("TaskItem", "ToDoList");
    }
}