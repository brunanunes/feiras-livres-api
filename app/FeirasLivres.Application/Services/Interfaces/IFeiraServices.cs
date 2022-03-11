using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeirasLivres.Application.Services.Interfaces
{
    public interface IFeiraServices
    {
        Task<Core.Entities.Feira> CreateFeira(Core.DTOs.FeiraDTO feira);
        Task<Core.Entities.Feira> UpdateFeira(int id, Core.DTOs.FeiraDTO feira);
        Task DeleteFeira(int id);
        Task<List<Core.Entities.Feira>> FindFeirasByFilters(string distrito = "", string regiao = "", string nome = "", string bairro = "");
        
    }
}
