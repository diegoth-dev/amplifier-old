# Quick Start

Amplifier is available through NuGet packages at [Nuget.org](https://www.nuget.org/)

## Installation

For now, we only support .Net Core projects using Entity Framework Core and ASP.NET Core Identity:

```bash
PM> Install-Package Amplifier.AspNetCore
PM> Install-Package Amplifier.EntityFrameworkCore
```

## Configuration

Add Amplifier in `Startup.cs`:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    //...
    services.AddAmplifier();
    //...
}
```

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    //...
    app.UseAmplifier<int>();
    //...
}
```

Project DbContext needs inherit from `IdentityDbContextBase<TTenant, TUser, TRole, TKey>`:

```csharp
public class ApplicationDbContext : IdentityDbContextBase<Tenant, User, Role, int>
{
    private readonly IUserSession<int> _userSession;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserSession<int> userSession)
        : base(options, userSession)
    {
        _userSession = userSession;
    }

    // DbSets....
}
```

Project `User` class needs inherit from `IdentityUser`, the `Tenant` class from `TenantBase` and the `Role` class from `IdentityRole`.

## Using

Just Apply these interfaces:

- `IMayHaveTenant` - for classes that may have a Tenant Id.
- `IHaveTenant` - for classes that have Tenant Id.
- `ITenantFilter` - for classes that should be filtered by Tenant Id.
- `IFullAuditedEntity` - for classes that needs be audited (creation, deletion and last modified time and user).
- `ISoftDelete` - for classes that should be soft deleted.
