namespace LogXtreme.Ifrastructure.ContractValidators {

    /// <summary>
    /// Refs
    /// https://www.daedtech.com/poor-mans-code-contracts/
    /// </summary>
    public interface IInvariantValidator {

        void VerifyNonNull<T>(
            T argument,
            string message) where T : class;

        void VerifyParamsNonNull(params object[] arguments);

        void VerifyNotNullOrEmpty(
            string target,
            string message);

        void VerifyNotNullOrEmptyOrWhiteSpace(
            string target,
            string message);
    }
}
