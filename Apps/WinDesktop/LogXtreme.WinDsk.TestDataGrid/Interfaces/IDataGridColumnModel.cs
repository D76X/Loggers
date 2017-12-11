namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Describes a column of a data grid.
    /// </summary>
    public interface IDataGridColumnModel {

        IHeaderModel Header { get; set; }

        IDataGridColumnSettingsModel ColumnSettings { get; set; }
    }
}
