using System.Data.Entity;
using SWAM.Models;

namespace SWAM
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Courier>().ToTable("Couriers");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<ExternalSupplier>().ToTable("ExternalSuppliers");
            #region Person

            #endregion
            #region EmailAddress
            /*
            modelBuilder.Entity<User>()
                .HasMany(e => e.EmailAddresses)
                .WithRequired(u => (User)u.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                 .HasMany(e => e.EmailAddresses)
                 .WithRequired(u => (Customer)u.Person)
                 .WillCascadeOnDelete(false);

            
            modelBuilder.Entity<EmailAddress>()
                .HasRequired(c => (User)c.Person);
                /*
            modelBuilder.Entity<EmailAddress>()
                          .HasRequired(c => (Customer)c.Person)
                          .WithMany(c => c.EmailAddresses); */
            #endregion
            /*
            modelBuilder.Entity<Phone>()
                .HasRequired(c => c.User)
                .WithMany(c => c.Phones)
                .HasForeignKey(u => u.UserId);


            modelBuilder.Entity<Warehouse>()
                .HasRequired(a => a.Address);

            modelBuilder.Entity<Warehouse>()
                               .HasMany(a => a.Accesses)
                               .WithRequired(w => w.Warehouse)
                               .HasForeignKey(w => w.WarehouseId);

            modelBuilder.Entity<Message>()
                  .HasRequired(s => s.Sender)
                  .WithMany()
                  .HasForeignKey(s => s.SenderId)
                  .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                  .HasRequired(r => r.Receiver)
                  .WithMany()
                  .HasForeignKey(r => r.ReceiverId)
                  .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(a => a.Accesess)
                .WithRequired(u => u.User)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<User>()
                     .HasMany(a => a.Accesess)
                     .WithRequired(u => u.Administrator)
                     .HasForeignKey(u => u.AdministratorId);

            modelBuilder.Entity<AccessUsersToWarehouses>()
                        .HasRequired(c => c.User)
                        .WithMany()
                        .HasForeignKey(u => u.UserId)
                        .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<AccessUsersToWarehouses>()
                  .HasRequired(c => c.Administrator)
                  .WithMany()
                  .HasForeignKey(a => a.AdministratorId)
                  .WillCascadeOnDelete(false);

            modelBuilder.Entity<AccessUsersToWarehouses>()
                  .HasRequired(w => w.Warehouse)
                  .WithMany()
                  .HasForeignKey(w => w.WarehouseId)
                  .WillCascadeOnDelete(false);*/

            //modelBuilder.Entity<Customer>()
            //    .HasRequired(c => c.Phone)
            //    .WithRequiredPrincipal(p => p.Customer)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Customer>()
            //   .HasRequired(c => c.Email)
            //   .WithRequiredPrincipal(p => p.Customer)
            //   .WillCascadeOnDelete(false);
        }
    }
}
