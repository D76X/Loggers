﻿using LogXtreme.WinDsk.Interfaces;
using System.Windows;
using LogXtreme.WinDsk.Infrastructure;
using System;

namespace LogXtreme.WinDsk {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : Window, IShellView {

        public Shell(IShellViewModel viewModel) {
            InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IViewModel ViewModel {
            get {
                return (IShellViewModel)this.DataContext;
            }

            set {
                this.DataContext = value;
            }
        }

    }
}
