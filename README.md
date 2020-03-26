![Xeroxcore logo](https://github.com/Xeroxcore/Xeroxcore/blob/master/resources/images/Xeroxcore_Logo.png)<br/><br/>
![GitHub repo size](https://img.shields.io/github/repo-size/xeroxcore/xSQL)
![GitHub issues](https://img.shields.io/github/issues/xeroxcore/xSQL)
[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://github.com/Xeroxcore/xSQL/blob/master/LICENSE)

## Introduction

### Getting started

These instructions are aimed at helping you set up the project for development or testing purposes.
If you wish to put the project in production, please check our [Deployment section](#deployment).

### Prerequisites

The following tools are required for the API to function please make sure that necessary tools
are installed and if not, install them utilizing the provider's main pages.

- [.Net Core](https://dotnet.microsoft.com/download/dotnet-core/3.0) - dotnet core 3 and greater

### Installing

This section will help you to set up your development environment. For production deployment please
check the [Deployment section](#deployment). Since the app was developed in CentOS 7, most of the
commands might be centos related.

#### Dotnet core 3

This installation process was made for centos 7 & Windows 10 and might not work on your system. if you are
utilizing an alternative OS or distribution then please check the [.Net Core](https://dotnet.microsoft.com/download/linux-package-manager/rhel/sdk-current) website for more info. Ps, you might need to enable
third-party repositories.

##### Linux-CentOS

```
begin by opening a new shell window and follow the steps below:
1. sudo rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
2. sudo yum install dotnet-sdk-3.0.x86_64 -y
3. enter the following command after the installation is complete to verify the installation
  dotnet --version
```

###### Windows 10

```
1. Go to the following URL and download the [.Net Core SDK](https://dotnet.microsoft.com/download).
2. Open the install dotnet-sdk-3.x.x-win-x64.exe.
3. follow the installation instructions.
4. Open CMD(Command prompt) and paste dotnet --Version to verify the installation.
```

### Deployment

This section explains how the middleware can be exported as a NuGet and uploaded to GitHub for
safekeeping. This section will also go through how you as a developer can download the package
to your .Net Core applications.

#### Nuget.Config

You will first need to create a nuget.config file in your project to enable the GitHub repository.
you nuget.config should look like the following.

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <!--To inherit the global NuGet package sources remove the <clear/> line below -->
    <clear />
        <add key="NuGet official package source" value="https://api.nuget.org/v3/index.json" />
        <add key="github" value="https://nuget.pkg.github.com/OWNER/index.json" />
  </packageSources>
      <packageSourceCredentials>
        <github>
            <add key="Username" value="#username" />
            <add key="ClearTextPassword" value="password or tokenkey" />
        </github>
    </packageSourceCredentials>
</configuration>
```

now that that is done you will need to change the #username to your username and
then add your password or token key you should also replace owner with your name or organization name.

#### Push To Github

now that you have configured the nuget.config you will be able to get and push to GitHub. Before pushing
you will need to alter the middleware.csproj and adjust the following content.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    #change me if netstander is higher
    <TargetFramework>netstandard2.0</TargetFramework>
    #name of the package
    <PackageId>xheader-security</PackageId>
     #the current version accorind to versioning
    <Version>1.2.6</Version>
    <Authors>Nasar Eddaoui</Authors>
    <Company>your company</Company>
    <PackageDescription> sample description</PackageDescription>
     # the repository url that will be used to push to
    <RepositoryUrl>https://github.com/Xeroxcore/xheader-security</RepositoryUrl>

  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Http" Version="2.2.2" />
  </ItemGroup>

</Project>
```

once the csproje has been modified to your liking it's time to push your changes to GitHub with the following commands.

```
1. open the project in the command line and navigate to the middleware folder.
2. enter dotnet pack --configuration=Release
3. dotnet push nuget "bin/Release/xheader-security.1.2.6.nupkg" --source "github"

*If you receive an error check the repositoryUrl in csproj or
the nuget.config and validate username and ClearTextPassword
```

#### Install package in your app

To install your nuget package you will first need to setup your [nuget.config](#nugetconfig). Once that is done
you can simply install the package with the following command.

dotnet add <PROJECT> package xheader-security --version 1.2.6

### Built With

Middleware was built with the following tools in a Linux Environment (CentOS 7).

- [Visual Studio Code](https://code.visualstudio.com/) - Code Editor
- [.Net Core](https://dotnet.microsoft.com/) - Dotnet Core Runtime

### Contributing

### Versioning

We use [SemVer](http://semver.org/) for versioning. Please visit their website for more
information to understand how different versions might affect you and your project.

### Authors

- **Nasar Eddaoui** - _Initial work_ - [Nasar Eddaoui](https://github.com/Nasar165)

See also the list of [contributors](https://github.com/Xeroxcore/xSQL//graphs/contributors) who participated in this project.

### License

This project is licensed under the GNU General Public License v3.0 - see the [LICENSE](LICENSE) file for details
