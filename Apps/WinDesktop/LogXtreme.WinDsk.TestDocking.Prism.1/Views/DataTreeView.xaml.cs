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
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;

namespace LogXtreme.WinDsk.TestDocking.Prism.Views {
    /// <summary>
    /// Interaction logic for DataTreeView.xaml
    /// </summary>
    public partial class DataTreeView : UserControl, IDataTreeView {

        public DataTreeView(IDataTreeViewModel viewModel) {
            InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IViewModel ViewModel {
            get {
                return (IDataTreeViewModel)this.DataContext;
            }

            set {
                this.DataContext = value;
            }
        }
    }
}
