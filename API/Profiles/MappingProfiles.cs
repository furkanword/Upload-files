using API.Dtos;
using AutoMapper;
using Dominio.Entities;

namespace API.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        //aqui va el mapeo de los Dtos a las entidades de la Db
        CreateMap<Rol, AddRoleDto>().ReverseMap();
        CreateMap<UploadResult, UploadRestoreDto>().ReverseMap();
    }

}
