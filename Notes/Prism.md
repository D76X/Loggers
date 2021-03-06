--------------------------------------------------------------------------------------------------------------------------------------------

SETTING UP AN APPLICATION TO WORK WITH PRISM i.e. LogXtreme.WinDsk or LogXtreme.WinDsk.TestDocking.Prism

1-Reference the Prism library with the NuGet Manager choose Prism.Unity.Wpf which includes the Unity DI container.

2-Reference the following projects	
	LogXtreme.WinDsk.Infrastructure
	SemanticLogging						>> Refer to LogXtreme.WinDsk.Notes.txt for how to set up
	SlabManagementTools					>> Refer to LogXtreme.WinDsk.Notes.txt for how to set up

3-In App.xaml remove the StartupUri attribute from the Window XAML as the Bootstrapper will initialise and show the Shell instead.

4-In App.xaml.cs override the Application.OnStartup and Application.OnExit in which the Bootstrapper is instantiated and the 
  Semantic logging is set up.

5-In Bootstrapper.cs add the statement [using Microsoft.Practices.Unity;] the Container.Resolve<T>

6-In Bootstrapper.cs use Application.Current.MainWindow = (Window)Shell; in the override of Bootstrapper.InitializeShell() to cast the 
  dependency object to the application window.
--------------------------------------------------------------------------------------------------------------------------------------------

THE BOOTSTRAPPER

Initialises the application
Initialises the Core Services
Initialises the application specific services

THE BOOTSPRAPPING PROCESS

Create the LoggerFacade
Create and Configure the Module Catalog
Create and Configure the Container i.e. Unity
Configure the default Region Adapter Mappings 
Configure Region Behaviours
Register the Framewok Exception Types
Create the Shell
Initialise the Shell
Initialise Modules

TO GET THE BOOTSTRAPPER TO WORK YOU MUST

1-Remove the StartupUri attribute from the Window XAML as the Bootstrapper will initialise and show the Shell instead
2-Override the Application.OnStartup in which the Bootstrapper must be instantiated.
3-Add the statement [using Microsoft.Practices.Unity;] to the Bootstrapper.cs for the Container.Resolve<T>
4-use Application.Current.MainWindow = (Window)Shell; in the override of Bootstrapper.InitializeShell() to cast the dependency object to the 
  application window.

--------------------------------------------------------------------------------------------------------------------------------------------

SETTING UP THE FOLDER FOR THE APPLICATION MODULES AND THE BOOSTRAPPER TO FIND THE MODULES FOLDER

Modules of a Prims application can be referenced in a number of ways
	1-Take a direct reference
	2-Copy the module to a folder and set the Boostrapper.cs so that Prism looks in that folder to find its modules
	3-Use config files to set up >> the same as 4 but has the problem of no syntax intelligence thus it is flaky
	4-Use XAML files to set up...

In Prism 6 for Wpf there are several breaking changes and apparently not all the options that used to be available in the past to load
modules into catalogs are still available. Notably

1-AggregateModuleCatalog seems to have gone!
2-Only DirectoryModuleCatalog and ConfigurationModuleCatalog seems to be available
3-App.config seems to be the only remaining means to work with explicit configuration. The XAML file seems to be no longer supported. 

The simplest option that avoids taking direct references to the module assemlies from the consumer application is therefore to use the
either DirectoryModuleCatalog or ConfigurationModuleCatalog. The former controls the order of loading using attributes on the Module class
for all the modules that are copied to a predifined folder accessible to the executable assembly. The latter is more verbose and prone to
syntax errors but you get to control the order in which the modules are loaded and the location as well and still can use attributes on the
Module class if required.

--------------------------------------------------------------------------------------------------------------------------------------------

SET UP THE Modules FOLDER AND THE MODULES PROJECTS TO COPY THEIR OUTPUT TO IT 

This is an example of how Application modules are copied to the Module folder for a shell application

copy /Y "$(TargetDir)$(ProjectName).*" "$(SolutionDir)\Apps\WinDesktop\LogXtreme.WinDsk\bin\Debug\Modules\"

--------------------------------------------------------------------------------------------------------------------------------------------

PRISM CORE SERVICES

--------------------------------------------------------------------------------------------------------------------------------------------

PRISM CORE INTERFACES

IModuleManager
IModuleCatalog
IModuleInitializer
IRegionManager
IEventAggregator
ILoggerFacade
IServiceLocator

--------------------------------------------------------------------------------------------------------------------------------------------

