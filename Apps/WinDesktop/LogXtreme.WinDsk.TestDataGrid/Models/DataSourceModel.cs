using LogXtreme.WinDsk.TestDataGrid.Interfaces;

namespace LogXtreme.WinDsk.TestDataGrid.Models {
    public class DataSourceModel : IDataSourceModel {

        private readonly ISampleSourceModel sampleSource;

        public DataSourceModel(ISampleSourceModel sampleSource) {

            this.sampleSource = sampleSource;
        }

        public ISampleSourceModel SampleSource => this.sampleSource;
    }
}
