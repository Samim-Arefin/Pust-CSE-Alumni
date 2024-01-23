namespace Alumni.Services.Utilities
{
    public static class AppSettings
    {
        public static Settings Settings { get; set; } = new();
    }

    public class Settings
    {
        public string AppUrl { get; set; }
        public string DBConnection { get; set; }
        public string ProtectionKey { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
    }
}