THE SHELL

Is a Window user control
In it you define Regions into which Views will be injected at runtime

--------------------------------------------------------------------------------------------------------------------------------------------

REGION -REGION MANAGER - REGION ADAPTER
REGION NAME ATTACHED PROPERTY
REGION CONTEXT ATTACHED PROPERTY


Regions are managed by a RegionManager.
Regions are not controls.
A region implements IRegion.

The RegionManager maintsins a collection of regions and creates regions for controls.
The RegionManager provides the RegionName attached property.
The RegionManager.RegionName attached property is applied to a host control via XAML or code.
The RegionManager maps Region Adapters to Controls.

The RegionManager.Context attached property is similar to the concept fo DataContext.
The RegionManager.Context is used to share data between a parent/child views hosted between a region.
The RegionManager.Context can be set through code or through XAML.

A RegonAdapter is responsible for associating a Region with a host control.
In order to expose a UI control as a region a Region Adapter must be defined for the UI control.

The default RegionAdaper in Prism are
	ContentControlRegionAdapter
	ItemControlRegionAdapter
	SelectorRegionAdapter

-------------------------------------------------------------------------------------------------------------------------------------------

CREATE A SCOPED REGION MANAGER - NECESSARY WHEN USING MULTIPLE SHELLS

Prism uses a global RegionManager instance by default and that instance is injected via DI into modules.
In IModules.Initialise() the reference to the global region manager is used to retrive the regions of the
Shell by their name (key) and then the views of the modules are added to corresponding regions of the shell. 
We cannot add the same two identical region names to the same instance of the RegionManager and this may 
cause exceptions when modules add their views to the global region manager when the added view has child
views also registered to regions of the global region manager. In order to prevent this kind of exceptions 
any module should rather use a dedicated instance of the region manager that is not the global 


Method 1 - use the IRegion.Add overload to return a scoped region manager 

IRegion region = regionManager.Regions["SomeRegionName"];
View view = container.Resolve<View>();
// IRegion.Add(-,-,true)
IRegionManager scopedRegion = region.Add(view, null, true);

Method 2 - use IRegionManager.CreateRegionManager() to obtained a scope region
		   use IRegionManager.SetRegionManager to set a scope region for a view

IRegionManager scopedRegion = regionManager.CreateRegionManager();
View view = container.Resolve<View>();
ResionManager.SetRegionManager(view, scopedRegion)

-------------------------------------------------------------------------------------------------------------------------------------------

CREATE A CUSTOM REGION ADAPTER

For any control other than ContentControl, ItemControl or Selector controls for which Prism comes with built-in Region Adapters
a Custom Region Adapter is required.

Derive from RegionAdapterBase<T> where T is the type of the host control for the CustomRegion

Provide a PUBLIC constructor such as
public RegionAdapterStackPanel(IRegionBehaviorFactory regionBehaviorFactory) 
			: base(regionBehaviorFactory) {

		}

Implement RegionAdapterBase<T>.CreateRegion which can return
	SingleActiveRegion	=> allows none or one ACTIVE view at any one time => used for ContentControl
	AllActiveRegion		=> all the views are ACTIVE and DEACTIVATION of views is NOT ALLOWED => used for ItemControl
	Region				=> allows multiple active views => used for SelectorControl

Implement RegionAdapterBase<T>.Adapt

Register the RegionAdapter with the Bootstrapper
override RegionAdapterMappings ConfigureRegionAdapterMappings() and return the base class mappings with the addition 
of the Custom Region Adapter

-------------------------------------------------------------------------------------------------------------------------------------------

MODULES
http://prismlibrary.readthedocs.io/en/latest/WPF/04-Modules/

Modules are implemented in WPF User Control Libraries.
In the library a single class implementing IModule exits.

MODULES LIFETIME = REGISTRATION => DISCOVERY => REGISTRATION => LOADING

THE MODULE CATALOG

A ModuleCatalog is used to maintain a collection of loaded modules.
The ModuleCatalog knows the location of each module.
The ModuleCatalog knows the order in which all modules are loaded.
The ModuleCatalog knows all the dependencies between modules.
A ModuleCatalog is instantiated in the Bootstrapper.ConfigureModuleCatalog override.

REGISTER MODULES
 
From Code
From XAML
From a Configuartion File.
From Dsik by specifying a directory from which compiled module assembly may be loaded at runtime. 

LOAD MODULES

