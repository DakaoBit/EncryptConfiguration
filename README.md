# EncryptedJsonConfiguration

## 前言:
為了避免 AppSetting.json 設定檔內的機敏參數在發佈環境，能不被其他人知道內容

## 相關套件:
1. [Kizuna](https://github.com/miqoas/Kizuna):
A .NET Global Tool to quickly encrypt and decrypt files using AES-256-GCM
2. [Miqo.EncryptedJsonConfiguration](https://github.com/miqoas/Miqo.EncryptedJsonConfiguration):
Configuring your .NET Core with encrypted JSON files has never been so easy


## Kizuna
### Part 1. Install Kizuna
Before you begin you need to install the Kizuna command line tool.

```
dotnet new tool-manifest # if you are setting up this repo
dotnet tool install --local Kizuna --version 1.0.0
```

### Part 2. Using Kizuna
Start by creating a new encryption key.

```
$ dotnet kizuna generate
```

Make sure you write down the encryption key in a safe location, like a password manager (1Password, LastPass, etc.). Never commit the encryption key into source code.

Create a JSON file in your favorite file editor. When you are ready to encrypt the JSON file, use the following command.

```
$ kizuna encrypt -k {key} {filename}
```

If you need to decrypt the file to make changes you can use the following command:
```
$ kizuna decrypt -k {key} {filename}
```
The file's contents is replaced with the encrypted or decrypted configuration when the encrypt or decrypt command is used. Add the -c option to output to your console instead of writing to the file system.


---

## Miqo.EncryptedJsonConfiguration
### Part 1. Install Miqo.EncryptedJsonConfigurationizuna

```
Install-Package Miqo.EncryptedJsonConfiguration
```

### Part 2. 使用

In Program.cs 

```
using Miqo.EncryptedJsonConfiguration;

var key = Convert.FromBase64String("your_sauce_key");

ConfigurationManager configuration = builder.Configuration;
configuration.AddEncryptedJsonFile("appsettings.ejson", key);
builder.Services.AddJsonEncryptedSettings<ConnectionStrings>(configuration, "ConnectionStrings");
builder.Services.AddJsonEncryptedSettings<Authentication>(configuration, "Authentication");
builder.Services.AddJsonEncryptedSettings<JwtSettings>(configuration, "JwtSettings");
```

取參數的範例

```
public class ConfigService
{
  private readonly ConnectionStrings dBConStr;
  private readonly Authentication auth;
  private readonly JwtSettings jwt;

  public ConfigService(IServiceProvider service)
  {
    this.dBConStr = service.GetService<ConnectionStrings>();
    this.auth = service.GetService<Authentication>();
    this.jwt = service.GetService<JwtSettings>();
  }
}


```