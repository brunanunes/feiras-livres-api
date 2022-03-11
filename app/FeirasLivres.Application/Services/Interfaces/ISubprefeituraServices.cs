using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeirasLivres.Application.Services.Interfaces
{
    public interface ISubprefeituraServices
    {
        Task<Core.DTOs.SubprefeituraDTO> FindSubprefeituraById(int id);

        Task<List<Core.DTOs.SubprefeituraDTO>> ListSubprefeituras();
    }
}
