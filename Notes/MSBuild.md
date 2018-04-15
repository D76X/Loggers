# MSBuild

---
### MSBuild from command line

In the Visual Studio Developer Command Propmt

```
> msbuild -help | more
> msbuild -version
> where msbuild.exe
```

The last command shows the location of the MSBuild.exe which is installed with Visual Studio.
MSBuild can also be installed as standalone executable indipendednt of Visual Studion as part 
of the .NET Framework. The where command will also show the path to the MSBuild.exe that is 
installed as part of the .NET Framework if this location is on the PATH for the user or the 
system. 

- C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin

The location below constains the __*.xsd__ used by Visual Studio to provide intellisense when 
editing __*.msbuild__ files.

- C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild

The files are 

- Microsoft.Build.CommonTypes.xsd
- Microsoft.Build.Core.xsd

### MSBuild as part of the .NET Framework

1. [.Net Framework installation include MSBuild?
](https://stackoverflow.com/questions/9558138/net-framework-installation-include-msbuild)

The loaction of the __MSBuild.exe__ that comes with the .NET Framework and that is not part 
of the Visual Studio Tools is the following. This path might not be available in the PATH
variable for the user or for the system and should be added if you want to make use of MSBuid.exe
on a system that does not have Visual Studio installed.

- C:\Windows\Microsoft.NET\Framework\v4.0.30319\ __MSBuild.exe__

In addition to the MSBuild executable, the __*.xsd__ files are located in the folder below.

- C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild

---

## Project

Any __*.msbuild__ files must begin with 

## Targets

## Tasks

## Properties

## Items

---

## MSBuild Extension Pack

- [MSBuild Extension Pack](http://www.msbuildextensionpack.com/)

The MSBuild Extension Pack provides the largest collection of MSBuild Tasks, MSBuild Loggers
and MSBuild TaskFactories available