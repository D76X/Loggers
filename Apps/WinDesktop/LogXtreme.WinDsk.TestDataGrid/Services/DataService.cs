
using System;
using System.Collections.Generic;
using LogXtreme.WinDsk.Infrastructure.Services;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System.Reactive.Linq;
using LogXtreme.Extensions;
using System.Reactive.Subjects;

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
            private object onStartDataReadsLock = new Object();
            private EventHandler<IConnectableObservable<IDataModel>> onStartDataReads;

            private object onStopDataReadsLock = new Object();
            public DataSourceModel(ISampleSourceModel sampleSourceModel) {

                this.sampleSourceModel = sampleSourceModel;

                this.dataDescriptorModel =
                    new DataDescriptorModel(this.sampleSourceModel.SampleDescriptor);
            }

            public IDataDescriptorModel DataDescriptor => this.dataDescriptorModel;

            public event EventHandler OnStopDataReads;

            event EventHandler<IConnectableObservable<IDataModel>> IDataSourceModel.OnStartDataReads {
                add {
                    lock (this.onStartDataReadsLock) {
                        this.onStartDataReads += value;
                    }
                }

                remove {
                    lock (this.onStartDataReadsLock) {
                        this.onStartDataReads -= value;
                    }
                }
            }

            public void StartDataReads(int count) {

                var observableData = this.sampleSourceModel
                    .GetSamples(count)
                    .Select(s => {

                        var dataModel = new DataModel(s);

                        var x = dataModel.Values.Stringify(StringExtentions.SingleSpace);

                        return dataModel;

                    });

                var connectableData = observableData.Publish();
                this.onStartDataReads?.Invoke(this, connectableData);
            }

            public void StopDataReads() {
                this.OnStopDataReads?.Invoke(this, new EventArgs());
            }
        }

        public IDataSourceModel GenerateDataSourceModel(
            ISampleSourceModel sampleSource) {

            return new DataSourceModel(sampleSource);
        }
    }
}
