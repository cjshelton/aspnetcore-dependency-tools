using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebDependencyValidator.Exceptions;

namespace WebDependencyValidator
{
    /// <summary>
    /// Extensions for web dependency validation.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Validates the dependencies of all MVC controllers to ensure that all dependencies can be resolved
        /// from IoC.
        /// </summary>
        /// <param name="services">The service collection to locate the dependencies in IoC.</param>
        /// <param name="mvcBuilder">The MVC services builder for locating MVC controllers.</param>
        public static void ValidateControllerDependencies(this IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            if (mvcBuilder == null) throw new System.ArgumentNullException(nameof(mvcBuilder));

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IEnumerable<Type> discoveredControllers = Validator.DiscoverControllers(mvcBuilder);

            // Web dependencies are checked only once at run time, and the limited number of controllers in a typical
            // ASP.NET project means these nested loops won't have any adverse effects on performance.
            foreach (Type controller in discoveredControllers)
            {
                ConstructorInfo[] constructors = controller.GetConstructors();

                foreach (ConstructorInfo constructor in constructors)
                {
                    ParameterInfo[] constructorParams = constructor.GetParameters();

                    foreach (ParameterInfo param in constructorParams)
                    {
                        Validator.TryResolveService(serviceProvider, param.ParameterType);
                    }
                }
            }
        }

        /// <summary>
        /// Discovers MVC controllers using the MVC builder.
        /// </summary>
        /// <param name="mvcBuilder">The MVC services builder for locating MVC controllers.</param>
        /// <returns>All MVC controller types in the web application.</returns>
        private static IEnumerable<Type> DiscoverControllers(IMvcBuilder mvcBuilder)
        {
            ControllerFeature feature = new ControllerFeature();
            mvcBuilder.PartManager.PopulateFeature(feature);

            return feature.Controllers.Select(c => c.AsType());
        }

        /// <summary>
        /// Attempts to resolve the service using the configured Service Provider.
        /// </summary>
        /// <param name="services">The service provider to locate the dependencies in IoC.</param>
        /// <param name="serviceType">The type of the dependency to try resolve from IoC.</param>
        private static void TryResolveService(IServiceProvider services, Type serviceType)
        {
            try
            {
                // Try resolve the parameter type using IoC. This will throw an exception if:
                //  * serviceType has not been registered
                //  * or a dependency of serviceType has not been registered.
                object service = services.GetRequiredService(serviceType);
            }
            catch (Exception ex)
            {
                const string ERROR_MESSAGE = 
                    "Application cannot be started because one or more dependencies have not been set up in IoC.";

                // Try get a logger to log the error.
                ILogger logger = services.GetService<ILogger>();
                logger?.LogCritical(ERROR_MESSAGE);

                // Preserve the original exception as the inner exception as it contains useful information about why
                // the service could not be resolved from IoC.
                throw new AppDependenciesNotMetException(ERROR_MESSAGE, ex);
            }
        }
    }
}
