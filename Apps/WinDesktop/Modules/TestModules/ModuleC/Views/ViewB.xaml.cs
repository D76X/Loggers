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
using LogXtreme.WinDsk.Infrastructure;
using ModuleC.Interfaces;

namespace ModuleC.Views {
    /// <summary>
    /// Interaction logic for ViewB.xaml
    /// </summary>
    public partial class ViewB : UserControl, IViewB {
        public ViewB() {
            InitializeComponent();
        }

        IViewModel IView.ViewModel {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }
    }
}
