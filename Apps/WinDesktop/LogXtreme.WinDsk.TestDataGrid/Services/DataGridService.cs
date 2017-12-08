using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using LogXtreme.WinDsk.TestDataGrid.Models;
using LogXtreme.WinDsk.TestDataGrid.ViewModels;
using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Services {

    public class DataGridService : IDataGridService {

        public IDataGridViewModel CreateDataGridViewModel(
            IDataSourceModel dataSourceModel,
            IDataGridSettingsModel dataGridSettingsModel = null) {          

            IDataGridStructureModel dataGridStructureModel =
                this.GenerateDataGridStructureModel(dataSourceModel);

            IDataGridSettingsModel dGridSettingsModel =
                dataGridSettingsModel ??
                new DataGridSettingsModel(0);

            var dataGridModel = new DataGridModel(
                dataGridStructureModel,
                dGridSettingsModel);

            return new DataGridViewModel(
                dataGridModel,
                dataSourceModel);
        }

        public IDataGridStructureModel GenerateDataGridStructureModel(
            IDataSourceModel dataSourceModel) {

            var dataGridColumns = new List<IDataGridColumnModel>(); 

            IDataDescriptorModel dataDescriptorModel =
                dataSourceModel.DataDescriptor;

            foreach (var valueName in dataDescriptorModel.ValueNames) {

                dataGridColumns.Add(
                    new DataGridColumnModel(
                        new HeaderModel(valueName),
                        new DataGridColumnSettingsModel() {
                            Visible = true}));
            }
            
            return new DataGridStructureModel(dataGridColumns);
        }
    }
}
