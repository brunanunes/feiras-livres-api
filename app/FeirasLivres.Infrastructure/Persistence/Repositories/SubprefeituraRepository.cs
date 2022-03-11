using FeirasLivres.Core.Interfaces.Repositories;
using FeirasLivres.Infrastructure.Persistence.Repositories.Base;

namespace FeirasLivres.Infrastructure.Persistence.Repositories
{
    public class SubprefeituraRepository : Repository<Core.Entities.Subprefeitura>, ISubprefeituraRepository
    {
        public SubprefeituraRepository(FeiraDBContext dbContext) : base(dbContext)
        {

        }
    }
}
