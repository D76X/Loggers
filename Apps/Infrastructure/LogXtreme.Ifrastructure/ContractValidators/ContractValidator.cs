using LogXtreme.Extensions;
using System;

namespace LogXtreme.Infrastructure.ContractValidators {

    /// <summary>
    /// Refs
    /// https://www.daedtech.com/poor-mans-code-contracts/
    /// https://rogerjohansson.blog/2008/05/10/followup-how-to-validate-a-methods-arguments/    
    /// </summary>
    public class ContractValidator<V> : IContractValidation {

        public readonly V Value;
        public readonly string Name;

        public static ContractValidator<V> CreateValidator(
            V value,
            string name) {

            return new ContractValidator<V>(value, name);
        }

        private ContractValidator(
            V value,
            string name){

            this.Value = value;
            this.Name = name;
        } 

        //public static IContractValidation CreateValidator() {
        //    return new ContractValidator();
        //}

        //private ContractValidator<M>() { }

        /// <summary>
        /// Verifies a (reference) method parameter as non-null and throws an 
        /// ArgumentNullException with a set or custom message if it is null.
        /// </summary>
        /// <param name="argument">The parameter to check</param>
        /// <param name="message">Optional message to use in the exception when thrown</param>
        public virtual IContractValidation NotNull<TException>(
            object argument,
            string argumentName,
            string message = null) where TException : Exception {

            if (argument == null) {

                Type exceptionType = typeof(TException);

                if (exceptionType == typeof(ArgumentNullException)) {

                    if( message == null) {
                        throw new ArgumentNullException(argumentName);
                    }

                    throw new ArgumentNullException(argumentName, message);
                }

                var exception = (TException)Activator.CreateInstance(typeof(TException), message);
                throw exception;
            }

            return this;
        }

        /// <summary>
        /// Verifies that a parameters list of objects are all not null
        /// and throws an exception when one is found to be null.
        /// </summary>
        /// <param name="arguments"></param>
        public virtual IContractValidation NotNull<TException>(
            params object[] arguments) where TException : Exception {

            NotNull<TException>((object)arguments);

            foreach (object myParameter in arguments) {
                NotNull<TException>(myParameter);
            }

            return this;
        }

        /// <summary>
        /// Verifies that a string is not null or empty
        /// </summary>
        /// <param name="argument">String to check</param>
        /// <param name="message">Optional parameter for exception message</param>
        public virtual IContractValidation NotNullOrEmpty<TException>(
            string argument,
            string argumentName,
            string message = null) where TException : Exception {

            if (argument == null) {
                throw new ArgumentNullException(argumentName);
            }

            if (string.IsNullOrEmpty(argument)) {

                var exception = (TException)Activator.CreateInstance(typeof(TException), message);
                throw exception;
            }

            return this;
        }

        /// <summary>Verify that a string is not null or empty</summary>
        /// <param name="argument">String to check</param>
        /// <param name="message">Optional parameter for exception message</param>
        public virtual IContractValidation NotNullOrEmptyOrWhiteSpace<TException>(
            string argument,
            string argumentName,
            string message = null) where TException : Exception {

            if (argument == null) {
                throw new ArgumentNullException(argumentName);
            }

            if (string.IsNullOrEmpty(argument)) {
                throw new ArgumentException($"{argumentName} is an empty string.");
            }

            if (string.IsNullOrWhiteSpace(argument)) {                

                var exception = (TException)Activator.CreateInstance(typeof(TException), message);
                throw exception;
            }

            return this;
        }

