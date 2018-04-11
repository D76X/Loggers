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

The __EnterpriseLibrary.SemanticLogging__ provides the definition of sinks.

***

### Sinks

Once one or more EventSource descendants are designed applications can either set a direct reference to the project(s) 
where these EventSource are defined or their binaries and start adding code to invoke their methods to log messages. 
These messages/events must ultimatly go somewhere so that they can be seen by consumers. Consumers are either humans or tools.
In order to capture the log messages it is required to set-up one or more sinks which will be the recepients of the logged
messages/events. Each sink can be set-up such that it also specifies one or more filters in order for the sink to receive 
only a subset of all the emitted events according to the definition of the filter. In addition to filters a sink may also 
specify a formatter and color mappers. For example, the simplest sink would be the Console sink that allow the events to be 
displayed on a command shell and possible formatters for this sink may be JsonFormatter, TextFormatter, XmlFormatter etc.

***

### Use semantic logging out-of-process

***

### Use semantic logging in-process

Semantic logging can be used in-process, but what does this exaclty mean and how does it happen?      

The simplest way Semantic Logging can be employed by an application is in-process which implies that the sink(s) 
are set up and run in the same process as the application that causes the log events to be emitted. __The most 
important implication__ with this approach is that if the application process crashes due to an uncaught exception
the also the sinks will crach and will not be able to collect all the events. For example, if the sick is a Console
sink on the application crash all is lost otherwise if the sink is a flat file or rolling file the crash might prevent
the sink from completing the write operation of buffered events available just prior to the crash end uncaught exeption 
and such events will be lost.

The in-process set up is the simplest because ot does not rely on starting a service or configuring it via an XML file,
it all reduces to simply run the application and a few lines of codes are used in Application.OnStart to set-up the sink(s). 

In order to use Semantic Logging in-process the application must reference either the __EnterpriseLibrary.SemanticLogging__
(this is __SLAB__) NuGet package directly or a custom library that reference it on its behalf as described above. __SLAB__
comes equipped with only the Console sink by default but it might also be used in conjunction with the __SQL__, __Azure Tables__
and __File__ sinks by adding the respective additional NuGet packages.

__The other important implication__ to keep in mind is that the transport mechanism of the events in in-process set-ups is __NOT__
ETW events! ETW is a piece of OS infrastructure and it is used to transport logged events only when the set-up is out-of-process.
The mechanism used by the in-process architecture is not as fast and as robust as ETW.

Ulitmately, the in-process set-up is a simpler more direct way to use EventSource to log events which offer easier set-up and 
configuration with respect to the out-of-process alternative. 

Out-of-process and in-process set-ups can be used in conjuction if so desired.

***

### Semantic Logging Service out-of-process set-up and usage
 
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

9. When you make chnages to __SemanticLogging-svc.xml__ the service must be restarted to apply them.

***

### Use the SemanticLogging-svc.exe as a console application.

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

### Use the EventSourceAnalyzer to verify any custom EventSource

1. Create a unit test project
2. Install the NuGet package __EnterpriseLibrary.SemanticLogging.EventSourceAnalyzer__ to the test project.
3. Add project references to the test project to the project(s) where the EventSource classe(s) are defined. 
3. Add the following unit test.

``` 
   
    using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Utility;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	namespace SemanticLogging.Tests {

    /// <summary>
    /// Refs
    /// https://msdn.microsoft.com/en-us/library/dn440729(v=pandp.60).aspx#sec9
    /// https://dzimchuk.net/troubleshooting-slab-out-of-process-logging/
    /// </summary>
    [TestClass]
    public class EventSourceValidationTest {

        [TestMethod]
        public void ShouldValidateApplicationEventSource() {

            EventSourceAnalyzer.InspectAll(ApplicationEventSource.Logger);
        }
    }
}

```
 

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