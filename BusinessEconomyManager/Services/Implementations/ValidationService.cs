using BusinessEconomyManager.Dtos;
using BusinessEconomyManager.Services.Interfaces;
using System.Net.Mail;

namespace BusinessEconomyManager.Services.Implementations
{
    public class ValidationService : IValidationService
    {
        /// <summary>
        /// Validates if the parameters sent to create a new user are valid.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool ValidateSignUpRequest(SignUpRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Name) || request.Name.Length > 100) return false;
            if (string.IsNullOrEmpty(request.Surname) || request.Surname.Length > 100) return false;
            if (string.IsNullOrEmpty(request.EmailAddress) || request.EmailAddress.Length > 100 || !MailAddress.TryCreate(request.EmailAddress, out _)) return false;
            if (string.IsNullOrEmpty(request.Password) || request.Password.Length < 8 || request.Password.Length > 100) return false;
            return true;
        }
        public bool ValidateCreateUserBusinessPeriodRequest(CreateUserBusinessPeriodRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Name) || request.Name.Length > 100) return false;
            if (request.DateFrom == DateTime.MinValue) return false;
            if (request.DateTo == DateTime.MinValue) return false;
            return true;
        }

        public bool ValidateCreateSupplierRequest(CreateSupplierRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Name) || request.Name.Length > 100) return false;
            return true;
        }
        public bool ValidateUpdateSupplierRequest(UpdateSupplierRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Name) || request.Name.Length > 100) return false;
            return true;
        }

        public bool ValidateCreateServiceSuppliedTypeRequest(CreateServiceSuppliedTypeRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Name) || request.Name.Length > 100) return false;
            return true;
        }

        public bool ValidateCreateSupplierOperationRequest(CreateSupplierOperationRequestDto request)
        {
            if (request.PaymentTime == DateTime.MinValue) return false;
            if (request.PaidAmount < 0.01) return false;
            if (!Enum.IsDefined(request.PaymentType)) return false;
            return true;
        }
    }
}
