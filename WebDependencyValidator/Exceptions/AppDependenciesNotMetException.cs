using System;

namespace WebDependencyValidator.Exceptions
{
    /// <summary>
    /// Exception to be raised when an application's dependencies have not been setup correctly in IoC.
    /// </summary>
    public class AppDependenciesNotMetException : WebDependencyValidatorException
    {
        /// <summary>
        /// Instantiates an <c>AppDependenciesNotMetException</c>.
        /// </summary>
        public AppDependenciesNotMetException()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a <c>AppDependenciesNotMetException</c> with a message.
        /// </summary>
        /// <param name="message">A description of why the exception is being raised.</param>
        public AppDependenciesNotMetException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// Instantiates a <c>AppDependenciesNotMetException</c> with a message and inner exception.
        /// </summary>
        /// <param name="message">A description of why the exception is being raised.</param>
        /// <param name="innerException">An inner exception for further details on the exception being raised.</param>
        public AppDependenciesNotMetException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
