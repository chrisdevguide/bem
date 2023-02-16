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
            CreateMap<CreateBusinessExpenseTransactionRequestDto, BusinessExpenseTransaction>();
            CreateMap<CreateSupplierRequestDto, Supplier>();
        }
    }
}
