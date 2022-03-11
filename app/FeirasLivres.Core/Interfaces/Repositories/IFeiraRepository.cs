using FeirasLivres.Core.Interfaces.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeirasLivres.Core.Interfaces.Repositories
{
    public interface IFeiraRepository : IRepository<Entities.Feira>
    {
        public Task<List<Entities.Feira>> ListFeirasAsync();
    }
}