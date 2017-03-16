using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEmf.Interfaces;
using System.Windows;

/// <summary>
/// http://stackoverflow.com/questions/12204614/load-wpf-ui-into-mvvm-application-from-plug-in-assembly
/// </summary>
namespace WpfEmf.PluginCustomer
{
    [Export(typeof(IBasePlugin))]
    public class PluginCustomer : IBasePlugin {

        [Import]
        //private PluginCustomerViewModel _viewModel { get; set; }
        private WorkSpaceViewModel _viewModel { get; set; }

        private ResourceDictionary _viewDictionary = new ResourceDictionary(); 

        public ResourceDictionary View
        {
            get
            {
                return this._viewDictionary;
            }
        }

        public WorkSpaceViewModel ViewModel
        {
            get
            {
                return this._viewModel;
            }
        }
    }
}
