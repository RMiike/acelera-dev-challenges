using Codenation.Challenge.Models;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Codenation.Challenge.Services
{
    public class PasswordValidatorService : IResourceOwnerPasswordValidator
    {
        private CodenationContext _dbContext;
        private readonly string custom = "custom";
        private readonly string invalid = "Invalid username or password";

        public PasswordValidatorService(CodenationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var result = _dbContext
                .Users
                .Where(x => x.Email == context.UserName &&
                x.Password == context.Password)
                .AsNoTracking()
                .FirstOrDefault();

            context.Result = (result == null) ?
            new GrantValidationResult(
                 TokenRequestErrors.InvalidGrant, invalid) :
            new GrantValidationResult(
                  result.Id.ToString(), custom, UserProfileService.GetUserClaims(result));

            return Task.CompletedTask;
        }

    }
}