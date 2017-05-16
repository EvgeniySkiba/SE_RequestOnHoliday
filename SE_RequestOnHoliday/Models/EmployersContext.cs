namespace SE_RequestOnHoliday.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EmployersContext : DbContext
    {
        public EmployersContext()
            : base("name=EmployersContext")
        {
        }

        public virtual DbSet<Employer> Employers { get; set; }
        public virtual DbSet<Rest> Rests { get; set; }
        public virtual DbSet<RestType> RestTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employer>()
                .HasMany(e => e.Rests)
                .WithMany(e => e.Employers)
                .Map(m => m.ToTable("EmployeesRest").MapLeftKey("EmployerID").MapRightKey("RestID"));

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Employers)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);
        }
    }
}
