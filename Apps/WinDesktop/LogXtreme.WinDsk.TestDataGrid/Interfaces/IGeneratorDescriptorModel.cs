using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {
    public interface IGeneratorDescriptorModel {

        IEnumerable<string> SourceNames { get; }
        int Length { get; }
    }
}