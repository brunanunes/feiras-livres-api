using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeirasLivres.Application.Services.Interfaces
{
    public interface IDistritoServices
    {
        Task<Core.DTOs.DistritoDTO> FindDistritoById(int id);

        Task<List<Core.DTOs.DistritoDTO>> ListDistritos();
    }
}
