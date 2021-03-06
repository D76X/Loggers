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

namespace WpfMef.Modules {
    /// <summary>
    /// Interaction logic for SupplierList.xaml
    /// </summary>
    public partial class SupplierList : UserControl {
        public SupplierList() {
            InitializeComponent();
            this.List.ItemsSource = Enumerable.Range(1, 100).Select(n => "Supplier " + n);
        }
    }
}