Modules can be loaded from Disk or from a remote location such as a shared directory.

Modules can be loaded in two ways
	When Available
	On Demand


INITIALISE MODULES

Make a module ready for comsumption in a Prims application

IModule.Initialize() =>
	Register types
	Subscribe to Services or Events
	Register Shared Services
	Compose Views into the Shell

--------------------------------------------------------------------------------------------------------------------------------------------

BindableBase 

Refs
https://stackoverflow.com/questions/28844518/bindablebase-vs-inotifychanged

Bindablebase is an abstract class that implements INotifyPropertyChanged interface and provide SetProperty<T>.You can reduce the set method 
to just one line also ref parameter allows you to update its value. 

--------------------------------------------------------------------------------------------------------------------------------------------

REGION, REGION ADAPTERS AND VIEWS

A Region in Prism is an attached property that may be applied to a control to indicate to Prism that the XAML element to which the AP is 
applied defines a Region and will therefore host some views as its children in the visual tree.

For example

<ContentControl prism:RegionManager.RegionName="{x:Static inf:RegionNames.RegionMainMenu}" Grid.Row="0"/>

Declares that that specifc element of the VT that is a ContentControl is a named region. The named region is registered with the global
RegionManager (an instance of IRegionManager) which is a Prism service that can be injected into view models. This allows view models to
inject views into region programmatically.

For a XAML element to be able to act as a region there must exist a Region Adapter. A Region Adapter is an implementation of an abstract
class RegionAdapterBase<T> where T is the type of XAML element that we would like to act as a region. There is an implementation of a 
custom region RegionAdapterStackPanel in the solution for illustrative purposes. In practice the logic in the Region Adapter specifies two
things.

1-What kind of Region that XAML item is supposed to be. => the RegionAdapterBase.CreateRegion
2-Some logic that handles the addition and removal of views into the XAML element of the region. => RegionAdapterBase.Adapt

In relation to (1) there are really only three types of Region in Prism
1-AllActiveRegion		=> all the views in the region are active i.e. ContentControl has an adapter of this kind.
2-SingleActiveRegion	=> only a single view in the region can be active at any one time i.e. ItemsControl has an adapter of this kind.
3-Region				=> this region allows for multiple active views i.e. controls that derive from teh Selector Class. 

Views in Prism must eventually become visible on the GUI by being associated to a Prism Region. There are three mechanisms by means of which 
a view can be displayed into a region.

1-View Discovery		(automatic injection of views into defined regions of the shell)
2-View Injection		(programmatic injection of views into defined regions of the shell)
3-Navigation			Prism built-in mechanism.

1-View Discovery Example	(automatic injection of views into defined regions of the shell)

// In the IModule.initialize regiter a view with a region in the shell and Prism will instantiate
// the view and inject it in the region for you. This is a good mechanism for some parts of the GUI 
// that are permanent. This mechanism does not afford explicit control an when to load and display 
// a view in a region.
public void Initialize() {
            regionManager.RegisterViewWithRegion(RegionNames.RegionContent, typeof(ContentView));
}

2-View Injection	(programmatic injection of views into defined regions of the shell)

IRegionManager rm = manager;
rm.Regions["RegionName"].Add(view,name)

or 

var viewModelForView = container.Resolve(IMyViewModel)
IRegion region = manager.Regions["RegionName"];
var activeView = region.ActiveViews[0];
....
region.Add(viewModelForView.View)

you may need to Activate/Deactive the view(s) in the region. For example, if a defined Prism region has a ContentControl has its host then
it must be of type SingleActiveRegion. You may inject multiple view into such region but only one can be active at any time and you must do
this programmatically. Deactivate the current view, inject the new view and activate it to show it othervwise you won't see it. 

With View Injection you can also remove views from a Region.
The region must exists prior to injecting views into it, you must check that the region has already being created before you try to inject 
any view into it.

--------------------------------------------------------------------------------------------------------------------------------------------

NAVIGATION

There are two kinds of navigation in Prism

1-STATE BASE NAVIGATION (Easy to implement)

Navigation is accomplished by applying state changes to existing controls of a view i.e. hiding or changing visibility or styles or applying
animations to controls. The key is that in this kind of navigation a view is not rrplaced by another view instead the view is updated. The 
state changes for the view are normally reflected in the backing view model. Often state changes are driven by user interaction but there 
may be other mechanisms as well.

