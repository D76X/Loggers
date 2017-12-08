using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using LogXtreme.Infrastructure.ContractValidators;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    public class DataGridSettingsModel : IDataGridSettingsModel {

        private int numberOfItemsToDisplay;
        private ResizeObservableCollectionCycleModeEnum cycleMode;

        public DataGridSettingsModel(
            int numberOfItemsToDisplay,
            ResizeObservableCollectionCycleModeEnum cycleMode = 
            ResizeObservableCollectionCycleModeEnum.Queue) {

            numberOfItemsToDisplay
                .Validate(nameof(numberOfItemsToDisplay))
                .GreaterThanOrEqualTo(0);

            this.numberOfItemsToDisplay = numberOfItemsToDisplay;
            this.cycleMode = cycleMode;
        }

        public int NumberOfItemsToDisplay => this.numberOfItemsToDisplay;
        public ResizeObservableCollectionCycleModeEnum CycleMode => this.cycleMode;
    }
}
