using BE_ToDoListApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Infrastructure.Data
{
    public partial class ToDoListDBContext : DbContext
    {
        public ToDoListDBContext()
        {
        }

        public ToDoListDBContext(DbContextOptions<ToDoListDBContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<ToDoTask> ToDoTasks { get; set; } = null!;
        public virtual DbSet<TaskStatistic> TaskStatistics { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.FirstName).HasMaxLength(150);
                entity.Property(e => e.LastName).HasMaxLength(150);
                entity.Property(e => e.UserName).HasMaxLength(150);
                entity.Property(e => e.Password).HasMaxLength(150);
                entity.Property(e => e.Email).HasMaxLength(150);
                entity.Property(e => e.PhoneNumber).HasMaxLength(12);

            });

            modelBuilder.Entity<ToDoTask>(entity =>
            {
                entity.ToTable("ToDoTask");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.HasOne(d => d.User)
                      .WithMany(p => p.ToDoTasks)
                      .HasForeignKey(d => d.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_ToDoTask_User");
            });

            modelBuilder.Entity<TaskStatistic>(entity =>
            {
                entity.ToTable("TaskStatistic");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CalcDate).HasColumnType("date");
                entity.Property(e => e.Completed).HasColumnType("decimal(6,3)");
                entity.Property(e => e.InProgress).HasColumnType("decimal(6,3)");
                entity.Property(e => e.NotStarted).HasColumnType("decimal(6,3)");

                entity.HasOne(d => d.User)
                      .WithMany(p => p.TaskStatistics)
                      .HasForeignKey(d => d.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_TaskStatistic_User");
            });
        }
    }
}
