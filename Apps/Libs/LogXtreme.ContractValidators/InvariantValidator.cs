using System;
using LogXtreme.Ifrastructure.ContractValidators;

namespace LogXtreme.ContractValidators {

    /// <summary>
    /// Refs
    /// https://www.daedtech.com/poor-mans-code-contracts/
    /// </summary>
    public class InvariantValidator : IInvariantValidator {

        /// <summary>Verify a (reference) method parameter as non-null</summary>
        /// <param name="argument">The parameter in question</param>
        /// <param name="message">Optional message to go along with the thrown exception</param>
        public virtual void VerifyNonNull<T>(
            T argument,
            string message = @"Invalid Argument") where T : class {

            if (argument == null) {
                throw new ArgumentNullException(@"argument", message);
            }
        }

        /// <summary>Verify a parameters list of objects</summary>
        /// <param name="arguments"></param>
        public virtual void VerifyParamsNonNull(params object[] arguments) {

            VerifyNonNull(arguments);

            foreach (object myParameter in arguments) {
                VerifyNonNull(myParameter);
            }
        }

        /// <summary>Verify that a string is not null or empty</summary>
        /// <param name="target">String to check</param>
        /// <param name="message">Optional parameter for exception message</param>
        public virtual void VerifyNotNullOrEmpty(
            string target,
            string message = @"String cannot be null or empty.") {

            if (string.IsNullOrEmpty(target)) {
                throw new InvalidOperationException(message);
            }
        }

        /// <summary>Verify that a string is not null or empty</summary>
        /// <param name="target">String to check</param>
        /// <param name="message">Optional parameter for exception message</param>
        public virtual void VerifyNotNullOrEmptyOrWhiteSpace(
            string target,
            string message = @"String cannot be null or empty.") {

            if (string.IsNullOrEmpty(target) || string.IsNullOrWhiteSpace(target)) {
                throw new InvalidOperationException(message);
            }
        }
    }
}
