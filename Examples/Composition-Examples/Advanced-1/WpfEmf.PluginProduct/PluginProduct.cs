using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfEmf.Interfaces;
using WpfEmf.ViewModels;

namespace WpfEmf.PluginProduct {
    [Export(typeof(IBasePlugin))]
    public class PluginProduct {

        [Import]
        private ProductViewModel _viewModel { get; set; }

        private ResourceDictionary _viewDictionary = new ResourceDictionary();

        [ImportingConstructor]
        public PluginProduct() { }

        public ResourceDictionary View {
            get {
                return this._viewDictionary;
            }
        }

        public WorkSpaceViewModel ViewModel {
            get {
                return this._viewModel;

            }
        }
    }
}
