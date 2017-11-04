namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    public interface IDataGridService {

        IDataGridStructureModel GenerateDataGridStructureModel(
            IDataSourceModel dataSourceModel);
    }
}
