using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {
    public interface IGeneratorDescriptor {

        IEnumerable<string> SourceNames { get; }
        int Length { get; }
    }
}