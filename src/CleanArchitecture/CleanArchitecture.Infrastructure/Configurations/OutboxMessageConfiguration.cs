using CleanArchitecture.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
  public void Configure(EntityTypeBuilder<OutboxMessage> builder)
  {
    builder.ToTable("outbox_messages");
    builder.HasKey(e => e.Id);

    builder.Property(o => o.Content)
      .HasColumnType("jsonb");
  }
}