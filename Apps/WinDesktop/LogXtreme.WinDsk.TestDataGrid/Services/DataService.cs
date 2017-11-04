
using System;
using System.Collections.Generic;
using LogXtreme.WinDsk.Infrastructure.Services;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System.Reactive.Linq;

namespace LogXtreme.WinDsk.TestDataGrid.Services {

    public class DataService : IDataService {

        private class DataModel : IDataModel {

            private readonly ISampleModel sampleModel;

            public DataModel(ISampleModel sampleModel) {
                this.sampleModel = sampleModel;
            }

            public IEnumerable<string> Values => this.sampleModel.Values;
        }

        private class DataDescriptorModel : IDataDescriptorModel {

            private readonly ISampleDescriptorModel sampleDescriptorModel;

            public DataDescriptorModel(ISampleDescriptorModel sampleDescriptorModel) {
                this.sampleDescriptorModel = sampleDescriptorModel;
            }

            public IEnumerable<string> ValueNames => this.sampleDescriptorModel.ValueNames;

            public int Length => this.sampleDescriptorModel.Length;            
        }

        private class DataSourceModel : IDataSourceModel {            

            private readonly ISampleSourceModel sampleSourceModel;
            private readonly IDataDescriptorModel dataDescriptorModel;

            public DataSourceModel(ISampleSourceModel sampleSourceModel) {

                this.sampleSourceModel = sampleSourceModel;
                this.dataDescriptorModel = new DataDescriptorModel(this.sampleSourceModel.SampleDescriptor);
            }

            public IDataDescriptorModel DataDescriptor => this.dataDescriptorModel;

            public IDataModel GetData() {
                return new DataModel(this.sampleSourceModel.GetSample());
            }

            public IObservable<IDataModel> GetDatas() {

                // http://www.introtorx.com/content/v1.0.10621.0/08_Transformation.html
                return this.sampleSourceModel
                    .GetSamples()
                    .Select(s => new DataModel(s));
            }
        }        

        public IDataSourceModel GenerateDataSourceModel(
            ISampleSourceModel sampleSource) {

            return new DataSourceModel(sampleSource);
        }
    }
}
