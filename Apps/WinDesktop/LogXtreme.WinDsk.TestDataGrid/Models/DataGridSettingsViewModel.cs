
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    /// <summary>
    /// This view model derives from the local implementation of ValidatableBindableBase
    /// 
    /// INotifyDataErrorInfo
    /// https://app.pluralsight.com/player?course=wpf-mvvm-in-depth&author=brian-noyes&name=wpf-mvvm-in-depth-m6&clip=6&mode=live
    /// 
    /// </summary>
    public class DataGridSettingsViewModel :
        LogXtreme.WinDsk.TestDataGrid.AbstractClasses.ValidatableBindableBase,
        IDataGridSettingsViewModel {

        private readonly IDataGridSettingsModel dataGridSettingsModel;

        private int numberOfItemsToDisplay;
        private ResizeObservableCollectionCycleModeEnum cycleMode;

        public DataGridSettingsViewModel(
            IDataGridSettingsModel dataGridSettingsModel) {

            this.dataGridSettingsModel = dataGridSettingsModel;
            this.numberOfItemsToDisplay = this.dataGridSettingsModel.NumberOfItemsToDisplay;
            this.cycleMode = this.dataGridSettingsModel.CycleMode;
            this.ViewModelValidation = this.ValidateViewModel;
        }

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = @"Number of Items must be grater or equal to 0")]
        public int NumberOfItemsToDisplay {

            get => this.numberOfItemsToDisplay;

            set {

                var updated = this.SetProperty(
                    ref this.numberOfItemsToDisplay,
                    value);

                if (updated && !this.HasErrors) {
                    this.RaiseOnGridSettingsChanged();
                }
            }
        }

        [Required]
        public ResizeObservableCollectionCycleModeEnum CycleMode {

            get => this.cycleMode;

            set {

                var updated = this.SetProperty(
                    ref this.cycleMode,
                    value);

                if (updated && !this.HasErrors) {
                    this.RaiseOnGridSettingsChanged();
                }
            }
        }

        public event EventHandler OnGridSettingsChanged;

        private void RaiseOnGridSettingsChanged() {
            this.OnGridSettingsChanged?.Invoke(this, EventArgs.Empty);
        }

        private void SetModel() {

            this.dataGridSettingsModel.NumberOfItemsToDisplay = this.NumberOfItemsToDisplay;
            this.dataGridSettingsModel.CycleMode = this.CycleMode;
        }

        /// <summary>
        /// All the validation logic that is not easy to specify via data
        /// attributes on single properties of the view model can be dealt
        /// by this method. For, example cross validation of properties
        /// may be executed as the values assigned to the individual 
        /// properties may be valid but at the same time their values taken
        /// in combination may not amount to a valid view model. 
        /// </summary>
        /// <returns></returns>
        private (bool IsValid, IEnumerable<string> ErrorMessage) ValidateViewModel() {

            var isValid = false;
            var errorMessages = new List<string>();

            if (this.NumberOfItemsToDisplay == 0 &&
                this.CycleMode != ResizeObservableCollectionCycleModeEnum.None) {

                isValid = false;
                errorMessages.Add($"{nameof(NumberOfItemsToDisplay)} = 0 requires {nameof(CycleMode)} = {ResizeObservableCollectionCycleModeEnum.None}");
            }
            else if (this.NumberOfItemsToDisplay > 0 &&
                this.CycleMode == ResizeObservableCollectionCycleModeEnum.None) {

                isValid = false;
                errorMessages.Add($"{nameof(NumberOfItemsToDisplay)} > 0 requires {nameof(CycleMode)} != {ResizeObservableCollectionCycleModeEnum.None}");
            }
            else {
                isValid = true;
            }

            if (isValid && !this.HasErrors) {
                this.SetModel();
            }

            return (isValid, errorMessages);
        }

        #region IDisposable Support     

        private bool disposedValue = false; // To detect redundant calls       

        protected virtual void Dispose(bool disposing) {

            if (!disposedValue) {

                if (disposing) {

                    // dispose of subscriptions, etc.
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose() {
            Dispose(true);
        }

        #endregion
    }
}
