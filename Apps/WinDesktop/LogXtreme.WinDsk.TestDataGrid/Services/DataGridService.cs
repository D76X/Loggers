using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using LogXtreme.WinDsk.TestDataGrid.Models;
using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Services {

    public class DataGridService : IDataGridService {

        public IDataGridStructureModel GenerateDataGridStructureModel(
            IDataSourceModel dataSourceModel) {

            var dataGridColumns = new List<IDataGridColumnModel>(); 

            IDataDescriptorModel dataDescriptorModel =
                dataSourceModel.DataDescriptor;

            foreach (var valueName in dataDescriptorModel.ValueNames) {

                dataGridColumns.Add(
                    new DataGridColumnModel() {
                        Header = valueName,
                        IsVisible = true
                    });
            }
            
            return new DataGridStructureModel(dataGridColumns);
        }
    }
}
