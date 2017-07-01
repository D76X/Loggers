using DeviceTreeModule.Interfaces;
using LogXtreme.WinDsk.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeviceTreeModule.Views {
    /// <summary>
    /// Interaction logic for DeviceTreeView.xaml
    /// </summary>
    public partial class DeviceTreeView : UserControl, IDeviceTreeView {

        public DeviceTreeView(IDeviceTreeViewModel viewModel) {
            InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IViewModel ViewModel {
            get {
                return (IDeviceTreeViewModel)this.DataContext;
            }

            set {
                this.DataContext = value;
            }
        }
    }
}