Composition-Examples Advanced-1

=================================================================================================

How it works

All the necessary abstractions are in the WpfEmf.Interfaces project which is a simple
library.

The entry point of the CompositionWpfEx4 application is its App.xaml.cs (code behind).
In the constructor of the Application object the composition container loads up the plug-ins from
the subfolder ./modules and store a reference to a collection of plugins marked with the MEF attr.
[ImportMany].

Plugins are implementations of the IBasePlugin and in the simplest case a plug in is a project
where a UserControl and a corresponding ViewModel exist and can be worked as an indipendent
unit. In order to develop a UserControl a control library project is used i.e. 

WpfEmf.Plugin.Produc.WpfControlLibrary

The control libraries projects for the plugins must be set up so that their binary is ultimately
deployed to the ./modules folder of the application that will load them as parts with MEF.
A simple way of achieving this may be to use a post build event command in the Build Events pane
of the project property window in Visual Studio. 

For example
copy /Y "$(TargetDir)$(ProjectName).*" "$(SolutionDir)\Loggers\Composition-Examples\Advanced-1\CompositionWpfEx4\bin\Debug\modules\"

will copy the binary 
WpfEmf.Plugin.Product.WpfControlLibrary.dll 
to the folder
C:\GitHub\Loggers\Loggers\Composition-Examples\Advanced-1\CompositionWpfEx4\bin\Debug\modules

so that the 
CompositionWpfEx4.exe in the folder
C:\GitHub\Loggers\Loggers\Composition-Examples\Advanced-1\CompositionWpfEx4\bin\Debug
can use MEF to load the plugin.

Notice that the /.modules folder must already exist and that beside the *.dll also the matching 
debug symbols *.pdb are also copied.


IBasePlugin interface carries two essential pieces of information about a plug in that is the 
ViewModel exposed as a class type implementing INotifyPropertyChange and the View exposed as a
ResourceDictionary to the outside world.

========================================================================================================

References 

-------------------------------------------------------------------------------------------------------
How to use App.xaml and App.xaml.cs
Create the main window of the application in Application_Startup
http://www.wpf-tutorial.com/wpf-application/working-with-app-xaml/

-------------------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------------------
Load WPF UI into MVVM application from plug-in assembly

This post has inspired this work.

http://stackoverflow.com/questions/12204614/load-wpf-ui-into-mvvm-application-from-plug-in-assembly
-------------------------------------------------------------------------------------------------------

***

-------------------------------------------------------------------------------------------------------
This article highlights the differences between DataTemplate and UserControl 
http://stackoverflow.com/questions/1807657/wpf-usercontrol-or-datatemplate
-------------------------------------------------------------------------------------------------------

***

-------------------------------------------------------------------------------------------------------
UWP 042 | Utilizing User Controls as Data Templates

This shows that an entire UserControl can be made available as a DataTemplate in a resource 
dictionary and therefore matched up to a corresponding ViewModel via implicit type binding in WPF.
https://www.youtube.com/watch?v=ksliZGMMj8k

The same concept is repeated in this article.
http://stackoverflow.com/questions/6137997/should-i-be-using-usercontrols-for-my-views-instead-of-datatemplates
-------------------------------------------------------------------------------------------------------
***
-------------------------------------------------------------------------------------------------------
Load a ResourceDictionary from an assembly

http://stackoverflow.com/questions/709087/load-a-resourcedictionary-from-an-assembly/919613#919613
-------------------------------------------------------------------------------------------------------

***
-------------------------------------------------------------------------------------------------------
XamlParseException thrown calling XamlReader.Load

This post talks about the issue related to the presence of the "x:Class" markup in XAML that is dinamically 
loaded. Note that the poster suggests that by simply removing the "x:Class" declaration from the XAML file 
allows the dynamic loading process to succeed.  
http://stackoverflow.com/questions/5201937/xamlparseexception-thrown-calling-xamlreader-load

Loading XAML XML through runtime?
How to attach the code behind at runtime?
http://stackoverflow.com/questions/4077318/loading-xaml-xml-through-runtime/4077786#4077786
-------------------------------------------------------------------------------------------------------

***

-------------------------------------------------------------------------------------------------------
This post describes how resources in a resource dictionary and embedded in an assembly may be enumerated.
It also partly discusses the structure of the *.g.resources section of an assembly and the extensions
xaml & balm of resorces according to the Build Action assigned to them in the property pane of Visual 
Studio. 

http://stackoverflow.com/questions/5069276/enumerating-files-in-an-embedded-resource-directory

Note - use ILSpy or similar to view the contents of a compiled assemby (.dll)
http://ilspy.net/
-------------------------------------------------------------------------------------------------------

***

-------------------------------------------------------------------------------------------------------
MEF - Import vs Importing Constructor 
http://www.brendanforster.com/mef-import-vs-importingconstructor.html
-------------------------------------------------------------------------------------------------------

***

-------------------------------------------------------------------------------------------------------

Pack Uri
http://stackoverflow.com/questions/3442000/is-it-really-important-to-use-pack-uris-in-wpf-apps

-------------------------------------------------------------------------------------------------------

