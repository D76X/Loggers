# WiX

## The tools in the WiX toolset

| Tool						| Executable																   |
| --------------------------|------------------------------------------------------------------------------|
| Compiler					| candle.exe = from source files *.wsx to object files.		                   |
| Linker					| light.exe	= from object files + Wix libraries to *msi or other executable.   |
| Bootstrapper, chainer,...	| burn.exe 												   |
| Harvester					| heat.exe	= used to automatice generation of source code from an existing directory structure |
| Decompiler			    | dark.exe  = from *.msi to *.wsx source code	|

### WiX in Visual Studio

Votive is the name of the Visual Studio add-in that is installed to VS by the WiX toolset to provide syntax
highlighting and intellisense support for *.wsx files and also MSI intergation and WiX project types.

--- 

### Location of the WiX Toolset and PATH

- C:\Program Files (x86)\WiX Toolset v3.10

The executables of interest are all located in the bin folder 

- C:\Program Files (x86)\WiX Toolset v3.10\bin

Precisely the executable tools __candle.exe, light.exe, heat.exe, dark.exe__ and others such as 
__insigna.exe, lux.exe, melt.exe, torch.exe__ are all directly under in the bin folder. The only 
exception is __burn.exe__.  

- C:\Program Files (x86)\WiX Toolset v3.10\bin\x86\burn.exe

--- 

### Use WiX from command line

Make sure that the location below is in the PATH variable for the user or the system

- C:\Program Files (x86)\WiX Toolset v3.10\bin  

Refere to the help for each executable tool as in the examples below.

```
> candle.exe --help  
> light.exe --help
```

### How to run an MSI with logging 

In order to produce an event log during of the operations performed by the
installer for a specific ```*.msi``` the flag __l*v__ must be provided to 
the __msiexec__.

```>msiexec /i myInstaller.msi /l*v log.log```

---

### WiX extensions as Windows  Installer Custom Actions

- SQL configuration
- ISS configuration
- etc..

---

### How To: Create a Shortcut on the Start Menu

1. [How To: Create a Shortcut on the Start Menu](http://wixtoolset.org/documentation/manual/v3/howtos/files_and_registry/create_start_menu_shortcut.html)  

---

### ICE Errors and Warnings

1. [Wix - ICE60 and ICE69 warnings](https://stackoverflow.com/questions/21320334/wix-ice60-and-ice69-warnings)  
   The cleanest way to handle ICE69 which may be recurrent is to just leave them in the build output and ignore 
   them where and when it makes sense.

