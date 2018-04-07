using LogXtreme.Ifrastructure.Enums;
using LogXtreme.WinDsk.DataTreeModule.Interfaces;
using LogXtreme.WinDsk.Infrastructure.Events;
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Infrastructure.Services;
using Prism.Regions;
using System;
using System.Windows.Controls;

namespace LogXtreme.WinDsk.DataTreeModule.Views {
    /// <summary>
    /// Interaction logic for DataTreeView.xaml
    /// </summary>
    public partial class DataTreeView : 
        UserControl, 
        IDataTreeView,
        IRegionManagerAware,
        IAvalonDockView {

        private IRegionManager regionManager;
        private IAvalonDockService avalonDockService;

        public DataTreeView(
            IDataTreeViewModel viewModel,
            IAvalonDockService avalonDockService) {

            InitializeComponent();
            this.avalonDockService = avalonDockService;
            this.ViewModel = viewModel;

            (this.ViewModel as ViewModelBase).NavigatedTo +=
                ViewModelNavigatedTo;
        }

        public IViewModel ViewModel {
            get => (IDataTreeViewModel)this.DataContext;
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

        public AvalonDockViewAnchorEnum AvalonDockViewAnchor => AvalonDockViewAnchorEnum.Right;

        private void ViewModelNavigatedTo(
            object sender,
            EventArgs e) {

            this.avalonDockService.RaiseDockingManagerChanged(
                this,
                new AvalonDockEventArgs(this, AvalonDockEventEnum.AvalonDockViewNavigatedTo));
        }
    }
}
