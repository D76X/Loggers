using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DataAnnotations = System.ComponentModel.DataAnnotations;

namespace LogXtreme.WinDsk.Infrastructure.Interfaces {

    /// <summary>
    /// A abstract base class based on the Prism BindableBase which implemets
    /// INotifyDataErrorInfo to provide validation according to .NET 4.5 standards.
    ///
    /// Refs
    /// https://blog.magnusmontin.net/2013/08/26/data-validation-in-wpf/
    /// https://app.pluralsight.com/player?course=wpf-mvvm-in-depth&author=brian-noyes&name=wpf-mvvm-in-depth-m6&clip=6&mode=live 
    /// https://social.technet.microsoft.com/wiki/contents/articles/19490.wpf-4-5-validating-data-in-using-the-inotifydataerrorinfo-interface.aspx
    /// 
    /// This implementation makes use of a validate property method in which the
    /// System.ComponentModel.DataAnnotations.ValidationContext is used. The project
    /// must reference the System.ComponentModel.DataAnnotations which must be added 
    /// manually or by the quick fix feature in Visual Studio.
    /// 
    /// </summary>
    public class ValidatableBindableBase :
        BindableBase,
        INotifyDataErrorInfo {

        protected Func<(bool IsValid, IEnumerable<string> ErrorMessages)> ViewModelValidation {
            get;
            set;
        }

        private Dictionary<string, List<string>> errors =
            new Dictionary<string, List<string>>();

        public bool HasErrors => this.errors.Count > 0;

        public IEnumerable<string> Errors => this.errors.Values.SelectMany(v => v);

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
                this.errors[@"ViewModelValidationError"] = result.ErrorMessages.ToList();
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

                this.errors[propertyName] = results.Select(c => c.ErrorMessage).ToList();
            }
            else {
                errors.Remove(propertyName);
            }

            this.ValidateViewModel(propertyName, value);

            this.RaisePropertyChanged(nameof(HasErrors));
            this.RaisePropertyChanged(nameof(Errors));

            this.ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
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
