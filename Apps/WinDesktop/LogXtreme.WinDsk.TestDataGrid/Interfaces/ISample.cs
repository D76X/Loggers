using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    public interface ISample {

        //string id { get; }

        //DateTime DateTime {get;}

        IEnumerable<string> Values { get; }
    }
}
