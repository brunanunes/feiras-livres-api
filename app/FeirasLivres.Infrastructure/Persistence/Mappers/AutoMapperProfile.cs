using AutoMapper;

namespace FeirasLivres.Infrastructure.Persistence.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Core.Entities.Feira, Core.DTOs.FeiraDTO>();
            CreateMap<Core.Entities.Distrito, Core.DTOs.DistritoDTO>();
            CreateMap<Core.Entities.Subprefeitura, Core.DTOs.SubprefeituraDTO>();

        }
    }
}
