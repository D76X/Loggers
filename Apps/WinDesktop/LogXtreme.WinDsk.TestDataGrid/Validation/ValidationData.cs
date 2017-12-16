namespace LogXtreme.WinDsk.TestDataGrid.Validation {

    /// <summary>
    /// Refs
    /// https://social.technet.microsoft.com/wiki/contents/articles/19490.wpf-4-5-validating-data-in-using-the-inotifydataerrorinfo-interface.aspx
    /// </summary>
    public class ValidationData {

        public ValidationData(
            string validationMessage, 
            ValidationErrorSeverity severity) {

            this.Message = validationMessage;
            this.Severity = severity;
        }

        public string Message { get; private set; }
        public ValidationErrorSeverity Severity { get; private set; }
    }
}
