# MSBuild

The official documentation for MSBuild.

https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild?view=vs-2017

---

### MSBuild from command line

In the Visual Studio Developer Command Console Windows

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

---

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

```
<?xml version="1.0" encoding="utf-8"?>
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003"
         DefaultTargets="HelloWorld">...
```

---

## Targets

*  Targets represent the sequence of instruction to MSBuild.  
*  Targets contain Tasks.

### Some examples from command line and **response files** (*.rsp)

From the command line.

```> msbuild dosomething.msbuild /t:Target10;Target3```

Alternative from the command line with a backing *.rsp file.

```> msbuild dosomething.msbuild @dosomething.rsp```

In the *.rsp file

```
...
/t:Target10;Target3
...
```


Another way is to set this in the **DefaultTargets** attribute of the **Project** 
element of the *.msbuild.
 
```
<Project DefaultTargets="Target10;Target3" ...
```

In all cases Target10 is run **before** Target3.

### Concatenation of Targets in the *.msbuild

It is possible to chain the calls to Target elements declared in the *.msbuild file as
in the following example.

```
<Target Name="TargetA">
	...
	<CallTarget Targets="TargetZ;TargetD" />
	...
</Target>
```

### Implicit Target invokation model - DependsOnTarget attribute

In addition to being able to concatenate Targets explicitely in the *.msbuild it is also
and common to concatenate Target elements by expressing their relative dependance by means 
of the attribute **DependsOnTargets** of the **Target** element. 

In the following example the chain af calls will be the following TargetA => TargetZ=> TargetD.

```
<Target Name="TargetA">

</Target>
	...
<Target Name="TargetD" DependsOnTargets="TargetZ">
	...
</Target>
	...
<Target Name="TargetZ" DependsOnTargets="TargetA">
	...
</Target>
	...
<Target Name="TargetR" DependsOnTargets="TargetA">
	...	
</Target>
```

#### Important fact about DependsOnTarget

Any Target that is set as a value of the **DependsOnTarget** attribute of any other
Target that is in the execution path provided to MSBuild **will be executed only once!**
This is always the case even when there might be multiple Target elements which invoke 
the same Target via the **CallTarget** element.

#### AfterTargets and BeforeTargets attributes

The semantic of **AfterTargets** and **BeforeTargets** is a bit different. These attributes 
both specify that when the specific Target to which they are applied is run from the console
**directly** the Target value of the attribute must also be run before or after respectively.

In the following example the sequence will be A => C => B but only when TargetC is invoked 
directly as in 

```
> msbuild dosomething.msbuild /t:"TargetC"
```

```
<Target Name="TargetA" BeforeTargets="TargetC">

</Target>
	...
<Target Name="TargetB" AfterTargets="TargetC">
	...
</Target>
	...
<Target Name="TargetC">
	...
</Target>
```

#### Conditional Targets

Conditions on the execution of Targtes can be set by means of the attribute **Condition**
whose value may be an expression evaluated at run time. Normally the expression makes use
of custom or built-in MSBuild properties or any combination of them into an extression that
evaluates to a boolean. 

```
<PropertyGroup>
	<DoIt>true</DoIt>
	<TestIt>yes<TestIt>
<PropertyGroup>

<Target Name="TargetA" DependsOnTargets="TargetC;TargetD">
	...
</Target>
	...
<Target Name="TargetB">
	...
</Target>
	...
<Target Name="TargetC" Condition="$(DoIt)">
	...
</Target>
<Target Name="TargetD" Condition="$(TestIt)=='yes'">
	...
</Target>
```

---

## Tasks

* Tasks are descendant of the Task class which implements ITask. 
* ITask declares the Execute method that is invoked by MSBuild when the *.msbuild is processed.

---

## Properties

- Properties are used to declare run-time values which can be consumed by the Tasks in
  the various Targets. 

- Properties can be combined in other properties.

- The are a number of MSBuild built-in properties.

- One frequent application of Properties is to compose paths to directories and files or search paths.

- Properties do not have to have a value. If no value for a named property is given and the property is used anywhere in the **msbuild** file **no runtime error is generated**. 

