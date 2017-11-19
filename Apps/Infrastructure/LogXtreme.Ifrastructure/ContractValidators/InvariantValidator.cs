using System;
using LogXtreme.Ifrastructure.ContractValidators;
using LogXtreme.Extensions;

namespace LogXtreme.Infrastructure.ContractValidators {

    /// <summary>
    /// Refs
    /// https://www.daedtech.com/poor-mans-code-contracts/
    /// </summary>
    public class InvariantValidator : IInvariantValidator {

        public static IInvariantValidator CreateValidator() {
            return new InvariantValidator();
        }

        private InvariantValidator() { }

        /// <summary>
        /// Verifies a (reference) method parameter as non-null and throws an 
        /// ArgumentNullException with a set or custom message if it si null.
        /// </summary>
        /// <param name="argument">The parameter to check</param>
        /// <param name="message">Optional message to use in the exception when thrown</param>
        public virtual void VerifyNonNull<T>(
            object argument,
            string message = @"Argument cannot be null") where T : Exception {

            if (argument == null) {

                Type exceptionType = typeof(T);

                if (exceptionType == typeof(ArgumentNullException)) {
                    throw new ArgumentNullException(@"argument", message);
                }

                var exception = (T)Activator.CreateInstance(typeof(T), message);
                throw exception;
            }
        }

        /// <summary>
        /// Verifies that a parameters list of objects are all not null
        /// and throws an exception when one is found to be null.
        /// </summary>
        /// <param name="arguments"></param>
        public virtual void VerifyParamsNonNull<T>(
            params object[] arguments) where T : Exception {

            VerifyNonNull<T>(arguments);

            foreach (object myParameter in arguments) {
                VerifyNonNull<T>(myParameter);
            }
        }

        /// <summary>
        /// Verifies that a string is not null or empty
        /// </summary>
        /// <param name="target">String to check</param>
        /// <param name="message">Optional parameter for exception message</param>
        public virtual void VerifyNotNullOrEmpty<T>(
            string target,
            string message = @"String cannot be null or empty.") where T : Exception {

            if (string.IsNullOrEmpty(target)) {

                Type exceptionType = typeof(T);

                if (exceptionType == typeof(ArgumentNullException)) {
                    throw new ArgumentNullException(@"argument", message);
                }

                var exception = (T)Activator.CreateInstance(typeof(T), message);
                throw exception;
            }
        }

        /// <summary>Verify that a string is not null or empty</summary>
        /// <param name="target">String to check</param>
        /// <param name="message">Optional parameter for exception message</param>
        public virtual void VerifyNotNullOrEmptyOrWhiteSpace<T>(
            string target,
            string message = @"String cannot be null or empty or white space.") where T : Exception {

            if (string.IsNullOrEmpty(target) ||
                string.IsNullOrWhiteSpace(target)) {

                Type exceptionType = typeof(T);

                if (exceptionType == typeof(ArgumentNullException)) {
                    throw new ArgumentNullException(@"argument", message);
                }

                var exception = (T)Activator.CreateInstance(typeof(T), message);
                throw exception;
            }
        }

        public virtual void VerifyValue<T>(
            object value,
            object compareTo,
            InvariantValidatorComparisonEnum comparison,
            string message = null) where T : Exception {

            var success = true;

            var typeOfValue = value.GetType();
            success = typeOfValue.IsTypeOf(compareTo, out Type outType);

            if (!success) {

                message = $"{nameof(VerifyValue)} cannot compare {value} of type {typeOfValue} to {compareTo} of type {outType}";
                var exception = (T)Activator.CreateInstance(typeof(T), message);
                throw exception;
            }

            switch (comparison) {

                case InvariantValidatorComparisonEnum.EqualTo:

                    if (outType == typeof(int)) {

                        success = (int)value == (int)compareTo;

                    }
                    else if (outType == typeof(double)) {

                        success = (double)value == (double)compareTo;

                    }
                    else if (outType == typeof(decimal)) {

                        success = (decimal)value == (decimal)compareTo;

                    }
                    else {

                        success = false;
                    }

                    message = message ??
                        $"{value} == {compareTo} is false";

                    break;

                case InvariantValidatorComparisonEnum.LargerThan:

                    if (outType == typeof(int)) {

                        success = (int)value > (int)compareTo;

                    }
                    else if (outType == typeof(double)) {

                        success = (double)value > (double)compareTo;

                    }
                    else if (outType == typeof(decimal)) {

                        success = (decimal)value > (decimal)compareTo;

                    }
                    else {

                        success = false;
                    }
                    message = message ??
                        $"{value} > {compareTo} is false";

                    break;

                case InvariantValidatorComparisonEnum.SmallerThan:

                    if (outType == typeof(int)) {

                        success = (int)value < (int)compareTo;

                    }
                    else if (outType == typeof(double)) {

                        success = (double)value < (double)compareTo;

                    }
                    else if (outType == typeof(decimal)) {

                        success = (decimal)value < (decimal)compareTo;

                    }
                    else {

                        success = false;
                    }

                    message = message ??
                        $"{value} < {compareTo} is false";

                    break;
                case InvariantValidatorComparisonEnum.LargerThanOrEqual:

                    if (outType == typeof(int)) {

                        success = (int)value >= (int)compareTo;

                    }
                    else if (outType == typeof(double)) {

                        success = (double)value >= (double)compareTo;

                    }
                    else if (outType == typeof(decimal)) {

                        success = (decimal)value >= (decimal)compareTo;

                    }
                    else {

                        success = false;
                    }

                    message = message ??
                        $"{value} >= {compareTo} is false";

                    break;

                case InvariantValidatorComparisonEnum.SmallerThanOrEqual:

                    if (outType == typeof(int)) {

                        success = (int)value <= (int)compareTo;

                    }
                    else if (outType == typeof(double)) {

                        success = (double)value <= (double)compareTo;

                    }
                    else if (outType == typeof(decimal)) {

                        success = (decimal)value <= (decimal)compareTo;

                    }
                    else {

                        success = false;
                    }

                    message = message ??
                        $"{value} <= {compareTo} is false";

                    break;

                default:
                    success = false;
                    message = $"{comparison} is not a valid operation for {nameof(VerifyValue)}";
                    break;
            }

            if (!success) {

                var exception = (T)Activator.CreateInstance(typeof(T), message);
                throw exception;
            }
        }

