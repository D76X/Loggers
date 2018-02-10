using LogXtreme.WinDsk.Infrastructure.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DataAnnotations = System.ComponentModel.DataAnnotations;

namespace LogXtreme.WinDsk.Infrastructure.Validation {

    /// <summary>
    /// A class that supports validation and error reporting according to INotifyDataErrorInfo.
    /// and INotifyPropertyChanged via the NotifyPropertyChangedBase. This is the same as the 
    /// ValidatableBindableBase but its bindable implementation does not rely on the class
    /// Prism.BindableBase instead it uses NotifyPropertyChangedBase which is provides the same
    /// function as Prism.BindableBase without requiring a reference to Prism by the consumer
    /// code.
    /// </summary>
    public class ValidatableNotifyPropertyChangedBase :
        NotifyPropertyChangedBase,
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

        protected override bool SetPropertyBase<T>(
            ref T storage,
            T value,
            [CallerMemberName] string propertyName = null) {

            // call the base.SetPropertyBase to set the value and get the return value
            // avoid stack overflow do not use this.SetPropertyBase of course.
            var propertyValueChanged = base.SetPropertyBase(ref storage, value, propertyName);
            ValidateProperty(propertyName, value);
            
            //TODO add support for server side validation (async)
            return propertyValueChanged;
        }
    }
}

