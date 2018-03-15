using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using Prism.Regions.Behaviors;
using System;
using System.Windows;
using Xceed.Wpf.AvalonDock.Layout;

namespace LogXtreme.WinDsk.Infrastructure.Prism {

    /// <summary>
    /// Exposes the attached property <see cref="NameProperty"/> which should 
    /// be used on XAML elements from the AvalonDock namespace to define their
    /// name as Prism regions.
    /// Refs
    /// 
    /// See the AvalonDockTestApps solution.
    /// https://github.com/D76X/AvalonDockTestApps
    /// AvalonDock with Prism Region Adapter
    /// https://stackoverflow.com/questions/10905238/avalondock-with-prism-region-adapter
    /// </summary>
    public class AvalonDockRegion : DependencyObject {

        public static string GetName(DependencyObject obj) {
            return (string)obj.GetValue(NameProperty);
        }

        public static void SetName(
            DependencyObject obj, 
            string value) {

            obj.SetValue(NameProperty, value);
        }

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.RegisterAttached(
                "Name", 
                typeof(string), 
                typeof(AvalonDockRegion), 
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnNameChanged)));

        private static void OnNameChanged(
            DependencyObject d, 
            DependencyPropertyChangedEventArgs e) {

            CreateRegion((LayoutAnchorable)d, (string)e.NewValue);
        }

        /// <summary>
        /// In Prism a control may be used as a named region container as long as 
        /// there exists a RegionAdapter for it. Custom region adapters for the 
        /// controls in the AvalonDock library can be designed and added to the 
        /// collection of region adapters that are present in the Prism library 
        /// by default. Two examples of such custom region adapters are
        /// <see cref="RegionAdapterDockingManager"/> 
        /// <see cref="RegionAdapterLayoutAnchorable"/>
        /// 
        /// When the XAML processor processes the XAML and encounters an instance
        /// of LayoutAnchorable controls on which this attached property is set
        /// the logic in the callback handler <see cref="OnNameChanged"/> of the 
        /// attached property <see cref="NameProperty"/> is executed and eventually
        /// <see cref="CreateRegion"/> instantiates of the right region adapter for
        /// for the named region.
        /// 
        /// The region adapter for the region depends on the type of the control
        /// on which the attached property <see cref="NameProperty"/> is used.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="regionName"></param>
        private static void CreateRegion(LayoutAnchorable element, string regionName) {

            if (element == null) {
                throw new ArgumentException(nameof(element));
            }

            // In design mode there is no main window so there is no reason to try
            // to create a Prism region for the anchorable element.
            if (Application.Current == null || 
                Application.Current.MainWindow == null) {

                return;
            }

            try {

                if (ServiceLocator.Current == null) { return; }

                // now we can use the service locator to build a Prism region with the 
                // given name for the anchorable element 

                var mappings = ServiceLocator
                                .Current
                                .GetInstance<RegionAdapterMappings>();

                if (mappings == null) { return; }

                IRegionAdapter regionAdapter = mappings.GetMapping(element.GetType());

                if (regionAdapter == null) { return; }

                regionAdapter.Initialize(element, regionName);
            }
            catch (Exception ex) {

                throw new RegionCreationException($"Unable to create region {regionName}", ex);
            }
        }
    }
}
