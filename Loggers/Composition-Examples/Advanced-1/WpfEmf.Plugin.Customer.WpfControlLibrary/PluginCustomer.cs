using System;
using System.ComponentModel.Composition;
using System.Windows;
using WpfEmf.Interfaces;

/// <summary>
/// http://stackoverflow.com/questions/12204614/load-wpf-ui-into-mvvm-application-from-plug-in-assembly
/// </summary>
namespace WpfEmf.Plugin.Customer.WpfControlLibrary {

    [Export(typeof(IBasePlugin))]
    public class PluginCustomer : IBasePlugin {

        public PluginCustomer() {

        }

        public ResourceDictionary View {
            get {
                throw new NotImplementedException();
            }
        }

        public WorkSpaceViewModel ViewModel {
            get {
                throw new NotImplementedException();
            }
        }
    }
}
