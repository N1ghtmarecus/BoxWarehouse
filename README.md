# BoxWarehouse Solution

This solution is designed to manage a warehouse of cardboard boxes and paper products, including features such as user authentication, box management, and picture uploads for boxes. 
It utilizes ASP.NET Core for the web API, Entity Framework Core for data access, and SQL Server as the database.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- An IDE such as [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)

### Installation

1. Clone the repository to your local machine.
2. Open the solution in your IDE.
3. Restore the NuGet packages.
4. Update the connection string in `appsettings.json` to point to your SQL Server instance.
5. Run the Entity Framework Core migrations to set up your database: dotnet ef database update
6. Start the application. If using Visual Studio, press `F5`. If using the .NET CLI, navigate to the WebAPI project directory and run: dotnet run

## Features

(Current features...)

- User Authentication: Register and authenticate users with different roles (Customer, Employee, Admin).
- Box Management: Add, update, and delete boxes in the warehouse.
- Picture Uploads: Attach pictures to boxes for better visualization.
- SDK: The BoxWarehouse SDK provides a comprehensive interface for integrating with the BoxWarehouse platform, enabling developers to easily implement user authentication, box management, and picture uploads in their applications.

Swagger UI is available for exploring the API in detail and testing the endpoints.

## Planned Features

To make BoxWarehouse even more powerful, I am planning to introduce the following features:

- **Box Store**:  A fully integrated online store where users can browse and purchase boxes directly through the application. 
                  This feature will include payment processing, order tracking, and inventory management.
- **Box Pricing Calculator**: An intuitive tool that allows users to get instant pricing estimates for custom boxes based on dimensions, material, and quantity. 
                  This calculator will help users make informed decisions and streamline the ordering process.

## Development

This solution follows the Clean Architecture principles, separating concerns into different projects:

- **Application**: Contains the core application logic and interfaces.
- **Domain**: Includes domain entities and enums.
- **Infrastructure**: Handles data access and external services.
- **WebAPI**: The entry point of the application, hosting the web API.

## Dependencies

This project uses several NuGet packages to provide its functionality. Below is a list of key packages and their versions:

- **AutoMapper** (`AutoMapper`, `AutoMapper.Extensions.Microsoft.DependencyInjection`): A convention-based object-object mapper. Version: 12.0.1
- **FluentEmail** (`FluentEmail.Core`, `FluentEmail.Razor`, `FluentEmail.Smtp`): An email sending library for .NET, making it easier to send emails with Razor templates. Version: 3.0.2
- **FluentValidation.AspNetCore**: Provides a way to use FluentValidation to validate objects in ASP.NET Core. Version: 11.3.0
- **Microsoft.AspNetCore.Authentication.JwtBearer**: Support for JWT (JSON Web Tokens) in ASP.NET Core. Version: 8.0.6
- **Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation**: Adds support for runtime compilation of Razor views in ASP.NET Core. Version: 8.0.6
- **Microsoft.EntityFrameworkCore** (`Microsoft.EntityFrameworkCore`, `Microsoft.EntityFrameworkCore.SqlServer`, `Microsoft.EntityFrameworkCore.Design`, `Microsoft.EntityFrameworkCore.Tools`): A framework for working with databases using objects and LINQ. Version: 8.0.6
- **Microsoft.AspNetCore.Identity** (`Microsoft.AspNetCore.Identity`, `Microsoft.AspNetCore.Identity.EntityFrameworkCore`): ASP.NET Core Identity framework for managing users, roles, and authentication. Version: 2.1.39 (Identity), 8.0.6 (Identity.EntityFrameworkCore)
- **Swashbuckle.AspNetCore** (`Swashbuckle.AspNetCore`, `Swashbuckle.AspNetCore.Annotations`, `Swashbuckle.AspNetCore.Filters`): Swagger tooling for API's built with ASP.NET Core. Version: 6.4.0, 6.6.1 (Annotations), 8.0.2 (Filters)
- **Cosmonaut** (`Cosmonaut`, `Cosmonaut.Extensions.Microsoft.DependencyInjection`): A library that simplifies the use of Azure Cosmos DB. Version: 2.11.3 (Cosmonaut), 2.3.0 (Extensions)
- **Humanizer.Core** A library that helps in manipulating and displaying strings, enums, dates, times, timespans, numbers, and quantities. Version: 2.14.1
- **Refit** (`Refit`) Simplifies the creation of REST API clients by turning interfaces into live HTTP services. Version: 6.0.94
- **HealthChecks** (`(AspNetCore.HealthChecks.UI`, `AspNetCore.HealthChecks.UI.Client`, `AspNetCore.HealthChecks.UI.InMemory.Storage`, `Microsoft.Extensions.Diagnostics.HealthChecks`, `Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore`) Provides health check endpoints and a UI for monitoring the health of your application and its dependencies. Version: 8.0.1
- **NLog** (`NLog.Web.AspNetCore`) A logging platform for .NET with rich log routing and management capabilities. Version: 5.3.11

Please refer to the `csproj` file or the NuGet Package Manager in Visual Studio for a complete list of dependencies and their versions.

### Adding a New Feature

To add a new feature, start by defining any necessary domain entities and interfaces in the Application and Domain projects. 
Implement the interfaces in the Infrastructure project. Finally, expose the functionality through controllers in the WebAPI project.

## Contributing

Contributions are welcome. Please open an issue first to discuss what you would like to change. Ensure to update tests as appropriate.
