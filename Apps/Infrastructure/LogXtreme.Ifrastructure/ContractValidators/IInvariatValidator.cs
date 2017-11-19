using System;

namespace LogXtreme.Ifrastructure.ContractValidators {

    /// <summary>
    /// Refs
    /// https://www.daedtech.com/poor-mans-code-contracts/
    /// </summary>
    public interface IInvariantValidator {

        void VerifyNonNull<TException>(
            object argument,
            string message = null) where TException : Exception;

        void VerifyParamsNonNull<TException>(
            params object[] arguments) where TException : Exception;

        void VerifyNotNullOrEmpty<TException>(
            string target,
            string message = null) where TException : Exception;

        void VerifyNotNullOrEmptyOrWhiteSpace<TException>(
            string target,
            string message = null) where TException : Exception;

        void VerifyValue<TException>(
            object value,
            object compareTo,
            InvariantValidatorComparisonEnum comparison,
            string message = null) where TException : Exception;

        void VerifyRange<TException>(
            object value,
            object left,
            object right,
            InvariantValidatorRangeCheckEnum rangeCheck,
            string message = null) where TException : Exception;
    }
}
