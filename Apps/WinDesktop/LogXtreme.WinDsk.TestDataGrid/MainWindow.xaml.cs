using LogXtreme.WinDsk.TestDataGrid.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace LogXtreme.WinDsk.TestDataGrid {

    /// <summary>
    /// Refs
    /// https://stackoverflow.com/questions/26218766/wpf-mvvm-binding-a-different-viewmodel-to-each-tabitem
    /// </summary>
    public partial class MainWindow : Window {       

        public MainWindow() {
            
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e) {

            if (this.DataContext is IDisposable disposable) {
                disposable.Dispose();
            }

            base.OnClosed(e);
        }
    }
}
