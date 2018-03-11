# Extended WPF Toolkit by Exceed 

## AvalonDoc Documentation

- [Welcome to Xceed Toolkit Plus for WPF v3.6](https://xceed.com/wp-content/documentation/xceed-toolkit-plus-for-wpf/webframe.html#Welcome.html)  

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

## Installing via NuGet

This has turned out to be problematic and is best avoided unless you need
some of the licenced component. The NuGet to the toolkit comes with a 45
days trial licence which once expired requires to provide a valid licence
key by adding some registration code into your application code. 

There are problems with this when the NuGet are unistalled from the 
solution or project.  

## Referencing the binaries

1. Download the zip with the binaries from
https://github.com/xceedsoftware/wpftoolkit/releases

2. Copy the dlls that you need to the folder of the executable or to a lib
folder.

3. Add a project reference to these dlls in your project. 