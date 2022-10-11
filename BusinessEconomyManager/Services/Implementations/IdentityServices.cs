using AutoMapper;
using BusinessEconomyManager.Constants;
using BusinessEconomyManager.Dtos;
using BusinessEconomyManager.Models;
using BusinessEconomyManager.Models.Enums;
using BusinessEconomyManager.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessEconomyManager.Services.Implementations
{
    public class IdentityServices : IIdentityServices
    {
        private readonly IRepositoryService _repositoryService;
        private readonly IValidationService _validationService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public IdentityServices(IRepositoryService repositoryService, IConfiguration configuration, IValidationService validationService, IMapper mapper)
        {
            _repositoryService = repositoryService;
            _configuration = configuration;
            _validationService = validationService;
            _mapper = mapper;
        }

        public async Task<User> SignUp(SignUpRequestDto request)
        {
            if (!_validationService.ValidateSignUpRequest(request)) throw new BadHttpRequestException("Request is not valid.");
            User newUser = _mapper.Map<User>(request);
            await _repositoryService.AddUser(newUser);
            return newUser;
        }

        public string SignIn(SignInRequestDto request)
        {
            User user = Authenticate(request);
            if (user == null) throw new Exception("User not found.");
            return GenerateToken(user);
        }

        private User Authenticate(SignInRequestDto request)
        {
            User user = _repositoryService.FindUserByUserLogin(request);
            return user;
        }

        private string GenerateToken(User user)
        {
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = {
                new(IdentityConstants.UserIdClaimType,user.Id.ToString()),
                new(ClaimTypes.Email,user.EmailAddress),
                new(ClaimTypes.GivenName,user.GivenName),
                new(ClaimTypes.Surname,user.Surname),
                new(ClaimTypes.Role, typeof(UserRoleType).GetEnumName(user.Role))
            };

            JwtSecurityToken token = new(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> GetUser(Guid userId)
        {
            return await _repositoryService.GetUser(userId);
        }

    }
}
