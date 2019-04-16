# aspnetcore-dependency-tools

A series of tools for working with web dependencies in ASP.NET Core.

# Web Dependency Validator

Verifies that all dependencies in the dependency chain of ASP.NET Controllers have been set up in IoC.

In a typical ASP.NET Core project utilising dependency injection, you will declare your dependencies in the constructors of your controllers, and it is likely that these dependencies will have constructor dependencies themselves.

All of the dependencies of your application must be setup in IoC in the `ConfigureServices` method in `Startup.cs` like below:

```
public void ConfigureServices(IServiceCollection services)
{
    // Code removed for brevity.

    services.AddTransient<IRandomNumberGenerator, RandomNumberGenerator>();
}
```

## How Does it Work?

The WebDependencyValidator runs once as part of application startup, and is designed to stop the application if it finds a dependency which has not been configured in IoC.

Dependencies are validated using the following steps:

1. All available Controllers in the application are discovered.
1. For each of the discovered Controllers, Reflection is used to get the type of each parameter to all available constructors.
1. An attempt is then made to try and resolve each parameter type from the IoC container.
1. If the attempt to resolve the parameter type throws an exception (either the service itself has not been setup in IoC or one of the service's dependencies has not), then an attempt is made to log the event and an `AppDependenciesNotMetException` is raised which will stop app execution (assuming no generic `Exception` error handling exists at this point).

## Compatibility

This is a .NET Standard library targeting .NET Standard 2.2 or higher.

## How to Use

After installing the package, you will need to make the following changes to your ASP.NET Core project.
Extension method available on IServiceCollection.