Normally STATE BASED NAVIGATION is apporpriate when there is the need to present roughly the same information as the current view but with 
some changes in style rather than content. In some cases a view needs to change when the user initiates some kind fo interaction with it that
places the view in a new state for the duration of the interaction i.e. importing new data or processing the current data in trhe view, etc.

2-VIEW BASED NAVIGATION with NAVIGATION SERVICES

In view based navigation a view is replaced with a new view or an existing view. A navigation request is initiated by invoking the 
*INavigateAsync.RequestNavigate*. Despite its name *INavigateAsync.RequestNavigate* is NOT async instead it returns synchronously 
following the completion of a navigation operation or while a navigation is still pending as in those cases where the navigation 
needs support for confirmation/cancellation.

Example1: navigation via IRegion (IRegion derives from INavigateAsync)

IRegion region = (grab a region i.e. via the RegionManager)
region.RequestNavigate(new Uri("MyView", UriKind.Relative));

Example2: navigation via RegionManager

IRegionManager regionManager = ...;
regionManager.RequestNavigate(RegionNames.SomeRegion, new Uri("MyView", UriKind.Relative));

REGISTERING VIEWS TO ENABLE VIEW BASED NAVIGATION

Standard registration
Container.RegisterType<HomeView>("HomeView");
Container.RegisterType<IHomeView, HomeView>("HomeView");

Registration for view based navigation
Container.RegisterType<object, HomeView>("HomeView");
Container.RegisterType(typeof(object), typeof(HomeView), "HomeView");

The latter is necessary because when the Region Navigation Service is asked to create a view 
it requests a type of Object from the contsainer where the name matches the name supplied in 
the navigation Uri.

I have added an extension method IUnityContainer.RegisterTypeForNavigation to make it easier
to register views for navigation. Notice that the name of teh view is set to the fully qualified
name of the corresponding class.

--------------------------------------------------------------------------------------------
public static void RegisterTypeForNavigation<T>(this IUnityContainer container) {
            container.RegisterType(typeof(object), typeof(T), typeof(T).FullName);
        }
--------------------------------------------------------------------------------------------

The preceeding examples are all based on teh View-First approach where the navigation request is
to the view. However, it is alos possible to adopt a ViewModel approach inn which case the registration
and Uri should be that of the ViewModel.

BASIC REGION NAVIGATION

Example1: see WinDsk.TestApp1/Shell.xaml

<MenuItem Header="Navigate to ViewA"
            Command="{Binding NavigateCommand}"
            CommandParameter="ModuleD.Views.ViewA" />
<MenuItem Header="Navigate to ViewB"
            Command="{Binding NavigateCommand}"
            CommandParameter="ModuleD.Views.ViewB" />
<MenuItem Header="Navigate to Tabview"
            Command="{Binding NavigateCommand}"
            CommandParameter="ModuleD.Views.TabView" />

In the ShellViewModel

this.NavigateCommand = new DelegateCommand<string>(Navigate);

private void Navigate(string viewName) {

this.RegionManager.RequestNavigate(
    RegionNames.RegionContent, 
    new Uri(viewName, UriKind.Relative),
    NavigateComplete);
}

private void NavigateComplete(NavigationResult navigationResult) {
    this.StatusMessage = navigationResult.Context.Uri.ToString();
}

VIEW & ViewModel PARTICIPATION IN NAVIGATION

Our ViewModelBase is designed to support the interface *IConfirmNavigationRequest:INavigationAware*

void IConfirmNavigationRequest.ConfirmNavigationRequest(
	NavigationContext navigationContext, 
	Action<bool> continuationCallback)

bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)	// see ViewModelBase
void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)		// called before navigation takes place
void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)		// called after navigation is completed

----------------------------------------------------------------------------
View Navigation Workflow for both the View and the corresponding ViewModel.
In the navigation interaction Prism checks whether the view implements the 
INavigationAware if it does it calls the contract methods on the view as in
the workflow below. If the view does not implement INavigationAware then 
Prism chacks whether its DataContext that is VoewModel does and if this is 
the case it calls the workflow on the VM.
----------------------------------------------------------------------------

-------------------------------------------------------------------------------------------------------------------
RequestNavigate => OnNavigatedFrom => IsNavigationTarget => Resolve the View => OnNavigatedTo => NavigationComplete
-------------------------------------------------------------------------------------------------------------------

CONTROL THE LIFETIME OF THE VIEW WITH IRegionMemberLifetime

Our ViewModelBase implements IRegionMemberLifetime.

