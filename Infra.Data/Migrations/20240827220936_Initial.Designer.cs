﻿// <auto-generated />
using System;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infra.Data.Migrations
{
    [DbContext(typeof(TaskDbContext))]
    [Migration("20240827220936_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.7.24405.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.TaskItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("CreationDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Tai_Description");

                    b.Property<DateTimeOffset?>("DueDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("Tai_DueDate");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit")
                        .HasColumnName("Tai_IsCompleted");

                    b.Property<Guid>("TaskListId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Tai_TaskListId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("Tai_Title");

                    b.Property<DateTimeOffset>("UpdateDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("TaskListId");

                    b.ToTable("TaskItem", "ToDoList");
                });

            modelBuilder.Entity("Domain.Entities.TaskList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("CreationDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Tal_Title");

                    b.Property<DateTimeOffset>("UpdateDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("TaskLists", "ToDoList");
                });

            modelBuilder.Entity("Domain.Entities.TaskItem", b =>
                {
                    b.HasOne("Domain.Entities.TaskList", "TaskList")
                        .WithMany("TaskItems")
                        .HasForeignKey("TaskListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaskList");
                });

            modelBuilder.Entity("Domain.Entities.TaskList", b =>
                {
                    b.Navigation("TaskItems");
                });
#pragma warning restore 612, 618
        }
    }
}
