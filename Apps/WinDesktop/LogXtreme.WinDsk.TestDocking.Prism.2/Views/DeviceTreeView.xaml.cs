﻿using System;
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
using LogXtreme.Ifrastructure.Enums;
using LogXtreme.WinDsk.Infrastructure.Events;
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Infrastructure.Services;
using LogXtreme.WinDsk.Infrastructure.Utils;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using Prism.Regions;

namespace LogXtreme.WinDsk.TestDocking.Prism.Views {
    /// <summary>
    /// Interaction logic for DeviceTreeView.xaml
    /// </summary>
    public partial class DeviceTreeView : 
        UserControl, 
        IDeviceTreeView,
        IRegionManagerAware,
        IAvalonDockView {

        IRegionManager regionManager;
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
