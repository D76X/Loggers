# Extended WPF Toolkit by Exceed 

## AvalonDoc Resources

- [Welcome to Xceed Toolkit Plus for WPF v3.6](https://xceed.com/wp-content/documentation/xceed-toolkit-plus-for-wpf/webframe.html#Welcome.html)  
- [Forum Archive](https://archive.codeplex.com/?p=avalondock)

***

## AvalonDock

AvalonDock is one of the free components in the Extended WPF Toolkit. 
There are other components in the toolkit some free some licenced but 
if only free components are used such as the AvalonDock a livence is 
not necessary. Normally components are packaged individually and are 
available as either binary (DLL) or as NuGet packages. Nonetheless, 
it is always necessary to add a reference the Xceed.Wpf.Toolkit.dll
as well in addition to the reference to the component library.
 
There are two ways to add the Extended WPF Toolkit and related components
to your application. 

1. Installing the NuGet packages into the solution or project.
2. Referencing the binaries directly.

***

## Installing via NuGet

This has turned out to be problematic and is best avoided unless you need
some of the licenced component. The NuGet to the toolkit comes with a 45
days trial licence which once expired requires to provide a valid licence
key by adding some registration code into your application code. 

There are problems with this when the NuGet are unistalled from the 
solution or project.  

***

## Referencing the binaries

1. Download the zip with the binaries from
https://github.com/xceedsoftware/wpftoolkit/releases

2. Copy the dlls that you need to the folder of the executable or to a lib
folder.

3. Add a project reference to these dlls in your project. 

***

## Navigation to region and child regions within the Avalon Docking Manager with multiple shells

This is not easy to get it right!

### Partition of the Shell UI

- The main window or Shell.
- The Main Menu region
- The StatusBar region
- The Content region sadwitched between the Main Menu and the StatusBar

For each of the Shell regions above the corresponding UI is injected from a corresponding isolated Prism module as described below.

- the Main Menu region is filled with the MainMenuView from the MainMenuModule.
- the StatusBar region is filled with the StatusBarView from the StatusBarModule.
- the Content region is filled with the ContentView from the ContentModule.  

### Partition of the Content region UI

***

- [AvalonDock 2.0 and MVVM](http://lostindetails.com/blog/post/AvalonDock-2.0-with-MVVM)
- [WPF Based Dynamic DataTemplateSelector](https://www.codeproject.com/Articles/418250/WPF-Based-Dynamic-DataTemplateSelector)  
- [AvalonDock 2.0 Tutorial In 5 Parts](https://www.codeproject.com/Articles/483507/AvalonDock-Tutorial-Part-Adding-a-Tool-Windo)
- [AvalonDock and MVVM Code Project](https://www.codeproject.com/Articles/239342/AvalonDock-and-MVVM)  
- [AvalonDock (docking window control)](https://github.com/xceedsoftware/wpftoolkit/wiki/AvalonDock)
- [Sample code to show how to use Avalondock in an MVVM application](https://stackoverflow.com/questions/23406451/sample-code-to-show-how-to-use-avalondock-in-an-mvvm-application)
- [Dynamically add DockablePanes](https://stackoverflow.com/questions/36737689/dynamically-add-dockablepanes)
- [DockingManager](https://github.com/xceedsoftware/wpftoolkit/wiki/DockingManager)
- [AvalonDock add tab dynamically](https://stackoverflow.com/questions/9324816/avalondock-add-tab-dynamically)  
- [AvalonDock with Prism Region Adapter](https://stackoverflow.com/questions/10905238/avalondock-with-prism-region-adapter)
- [PRISM 5 MEF AvalonDock 2.0 DataAdapter Registered Views and Parent IsSelected](https://stackoverflow.com/questions/25393850/prism-5-mef-avalondock-2-0-dataadapter-registered-views-and-parent-isselected)
- [Prism navigation - how to handle child view initialisation and cleanup?](https://stackoverflow.com/questions/18618114/prism-navigation-how-to-handle-child-view-initialisation-and-cleanup)
- [Prism 6 Region Manager RequestNavigate fails to navigate for some regions](https://stackoverflow.com/questions/44577082/prism-6-region-manager-requestnavigate-fails-to-navigate-for-some-regions)


### Sequence of loading

1. ctro RegionAdapterLayoutAnchorable
2. ctro ContentViewModel and setters of databound propeties from the ctro.
3. AvalonDock.CreateRegion which defines the attached property NamePropertyy used on LayoutAnchorable this isthe point at which the XAML is process and the LayoutAnchorable is created  in the DockingManager. The AvalonDockRegion is in chare to create a Prism region for the LayoutAnchorable instance and the name assigned to the value assigned to the attached property i.e.
infprism:AvalonDockRegion.Name="RegionDeviceTree"> associates a Prism region named RegionDeviceTree with the instance of LayoutAnchorable. 
4. The actual region is created in RegionAdapterLayoutAnchorable.CreateRegion()
5.  RegionAdapterLayoutAnchorable.Adapt() to latch up the logic to synch the region for the LayoutAnchorable with the LayoutAnchorable instance contents.
6. ctro ContentView. The ContentView is intsantitiated by Prism because it is registed against a named region by its module.
7. ContentView.RegionManager setter is called to set the region manager instance on the View because the ContentView imnplements IRegionManagerAware and the RegionManagerAwareBehavior is registered with Prism in the Bootstrapper thus the behavior intercepts all the implementations of IRegionManagerAware and sets their IRegionManagerAware.RegionManager propert to point to the RegionManager of the shell within which the view or view model has been created.

Here you need to set the region manager on the sub regions!
For example the that for the DeviceTreeManager in the LayoutAnchorable.
If you do not do that it Prism fails to navigate to it.

8. The ContentView.RegionManager is set notice that this setter is invoked after that on the view!
9. DeviceTreeModule.Initialize => this.container.RegisterTypeForNavigation<DeviceTreeView>();
this register the type DeviceTreeView for navigation
10. The getter on ContentViewModel for teh bound properties. DataBinding of teh ContetView happens now.
11. DockingManagerDocumentsSourceSyncBehavior.OnAttach => SynchronizeItems => latch up events
this latches up the DockingManager instance with the corresponding Prism region to enforce the synch 
logic by means of event handlers.
10. Prism logic is done the UI is shown!
11. Now it is time to navigate to the DeviceTreeView in the DeviveTree region via Prism navigation as the DeviceTreeView has been registered for navigation with Prism and the ContentView which has the DeviceTree region as chield region as set the RegionManager on the child LayoutAnchorable instance so that the Prism navigation can navigate to the DeviceTree region!
11. ShellViewModel.Navigate triggered from teh MainMenu
12. The events that where set in RegionAdapterLayoutAnchorable fires because a view is going to be injected into the DeviceTree region
13. ctro DeviceTreeViewModel Prism is going to navigate to the DeviceTree region and in the navigation instructin in the Shell Prism is asked to inject an instance of the DeviceTreeView into the DeviceTree region so Prism builds an instance of the DeviceTreeViewModel.
14. the events set up in RegionAdapterLayoutAnchorable are fired
15. the DeviceTreeViewModel.OnNavigatedTo
16. the ShellViewModel.NavigateComplete
17. done an instance of the DeviceTreeView has been injeted into the Prism region named "DeviceTree" that was set up as child region on an instance of the LayoutAnchorable inside the DockingManager which is marked as the "DockingManager" parent region.  


***


















