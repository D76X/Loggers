﻿
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

            private object onStartDataReadsLock = new Object();
            private EventHandler<IObservable<IDataModel>> onStartDataReads;

            private object onStopDataReadsLock = new Object();
            private EventHandler onStopDataReads;

            public DataSourceModel(ISampleSourceModel sampleSourceModel) {

                this.sampleSourceModel = sampleSourceModel;

                this.dataDescriptorModel = 
                    new DataDescriptorModel(this.sampleSourceModel.SampleDescriptor);
            }

            public IDataDescriptorModel DataDescriptor => this.dataDescriptorModel;

            public event EventHandler OnStopDataReads;

            event EventHandler<IObservable<IDataModel>> IDataSourceModel.OnStartDataReads {

                add {
                    lock (this.onStartDataReadsLock) {
                        this.onStartDataReads += value;
                    }
                }

                remove {
                    lock (this.onStartDataReadsLock) {
                        this.onStartDataReads += value;
                    }
                }
            }             

            public void StartDataReads(int count) {

                var observableData = this.sampleSourceModel
                    .GetSamples(count)
                    .Select(s => new DataModel(s));

                this.onStartDataReads(this, observableData);
            }

            public void StopDataReads() {
                this.onStopDataReads?.Invoke(this, new EventArgs());
            }

            //public IObservable<IDataModel> GetData(int count) {

            //    // http://www.introtorx.com/content/v1.0.10621.0/08_Transformation.html
            //    var observable = this.sampleSourceModel
            //        .GetSamples(count)
            //        .Select(s => new DataModel(s));

            //    //dataRequested?.Invoke(this, observable);

            //    return observable;
            //}

            //IObservable<IObservable<IDataModel>> GetData(int count) {

            //    var observableData = this.sampleSourceModel
            //        .GetSamples(count)
            //        .Select(s => new DataModel(s));

            //    return Observable.Return(observableData);
            //}

            //public IDataModel GetData() {
            //    return new DataModel(this.sampleSourceModel.GetSample());
            //}

            //public IObservable<IDataModel> GetDatas() {

            //    // http://www.introtorx.com/content/v1.0.10621.0/08_Transformation.html
            //    return this.sampleSourceModel
            //        .GetSamples()
            //        .Select(s => new DataModel(s));
            //}
        }

        public IDataSourceModel GenerateDataSourceModel(
            ISampleSourceModel sampleSource) {

            return new DataSourceModel(sampleSource);
        }
    }
}
