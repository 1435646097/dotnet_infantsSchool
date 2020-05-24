using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Model.Entitys
{
    public partial class InfantsSchoolSystemContext : DbContext
    {

        public InfantsSchoolSystemContext(DbContextOptions<InfantsSchoolSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Action> Action { get; set; }
        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<ActivityPicture> ActivityPicture { get; set; }
        public virtual DbSet<CostType> CostType { get; set; }
        public virtual DbSet<Grade> Grade { get; set; }
        public virtual DbSet<GradeCost> GradeCost { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleAction> RoleAction { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>(entity =>
            {
                entity.Property(e => e.Icon).HasMaxLength(50);

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Path).HasMaxLength(50);

                entity.Property(e => e.Pid).HasColumnName("PId");

                entity.Property(e => e.Remark).HasMaxLength(50);
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.EndTime).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Remark).HasMaxLength(50);

                entity.Property(e => e.StartTime).HasColumnType("date");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("FK__Activity__GradeI__45F365D3");
            });

            modelBuilder.Entity<ActivityPicture>(entity =>
            {
                entity.Property(e => e.Path).HasMaxLength(50);

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.ActivityPicture)
                    .HasForeignKey(d => d.ActivityId)
                    .HasConstraintName("FK__ActivityP__Activ__48CFD27E");
            });

            modelBuilder.Entity<CostType>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.TypeName).HasMaxLength(30);
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("date");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Grade)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Grade__AccountId__35BCFE0A");
            });

            modelBuilder.Entity<GradeCost>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("date");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsEpense).HasDefaultValueSql("((0))");

                entity.Property(e => e.Remark).HasMaxLength(50);

                entity.HasOne(d => d.CostType)
                    .WithMany(p => p.GradeCost)
                    .HasForeignKey(d => d.CostTypeId)
                    .HasConstraintName("FK__GradeCost__CostT__3D5E1FD2");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.GradeCost)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("FK__GradeCost__Grade__3E52440B");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Remark).HasMaxLength(20);
            });

            modelBuilder.Entity<RoleAction>(entity =>
            {
                entity.HasOne(d => d.Action)
                    .WithMany(p => p.RoleAction)
                    .HasForeignKey(d => d.ActionId)
                    .HasConstraintName("FK__RoleActio__Actio__32E0915F");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleAction)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__RoleActio__RoleI__31EC6D26");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Phone).HasMaxLength(30);

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("FK__Student__GradeId__4222D4EF");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Account).HasMaxLength(20);

                entity.Property(e => e.Address).HasMaxLength(20);

                entity.Property(e => e.AddressDetail).HasMaxLength(50);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Pwd).HasMaxLength(20);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__AccountRo__Accou__2B3F6F97");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__AccountRol__Role__2C3393D0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
