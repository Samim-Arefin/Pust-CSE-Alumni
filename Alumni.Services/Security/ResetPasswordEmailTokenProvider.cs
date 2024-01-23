using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Alumni.Services.Security
{
    public class ResetPasswordEmailTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public ResetPasswordEmailTokenProvider(IDataProtectionProvider dataProtectionProvider,
            IOptions<ResetPasswordEmailTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<TUser>> logger)
            : base(dataProtectionProvider, options, logger)
        {
        }
    }
    public class ResetPasswordEmailTokenProviderOptions : DataProtectionTokenProviderOptions
    {
    }
}
