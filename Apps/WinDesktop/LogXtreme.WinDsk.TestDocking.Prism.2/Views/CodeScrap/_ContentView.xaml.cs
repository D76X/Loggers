//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using LogXtreme.WinDsk.Infrastructure.Models;
//using LogXtreme.WinDsk.Infrastructure.Prism;
//using LogXtreme.WinDsk.Infrastructure.Utils;
//using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
//using Prism.Regions;
//using PrismRegions = Prism.Regions;

//namespace LogXtreme.WinDsk.TestDocking.Prism.Views {

//    /// <summary>
//    /// Interaction logic for ContentView.xaml
//    /// </summary>
//    public partial class ContentView : 
//        UserControl, 
//        IContentView, 
//        IRegionManagerAware {

//        IRegionManager regionManager;

//        public ContentView(IContentViewModel viewModel) {

//            InitializeComponent();
//            this.ViewModel = viewModel;
//        }        
//        public IRegionManager RegionManager {

//            get => this.regionManager;

//            set {

//                this.regionManager = value;
//                this.SetRegionManagerOnChildRegions();
//            }
//        }

//        public IViewModel ViewModel {
//            get => (IContentViewModel)this.DataContext;
//            set => this.DataContext = value;
//        }        

//        /// <summary>
//        /// This is necessary to make sure that navigation to child regions declared 
//        /// within the DockingManager can be navigated to in Prism. The RegionManager
//        /// within the scope of the view must be told about the child regions for the
//        /// navigation to work.      
//        /// Refs
//        /// https://stackoverflow.com/questions/44577082/prism-6-region-manager-requestnavigate-fails-to-navigate-for-some-regions
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void SetRegionManagerOnChildRegions() {

//            //PrismRegions.RegionManager.SetRegionManager(regionDeviceTree, this.RegionManager);
//            //PrismRegions.RegionManager.SetRegionManager(regionDataTree, this.RegionManager);
//            //PrismRegions.RegionManager.SetRegionManager(regionLayoutDocumentPane, this.RegionManager);
//            //PrismRegions.RegionManager.SetRegionManager(regionLayoutAchorablePane, this.RegionManager);
//        }

//        private void Button_Click_ShowDataTree(object sender, RoutedEventArgs e) {
//            //this.regionDataTree.Show();
//        }

//        private void Button_Click_ShowDeviceTree(object sender, RoutedEventArgs e) {
//            //this.regionDeviceTree.Show();
//        }
//    }
//}
