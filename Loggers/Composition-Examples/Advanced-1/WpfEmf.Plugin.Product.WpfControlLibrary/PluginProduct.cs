using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfEmf.Interfaces;
using WpfEmf.ViewModels;

/// <summary>
/// http://stackoverflow.com/questions/12204614/load-wpf-ui-into-mvvm-application-from-plug-in-assembly
/// 
/// To load the resource dictionary 
/// 
/// 
/// </summary>
namespace WpfEmf.Plugin.Product.WpfControlLibrary {

    [Export(typeof(IBasePlugin))]
    public class PluginProduct: IBasePlugin {

        //[Import]
        private ProductViewModel _viewModel { get; set; }

        private ResourceDictionary _viewDictionary = new ResourceDictionary();

        [ImportingConstructor]
        public PluginProduct() {


            //var x  = Assembly.LoadFrom(@"C:\GitHub\Loggers\Loggers\Composition-Examples\Advanced-1\CompositionWpfEx4\bin\Debug\modules\WpfEmf.Plugin.Product.WpfControlLibrary.dll");


            //this._viewDictionary.Source = new Uri("pack://application:,,,/WpfEmf.Plugin.Product.WpfControlLibrary;component/TestDictionary1.baml", UriKind.RelativeOrAbsolute);
            
            //this._viewDictionary.Source = new Uri("/WpfEmf.Plugin.Product.WpfControlLibrary;component/TestDictionary1.xaml", UriKind.RelativeOrAbsolute);
            this._viewDictionary.Source = new Uri("/WpfEmf.Plugin.Product.WpfControlLibrary;component/ProductResourceDictionary.xaml", UriKind.RelativeOrAbsolute);

            //http://stackoverflow.com/questions/5069276/enumerating-files-in-an-embedded-resource-directory


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
