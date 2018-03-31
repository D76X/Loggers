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
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using Prism.Regions;

namespace LogXtreme.WinDsk.TestDocking.Prism.Views {
    /// <summary>
    /// Interaction logic for PlottingView.xaml
    /// </summary>
    public partial class PlottingView : 
        UserControl, 
        IPlottingView, 
        IRegionManagerAware {

        private IRegionManager regionManager;

        public PlottingView(IPlottingViewModel viewModel) {
            InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IViewModel ViewModel {
            get => (IPlottingViewModel)this.DataContext;
            set => this.DataContext = value;
        }

        public IRegionManager RegionManager {

            get => this.scopedRegionManager;

            set {
                if (this.scopedRegionManager != null) { return; }
                this.scopedRegionManager = value;
            }
        }
    }
}
