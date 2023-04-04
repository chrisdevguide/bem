using AutoMapper;
using BusinessEconomyManager.DTOs;
using BusinessEconomyManager.Models;
using BusinessEconomyManager.Models.Exceptions;

namespace BusinessEconomyManager.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApiException, ApiExceptionDto>();
            CreateMap<AppUser, AppUserDto>();
            CreateMap<CreateBusinessRequestDto, Business>();
            CreateMap<CreateBusinessPeriodRequestDto, BusinessPeriod>();
            CreateMap<CreateBusinessSaleTransactionRequestDto, BusinessSaleTransaction>();
            CreateMap<UpdateBusinessSaleTransactionRequestDto, BusinessSaleTransaction>().ForMember(x => x.Id, opt => opt.MapFrom(x => x.BusinessSaleTransactionId));
            CreateMap<CreateBusinessExpenseTransactionRequestDto, BusinessExpenseTransaction>();
            CreateMap<UpdateBusinessExpenseTransactionRequestDto, BusinessExpenseTransaction>().ForMember(x => x.Id, opt => opt.MapFrom(x => x.BusinessExpenseTransactionId));
            CreateMap<CreateSupplierRequestDto, Supplier>();
            CreateMap<UpdateSupplierRequestDto, Supplier>();
            CreateMap<CreateSupplierCategoryRequestDto, SupplierCategory>();
        }
    }
}
