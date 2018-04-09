# Semantic Logging

The are two projects which provide the basis for logging through ETW.

1. SemanticLogging
2. SlabManagementTools

The __SemanticLogging__ project is used to provide the __EventSource__ implementations. 
These are classes that inherits from __System.Diagnostics.Tracing.EventSource__ and their
purpose is to provide the semantic definition of the events. 

This project references the NuGet package __EnterpriseLibrary.SemanticLogging__ which provides 
extension to the definitions in the namespace __System.Diagnostics.Tracing.EventSource__ via 
the additional namespace __Microsoft.Practices.EnterpriseLibrary.SemanticLogging__. However, 
this reference may not be strictly necessary for this project. 

The __SlabManagementTools__ is a library that can be referenced by any other project in the 
solution in order to equip it with __in-process__ semantic logging. The __SlabManagementTools__ 
must have a reference to __EnterpriseLibrary.SemanticLogging__ Nuget package because this provides 
the namespace __Microsoft.Practices.EnterpriseLibrary.SemanticLogging__ in which the __ObservableEventListener__
class is defined. The __ObservableEventListener__ is an implementation of __IObservable\<EventEntry\>__ 
which can be used as a listeners for event sources. 

The __EnterpriseLibrary.SemanticLogging__ provides the definition of sinks 

### Use In-Process

Semantic logging can be used in-process. This means that the listeners In order to use the logger in-process 

***

### Use Out-of-Process


***

### Semantic Logging Service (controller) set-up
 
Semantic logging is most useful when used out-of-process. In order to do so a specific service must be installed 
on the machine where the tracing from the application that uses EventSource decendants is to be monitored or collected.

The service can be installed on the developer machine by using NuGet. The instructions on how to do so are found at
the post below.

???
In the architecture of ETW this service is a __controller__. For example, another ETW controller of a different kind is 
Perfview. Controllers are one end of the ETW pipeline, they set-up listeners and filters fro the messages that stem from
the __providers__ which tipically are implementations of  the __EventSource__ class. The ETW events emitted by the provi
???

[Installing and running the Out-of-Process Windows Service/Console Host](https://msdn.microsoft.com/en-us/library/dn774996(v=pandp.20).aspx)  

In Summary.

1. Use a dummy project in Visual Studio just to download the NuGet __EnterpriseLibrary.SemanticLogging.Service__ which contains the service 
   to install or run. I have downloaded this into the packages folder for the solution. 

2. The __SemanticLogging-svc.exe__ is the service binary that can be installed and started on the machine as service and 
   __SemanticLogging-svc.exe.config__ is the files for its configuration as a service while __SemanticLogging-svc.xml__ is 
   the file where you setup the listeners with the corresponding sources, sinks etc. for the machine-wide collection of ETW
   events.

3. In get the service installed start either a __admin__ command prompt or powershell window pointing to the folder where the 
   __SemanticLogging-svc.exe__ is located. In our case it is ```C:\GitHub\Loggers\packages\EnterpriseLibrary.SemanticLogging.Service.2.0.1406.1\tools```.

4. Run the __install-packages.ps1__ in the powershell window or use __> powershell install-packages.ps1__ in the command prompt.
   This downloads all trhe dependencies that are required by __SemanticLogging-svc.exe__ ot run as a service into the __tool__ folder.

5. The in either the powershell window or command shell run __SemanticLogging-svc.exe -install__ or ____SemanticLogging-svc.exe -start__.
   This willinstall the executable as a service on the machine and following the execution of the command it should be found in the list of services 
   available on teh machine - its name n the list is __Enterprose Library Semantic Logging Out-of-Process Service__ it might not be running if the 
   -start option was not used. You can start it manually from the service panel or change the properties to start automatically.

6. Change __SemanticLogging-svc.xml__ to set the service up according to your needs.

7. If the service is running start the applications that emit the ETW events of interest to let the service collect them produce the inputs for the 
   configured listeners according to the filters as defined in the __SemanticLogging-svc.xml__.

8. If __SemanticLogging-svc.xml__ defines file-based sinks and they are empty it might be the case that __SemanticLogging-svc.exe__ is running under
   an account that does not have enough permission ot write to the files that are used as logs. Either changes the account used to run the service to
   one with enough permissions or changes the permission on the files used to log the messages.

***

### Examples of configuration of sinks

***

### Rules to apply to the design of any EventSource

***

### Create a manifest

***

### Formatters

***

### Color Mappers

***

### How to send ETW events to the Event Viewer

***

### How to collect events into a .etl file

***

### How to use Perfview with ETW

***

### Severity Level Guidelines

1. __Critical__ - major failures such as wrong data, no DB connection, etc.		

2. __Error__ - there is a problem.

3. __Informational__ - something interesting but expected has happned. 

4. __LogAlways__ - no level filtering is done on any messages logged at this level.  
  
5. __Verbose__ - some event may have lengthy payloads with more detailed info.

6. __Warning__ - for events that have being detected as significant but are neither critical or errors. 

***

### References

[How can I organize EventSources for the Semantic Logging Application Block?
](https://stackoverflow.com/questions/22582149/how-can-i-organize-eventsources-for-the-semantic-logging-application-block)  

[6 – Logging What You Mean: Using the Semantic Logging Application Block](https://msdn.microsoft.com/en-us/library/dn440729(v=pandp.60).aspx)

[What is the best way to log exceptions using ETW?](https://stackoverflow.com/questions/19053307/what-is-the-best-way-to-log-exceptions-using-etw)

https://blogs.msdn.microsoft.com/agile/2013/02/07/embracing-semantic-logging/

***