### Curso Asp.Net Core 6.0 - Do Zero ao Ninja com SmartAdmin Template

SmartAdmin for ASP.NET Core is an open-source and cross-platform framework for building modern cloud based internet connected applications, such as web apps, IoT apps and mobile backends. SmartAdmin for ASP.NET Core apps can run on .NET Core or on the full .NET Framework. It was architected to provide an optimized development framework for apps that are deployed to the cloud or run on-premises. It consists of modular components with minimal overhead, so you retain flexibility while constructing your solutions. You can develop and run your SmartAdmin for ASP.NET Core apps cross-platform on Windows, Mac and Linux. [Learn more about ASP.NET Core](https://docs.microsoft.com/aspnet/core/).

## Get Started

1. Download and install the **ASP.NET Core 3.1 SDK** here: https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.102-windows-x64-installer
    * You can verify the installation by opening a command line tool of your choice and running the following command: `dotnet --info`
    * If you get a message similar to: `dotnet is not recognized as an internal command`, then please try downloading the `32-bit` version of the ASP.NET Core 3.1 SDK
    * You can find it here: https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.102-windows-x86-installer
1. Download and install **SQL Server Express** here: https://go.microsoft.com/fwlink/?linkid=853017
    * Note: You can also install **LocalDB** as part of Visual Studio. During Visual Studio installation, select the **.NET desktop development** workload, which includes SQL Server Express LocalDB.
    * If you are going to SQL Express then the default connection string *value* in `appSettings.json` will need to be adjusted
    * The value and instructions are written in `Startup.cs` but also listed here for your convinience:
    * `"Server=localhost\\SQLEXPRESS;Database=aspnet-smartadmin;Trusted_Connection=True;MultipleActiveResultSets=true"`
1. Open a command prompt and ensure you are inside the directory containing the **ASP.NET Core 3.1 project sources** of SmartAdmin 4.0
    * This is the directory containing the `SmartAdminCore.sln` file
1. Run the following commands (in order) using the command line tool of your choice:
    * `dotnet build`
    * `dotnet publish` *(optional for localhost only deployment)*
    * `dotnet run --project ./src/Cooperchip.ITDeveloper.Mvc/Cooperchip.ITDeveloper.Mvc.csproj`
1. Launch your favorite browser and enter the following URL: https://localhost:5001, and try to login using the provided credentials
    * You may get a page mentioning that: 'Applying the existing migrations may resolve this issue'
    * Go ahead and click on the blue `Apply Migrations` button
1. If you receive a message stating: 'Invalid login attempt' then go ahead and Register the user
    * The username and password should be prefilled for demo purposes

> Note: You might get a security warning when browsing to your website, as the default `localhost` server will typically not have a trusted **localhost** SSL certificate!

Also check out the [.NET Homepage](https://www.microsoft.com/net) for released versions of .NET, getting started guides, and learning resources.


## Code of conduct

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).  For more information, see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [contato.cooperchip@gmail.com](mailto:contato.cooperchip@gmail.com) with any additional questions or comments.
