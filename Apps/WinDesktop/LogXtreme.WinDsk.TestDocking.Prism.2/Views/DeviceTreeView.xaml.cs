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
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
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
        IRegionManagerAware {

        IRegionManager regionManager;

        public DeviceTreeView(IDeviceTreeViewModel viewModel) {

            InitializeComponent();
            this.ViewModel = viewModel;                        
        }

        public IViewModel ViewModel {
            get => (IDeviceTreeViewModel)this.DataContext;
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
