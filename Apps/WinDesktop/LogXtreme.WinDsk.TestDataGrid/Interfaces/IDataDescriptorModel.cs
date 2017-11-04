using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Description of some data.
    /// </summary>
    public interface IDataDescriptorModel {

        IEnumerable<string> ValueNames { get; }

        int Length { get; }
    }
}