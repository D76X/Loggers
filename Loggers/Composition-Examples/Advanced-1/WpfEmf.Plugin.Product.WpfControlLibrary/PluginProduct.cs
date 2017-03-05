using System;
using System.ComponentModel.Composition;
using System.Windows;
using WpfEmf.Interfaces;
using WpfEmf.ViewModels;

/// <summary>
/// http://stackoverflow.com/questions/12204614/load-wpf-ui-into-mvvm-application-from-plug-in-assembly
/// </summary>
namespace WpfEmf.Plugin.Product.WpfControlLibrary {

    [Export(typeof(IBasePlugin))]
    public class PluginProduct: IBasePlugin {

        private ProductViewModel _viewModel { get; set; }

        private ResourceDictionary _viewDictionary = new ResourceDictionary();

        /// <summary>
        /// 
        /// </summary>
        [ImportingConstructor]
        public PluginProduct() {           

            // This is a test that shows how a resource dictionary can be parsed and loaded in memory.
            // In TestDictionary1.xaml contains a single style. It is worth noting that if the resource dictionary is left
            // with no content in it this code throes an exceptions. 
            // Notice that the URI in this case is not using the PACK notation.            
            ResourceDictionary testResourceDictionary1 = new ResourceDictionary();
            testResourceDictionary1.Source = new Uri("/WpfEmf.Plugin.Product.WpfControlLibrary;component/TestDictionary1.xaml", UriKind.RelativeOrAbsolute);

            //This is the same dictionary as above. However this time the equivalent PACK notation is used in the URI.
            ResourceDictionary testResourceDictionary2 = new ResourceDictionary();
            testResourceDictionary2.Source = new Uri("pack://application:,,,/WpfEmf.Plugin.Product.WpfControlLibrary;component/ProductResourceDictionary.xaml", UriKind.RelativeOrAbsolute);

            this._viewDictionary.Source = new Uri("/WpfEmf.Plugin.Product.WpfControlLibrary;component/ProductResourceDictionary.xaml", UriKind.RelativeOrAbsolute);           


        }

        public ResourceDictionary View {
            get {
                return this._viewDictionary;
            }
        }

        public WorkSpaceViewModel ViewModel {
            get {
                return this._viewModel == null ? this._viewModel = new ProductViewModel() : this._viewModel;
            }
        }
    }
}
