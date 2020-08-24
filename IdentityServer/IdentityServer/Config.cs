using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("myresourceapi", "My Resource API")
                {
                    Scopes = {
                        new Scope("apiscope"),
                        new Scope("DALApi1"),
                        new Scope("otherscope")
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // for public api
                new Client
                {
                    ClientId = "PegaClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("ShareASecretWithIDS4".Sha256())
                    },
                    AllowedScopes = { "apiscope", "DALApi1" }
                },
                new Client
                {
                    ClientId = "Alice",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret_alice_password".Sha256())
                    },
                    
                    AllowedScopes = { "apiscope" }
                },
                new Client
                {
                    ClientId = "Bob",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret_bob_password".Sha256())
                    },
                    AllowedScopes = { "apiscope" }
                }
            };
        }
    }
}
