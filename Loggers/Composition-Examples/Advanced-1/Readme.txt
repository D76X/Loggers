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

================================================================================================

References 

UWP 042 | Utilizing User Controls as Data Templates
This shows that an entire UserControl can be made available as a DataTemplate in a resource 
dictionary and therefore matched up to a corresponding ViewModel via implicit type binding in WPF.
https://www.youtube.com/watch?v=ksliZGMMj8k

MEF - Import vs Importing Constructor 
http://www.brendanforster.com/mef-import-vs-importingconstructor.html