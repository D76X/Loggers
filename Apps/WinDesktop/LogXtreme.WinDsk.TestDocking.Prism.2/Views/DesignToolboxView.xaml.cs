using LogXtreme.Ifrastructure.Enums;
using LogXtreme.WinDsk.Infrastructure.Events;
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Infrastructure.Services;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using Prism.Regions;
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

namespace LogXtreme.WinDsk.TestDocking.Prism.Views {
    /// <summary>
    /// Interaction logic for DesignToolboxView.xaml
    /// </summary>
    public partial class DesignToolboxView :
        UserControl,
        IDesignToolboxView,
        IRegionManagerAware,
        IAvalonDockView {

        private IRegionManager regionManager;
        private IAvalonDockService avalonDockService;

        public DesignToolboxView(
            IDesignToolboxViewModel viewModel,
            IAvalonDockService avalonDockService) {

            InitializeComponent();
            this.avalonDockService = avalonDockService;
            this.ViewModel = viewModel;

            (this.ViewModel as ViewModelBase).NavigatedTo +=
                ViewModelNavigatedTo;
        }

        public IViewModel ViewModel {
            get => (IDesignToolboxViewModel)this.DataContext;
            set => this.DataContext = value;
        }

        public IRegionManager RegionManager {

            get => this.regionManager;

            set {
                if (this.regionManager != null) { return; }
                this.regionManager = value;
            }
        }

        public AvalonDockViewTypeEnum AvalonDockViewType => AvalonDockViewTypeEnum.Anchorable;

        public AvalonDockViewAnchorEnum AvalonDockViewAnchor => AvalonDockViewAnchorEnum.Left;

        private void ViewModelNavigatedTo(
            object sender,
            EventArgs e) {

            this.avalonDockService.RaiseDockingManagerChanged(
                this,
                new AvalonDockEventArgs(this, AvalonDockEventEnum.AvalonDockViewNavigatedTo));
        }
    }
}
