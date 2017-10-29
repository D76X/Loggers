using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    public interface ISampleDescriptorModel {

        IEnumerable<string> ValueNames { get; }   
        int Length { get; }
    }
}
