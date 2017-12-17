using LogXtreme.WinDsk.TestDataGrid.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DataAnnotations = System.ComponentModel.DataAnnotations;

namespace LogXtreme.WinDsk.TestDataGrid.AbstractClasses {

    public class ValidatableBindableBase :
        BindableBase,
        INotifyDataErrorInfo {       

        protected Func<(bool IsValid, IEnumerable<ValidationData> ValidationData)> ViewModelValidation {
            get;
            set;
        }

        private Dictionary<string, List<ValidationData>> errors =
            new Dictionary<string, List<ValidationData>>();

        public bool HasErrors => this.errors.Count > 0;

        public IEnumerable<ValidationData> Errors => this.errors.Values.SelectMany(v => v);

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
            => this.errors.ContainsKey(propertyName) ?
            this.errors[propertyName] :
            null;

        private void ValidateViewModel<T>(
            string propertyName,
            T value) {

            if (ViewModelValidation == null) {
                return;
            }

            var result = ViewModelValidation();

            if (!result.IsValid) {
                errors.Remove(@"ViewModelValidationError");
                this.errors[@"ViewModelValidationError"] = result.ValidationData.ToList();
            }
            else {
                errors.Remove(@"ViewModelValidationError");
            }
        }

        /// <summary>
        /// Implementation based on Data Annotations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        private void ValidateProperty<T>(
        string propertyName,
        T value) {

            var results = new List<DataAnnotations.ValidationResult>();

            DataAnnotations.ValidationContext context =
                new DataAnnotations.ValidationContext(this);

            context.MemberName = propertyName;

            DataAnnotations.Validator.TryValidateProperty(value, context, results);

            if (results.Any()) {

                this.errors[propertyName] = results.Select(c => new ValidationData(c.ErrorMessage, ValidationErrorSeverity.Error)).ToList();
            }
            else {
                errors.Remove(propertyName);
            }

            this.ValidateViewModel(propertyName, value);

            this.OnPropertyChanged(nameof(HasErrors));
            this.OnPropertyChanged(nameof(Errors));

            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected override bool SetProperty<T>(
            ref T storage,
            T value,
            [CallerMemberName]
            string propertyName = null) {

            var propertyValueChanged = base.SetProperty(ref storage, value, propertyName);
            ValidateProperty(propertyName, value);          
            return propertyValueChanged;
        }
    }
}
