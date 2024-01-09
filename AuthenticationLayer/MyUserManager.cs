using IdentityLayer.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityLayer
{
    public class MyUserManager<T> : UserManager<T>,IMyUserManager<T> where T : IdentityUser
    {
        public MyUserManager(IUserStore<T> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<T> passwordHasher, IEnumerable<IUserValidator<T>> userValidators, IEnumerable<IPasswordValidator<T>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<T>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}
