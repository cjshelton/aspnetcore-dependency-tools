using System;

namespace WebDependencyValidator.Exceptions
{
    /// <summary>
    /// Exception to be raised for a generic WebDependencyValidator exception.
    /// </summary>
    public class WebDependencyValidatorException : Exception
    {
        /// <summary>
        /// Instantiates a <c>WebDependencyValidatorException</c>.
        /// </summary>
        public WebDependencyValidatorException()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a <c>WebDependencyValidatorException</c> with a message.
        /// </summary>
        /// <param name="message">A description of why the exception is being raised.</param>
        public WebDependencyValidatorException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Instantiates a <c>WebDependencyValidatorException</c> with a message and inner exception.
        /// </summary>
        /// <param name="message">A description of why the exception is being raised.</param>
        /// <param name="innerException">An inner exception for further details on the exception being raised.</param>
        public WebDependencyValidatorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
