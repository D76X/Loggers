using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Model of some data.
    /// </summary>
    public interface IDataModel {

        IEnumerable<string> Values { get; }
    }
}