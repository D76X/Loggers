using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using LogXtreme.WinDsk.TestDataGrid.Models;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    /// <summary>
    /// 
    /// Refs
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
    /// </summary>
    public class DataSourceViewModel: INotifyPropertyChanged {

        private ObservableCollection<string> sampleHeader;
        private ObservableCollection<ISample> samples;

        public DataSourceViewModel() {

            this.sampleHeader = new ObservableCollection<string>();
            this.samples = new ObservableCollection<ISample>();

            this.sampleHeader.Add("CHN0");
            this.sampleHeader.Add("CHN1");
            this.sampleHeader.Add("CHN2");

            this.samples.Add(new Sample(new string[3] {"2", "1", "2" }));
            this.samples.Add(new Sample(new string[3] { "2", "10", "20" }));
            this.samples.Add(new Sample(new string[3] { "3", "100", "200" }));
        }

        public ObservableCollection<string> SampleHeader => this.sampleHeader;

        public ObservableCollection<ISample> Samples {

            get { return this.samples; }

            set {

                if(value != this.samples) {
                    this.samples = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