bool IRegionMemberLifetime.KeepAlive

Tells Prism whether the view once deativated i.e. it has been navigated from, should be dereferenced by the containing 
region and GCed. The next time the same view is navigated to Prism container will resolve a new istance instead of 
recycling one.

PASSING PARAMETERS

To pass parameters use domething like this in your view model.

this.NavigateCommand = new DelegateCommand<string>(Navigate);

private void Navigate(string viewName) {

            var parameters = new NavigationParameters();
            parameters.Add(NavigationRequestParametersBase.KeyNavigationRequestedBy, this);
            
            this.RegionManager.RequestNavigate(
                RegionNames.RegionContent,
                new Uri(viewName, UriKind.Relative),
                NavigateComplete,
                parameters);
        }

In the recepient view model that is the view model of the view that it is navigated to there are two hooks
on the INavigationAware interface or its descendent IConfirmNavigationRequest. ViewModelBase implements the 
latter.

public override bool IsNavigationTarget(NavigationContext navigationContext) {

    var sender = navigationContext.Parameters[NavigationRequestParametersBase.KeyNavigationRequestedBy];
    // use the params here to decide whether to return true or false.
    // true to tell prism to reuse this view model and the corresponding view to satisfy the navigation request.
    // false to tell prism to satisfy the navigation request by creating a new view and view model. 

    return false;
}

public override void OnNavigatedTo(NavigationContext navigationContext) {

    var navigationRequestedBy = navigationContext.Parameters[NavigationRequestParametersBase.KeyNavigationRequestedBy];
    // do something with this information
}

NAVIGATION TO EXISTING VIEWS

This is controlled by implementing the INavigationAware.IsNavigationTarget as above.

CONFIRMING OR CANCELLING NAVIGATION

This is controlled by implementing IConfirmNavigationRequest.confirmNavigationRequest. 
ViewModelBase implements IConfirmNavigationRequest and you may override it.
The workflow of the navigation logic gains an additional step - (ConfirmNavigationRequest).
-------------------------------------------------------------------------------------------------------------------
RequestNavigate => 
(ConfirmNavigationRequest)
OnNavigatedFrom => IsNavigationTarget => Resolve the View => OnNavigatedTo => NavigationComplete
-------------------------------------------------------------------------------------------------------------------

Example

public override void ConfirmNavigationRequest(
            NavigationContext navigationContext,
            Action<bool> continuationCallback) {

var isNavigationConfirmed = false;

if (this.IsConfirmNavigationActive) {

    MessageBoxResult messageResult = MessageBox.Show("Confirm navigation?", "Navigation", MessageBoxButton.YesNoCancel);

    if (messageResult == MessageBoxResult.Yes) {
        // save or persist state or do something before Navigation accurs...
        isNavigationConfirmed = true;
    }
    else {
        isNavigationConfirmed = false;
    }
}
else {
    isNavigationConfirmed = true;
}  

isNavigationConfirmed = isNavigationConfirmed && this.IsClosable;

continuationCallback(isNavigationConfirmed);
}

THE NAVIGATION JOURNAL

1-It is stack-based
2-Only works with Region Navigation Services
3-Accessed from NavigationService.Hournal property
4-the NavigationService is accessible via the NavigationContext that is passed to all of the methods of 
  INavigationAware & IConfirmNavigationRequest
5-You can grab a reference to the NavigationService in the VM's INavigationAware.OnVavigatedTo override
6-Use the NavigationService.Journs.GoBack()/CanGoBach()/GoForward()/CanGoForward()/etc.
--------------------------------------------------------------------------------------------------------------------------------------------

SOME USEFUL TOOLS IN PRISM

01-How to get a reference to the region attached to a control.
   How to remove contents from a region.

Refs
https://app.pluralsight.com/player?course=prism-mastering-tabcontrol&author=brian-lagunas&name=prism-mastering-tabcontrol-m3&clip=9&mode=live

// this examples shows how to correctly remove a tab item from a TabControl
// to which a Prism region is attached
var tabControl = ...
IRegion region = RegionManager.GetObservableRegion(tabControl).Value;
if(region == null) {...}
else if(region.Views.Contains(tabControl.Content)){
	
	// once there's a ref to a region the IRegion.Remove must be used to 
	// remove content. Prims handles all the logic for ItemsControl as well.
	region.Remove(tabItem.Content)
}

02-
--------------------------------------------------------------------------------------------------------------------------------------------