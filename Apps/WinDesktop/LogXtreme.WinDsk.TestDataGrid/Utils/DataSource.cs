
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using LogXtreme.WinDsk.TestDataGrid.Models;

namespace LogXtreme.WinDsk.TestDataGrid.Utils {

    public class DataSource : ISampleSource {

        public ISample GetSample() {

            return new Sample(new string[3] { "X", "Y", "Z" });
        }
    }
}
