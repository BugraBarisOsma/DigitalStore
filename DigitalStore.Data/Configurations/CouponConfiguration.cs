using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(c => c.Amount)
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.ExpiryDate)
            .IsRequired();

        builder.Property(c => c.IsActive)
            .IsRequired();
    }
}