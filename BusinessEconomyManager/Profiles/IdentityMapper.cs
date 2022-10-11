using AutoMapper;
using BusinessEconomyManager.Dtos;
using BusinessEconomyManager.Models;
using BusinessEconomyManager.Models.Enums;

namespace BusinessEconomyManager.Profiles
{
    public class IdentityMapper : Profile
    {
        public IdentityMapper()
        {
            CreateMap<SignUpRequestDto, User>().ForMember(x => x.GivenName, opt => opt.MapFrom(x => x.Name)).ForMember(x => x.Role, opt => opt.MapFrom(x => UserRoleType.Client)).ReverseMap();
            CreateMap<CreateUserBusinessRequestDto, UserBusiness>().ReverseMap();
            CreateMap<CreateUserBusinessPeriodRequestDto, UserBusinessPeriod>().ReverseMap();
            CreateMap<CreateSupplierRequestDto, Supplier>().ReverseMap();
            CreateMap<UpdateSupplierRequestDto, Supplier>().ReverseMap();
            CreateMap<CreateServiceSuppliedTypeRequestDto, ServiceSuppliedType>().ReverseMap();
            CreateMap<CreateSupplierOperationRequestDto, SupplierOperation>().ReverseMap();
        }
    }
}
