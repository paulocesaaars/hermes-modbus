using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Data.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.Data.Mapping
{
    [ExcludeFromCodeCoverage]
    public class UserMapping : MappingBase, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureBase<User>(builder, $"User");

            builder.Property(o => o.Name)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(o => o.Enabled)
                .IsRequired();

            builder.Property(o => o.UserName)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(o => o.Password)
                .HasColumnType("varchar(150)")
                .IsRequired();
        }
    }
}
