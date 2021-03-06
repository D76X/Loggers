﻿using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Describes a generator of values.
    /// </summary>
    public interface IGeneratorDescriptorModel {

        IEnumerable<string> SourceNames { get; }
        int Length { get; }
    }
}