using BusinessEconomyManager.Dtos;

namespace BusinessEconomyManager.Services.Interfaces
{
    public interface IValidationService
    {
        public bool ValidateSignUpRequest(SignUpRequestDto request);
        public bool ValidateCreateUserBusinessPeriodRequest(CreateUserBusinessPeriodRequestDto request);
        public bool ValidateCreateSupplierRequest(CreateSupplierRequestDto request);
        public bool ValidateCreateServiceSuppliedTypeRequest(CreateServiceSuppliedTypeRequestDto request);
        public bool ValidateCreateSupplierOperationRequest(CreateSupplierOperationRequestDto request);
        public bool ValidateUpdateSupplierRequest(UpdateSupplierRequestDto request);
    }
}
