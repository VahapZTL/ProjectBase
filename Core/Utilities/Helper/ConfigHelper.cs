using Core.Utilities.Security.Jwt;
using log4net.Util;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Core.Utilities.Helper
{
    public static class ConfigHelper
    {
        private static IConfiguration config;
        public static void Initialize(IConfiguration configuration)
        {
            config = configuration;
        }

        private static void SetConfigurationManager()
        {
            if (config == null)
            {
                ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location));
                configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config = configurationBuilder.Build();
            }
        }

        public static string GetConfig(string ConfigKey)
        {
            SetConfigurationManager();
            if (config![ConfigKey] == null)
                return string.Empty;
            else
                return config[ConfigKey].ToString();
        }

        public static T GetConfigSection<T>(string ConfigKey) where T : class, new()
        {
            SetConfigurationManager();

            if (config.GetSection(ConfigKey).Get<T>() == null)
                return default(T);
            else
                return config.GetSection(ConfigKey).Get<T>();
        }

        public static string GetConfig(string ConfigKey, string defaultValue)
        {
            SetConfigurationManager();
            if (config[ConfigKey] == null)
                return defaultValue;
            else
                return config[ConfigKey].ToString();
        }

        public static int GetConfigInt(string ConfigKey, int defaultValue)
        {
            SetConfigurationManager();
            if (config[ConfigKey] == null)
                return defaultValue;
            else
                return int.Parse(config[ConfigKey]);
        }
    }
}
