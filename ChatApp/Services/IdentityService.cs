using ChatApp.Data;
using ChatApp.Domain;
using ChatApp.Dto;
using ChatApp.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly DataContext _data;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly JwtSettings _jwtSettings;
        private readonly IUserService _userService;

        public IdentityService(IUserService userService,DataContext data, UserManager<IdentityUser> userManager, TokenValidationParameters tokenValidationParameters, JwtSettings jwtSettings)
        {
            _data = data;
            _userManager = userManager;
            _tokenValidationParameters = tokenValidationParameters;
            _jwtSettings = jwtSettings;
            _userService = userService;
        }
        public async Task<AuthenticationResult> RegisterAsync(string password, string username, string email)
        {
            var existedUser = await _userManager.FindByEmailAsync(email);

            if (existedUser is not null)
            {
                return new AuthenticationResult { Errors = new string[] { "User already exist" } };
            }
            existedUser = await _userManager.FindByNameAsync(username);
            if (existedUser is not null)
            {
                return new AuthenticationResult { Errors = new string[] { "User already exist" } };
            }

            var newUserId = Guid.NewGuid();
            var user = new IdentityUser
            {
                Id = newUserId.ToString(),
                Email = email,
                UserName = username
            };

            var result = await _userManager.CreateAsync(user, password);
            await _userService.CreateAsync(username);

            await _data.SaveChangesAsync();
            return await GenereteAuthenticationForUser(user);



        }
        public async Task<AuthenticationResult> LoginAsync( string email, string password)
        {
            
          var user = await  _userManager.FindByEmailAsync(email);
            if(user is null)
            {
                return new AuthenticationResult { Errors = new string[] { "User doesnt exist" } };
            }

       var isCorrectPassword=     await _userManager.CheckPasswordAsync(user, password);
            if(!isCorrectPassword)
            {
                return new AuthenticationResult { Errors = new string[] { "Credentials are not correct " } };
            }

            return await  GenereteAuthenticationForUser(user);


        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string jwtToken,string refreshToken)
        {
            var validatedToken = GetPrincipalFromTokenPrincipal(jwtToken);

            if(validatedToken is null)
            {

                return new AuthenticationResult { Errors = new[] { "Invalid Token" } };
            }

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDateUnix);
            if(expiryDateTimeUtc>DateTime.UtcNow)
            {
                return new AuthenticationResult { Errors = new[] { "This token hasn't expired yet" } };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;


            var storedRefreshToken =  await _data.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);
            if(storedRefreshToken is null)
            {
                return new AuthenticationResult { Errors = new[] { "Refresh token is not correct" } };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryTime)
            {
                return new AuthenticationResult { Errors = new[] { "this token has expired" } };
            }
            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResult { Errors = new[] { "this token is invalidated" } };
            }
            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult { Errors = new[] { "this token dont match the jwt token" } };
            }
            var userId = validatedToken.Claims.Single(x => x.Type == "id").Value;
            storedRefreshToken.Used = true;
            _data.RefreshTokens.Update(storedRefreshToken);

            await _data.SaveChangesAsync();
            var user = await _userManager.FindByIdAsync(userId);
            return await GenereteAuthenticationForUser(user);

        }


        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken)
                    && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture);

        }

        private ClaimsPrincipal GetPrincipalFromTokenPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }
                else
                {
                    return principal;
                }

            }

            catch
            {
                return null;
            }
        }
        private async Task<AuthenticationResult> GenereteAuthenticationForUser(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            Claim[] claims = new[]
            {
                   new  Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new  Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("id",user.Id),
                new Claim("name",user.UserName)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
               claims
                ),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshTokenDto
            {
                Token = Guid.NewGuid().ToString(),
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryTime = DateTime.UtcNow.AddMonths(6),
                User = await _data.Users.FindAsync(user.Id)


            };

         await   _data.RefreshTokens.AddAsync(refreshToken);
            await _data.SaveChangesAsync();
            return new AuthenticationResult
            {
                IsSuccess = true,
                JwtToken = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token

            };



        }

       
    }
}
