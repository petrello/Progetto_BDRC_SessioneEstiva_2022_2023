using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityServer
{
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            // var requestedClaimTypes = context.RequestedClaimTypes;
            var user = context.Subject;

            //if(!user.Claims.Where(x => x.Type.Equals("amr")).First().Value.Equals("external"))
            if(user.Claims.Any(x => x.Type.Equals("position"))
               && user.Claims.Any(x => x.Type.Equals("country"))
               && user.Claims.Any(x => x.Type.Equals("role")))
            { 
                var claims = new List<Claim>
                {
                    new Claim("position", user.Claims.Where(x => x.Type.Equals("position")).First().Value),
                    new Claim("country", user.Claims.Where(x => x.Type.Equals("country")).First().Value),
                    new Claim("role", user.Claims.Where(x => x.Type.Equals("role")).First().Value)
                };
                
                context.IssuedClaims.AddRange(claims);
            }

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}
