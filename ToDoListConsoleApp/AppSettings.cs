using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListConsoleApp
{
    static class AppSettings
    {
        public static string DefaultConnectionString => GetConfigValue("DefaultConnectionString");
        public static string DatabaseFileName => GetConfigValue("DatabaseFileName");
        public static string DatabaseTargetDirectory => GetConfigValue("DatabaseTargetDirectory");

        private static IConfigurationRoot config;
        private static bool initialized;

        /// <summary>
        ///  This method is called <u>automatically</u>.
        ///  Feel free to call it manually if you want things to get initialized
        ///  before using any properties inside this class.
        /// </summary>
        public static void Initialize()
        {
            if (initialized)
                throw new InvalidOperationException("App settings have already been initialized!");

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true);
            config = builder.Build();
        }

        private static string GetConfigValue(string name)
        {
            if (!initialized)
                Initialize();

            return config[name];
        }
    }
}
