using System.Security.Principal;
using CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INFRASTRUCTURE.Data.Config
{
    public class EventConfig : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Description)
                .HasColumnType("nvarchar(500)")
                .IsRequired();

            builder.Property(x => x.IsApproved)
                .HasDefaultValue(false);

            builder.Property(x => x.Photo)
                .HasColumnType("varbinary(MAX)");

            builder.Property(x => x.UserId)
                .IsRequired();

        }
    }
}