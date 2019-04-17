# AspNetCore Dependency Tools

A series of tools for working with web dependencies in ASP.NET Core.

# Web Dependency Validator

Verifies that all dependencies in the dependency chain of ASP.NET controllers have been set up in IoC.

In a typical ASP.NET Core project utilising dependency injection, you will declare your dependencies in the constructors of your controllers, and it is likely that these dependencies will have constructor dependencies themselves.
If any of these dependencies have not been setup in IoC, the IoC container will not be able to instantiate the requested controller and you will receive a runtime error, but the app will continue to run.

The Web Dependency Validator stops app execution and logs an error in this situation to make it easier to identify these dependency problems.

## How Does it Work?

The Web Dependency Validator runs once as part of application startup, and is designed to stop the application if it finds a dependency which has not been configured in IoC.

Dependencies are validated using the following steps:

1. All available controllers in the application are discovered.
1. For each of the discovered controllers, Reflection is used to get the type of each parameter in all available constructors.
1. An attempt is then made to try and resolve each parameter type from the IoC container.
1. If the attempt to resolve the parameter type throws an exception (either the service itself has not been setup in IoC or one of the service's dependencies has not), then an attempt is made to log the event and an `AppDependenciesNotMetException` is raised which will stop app execution (assuming no generic `Exception` error handling exists above this level).

## Compatibility

This is a .NET Standard library targeting .NET Standard 2.0 or higher.

## How to Use It

After installing the package, you will need to make the following change to your `Startup.cs` file. The WebDependencyValidator is available by calling the `ValidateControllerDependencies` extension method on `IServiceCollection`. Ensure this step is after MVC and any custom services have been added to the IoC container.

```
public void ConfigureServices(IServiceCollection services)
{
    // You need to get the instance of IMvcBuilder after adding the MVC services to the IoC container.
    IMvcBuilder mvcBuilder = services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

    services.AddSingleton<IRandomNumberGenerator, ExampleWebAPI.Services.RandomNumberGenerator>();
    // Add any remaining services to the IoC container.

    // Finally, validate that all dependencies can be satisfied from the IoC container.
    services.ValidateControllerDependencies(mvcBuilder);
}
```
