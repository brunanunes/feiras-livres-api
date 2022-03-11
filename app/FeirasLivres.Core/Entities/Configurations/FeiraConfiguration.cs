using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeirasLivres.Core.Entities.Configurations
{
    public class FeiraConfiguration : IEntityTypeConfiguration<Feira>
    {
        public void Configure(EntityTypeBuilder<Feira> entity)
        {
            entity.HasKey(x => x.Id);

            entity.HasOne(d => d.Distrito)
                    .WithMany(p => p.Feiras)
                    .HasForeignKey(d => d.CodigoDistrito)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feiras_Distritos_Id");

            entity.HasOne(d => d.Subprefeitura)
                .WithMany(p => p.Feiras)
                .HasForeignKey(d => d.CodigoSubprefeitura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Feira_Subprefeitura_Id");
        }
    }
}