using FeirasLivres.Core.Interfaces.Repositories;
using FeirasLivres.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeirasLivres.Infrastructure.Persistence.Repositories
{
    public class FeiraRepository : Repository<Core.Entities.Feira>, IFeiraRepository
    {
        public FeiraRepository(FeiraDBContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<List<Core.Entities.Feira>> ListFeirasAsync()
        {
            return await _dbContext.Set<Core.Entities.Feira>().Include(d => d.Distrito).Include(s => s.Subprefeitura).ToListAsync();
        }
    }
}
