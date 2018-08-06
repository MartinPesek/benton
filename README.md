# Benton

Utility library. So far only supports faster set up of Azure Key Vault.

### Installation

Grab it from [NuGet](https://www.nuget.org/packages/Benton/).

### Quickly configure Azure KeyVault
```csharp
private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseKestrel()
        .UseAzureKeyVault()
        .UseStartup<Startup>();
```


After that you can use `IConfiguration` to retrieve values from Azure KeyStore:
```csharp
var someSecret = configuration["SECRET"];
```

### How it works

It will automatically grab three environmental variables and configure Azure KeyVault.

The three environmental variables are:
* KEYVAULT_NAME
* KEYVAULT_CLIENT_ID
* KEYVAULT_CLIENT_SECRET

Alternatively these variables can be provided as `akv.json` file:
```json
{
    "KEYVAULT_NAME": "vault name",
    "KEYVAULT_CLIENT_ID": "client id",
    "KEYVAULT_CLIENT_SECRET": "client secret",
}
```

Create this file inside `ContentRootPath` (e.g. root of a web project) or where the binary is located.
