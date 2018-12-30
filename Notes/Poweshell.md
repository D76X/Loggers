# Powershell

## References

1. [PowerShell: Getting Started](https://www.pluralsight.com/courses/powershell-getting-started)

2. [Your First Day with Powershell](https://app.pluralsight.com/library/courses/powershell-first-day/table-of-contents)

## Powershell Tools

### Powershell ISE 
- [How to start the Powershell ISE](https://social.technet.microsoft.com/Forums/en-US/56e2ef15-66fc-4af7-acc5-21fdc98438f8/where-is-the-powershell-ise?forum=W8ITProPreRel)

## RSAT 
- [Remote Server Administration Tools for Windows 10](https://www.microsoft.com/en-us/download/details.aspx?id=45520)  

### PowerShell Cmdlet and Functions

- https://stackoverflow.com/questions/18469012/why-are-scripted-cmdlets-listed-as-functions

A cmdlet (pronounced "command-let") is a lightweight Windows PowerShell script that performs a single function. A command, in this context, is a specific order from a user to the computer's operating system or to an application to perform a service, such as "Show me all my files" or "Run this program for me." Cmdlets amy be written by using one of the 
.Net languages i.e. C# or F#.

Potentially isn't much difference between a function and a cmdlet, but that depends on how much work you're willing to put into writing the function. The real difference between a mere function and a full cmdlet is that cmdlets support powerful parameter bindings. You can use positional parameters, named parameters, mandatory parameters, and even do basic parameter validation checks—all by simply describing the parameter to the shell. Functions,are written entirely in script, have the same capabilities as a “real” cmdlet written in C# or Visual Basic and compiled in Visual Studio. These advanced functions (they were originally called “script cmdlets” early in the v2 development cycle) help you write more flexible functions you can then use seamlessly alongside regular cmdlets.

### PowerShell getting help on Cmdlet and Functions

```
get-help <cmdlet>
```

In particular look at the section ```RELATED LINKS``` in which Cmdlets and Functions are provived that are related to  the given ```<cmdlet>``` parameter. Instead the ```REMARKS``` section of the
output usually shows examples of the usage for the Cmdlet or Function.


### PowerShell Tips

1. In order to break out of the output or the execution of a Cmdlet or Function use ```CTRL+C```.

---

## Basic Commands

### Getting Help

| Command | Results |
| ------- | ------- |
| `get-help` | shows how to get help on areas of PowerShell.|
| `help` | help is an alias for get-help.|
| `get-help | more` | as above but tabbed.|
| `get-help *DNS*` | find all help docs for any cmdlet that has DNS in its name.|
| `get-help get-service` | Obvious.|
| `help get-service -examples` | pulls out only the examples of usage. |
| `get-help get-service -detailed` | provides more output. |
| `get-help get-service -full` | provides more output.|
| `get-help get-service -online` | provides up-to-date help from  the online documentation.|
| `help *about*` | shows all the about files in the help system.|
| `help ` |.|
| `help about_aliases` | shows the help about aliases.|
| `update-help` | Updates the local PowerShell help files - must run as admin.|
| `help get-command` | Shows the help for the get-command cmdlet.|
| `help get-command -examples` |  Shows the examples from the help for the get-command cmdlet.|
| `` |.|

---

## Querying the History

The PowerShell console retails the history of the commands invoked for the session. This history is useful when working towards composing
a script for a cmdlet, function etc. by trial and error. It is also
useful to save the commnads of the session to a text file as illustrated in the table below. 

Another very useful tool is the **start-transcript/stop-transcript** pair of comdlets. The first starts the process that saves the history of the session to a text file of your choosing while the second obviously stops it. This saves the complete history of teh session and includes important information regarding it. Moreover, **it captures any errors that may be thrown by the ciommands run in the session** which is complete, useful information when trying to debug a script. **Transcript** is more powerful than simply dumping the history to a text file as in addition to extra information about the session and errors it also captures any output obtained from the command run as part of the session.


| Command | Results |
| ------- | ------- |
| `get-help history` | Shows the help for the alias history toget-history.|
| `get-history` | Lists all the commands used in the current session.|
| `invoke-history -id 1` | Runs the command at the top of the history list for the current session.|
| `get-history | out-file c:\history.txt` | Save the session history to a text file.|
| `Notepad c:\history.txt` | Open the said file in Notepad.|
| `help start-transcript` | Gets help on the start-transcript cmdlet.|
| `start-transcript path c:\mysession.txt` | Captures the entore PowerShell session into a text file.|
| `stop-trascript` | It stops the transcription of the session to text file.|
| `` |.|

---

## Querying for PowerShell verbs

| Command | Results |
| ------- | ------- |
| `get-verb` | shows all the verbs available in PoweShell.|
| `get-verb | more` | use the spacebare to display the next window of results.|
| `get-verb -verb s*` | shows all the verbs that starts with the letter s.|
| `get-service -Name M*` | shows all the services that starts with the letter M.|
| `gsv -Name M*` | the same as above but the **gsv** alias is used.|
| `gsv -Name M*, S*` | get the services starting by m and then those starting by s.|
| `` |.|

---

## Objects, Properties, Methods, Members and Piping.

Normally the output from and often also the imput to any PowerShell cmdlet or Function is modelled as an **object**. Any object have **members** which may be either **properties** or **methods**. **Properties** bear values for the object while **methods** are actions thatcan be performed over the object just as in the traditional OOP model.

The output of any cmdlet or Function can be piped into another cmdlet or Function by means of the **pipe** ```|```.

The ```select-object``` cmdlet is often used in combination with other command to filter the objects provided as output into objects bearing only a subset of properties. In addition ```where-object``` is also useful as it allowed to filter the output of a cmdlet or Function on a specified condition.

| Command | Results |
| ------- | ------- |
| `help get-member` | Shows the help about the get-member cmdlet.|
| `get-service | get-member | more` | Shows all the members of the object type for the output from the cmdlet get-service .|
| `get-service | select-object Name,Status | more` | Selects only the properties Name and Status of any object from the output of the cmdlet get-service.|
| `get-service | where-object status -eq "stopped" | select-object Name,Status | more` | Filters the output of get-service to only the stopped services and projects it into an object with only Name and Status properties.|
| `get-service | where-object status -eq "stopped" | format-list | more` | As above but formats the output into a list insted of the standard Powershell table alos all the properties are shown for each item of the list.|
| `` |.|

---

## Querying for system services

| Command | Results |
| ------- | ------- |
| `get-service` | List all the OS services and their current status.|
| `get-service | where-object Status -eq 'Stopped'` | List all stopped services.|
| `get-service | where-object Status -eq 'Stopped' |  export-csv $path` | Export the list of all stopped services to a CSV file of specified full path.|
| `get-service | where-object Status -eq 'Stopped' |  out-file $path` | Export the list of all stopped services to a text file of specified full path.|
| `notepad $path` | Opens the file created above.|
| `get-service | where-object Status -eq 'Stopped' | select-object Status, Name, DisplayName` | List of the three properties Status, Name, DisplayName of all Stopped services.|
| `get-service | where-object Status -eq 'Stopped' | select-object Status, Name, DisplayName |  export-csv $path` | As above but exported to a CSV file.|
| `` |.|

---

## Formatting

The output of Cmdlets and Functions to the console of files can be formatted in different ways by means of a set of three Cmdlets.

- Format-list (fl)
- Format-table (ft) >> for more details about each item
- Out-Gridview >> opens a separate GridView Window with complex filter and sort controls.

| Command | Results |
| ------- | ------- |
| `help Format-list` | Gets help on the Cmdlet Format-List.|
| `help format-list -examples` | Pulls out only the examples.|
| `get-service|fl Name,Status|more` | The output of get-service is projected into a list of items with properties Name and Status.|
| `get-service|ft -AutoSize|more` | Project into a table and autosizes its column width - this prevents cropping and uses the full size of the console window.|
| `get-service|ft -Wrap|more` | This wraps longer content on any columns on a separate line.|
| `get-service|ft -Wrap - Autosize|more` | The two above combined.|
| `get-service|out-gridview` |Pushes the output to a separate GridView with complex filter and sort controls.|
| `` |.|

---

## Some interesting commands

| Command | Results |
| ------- | ------- |
| `$PSVersionTable` | It shows details about the version of PS on the system.|
| `ise` | Starts the Integrated Scripting Environment.|
| `notepad $path` | Opens the file created above.|
| `get-command | more` | Shows all the commands i.e. cmdlets, functions, aliases, etc. that are installed on the system.|
| `get-command -Type Cmdlet | Sort-Object | more` | Pulls out only cmdlets and pipes into the sort-objetc cmdlet.|
| `get-command -Type Cmdlet | Sort-Object -Property Noun | Format-Table -GroupBy Noun | more` | 
sorts all the cmdlets by their Noun property and formats the output into a table grouped by the Noun property.|
| ``get-command -Type Cmdlet | Sort-Object -Property Noun | Format-List -GroupBy Noun | more` |.|
| `get-command -name *IP* | more` | Shows all the installed commands that include IP in their name.|
| `get-command -name *IP* | Sort-object -Property name | Format-Table | more` | Pipe the results into the sort command by the property name.|
| `get-command -name *IP* | Sort-object -Property name | Format-Table -GropuBy CommandType | more` | Sort by name and group by CommandType.|
| `get-command -name *IP* -module NETTCPIP | more` | Limits the search to the module NETTCPIP.|
| `` |.|
| `` |.|

---

## Typical Diagnostic Process

A good **general strategy for diagnostic** of problems on a system by means of PowerShell is based on three steps and important Comdlets.

1. Get-Command >> research the commands that address a user case.

2. Help (-example) >> investigate the usage of the relevant commands. 

3. Get-Member >> finds out which members are available on a the objects returned as output of a command in order to project it into a meaningful and relevant subset.

### Querying for System resources via get-command

One way to access system wide resources and diagnostics is by menas of teh ```get-command`` Cmdlets. In the folowing several examples of diagnostics of this kind are presented.

### The Windows Management Instrumentation (WMI) and Common Information Model (CMI).

Is the **Windows OS component** that can be queried to gather system-wide diagnostics. There arebasically a few PowerShell commands that can be used to investigate the Hardware and Network resources of the System according to two sets of corresponding objects that is the **WMI and CMI objects**. These are mostly the same but are accessed by different Cmdlets. 

WMI is based on the **Common Information Model (CIM)** which is an open **standard** that defines how managed element in an IT environment are to be represented as a **common set of objetcs as well as the relationships between them**. CIM was introduced in **PowerShell vesrion 3.0 with Get-CimInstance** while to access the **WMI Repository** the **Get-WmiObject** Cmslet is used.

WMI information is stored in the **WMI Repository** in which information is organized into **namespaces**. One particualarly important and useful namespace is **CIMv2**. Information in the WMI Repository is stored as objects and properties of those objects.

---

### Example 1 of diagnostic process - extraction of a counter using get-command

Look up commands that have anything to do with counters.

```
> get-command *counter* 
```

Find more information of a specific command that fits the user case at hand i.e. measuring the commited memory.

```
> help get-counter
> help get-counter -examples
```

List all the counter sets that can be queried through the get-command Cmdlet and have anything to do with memory.
From this list you may read the ```Description``` value of the returned objects and decide which of the CounterSetName provides the counter that you need.

```
> get-counter -listset *memory*
```

For example for the CounterSetName Memory the description is the following.

```
The Memory performance object  consists of counters that describe the behavior of physical an virtual memory on the computer...
```

It is now possible to pick the specific counter.

```
get-counter -listset *memory* | where CounterSetName -eq Memory
```

In order to extract the values that are relevant to the selected counter the Paths property must be selected from the counter set object. Of course, the Paths property is itself an array of object which is possible to ```expand``` as follows.

```
get-counter -listset *memory* | where CounterSetName -eq Memory | select -expand Paths
```

Lastly, the value of the counter of interest can be extracted as as follows.

```
get-counter "\Memory\% Committed Bytes in use"
```
---

### Example 2 - accessing the **Common Information Model (CIM)** via get-cmiinstance and get-cmiclass investigate the disks and volumes

The **Windows OS component** can be queried to gather system-wide diagnostics. WMI is based on the **Common Information Model (CIM)** which is an open **standard** that defines how managed element in an IT environment are to be represented as a **common set of objetcs as well as the relationships between them**. CIM was introduced in **PowerShell vesrion 3.0 with Get-CimInstance**.

For example, one of the CMI objects is the ```Win32_PhysicalMedia``` object that can be quesried as follows.

```
get-ciminstance Win32_PhysicalMedia
get-ciminstance Win32_PhysicalMemory
get-ciminstance Win32_PhysicalMemory | select Capacity
```

More information of the vailable WMI classes on Windows OS can be found at the follwing URLs.

[Retrieving a WMI Class](https://docs.microsoft.com/en-us/windows/desktop/WmiSdk/retrieving-a-class)  
[Win32_PhysicalMemory class](https://docs.microsoft.com/en-us/windows/desktop/cimwin32prov/win32-physicalmemory)

As usual in PowerShell one of the most useful assets is the possibility to investigate the availability of commands and objects using query without the need to necessarily refer to the corresponding documentations. For example, the following queries employes the ```get-cmiclass``` cmdlet to query for any the WIM classes (CIM objetcs) that have anyting to do with ```disk``` as part of their class name.

```
get-cimclass -ClassName *disk*
```

The results obtained by this Cmdlet includes **a set of CMI and WMI objetcs** normally these objects overlap their responsibilities but the CMI version adhere to an open standard. Of course, **WMI objetcs** can then be queried with the ```get-wmiobject``` Cmdlet while **CMI objects** can likewise be queried bymeans of the ```get-cmiinstance``` Cmdlet. 

For example the following command brings back the list of logical volumes (disks) including mapped drives that are available on the system as a set of WMI objects.

```
get-wmiobject -Class Win32_logicaldisk
```

---

### Example 3 - accessing the **Common Information Model (CIM)** via get-cmiinstance and get-cmiclass investigate the BIOS

In a similar fashion to what was done in the prvious example the steps below illustrate the investigative process once again.

Find all theCIM classes taht have something to do with the BIOS.

```
get-cimclass *BIOS*
```

Find more info about one interesting candidate.

```
get-cimclass WIN32_BIOS | select -Property Description
```

Inspect the WMI object or the CIM instance to check its properties and their values.

```
get-wmiobject WIN32_BIOS
get-ciminstance WIN32_BIOS
```

Pull out individual details about the system BIOS.

```
get-wmiobject WIN32_BIOS -Property Version
get-wmiobject WIN32_BIOS | select -Property Version
get-wmiobject WIN32_BIOS | select -Property SerialNumber
get-wmiobject WIN32_BIOS | select -Property Version, SerialNumber
...
```

---

### Example 4 - inspect the system events in the event viewer

System events are logged to the Event Viewer. We are interestd to know more about recent event on the machine butwhere to start?
One of the most effective ways to start any investigation is to first explore whether there exist Cmdlets which can help to do wat we need to do. The ```get-command``` approach is the same used in **example 1**.

Find out which Cmdlets are available on teh system that have anything to do with events. The query the Cmdlets for help to know more about what they are meant to accomplish until you find what fits your case.

```
get-command *event*
gcm *event*

...
help Get-EventLog
help Get-EventLog -examples
...
```

For example in this case we would like to use one of the examples for Get-EventLog as follows.

```
get-eventlog -log application -source outlook | where {$_.eventID -eq 34}

This command gets events in the Application event log where the source is Outlook and the event ID is 34. Even though Get-EventLog does not have an EventID parameter, you can use the Where-Object cmdlet to select events based on the value of any event property.

```

In our case with the help of the example above among the other we can conjure up something like the following.

```
get-eventlog -log system -newest 1000 | where-object eventID -eq '1074'
```

The ```-log system``` specifies that the system log and not the applications log is to be queried for the 1000 most recent system events. The resultset is filtered by a where clause to only pick the   syset reboot events which are identified by its ID=1074.

The output to the console from the previous query can be improved by providing some **formatting instructions**.

```
get-eventlog -log system -newest 1000 | where-object eventID -eq '1074' | format-table machinename, username, timegenerated -autosize
```

---

| Command | Results |
| ------- | ------- |
| `get-counter` | Used to queried a set of counters available on the OS.|
| `get-cmiclass` | Used to access the **Common Information Model (CIM)** class metadata on the OS.|
| `get-wmiobject` | Used to access the **Windows Management Instrumentation (WMI)** objetcs.|
| `get-cmiinstance` | Used to access the **Common Information Model (CIM)** instances on the OS.|
| `get-eventlog` | .|