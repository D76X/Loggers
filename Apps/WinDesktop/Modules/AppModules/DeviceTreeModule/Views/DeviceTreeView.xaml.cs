using LogXtreme.Ifrastructure.Enums;
using LogXtreme.WinDsk.DeviceTreeModule.Interfaces;
using LogXtreme.WinDsk.Infrastructure.Events;
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Infrastructure.Services;
using Prism.Regions;
using System;
using System.Windows.Controls;

namespace LogXtreme.WinDsk.DeviceTreeModule.Views {
    /// <summary>
    /// Interaction logic for DeviceTreeView.xaml
    /// </summary>
    public partial class DeviceTreeView : 
        UserControl,
        IRegionManagerAware,
        IDeviceTreeView {

        private IRegionManager regionManager;
        private IAvalonDockService avalonDockService;

        public DeviceTreeView(
            IDeviceTreeViewModel viewModel,
            IAvalonDockService avalonDockService) {

            InitializeComponent();
            this.avalonDockService = avalonDockService;
            this.ViewModel = viewModel;

            (this.ViewModel as ViewModelBase).NavigatedTo +=
                ViewModelNavigatedTo;
        }

        public IViewModel ViewModel {
            get => (IDeviceTreeViewModel)this.DataContext;
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