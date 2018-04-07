using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Infrastructure.Services;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using Prism.Regions;
using System.Windows.Controls;
using Xceed.Wpf.AvalonDock.Layout;

namespace LogXtreme.WinDsk.TestDocking.Prism.Views {

    /// <summary>
    /// Interaction logic for ContentView.xaml
    /// </summary>
    public partial class ContentView : 
        UserControl, 
        IContentView, 
        IRegionManagerAware {

        IRegionManager regionManager;

        public ContentView(
            IContentViewModel viewModel,
            IAvalonDockService avalonDockService) {

            InitializeComponent();

            avalonDockService.RegisterPart<LayoutAnchorablePane>(leftLayoutAnchorablePane.Name);
            avalonDockService.RegisterPart<LayoutDocumentPane>(@"layoutDocumentPane");
            avalonDockService.RegisterPart<LayoutAnchorablePane>(rightLayoutAnchorablePane.Name);

            this.ViewModel = viewModel;
        }
        public IRegionManager RegionManager {

            get => this.regionManager;

            set {
                if (this.regionManager != null) { return; }
                this.regionManager = value;
            }
        }

        public IViewModel ViewModel {
            get => (IContentViewModel)this.DataContext;
            set => this.DataContext = value;
        }    
    }
}