- Properties can also be injected at run time as shown below. When the values of properties is injected at runtime any properties declared in the **msbuild** file or **rsp** file bearing the same name will have its value overridden by the value injected at run-time.

```
>msbuild dosomething.msbuild @responsefile.rsp /p:Name=Lisa
```
- The following shows an exampleof declared properties within a **msbuild** file.

```
<PropertyGroup>
    <Name>Peter</Name>
    <FullName>$(Name) Pan</FullName>
</PropertyGroup>
...
...
<Target Name="DefaultImportanceMessage">
    <Message Text="Default Importance Message: Hello $(FullName)!"/>
</Target>
```

- There are lost of [MSBuild reserved and well-known properties](https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild-reserved-and-well-known-properties?view=vs-2017) to use in custom **msbuild** scripts. The value of these properties is assigned by the mbuild runtime environment. One way to show the complete list of the builtin properties that are used at runtime and their values is to **run the msbuild file** with **verbosity set to the diagnostic level**.

```
> msbuild dosomethong.msbuild @responsefile.rsp /v:diagnostic /t:ReservedProps
```

---

## Items

- Items are used to define array of items with some **metadata** attached to them. One of the most frequent user cases of ```<ItemGroup>``` and the contained items is to collect file paths.

- It is important to understand that the items are not just text values instead they are objects with associated metadata which can be accessed withing the **msbuild** script with the following syntaxt ```@(Rsps->'%(ModifiedTime)```. The metadata available on the item depends on its type.

- [MSBuild items](https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild-items?view=vs-2017)  

```
<PropertyGroup>
    <Property1>Property1Value</Property1>
    <Property2>$(Property1)+Property2Value</Property2>
    <RspPath>$(MSBuildProjectDirectory)\*.rsp</RspPath>
  </PropertyGroup>

  <!--An item group that pulls in files-->
  <ItemGroup>
    <Rsps Include="$(RspPath)"/>
  </ItemGroup>
  ...
  ...
  <!--Accessing declared items-->
  <Target Name="ListRps">
    <Message Text="@(Rsps)"/>
    <Message Text="@(Rsps->'%(ModifiedTime)')"/>
  </Target>
```

## Custom items

It is also possible to declare custom items within a **msbuild**.

```
<!--An item group to hold custom items-->
  <ItemGroup>
    <Staff Include="code1">
      <Name>Jane</Name>
      <Surname>Simpson</Surname>
    </Staff>
    <Staff Include="code2">
      <Name>Peter</Name>
      <Surname>Kleng</Surname>
    </Staff>
    <Staff Include="code3">
      <Name>Paul</Name>
      <Surname>Tinker</Surname>
    </Staff>
  </ItemGroup>
  ...
  ...
  <!--Accessing a declared custom items-->
  <Target Name="ListStaff">
    <Message Text="@(Staff)"/>
    <Message Text="@(Staff->'%(Name)')"/>
    <Message Text="@(Staff->'%(Surname)')"/>
  </Target>
```

---

## Logging and Verbosity

When any **\*.msbuild** is run in any of the available execution
tools such as the command line window or the powershell window etc. some of the tasks in the targets declared in the build file **\*.msbuild** may produce some textual output. For example, the ```<Message>``` tag declares a executable task of type message that prints a message to the output stream.

The textual information that is available available in the output stream depends on the level of logging set for the output stream and that of the task that produces such output. 

For example ```<Message Text="High Importance Message"             Importance="high"/>``` outputas in red or similar colour while ```<Message Text="High Importance Message"             Importance="low"/>``` does not reach the output stream as the default level of the output stream is **normal**. 

The verbosity level of the putput stream can controlled by means of the **msbuild.exe** switch **/v**. The levels available are the following.

```
/v:minimal
/v:normal
/v:detailed
/v:diagnostic
```

---

## MSBuild Extension Pack

- [MSBuild Extension Pack](http://www.msbuildextensionpack.com/)

The MSBuild Extension Pack provides the largest collection of MSBuild Tasks, MSBuild Loggers
and MSBuild TaskFactories available.

---