        /// <summary>
        /// Refs
        /// https://msdn.microsoft.com/en-us/library/system.icomparable(v=vs.110).aspx
        /// https://stackoverflow.com/questions/34242746/difference-between-icomparable-and-icomparablet-in-this-search-method
        /// https://stackoverflow.com/questions/7301277/icomparable-and-icomparablet   
        /// Refs
        /// http://www.codinghelmet.com/?path=howto/implement-icomparable-t
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        /// <param name="target"></param>
        /// <param name="comparison"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual IContractValidation VerifyValue<TException>(
            object argument,
            string argumentName,
            object target,
            EnumComparisonOperations comparison,
            string message = null) where TException : Exception {

            var success = true;

            var typeOfArgument = argument.GetType();
            success = typeOfArgument.IsTypeOf(target, out Type outType);

            if (!success) {

                message = $"{nameof(VerifyValue)} cannot compare {argument} of type {typeOfArgument} to {target} of type {outType}";
                var exception = (TException)Activator.CreateInstance(typeof(TException), message);
                throw exception;
            }

            switch (comparison) {

                case EnumComparisonOperations.EqualTo:

                    if (outType == typeof(string)) {

                        success = (string)argument == (string)target;
                    }
                    else if (outType == typeof(int)) {

                        success = (int)argument == (int)target;

                    }
                    else if (outType == typeof(double)) {

                        success = (double)argument == (double)target;

                    }
                    else if (outType == typeof(decimal)) {

                        success = (decimal)argument == (decimal)target;

                    }
                    else if (outType.IsIComparable<V>()) {

                        IComparable<V> comparable = (IComparable<V>)argument;
                        V comparableTarget = (V)target;
                        success = comparable.CompareTo(comparableTarget) == 0;
                    }
                    else if (outType.IsIComparable()) {

                        success = ((IComparable)argument).CompareTo(target) == 0;
                    }
                    else {

                        success = false;
                    }

                    message = message ??
                        $"{argument} == {target} is false";

                    break;

                case EnumComparisonOperations.GreaterThan:

                    if (outType == typeof(int)) {

                        success = (int)argument > (int)target;

                    }
                    else if (outType == typeof(double)) {

                        success = (double)argument > (double)target;

                    }
                    else if (outType == typeof(decimal)) {

                        success = (decimal)argument > (decimal)target;

                    }
                    else if (outType.IsIComparable<object>()) {

                        success = ((IComparable<object>)argument).CompareTo(target) > 0;
                    }
                    else if (outType.IsIComparable()) {

                        success = ((IComparable)argument).CompareTo(target) > 0;
                    }
                    else {

                        success = false;
                    }
                    message = message ??
                        $"{argument} > {target} is false";

                    break;

                case EnumComparisonOperations.SmallerThan:

                    if (outType == typeof(int)) {

                        success = (int)argument < (int)target;

                    }
                    else if (outType == typeof(double)) {

                        success = (double)argument < (double)target;

                    }
                    else if (outType == typeof(decimal)) {

                        success = (decimal)argument < (decimal)target;

                    }
                    else if (outType.IsIComparable<object>()) {

                        success = ((IComparable<object>)argument).CompareTo(target) < 0;
                    }
                    else if (outType.IsIComparable()) {

                        success = ((IComparable)argument).CompareTo(target) < 0;
                    }
                    else {

                        success = false;
                    }

                    message = message ??
                        $"{argument} < {target} is false";

                    break;
                case EnumComparisonOperations.GreaterThanOrEqualTo:

                    if (outType == typeof(int)) {

                        success = (int)argument >= (int)target;

                    }
                    else if (outType == typeof(double)) {

                        success = (double)argument >= (double)target;

                    }
                    else if (outType == typeof(decimal)) {

                        success = (decimal)argument >= (decimal)target;

                    }
                    else if (outType.IsIComparable<object>()) {

                        success = ((IComparable<object>)argument).CompareTo(target) >= 0;
                    }
                    else if (outType.IsIComparable()) {

                        success = ((IComparable)argument).CompareTo(target) >= 0;
                    }
                    else {

                        success = false;
                    }

                    message = message ??
                        $"{argument} >= {target} is false";

                    break;

                case EnumComparisonOperations.SmallerThanOrEqualTo:

                    if (outType == typeof(int)) {

                        success = (int)argument <= (int)target;

                    }
                    else if (outType == typeof(double)) {

                        success = (double)argument <= (double)target;

                    }
                    else if (outType == typeof(decimal)) {

                        success = (decimal)argument <= (decimal)target;

                    }
                    else if (outType.IsIComparable<object>()) {

                        success = ((IComparable<object>)argument).CompareTo(target) <= 0;
                    }
                    else if (outType.IsIComparable()) {

                        success = ((IComparable)argument).CompareTo(target) <= 0;
                    }
                    else {

                        success = false;
                    }

                    message = message ??
                        $"{argument} <= {target} is false";

                    break;

                default:
                    success = false;
                    message = $"{comparison} is not a valid operation for {nameof(VerifyValue)}";
                    break;
            }

            if (!success) {

                var exception = (TException)Activator.CreateInstance(typeof(TException), message);
                throw exception;
            }

            return this;
        }

        public virtual IContractValidation VerifyRange<TException>(
            object value,
            object left,
            object right,
            EnumRangeCheck rangeCheck,
            string message = @"value range check failed") where TException : Exception {

            var success = true;

            var typeOfValue = value.GetType();
            success = typeOfValue.IsTypeOf(left, out Type outType);

            if (!success) {

                message = $"{nameof(VerifyRange)} cannot compare {value} of type {typeOfValue} to {left} of type {outType}";
                var exception = (TException)Activator.CreateInstance(typeof(TException), message);
                throw exception;
            }

            success = typeOfValue.IsTypeOf(right, out outType);

            if (!success) {

                message = $"{nameof(VerifyRange)} cannot compare {value} of type {typeOfValue} to {right} of type {outType}";
                var exception = (TException)Activator.CreateInstance(typeof(TException), message);
                throw exception;
            }

            switch (rangeCheck) {

                case EnumRangeCheck.InRange:

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

                case EnumRangeCheck.OutOfRange:

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

                var exception = (TException)Activator.CreateInstance(typeof(TException), message);
                throw exception;
            }

            return this;    
        }
    }
}
