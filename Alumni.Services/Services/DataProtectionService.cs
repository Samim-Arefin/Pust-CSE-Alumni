using Alumni.Services.Utilities;
using Microsoft.AspNetCore.DataProtection;

namespace Alumni.Services.Services
{
    public interface IDataProtectionService
    {
        Task<string> Encrypt(string text);
        Task<string> Decrypt(string text);
    }

    public class DataProtectionService : IDataProtectionService
    {
        private readonly IDataProtectionProvider _dataProtection;
        public DataProtectionService(IDataProtectionProvider dataProtection)
        {
            _dataProtection = dataProtection;
        }

        public async Task<string> Encrypt(string text)
        {
            var protector = _dataProtection.CreateProtector(AppSettings.Settings.ProtectionKey);
            return protector.Protect(text);
        }

        public async Task<string> Decrypt(string text)
        {
            var protector = _dataProtection.CreateProtector(AppSettings.Settings.ProtectionKey);
            return protector.Unprotect(text);
        }
    }
}
