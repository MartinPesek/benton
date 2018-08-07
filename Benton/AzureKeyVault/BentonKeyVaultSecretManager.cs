using System.Globalization;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace Benton.AzureKeyVault
{
    public class BentonKeyVaultSecretManager : DefaultKeyVaultSecretManager
    {
        public override string GetKey(SecretBundle secret)
        {
            var key = base.GetKey(secret).Replace("-", " ").ToLowerInvariant();
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(key).Replace(" ", "");
        }
    }
}