namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Describes a column of a data grid.
    /// </summary>
    public interface IDataGridColumnModel {

        string Header { get; set; }

        bool IsVisible { get; set; }
    }
}
