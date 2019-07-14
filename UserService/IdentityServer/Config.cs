using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var customProfile = new IdentityResource(
                   name: "custom.profile",
                   displayName: "Custom profile",
                   claimTypes: new[] { "role", "user_id"});

            return new List<IdentityResource>
           {
           new IdentityResources.OpenId(),

               new IdentityResource
               {
                   Name = "username",
                   UserClaims = new List<string> { "username",  }
               },
               new IdentityResource
               {
                   Name="role",
                   UserClaims= new List<string>{"role"}
               },
               new IdentityResource
               {
                   Name = "user_id",
                   UserClaims = new List<string>{"user_id"}
               }
           };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("BookService", "Book API"),
                new ApiResource("UserService", "User API"),
                new ApiResource("ProfileAPI", "Profile API"),
                new ApiResource("TestClient", "Client API")
            };
            
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "BookService", "UserService", "ProfileAPI", "TestClient" }
                }
            };
        }
    }
}