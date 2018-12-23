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

## How to link the execution of targets 

### The Explicit Invokation Model

1. Use the ```<CallTarget>``` task as shown below. One important thing to understand about this model of execution of targets is that ```TargetA``` compltes only after the execution of ```TargetD```. This is the same model as the **synchronous invocation stack** of functions in .Net.

```
<?xml version="1.0" encoding="utf-8"?>
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003" 
DefaultTargets="TargetA">
  <Target Name="TargetA">
    <Message Text="This is TargetA"/>
    <CallTarget Targets="TargetB;TargetC;TargetD"/>
  </Target>
  <Target Name="TargetB">
    <Message Text="This is TargetB"
             Importance="normal"/>
  </Target>
  <Target Name="TargetC">
    <Message Text="This is TargetC"/>
  </Target>
  <Target Name="TargetD">
    <Message Text="This is TargetD"
             Importance="high"/>
  </Target>
</Project>
```

2. The same as before but the chaining is declared by means of multiple ```<CallTarget>``` tasks.

```
<?xml version="1.0" encoding="utf-8"?>
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003" 
DefaultTargets="TargetA">
  <Target Name="TargetA">
    <Message Text="This is TargetA"/>
    <CallTarget Targets="TargetB;TargetC;TargetD"/>
  </Target>
  <Target Name="TargetB">
    <Message Text="This is TargetB"
             Importance="normal"/>
  </Target>
  <Target Name="TargetC">
    <Message Text="This is TargetC"/>
  </Target>
  <Target Name="TargetD">
    <Message Text="This is TargetD"
             Importance="high"/>
  </Target>
</Project>
```

### The Implicit Invokation Model

In this model of execution the ```DependsOnTargets="TargetA"``` attribute of the ```<Target>``` is exploited. One **important difference between the implicit and explicit models of execution** is that in the implicit model the target that is used as a value of the **DependsOnTargets** attribute i.e.  ```DependsOnTargets="TargetA"``` is going to be executed only once regardless the number of other targets that dependsupon it.

```
<?xml version="1.0" encoding="utf-8"?>
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003" 
DefaultTargets="TargetD">
  <Target Name="TargetA">
    <Message Text="This is TargetA"/>    
  </Target>
  <Target Name="TargetB" DependsOnTargets="TargetA">
    <Message Text="This is TargetB"
             Importance="normal"/>  
  </Target>
  <Target Name="TargetC" DependsOnTargets="TargetB">
    <Message Text="This is TargetC"/>    
  </Target>
  <Target Name="TargetD" DependsOnTargets="TargetC">
    <Message Text="This is TargetD"
             Importance="high"/>   
  </Target>
</Project>
```

### AfterTargets and BeforeTargets

1. Usage of the ```AfterTargets``` attribute requires care. Following there are two examples to use in order to better understand the execution model.

This invokation only runs TargetA. It invokes the direct execution of TargetA which is set as default target by ```DefaultTargets="TargetA"```.

```> msbuild 10.LinkTargetsTogether.msbuild```

This invokation runs TargetB followed by TargetA.

```> msbuild 10.LinkTargetsTogether.msbuild /t:TargetB```

For more detailed information on the mechanics of the ```AfterTargets``` and ```BeforeTargets``` attributes of the ```<Target>``` refer to the accompaining examples. 

```
<?xml version="1.0" encoding="utf-8"?>
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003" 
DefaultTargets="TargetA">
  <Target Name="TargetA" AfterTargets="TargetB">
    <Message Text="This is TargetA"/>    
  </Target>
  <Target Name="TargetB">
    <Message Text="This is TargetB"
             Importance="normal"/>  
  </Target>  
</Project>
```
---

## Use MSBuild with Visual Studio solutions and projects 

Visual Studio solutions **\*.sln** files **are not** directly valid XML that is understood by the MSBuild engine. However, the MSBuild engine has been designed to undestand such syntax and parsed it in memory into valid MSBuld XML instructions at runtime which can then be executed. 

On the contrary **\*.csproj** files are instead valid MSBuild XML files. There are typical tags which appears in **\*.csproj** such as ```<Reference>``` that marks a dependency reference to a project or assembly and the ```<Compile>``` which is a reference to **\*.cs** files that are part of the project source code and need to be compiled.

Normally the most important work done by the MSBuild engine in relation to **\*.csproj** is to **invoke the csc.exe compiler** and **pass to it all the artifacts of the project as argument of the invokation**.

It is possible to have a look at the typical contents of any **\*.csproj** by unloading the project and then open it for editing. The following instruction to the MSBuild engine is imprtant and kicks start the compilation process by executing ```Microsoft.CSharp.targets```.  

```
<Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
```

You can see the contents of this file online for example [here](https://referencesource.microsoft.com/#MSBuildFiles/C/ProgramFiles(x86)/MSBuild/14.0/bin_/amd64/Microsoft.CSharp.targets).

---

## MSBuild Task Reference

The [MSBuild Task Reference](https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild-task-reference?view=vs-2017) is a repository of MSBuild tasks that are included with it.

## MSBuild Extension Pack

- [MSBuild Extension Pack](http://www.msbuildextensionpack.com/)

The MSBuild Extension Pack provides the largest collection of MSBuild Tasks, MSBuild Loggers
and MSBuild TaskFactories available.

---