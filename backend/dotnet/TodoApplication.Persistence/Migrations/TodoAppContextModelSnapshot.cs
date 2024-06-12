﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApplication.Persistence;

#nullable disable

namespace TodoApplication.Persistence.Migrations
{
    [DbContext(typeof(TodoAppContext))]
    partial class TodoAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TodoApplication.Domain.TodoTasks.TodoTask", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("BigInt");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("DateTime");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("DateTime");

                    b.Property<byte>("Status")
                        .HasColumnType("TinyInt");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("TodoTasks", (string)null);
                });

            modelBuilder.Entity("TodoApplication.Domain.TodoTasks.TodoTask", b =>
                {
                    b.OwnsMany("TodoApplication.Domain.TodoTasks.TaskHistory", "TaskHistories", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<DateTime>("DateTime")
                                .HasColumnType("DateTime");

                            b1.Property<long>("TaskId")
                                .HasColumnType("BigInt");

                            b1.Property<byte>("TaskStatus")
                                .HasColumnType("TinyInt");

                            b1.HasKey("Id");

                            b1.HasIndex("TaskId");

                            b1.ToTable("TaskHistories", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TaskId");
                        });

                    b.Navigation("TaskHistories");
                });
#pragma warning restore 612, 618
        }
    }
}