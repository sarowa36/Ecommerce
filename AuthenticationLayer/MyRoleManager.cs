using IdentityLayer.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IdentityLayer
{
    public class MyRoleManager<TRole> : RoleManager<TRole>, IMyRoleManager<TRole> where TRole : IdentityRole
    {
        public MyRoleManager(IRoleStore<TRole> store, IEnumerable<IRoleValidator<TRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<TRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}
