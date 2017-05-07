using LogXtreme.WinDsk.Infrastructure;
using ModuleD.Interfaces;
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

namespace ModuleD.Views {
    /// <summary>
    /// Interaction logic for ViewB.xaml
    /// </summary>
    public partial class ViewB : UserControl, IViewB {

        public ViewB(IViewBViewModel viewModel) {
            InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IViewModel ViewModel {
            get {
                return (IViewBViewModel)this.DataContext;
            }

            set {
                this.DataContext = value;
            }
        }
    }
}
