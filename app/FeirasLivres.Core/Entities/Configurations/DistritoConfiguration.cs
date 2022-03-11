using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeirasLivres.Core.Entities.Configurations
{
    public class DistritoConfiguration : IEntityTypeConfiguration<Distrito>
    {
        public void Configure(EntityTypeBuilder<Distrito> entity)
        {
            entity.HasKey(x => x.CodigoDistrito);
        }
    }
}