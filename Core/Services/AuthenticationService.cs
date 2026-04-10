using AutoMapper;
using Domain_Layer.Exceptions;
using Domain_Layer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstraction;
using Shared.DTO.IdentityModule;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration, IMapper _mapper) : IAuthenticationService
    {

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
            {
                throw new UserNotFoundException(loginDto.Email);
            }
            var isPassswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (isPassswordValid)
            {
                return new UserDto
                {
                    Email = user.Email!,
                    DisplayName = user.DisplayName,
                    Token = await CreateTokenAsync(user)
                };
            }
            throw new UnAuthorizedException();
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return new UserDto()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = await CreateTokenAsync(user)
                };
            }
            else
            {
                throw new BadRequestException(result.Errors.Select(E => E.Description).ToList());
            }
        }


        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var secretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var crads = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JWTOptions")["Issuer"],
                audience: _configuration.GetSection("JWTOptions")["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: crads
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user is not null;
        }

        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new UserNotFoundException(email);
            return new UserDto()
            {
                Email = email,
                DisplayName = user.DisplayName,
                Token = await CreateTokenAsync(user)
            };
        }
        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            var user = await _userManager.Users.Include(U => U.Address)
                                               .FirstOrDefaultAsync(U => U.Email == email);
            if (user is null)
                throw new UserNotFoundException(email);
            if (user.Address is null)
                throw new AddressNotFoundException(user.UserName!);
            return _mapper.Map<Address, AddressDto>(user.Address);

        }
        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(U => U.Address)
                                               .FirstOrDefaultAsync(U => U.Email == email);
            if (user is null)
                throw new UserNotFoundException(email);
            if (user.Address is not null)
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.Street = addressDto.Street;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
            }
            else
            {
                user.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }
            await _userManager.UpdateAsync(user);
            return _mapper.Map<Address, AddressDto>(user.Address);
        }

    }
}
