using System.Collections.Generic;
using System.Windows;
using WpfEmf.Interfaces;
using WpfEmf.ViewModels;

/// <summary>
/// http://stackoverflow.com/questions/12204614/load-wpf-ui-into-mvvm-application-from-plug-in-assembly
/// </summary>
namespace CompositionWpfEx4 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow(IEnumerable<WorkSpaceViewModel> viewModels) {

            this.DataContext = new MainWindowViewModel(viewModels);
            InitializeComponent();
        }
                
    }

}
