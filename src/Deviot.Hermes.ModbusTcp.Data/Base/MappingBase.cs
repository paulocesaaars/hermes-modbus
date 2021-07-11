using Deviot.Hermes.ModbusTcp.Business.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.Data.Base
{
    [ExcludeFromCodeCoverage]
    public abstract class MappingBase
    {
        protected static void ConfigureBase<TEntity>(EntityTypeBuilder<TEntity> builder, string tableName) where TEntity : EntityBase
        {
            builder.ToTable(tableName);

            builder.HasKey(t => t.Id);

            builder.HasIndex(t => new { t.Id });
        }
    }
}
