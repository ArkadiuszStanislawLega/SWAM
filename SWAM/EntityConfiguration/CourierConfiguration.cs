using SWAM.Models.Courier;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class CourierConfiguration : EntityTypeConfiguration<Courier>
    {
        public CourierConfiguration()
        {
            ToTable("Couriers");

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_FirstName", 1) { IsUnique = true }));
            HasMany(c => c.CustomerOrders)
            .WithOptional(c => c.Courier)
            .HasForeignKey(c => c.CourierId);
        }
    }
}
