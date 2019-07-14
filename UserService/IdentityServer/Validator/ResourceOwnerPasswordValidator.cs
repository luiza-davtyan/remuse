using IdentityServer.Models;
using IdentityServer.Services;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Validator
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //get your user model from db (by username)
                UserDTO user = await userRepository.GetUserByUsername(context.UserName);
                if (user != null)
                {
                    if (user.Password == context.Password)
                    {
                        //set the result
                        context.Result = new GrantValidationResult(

                            subject: user.Id.ToString(),
                            authenticationMethod: "custom",
                            claims: GetUserClaims(user));
                        return;
                    }

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }

        public static Claim[] GetUserClaims(UserDTO user)
        {
            return new Claim[]
            {
                new Claim("user_id",  user.Id.ToString() ?? string.Empty),
                new Claim("role", user.Role ?? string.Empty),
                new Claim("username", user.Username ?? string.Empty)
            };
        }

    }
}
