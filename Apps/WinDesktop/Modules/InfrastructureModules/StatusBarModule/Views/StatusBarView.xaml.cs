using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Models;
using StatusBarModule.Interfaces;
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

namespace StatusBarModule.Views {
    /// <summary>
    /// Interaction logic for StatusBarView.xaml
    /// </summary>
    public partial class StatusBarView : UserControl, IStatusBarView {

        public StatusBarView(IStatusBarViewModel viewModel) {
            InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IViewModel ViewModel {
            get {
                return (IStatusBarViewModel)this.DataContext;
            }

            set {
                this.DataContext = value;
            }
        }
    }
}