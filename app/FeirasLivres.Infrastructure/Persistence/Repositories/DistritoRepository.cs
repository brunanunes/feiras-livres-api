using FeirasLivres.Core.Interfaces.Repositories;
using FeirasLivres.Infrastructure.Persistence.Repositories.Base;

namespace FeirasLivres.Infrastructure.Persistence.Repositories
{
    public class DistritoRepository : Repository<Core.Entities.Distrito>, IDistritoRepository
    {
        public DistritoRepository(FeiraDBContext dbContext) : base(dbContext)
        {

        }
    }
}
