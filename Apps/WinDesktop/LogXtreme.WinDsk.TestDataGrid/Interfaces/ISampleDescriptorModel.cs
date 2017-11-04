using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Description of a sample.
    /// </summary>
    public interface ISampleDescriptorModel {

        IEnumerable<string> ValueNames { get; }   
        int Length { get; }
    }
}
