using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Quickstart.Account.RegistrationModel, IdentityUser>();
        }
    }
}
