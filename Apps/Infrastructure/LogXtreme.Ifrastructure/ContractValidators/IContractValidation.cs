using System;

namespace LogXtreme.Infrastructure.ContractValidators {

    /// <summary>
    /// Refs
    /// https://www.daedtech.com/poor-mans-code-contracts/
    /// </summary>
    public interface IContractValidation {

        IContractValidation NotNull<TException>(
            object argument,
            string argumentName,
            string message = null) where TException : Exception;

        IContractValidation NotNull<TException>(
            params object[] arguments) where TException : Exception;

        IContractValidation NotNullOrEmpty<TException>(
            string argument,
            string argumentName,
            string message = null) where TException : Exception;

        IContractValidation NotNullOrEmptyOrWhiteSpace<TException>(
            string argument,
            string argumentName,
            string message = null) where TException : Exception;

        IContractValidation VerifyValue<TException>(
            object argument,
            string argumentName,
            object target,
            EnumComparisonOperations comparison,
            string message = null) where TException : Exception;

        IContractValidation VerifyRange<TException>(
            object value,
            object left,
            object right,
            EnumRangeCheck rangeCheck,
            string message = null) where TException : Exception;
    }
}
