using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfEmf.Interfaces;
using WpfEmf.ViewModels;

namespace WpfEmf.Plugin.Product.WpfControlLibrary {

    [Export(typeof(IBasePlugin))]
    public class PluginProduct: IBasePlugin {

        //[Import]
        private ProductViewModel _viewModel { get; set; }

        private ResourceDictionary _viewDictionary = new ResourceDictionary();

        [ImportingConstructor]
        public PluginProduct() {
            //this._viewDictionary.Source = new Uri("pack://application:,,,/ProductResourceDictionary.xaml", UriKind.RelativeOrAbsolute);
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
