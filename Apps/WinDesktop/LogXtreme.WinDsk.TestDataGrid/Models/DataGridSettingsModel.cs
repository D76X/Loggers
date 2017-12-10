using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using LogXtreme.Infrastructure.ContractValidators;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    public class DataGridSettingsModel : IDataGridSettingsModel {

        private int numberOfItemsToDisplay;
        private ResizeObservableCollectionCycleModeEnum cycleMode;

        public DataGridSettingsModel(
            int numberOfItemsToDisplay,
            ResizeObservableCollectionCycleModeEnum cycleMode) {

            numberOfItemsToDisplay
                .Validate(nameof(numberOfItemsToDisplay))
                .GreaterThanOrEqualTo(0);

            this.numberOfItemsToDisplay = numberOfItemsToDisplay;
            this.cycleMode = cycleMode;
        }

        public int NumberOfItemsToDisplay {

            get => this.numberOfItemsToDisplay;

            set {

                value
                    .Validate(nameof(NumberOfItemsToDisplay))
                    .GreaterThanOrEqualTo(0);
                
                    this.numberOfItemsToDisplay = value;
            }
        }

        public ResizeObservableCollectionCycleModeEnum CycleMode {

            get => this.cycleMode;

            set {

                this.cycleMode = value;
            }
        }
    }
}
