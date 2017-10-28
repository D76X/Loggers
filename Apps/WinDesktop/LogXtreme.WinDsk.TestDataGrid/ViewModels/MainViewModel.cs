using LogXtreme.WinDsk.TestDataGrid.Models;
using LogXtreme.WinDsk.TestDataGrid.Utils;
using System.Collections.ObjectModel;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {
    public class MainViewModel {

        ObservableCollection<object> viewModels;

        public MainViewModel() {

            this.viewModels = new ObservableCollection<object>();
            this.viewModels.Add(new DataGrid1ViewModel());
            this.viewModels.Add(new DataGrid2ViewModel());

            // TODO requirements for a datasource
            // a generator has a generator descriptor 
            // a generator descriptor can be used to create a sample descriptor
            // a sample generator and a sample descriptor are used to create a datasoure
            // a datasource is exposed to the UI via a DataSourceViewModel
            // user can change the sample descriptor for the generator used by the datasource using the UI
            // users can limit the size of the datagrid via the UI

            var generatorDescriptor1 = new RandomGeneratorDescriptor(new string[] { "CHN0", "CHN1", "CHN2" });
            var sampleGenerator1 = new RandomGenerator(generatorDescriptor1);
            var sampleDescriptor1 = new SampleDescriptor(sampleGenerator1.Descriptor);            
            var dataSource1 = new DataSource1(sampleDescriptor1, sampleGenerator1);
            this.viewModels.Add(new DataSourceViewModel1(dataSource1));
            
            //var 
            //this.viewModels.Add(new DataSourceViewModel2(new DataSource2()));
        }

        public ObservableCollection<object> ViewModels {
            get {
                return this.viewModels;
            }
        }
    }
}
