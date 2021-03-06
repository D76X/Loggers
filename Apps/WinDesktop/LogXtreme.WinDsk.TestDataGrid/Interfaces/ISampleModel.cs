﻿using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Model fo a sample.
    /// </summary>
    public interface ISampleModel {

        IEnumerable<string> Values { get; }
    }
}