        public virtual void VerifyRange<T>(
            object value,
            object left,
            object right,
            InvariantValidatorRangeCheckEnum rangeCheck,
            string message = @"value range check failed") where T : Exception {

            var success = true;

            var typeOfValue = value.GetType();
            success = typeOfValue.IsTypeOf(left, out Type outType);

            if (!success) {

                message = $"{nameof(VerifyRange)} cannot compare {value} of type {typeOfValue} to {left} of type {outType}";
                var exception = (T)Activator.CreateInstance(typeof(T), message);
                throw exception;
            }

            success = typeOfValue.IsTypeOf(right, out outType);

            if (!success) {

                message = $"{nameof(VerifyRange)} cannot compare {value} of type {typeOfValue} to {right} of type {outType}";
                var exception = (T)Activator.CreateInstance(typeof(T), message);
                throw exception;
            }

            switch (rangeCheck) {

                case InvariantValidatorRangeCheckEnum.InRange:

                    if (outType == typeof(int)) {

                        var v = (int)value;
                        var l = (int)left;
                        var r = (int)right;

                        if (l > r) {
                            throw new ArgumentOutOfRangeException($"the range [{l},{r}] is invalid");
                        }

                        success =  v >= l  && v <= r;

                    }
                    else if (outType == typeof(double)) {

                        var v = (double)value;
                        var l = (double)left;
                        var r = (double)right;

                        if (l > r) {
                            throw new ArgumentOutOfRangeException($"the range [{l},{r}] is invalid");
                        }

                        success = v >= l && v <= r;

                    }
                    else if (outType == typeof(decimal)) {

                        var v = (decimal)value;
                        var l = (decimal)left;
                        var r = (decimal)right;

                        if (l > r) {
                            throw new ArgumentOutOfRangeException($"the range [{l},{r}] is invalid");
                        }

                        success = v >= l && v <= r;

                    }
                    else {

                        success = false;
                    }

                    message = message ??
                        $"the value {value} is not in the range [{left}, {right}]";

                    break;

                case InvariantValidatorRangeCheckEnum.OutOfRange:

                    if (outType == typeof(int)) {

                        var v = (int)value;
                        var l = (int)left;
                        var r = (int)right;

                        if (l > r) {
                            throw new ArgumentOutOfRangeException($"the range [{l},{r}] is invalid");
                        }

                        success = v > r || v < l;

                    }
                    else if (outType == typeof(double)) {

                        var v = (double)value;
                        var l = (double)left;
                        var r = (double)right;

                        if (l > r) {
                            throw new ArgumentOutOfRangeException($"the range [{l},{r}] is invalid");
                        }

                        success = v > r || v < l;

                    }
                    else if (outType == typeof(decimal)) {

                        var v = (decimal)value;
                        var l = (decimal)left;
                        var r = (decimal)right;

                        if (l > r) {
                            throw new ArgumentOutOfRangeException($"the range [{l},{r}] is invalid");
                        }

                        success = v > r || v < l;

                    }
                    else {

                        success = false;
                    }

                    message = message ??
                        $"the value {value} is in the range [{left}, {right}]";

                    break;

                default:
                    success = false;
                    message = $"{rangeCheck} is not a valid operation for {nameof(VerifyRange)}";
                    break;
            }

            if (!success) {

                var exception = (T)Activator.CreateInstance(typeof(T), message);
                throw exception;
            }
        }
    }
}
