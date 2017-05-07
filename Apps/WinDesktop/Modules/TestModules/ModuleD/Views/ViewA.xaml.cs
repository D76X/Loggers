using LogXtreme.WinDsk.Infrastructure;
using ModuleD.Interfaces;
using System.Windows.Controls;

namespace ModuleD.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class ViewA : UserControl, IViewA {

        public ViewA(IViewAViewModel viewModel) {
            InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IViewModel ViewModel {
            get {
                return (IViewAViewModel)this.DataContext;
            }

            set {
                this.DataContext = value;
            }
        }
    }
}
