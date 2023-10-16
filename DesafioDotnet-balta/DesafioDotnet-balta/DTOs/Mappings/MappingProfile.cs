using AutoMapper;
using Models;

namespace DesafioDotnet_balta.DTOs.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<LocalityModel, LocalityDTO>().ReverseMap();
            CreateMap<UserModel, UserRegisterDTO>().ReverseMap();
            CreateMap<UserModel, UserLoginDTO>().ReverseMap();
        }
    }
}
