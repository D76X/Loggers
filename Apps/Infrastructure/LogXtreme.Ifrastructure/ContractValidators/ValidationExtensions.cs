using LogXtreme.Infrastructure.ContractValidators;
using System;

namespace LogXtreme.Infrastructure.ContractValidators {

    /// <summary>
    /// Provides a convenient fluent API for contract validation.
    /// </summary>
    public static class ValidationExtensions {

        public static ContractValidator<V> Validate<V>(
            this V item,
            string argName) {

            return ContractValidator<V>
                .CreateValidator(item, argName);
        }

        public static ContractValidator<V> NotNull<V>(
            this ContractValidator<V> validationItem) =>
            validationItem.NotNull < ArgumentNullException>(
                validationItem.Value,
                validationItem.Name) as 
            ContractValidator<V>;

        public static ContractValidator<V> NotEmpty<V>(
            this ContractValidator<V> validationItem) =>
            validationItem.NotNullOrEmpty<ArgumentException>(
                validationItem.Value as string,
                validationItem.Name,
                $"{validationItem.Name} is an empty string") 
            as ContractValidator<V>;

        public static ContractValidator<V> NotWhiteSpace<V>(
            this ContractValidator<V> validationItem) =>
            validationItem.NotNullOrEmptyOrWhiteSpace<ArgumentException>(
                validationItem.Value as string,
                validationItem.Name,
                $"{validationItem.Name} is white space") 
            as ContractValidator<V>;

        public static ContractValidator<V> EqualTo<V>(
            this ContractValidator<V> validationItem,
            V val) => validationItem.CompareValues(
                EnumComparisonOperations.EqualTo, 
                val);

        public static ContractValidator<V> GreaterThan<V>(
            this ContractValidator<V> validationItem,
            V val) => validationItem.CompareValues(EnumComparisonOperations.GreaterThan, val);

        public static ContractValidator<V> GreaterThanOrEqualTo<V>(
            this ContractValidator<V> validationItem,
            V val) => validationItem.CompareValues(EnumComparisonOperations.GreaterThanOrEqualTo, val);

        public static ContractValidator<V> SmallerThan<V>(
            this ContractValidator<V> validationItem,
            V val) => validationItem.CompareValues(EnumComparisonOperations.SmallerThan, val);

        public static ContractValidator<V> SmallerThanOrEqualTo<V>(
            this ContractValidator<V> validationItem,
            V val) => validationItem.CompareValues(EnumComparisonOperations.SmallerThanOrEqualTo, val);

        private static ContractValidator<V> CompareValues<V>(
            this ContractValidator<V> validationItem,            
            EnumComparisonOperations comparison,
            V val) {

            switch (comparison) {

                case EnumComparisonOperations.EqualTo:

                    validationItem.VerifyValue<ArgumentException>(
                            validationItem.Value,
                            validationItem.Name,
                            val,
                            EnumComparisonOperations.EqualTo,
                            $"{validationItem.Name} should be equal to {val} instead is {validationItem.Value}");

                    break;

                case EnumComparisonOperations.GreaterThan:

                    validationItem.VerifyValue<ArgumentException>(
                            validationItem.Value,
                            validationItem.Name,
                            val,
                            EnumComparisonOperations.GreaterThan,
                            $"{validationItem.Name} should be greater than {val} instead is {validationItem.Value}");

                    break;

                case EnumComparisonOperations.GreaterThanOrEqualTo:

                    validationItem.VerifyValue<ArgumentException>(
                            validationItem.Value,
                            validationItem.Name,
                            val,
                            EnumComparisonOperations.GreaterThanOrEqualTo,
                            $"{validationItem.Name} should be greater than or equal to {val} instead is {validationItem.Value}");

                    break;

                case EnumComparisonOperations.SmallerThan:

                    validationItem.VerifyValue<ArgumentException>(
                            validationItem.Value,
                            validationItem.Name,
                            val,
                            EnumComparisonOperations.SmallerThan,
                            $"{validationItem.Name} should be smaller than {val} instead is {validationItem.Value}");

                    break;
                
                case EnumComparisonOperations.SmallerThanOrEqualTo:

                    validationItem.VerifyValue<ArgumentException>(
                            validationItem.Value,
                            validationItem.Name,
                            val,
                            EnumComparisonOperations.SmallerThanOrEqualTo,
                            $"{validationItem.Name} should be smaller than or equal to {val} instead is {validationItem.Value}");

                    break;

                default:
                    throw new ArgumentException($"{nameof(ValidationExtensions)} the enumeration value {comparison} is not an expected value.");                    
            }

            return validationItem;
        }
    }
}
