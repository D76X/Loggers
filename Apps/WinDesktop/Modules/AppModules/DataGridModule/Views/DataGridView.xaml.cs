using DLogXtreme.WinDsk.ataGridModule.Interfaces;
using LogXtreme.Ifrastructure.Enums;
using LogXtreme.WinDsk.DataGridModule.Interfaces;
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using Prism.Regions;
using System.Windows.Controls;

namespace LogXtreme.WinDsk.DataGridModule.Views {
    /// <summary>
    /// Interaction logic for DataGridView.xaml
    /// </summary>
    public partial class DataGridView : 
        UserControl, 
        IDataGridView,
        IRegionManagerAware,
        IAvalonDockView {

        IRegionManager regionManager;

        public DataGridView(IDataGridViewModel viewModel) {
            InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IViewModel ViewModel {
            get => (IDataGridViewModel)this.DataContext;
            set => this.DataContext = value;
        }

        public IRegionManager RegionManager {

            get => this.regionManager;

            set {
                if (this.regionManager != null) { return; }
                this.regionManager = value;
            }
        }

        public AvalonDockViewTypeEnum AvalonDockViewType => AvalonDockViewTypeEnum.Document;

        public AvalonDockViewAnchorEnum AvalonDockViewAnchor => AvalonDockViewAnchorEnum.None;
    }
}
