using System;
using System.ComponentModel.Composition;
using System.Windows;
using WpfEmf.Interfaces;
using WpfEmf.ViewModels;

/// <summary>
/// http://stackoverflow.com/questions/12204614/load-wpf-ui-into-mvvm-application-from-plug-in-assembly
/// </summary>
namespace WpfEmf.Plugin.Customer.WpfControlLibrary {

    [Export(typeof(IBasePlugin))]
    public class PluginCustomer : IBasePlugin {

        private CustomerViewModel _viewModel { get; set; }

        private ResourceDictionary _viewDictionary = new ResourceDictionary();


        /// <summary>
        /// Use importing constructor if you want to import MEF dependencies
        /// at construction time as explaied here 
        /// http://www.brendanforster.com/mef-import-vs-importingconstructor.html
        /// </summary>
        public PluginCustomer() {

            // there exist two URI styles - the first is an example of PACK URI  

            ResourceDictionary testResourceDisctionary = new ResourceDictionary();
            testResourceDisctionary.Source = new Uri("pack://application:,,,/WpfEmf.Plugin.Customer.WpfControlLibrary;component/CustomerResourceDictionary.xaml", UriKind.RelativeOrAbsolute);

            this._viewDictionary.Source = new Uri("/WpfEmf.Plugin.Customer.WpfControlLibrary;component/CustomerResourceDictionary.xaml", UriKind.RelativeOrAbsolute);
        }

        public ResourceDictionary View {
            get {
                return this._viewDictionary;
            }
        }

        public WorkSpaceViewModel ViewModel {
            get {
                return this._viewModel == null ? this._viewModel = new CustomerViewModel() : this._viewModel;
            }
        }
    }
}
