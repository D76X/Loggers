using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    public interface ISampleDescriptor {

        IEnumerable<string> ValueNames { get; }   
        int Length { get; }
    }
}
