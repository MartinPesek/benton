using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Benton.Extensions
{
    public static class AzureKeyVaultWebHostBuilderExtension
    {
        /// <summary>
        /// Adds an <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" /> that reads configuration
        /// values from the Azure KeyVault. Azure KeyVault is automatically configured through environmental
        /// variables or a config file.
        /// </summary>
        /// <returns>The <see cref="T:Microsoft.AspNetCore.Hosting.IWebHostBuilder" />.</returns>
        public static IWebHostBuilder UseAzureKeyVault(this IWebHostBuilder builder)
        {
            return builder.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                configurationBuilder.AddConfiguration(CreateAzureKeyVaultConfig(context));
            });
        }

        private static IConfigurationRoot CreateAzureKeyVaultConfig(WebHostBuilderContext context)
        {
            var envConfig = CreateEnvConfig(context);

            var keyVaultConfigBuilder = new ConfigurationBuilder();

            keyVaultConfigBuilder.AddAzureKeyVault(
                $"https://{envConfig["KEYVAULT_NAME"]}.vault.azure.net/",
                envConfig["KEYVAULT_CLIENT_ID"],
                envConfig["KEYVAULT_CLIENT_SECRET"]);

            return keyVaultConfigBuilder.Build();
        }

        private static IConfigurationRoot CreateEnvConfig(WebHostBuilderContext context)
        {
            var configBuilder = new ConfigurationBuilder();

            configBuilder.SetBasePath(context.HostingEnvironment.ContentRootPath);
            configBuilder.AddEnvironmentVariables();
            configBuilder.AddJsonFile("akv.json", true, false);

            return configBuilder.Build();
        }
    }
}