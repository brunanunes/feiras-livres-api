using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeirasLivres.Core.Entities.Configurations
{
    public class SubprefeituraConfiguration : IEntityTypeConfiguration<Subprefeitura>
    {
        public void Configure(EntityTypeBuilder<Subprefeitura> entity)
        {
            entity.HasKey(x => x.CodigoSubprefeitura);
        }
    }
}
