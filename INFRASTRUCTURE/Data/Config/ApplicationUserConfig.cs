using INFRASTRUCTURE.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INFRASTRUCTURE.Data.Config
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany(x => x.Events)
                .WithOne()
                .HasForeignKey(e=>e.UserId)
                .IsRequired(false);

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasColumnType("nvarchar(50)")
                .IsRequired();
        }
    }
}