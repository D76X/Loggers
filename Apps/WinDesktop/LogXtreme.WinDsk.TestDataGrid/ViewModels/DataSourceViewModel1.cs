using LogXtreme.WinDsk.Infrastructure.Commands;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using LogXtreme.WinDsk.TestDataGrid.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    /// <summary>
    /// 
    /// Refs
    /// 
    /// Commands
    /// https://msdn.microsoft.com/en-us/library/gg405484(v=pandp.40).aspx
    /// https://stackoverflow.com/questions/1468791/wpf-icommand-mvvm-implementation
    /// 
    /// Dynamic DataGrid
    /// https://blogs.msmvps.com/deborahk/populating-a-datagrid-with-dynamic-columns-in-a-silverlight-application-using-mvvm/
    /// https://stackoverflow.com/questions/320089/how-do-i-bind-a-wpf-datagrid-to-a-variable-number-of-columns?rq=1
    /// https://stackoverflow.com/questions/18452134/filling-a-datagrid-with-dynamic-columns
    /// https://msdn.microsoft.com/en-us/library/system.data.datatable.aspx
    /// https://stackoverflow.com/questions/1834454/wpf-data-binding-example-of-custom-type-descriptor
    /// https://stackoverflow.com/questions/12606820/dynamic-datagrid-optional-columns?rq=1
    /// https://stackoverflow.com/questions/35682417/dynamic-datagrid-columns-and-content-in-xaml?rq=1
    /// 
    /// INotifyPropertyChanged
    /// https://docs.microsoft.com/en-us/dotnet/framework/winforms/how-to-implement-the-inotifypropertychanged-interface
    /// 
    /// Reactive Extensiosn 
    /// https://stackoverflow.com/questions/44984730/rxandroid-whats-the-difference-between-subscribeon-and-observeon
    /// 
    /// </summary>
    public class DataSourceViewModel1 : 
        INotifyPropertyChanged, 
        IDisposable {

        private readonly ISampleSourceModel sampleSource;

        private ObservableCollection<string> sampleHeader;
        private ObservableCollection<ISampleModel> samples;
        private RelayCommand cmdGetOneSample;
        private ICommand cmdStartSampling;
        private ICommand cmdStopSampling;
        private bool readingSamples;
        private IDisposable samplesObsevable;

        public DataSourceViewModel1(ISampleSourceModel sampleSource) {

            this.sampleSource = sampleSource;

            var sampleDescriptor = sampleSource.SampleDescriptor.ValueNames;
            this.sampleHeader = new ObservableCollection<string>(sampleDescriptor);
            this.samples = new ObservableCollection<ISampleModel>();

            this.cmdGetOneSample = new RelayCommand(
                this.ExecuteGetOneSample, 
                this.CanExecuteGetOneSample);

            this.cmdStartSampling = new RelayCommand(this.ExecuteStartSampling);
            this.cmdStopSampling = new RelayCommand(this.ExecuteStopSampling);   
        }        

        public ObservableCollection<string> SampleHeader => this.sampleHeader;

        public ObservableCollection<ISampleModel> Samples {

            get { return this.samples; }

            set {

                if(value != this.samples) {
                    this.samples = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ReadingSamples {

            get { return this.readingSamples;  }

            private set {

                if(value != this.readingSamples) {
                    this.readingSamples = value;
                    this.cmdGetOneSample.RaiseCanExecuteChanged();
                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand CommandGetOneSample => this.cmdGetOneSample;
        public ICommand CommandStartSampling => this.cmdStartSampling;
        public ICommand CommandStopSampling => this.cmdStopSampling;

        private bool CanExecuteGetOneSample() {
            return !this.readingSamples;
        }

        private void ExecuteGetOneSample() {

            this.samples.Add(this.sampleSource.GetSample());
        }        

        private void ExecuteStartSampling() {

            this.samplesObsevable?.Dispose();
            this.samplesObsevable = null;            

            // put the subscription delegate on a separate thread
            // run the observation delegates on the dispatcher thread
            // to satisy the WPF STA model. 
            this.samplesObsevable = this.sampleSource.GetSamples().
            SubscribeOn(ThreadPoolScheduler.Instance).
            ObserveOn(DispatcherScheduler.Current).
            Subscribe(
                s => this.samples.Add(s),
                e => { },
                () => { });

            this.ReadingSamples = true;            
        }

        private void ExecuteStopSampling() {

            this.samplesObsevable?.Dispose();
            this.samplesObsevable = null;
            this.ReadingSamples = false;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IDisposable Support     


        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing) {

            if(!disposedValue) {

                if(disposing) {

                    this.samplesObsevable?.Dispose();
                    this.samplesObsevable = null;
                }
                
                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose() {
            Dispose(true);
        }
        #endregion
    }
}
