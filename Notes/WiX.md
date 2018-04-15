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

---

### WiX extensions as Windows  Installer Custom Actions

- SQL configuration
- ISS configuration
- etc..


========================================================================================================================

HOW TO RUN AN MSI WITH LOGGING: THE l*v flag

msiexec /i myInstaller.msi /l*v log.log
========================================================================================================================