using LogXtreme.WinDsk.TestDataGrid.Models;
using System.Collections.ObjectModel;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {
    public class MainViewModel {

        ObservableCollection<object> viewModels;

        public MainViewModel() {

            this.viewModels = new ObservableCollection<object>();

            // Tab 1
            this.viewModels.Add(new DataGrid1ViewModel());

            // Tab 2
            this.viewModels.Add(new DataGrid2ViewModel());

            
            // Tab 3

            // TODO requirements for a datasource
            // a generator has a generator descriptor 
            // a generator descriptor can be used to create a sample descriptor
            // a sample generator and a sample descriptor are used to create a datasoure
            // a datasource is exposed to the UI via a DataSourceViewModel
            // user can change the sample descriptor for the generator used by the datasource using the UI
            // users can limit the size of the datagrid via the UI

            var generatorDescriptor1 = new RandomGeneratorDescriptorModel(new string[] { "CHN0", "CHN1", "CHN2" });
            var sampleGenerator1 = new RandomGeneratorModel(generatorDescriptor1);
            var sampleDescriptor1 = new SampleDescriptorModel(sampleGenerator1.Descriptor);            
            var dataSource1 = new DataSource1Model(sampleDescriptor1, sampleGenerator1);
            this.viewModels.Add(new DataSourceViewModel1(dataSource1));

            // Tab 4
            this.viewModels.Add(new DataTab4ViewModel());
        }

        public ObservableCollection<object> ViewModels {
            get {
                return this.viewModels;
            }
        }
    }
}
