--------------------------------------------------------------------------------------------------------------------------------------------

LogXtreme.WinDsk

It's the WPF Windows Desktop application.

Some References to relevant topics are given below.

For the main window grid
https://social.msdn.microsoft.com/Forums/vstudio/en-US/c7edafe9-d4ac-4bd8-ac25-f4482cfdaa75/dockpanel-stackpanel-or-grid?forum=wpf

--------------------------------------------------------------------------------------------------------------------------------------------

------------------------------------
ENABLE LOGGING TO A CONSOLE WINDOW
------------------------------------

Once the references to the projects SemanticLogging and SlabManagementTools have been added the output of the semantic logger can be 
directed to a console window by changing the properties of the project. 

1-Select the project in solution explorer and the Properties
2-In the Application tab set the Output type to Console Application regardless that is even if the app has a GUI

Now when the app is lauched a console window opens first and the output of the semantic logger may be directed to this window.
In order to do so you must set up the semantic logger in the App.xaml.cs file or equivalent start up file by overriding the OnStartup and
OnExit event.

--------------------------------------------------------------------------------------------------------------------------------------------
LogXtreme.WinDsk.Infrastructure 

It's a WPF User Control Library so that the Add item menu displays WPF related items.

On Events and Delegates
http://csharpindepth.com/Articles/Chapter2/Events.aspx
http://jonskeet.uk/csharp/events.html
https://stackoverflow.com/questions/1015166/c-event-with-explicity-add-remove-typical-event

Weak Events and implementation of the Weak Event Pattern with RX
http://blog.functionalfun.net/2009/06/managed-memory-leaks-and-their.html 
https://www.codeproject.com/Articles/29922/Weak-Events-in-C
WeakReference
https://msdn.microsoft.com/en-us/library/system.weakreference.aspx
https://www.codeproject.com/Articles/664282/Understanding-weak-references-in-NET
https://www.codeproject.com/Articles/35152/WeakReferences-as-a-Good-Caching-Mechanism
Samuel Jack's article
https://dzone.com/articles/weak-events-net-easy-way
http://blog.functionalfun.net/2012/03/weak-events-in-net-easy-way.html
Weak events in .NET using Reactive Extensions by Kenneth Haugland
https://www.codeproject.com/Tips/1078183/Weak-events-in-NET-using-Reactive-Extensions-Rx
Bridging with Existing .NET Events with IObservable.FromEventPattern
https://msdn.microsoft.com/en-us/library/system.reactive.linq.observable.fromeventpattern(v=vs.103).aspx
https://msdn.microsoft.com/en-us/library/hh242978(v=vs.103).aspx
https://msdn.microsoft.com/en-us/library/hh229705(v=vs.103).aspx
http://synchronicity-life.blogspot.co.uk/2011/12/how-to-use-fromeventpattern-in-reactive.html

***
RX can convert events to IObservable<EventPattern<SomeEventArgs>> using one of the overloads of the extension methods 
IObservable.FromEventPattern. There are two main overloads to remember according to the type of event that we want to
obtain a IObservable for.

1-An example of event defined with the generic EventHanlder<T> signature
https://msdn.microsoft.com/en-us/library/system.threading.tasks.taskscheduler.unobservedtaskexception(v=vs.110).aspx

2-An Example of an event that is defined using a delegate and the non generic EventHanlder
https://msdn.microsoft.com/en-us/library/system.collections.specialized.notifycollectionchangedeventhandler(v=vs.110).aspx
***

--------------------------------------------------------------------------------------------------------------------------------------------

CA1063: Implement IDisposable correctly
https://msdn.microsoft.com/en-us/library/ms244737.aspx

--------------------------------------------------------------------------------------------------------------------------------------------

LOgXtrem.WinDsk.TestDataGrid

INTERACTION PATTERN FOR A SINGLE READ 

1-DataSourceViewModel ExecuteStartReadingData
2-DataService.DataSourceModel.StartDataReads
3-DataSourceViewModel.SubscribeToDataModelsObservable
4-DataGridViewModel.SubscribeAndConnectToDataModelsObservable
5-SampleSourceModel.DrawSample
6-RandomGeneratorModel.GenerateSample "222 168 210 118"
7-DataService.DataSourceModel produces DataModel "222 168 210 118"
8-DataService.DataSourceModel.StopDataReads
9-DataSourceViewModel.DisposeSubscriptionToDataModelsObservable
10-DataGridViewModel.DisposeSubscriptionToDataModelsObservable

1-Execution of a command on the DataSourceVM
The DataSourceVM has access to an instance of IDataSourceModel and invokes IDataSourceModel.StartDataReads with the number of data models
to read.

2-DataService.DataSourceModel.StartDataReads implements IDataSourceModel.StartDataReads
It creates a plain observable from an instance of ISampleSourceModel by invoking ISampleSourceModel.GetSamples the number of samples to 
draw. It then turns the IObservable in an IConnectableObservable by using IObservable.Publish and passes the IConnectableObservable as 
payload of an event. The IConnectableObservable remains cold until any of its subscribers connects it.

3-DataSourceViewModel.SubscribeToDataModelsObservable
Is the event handler for the event in (2) on the DataSourceViewModel. In this case we subscribe but do not connect as we leave the duty to 
connect to the grid. The handler only sets up a handler for the OnComplete event on the observable so that when all the data models have 
been drawn the IDataSourceModel.StopDataReads can be invoked and events to notify that all the reads have been completed are raised.

4-DataGridViewModel.SubscribeAndConnectToDataModelsObservable
Is the event handler for the event in (2) (like in (3)) on the DataGridViewModel. An subscription is made to the event payload 
IConnectableObservable and an observable is created by connecting to it. This starts the process of drawing the data models from the data 
source.

5-6-7 
The sample is drawn and wrapped in a data model and this is then pushed down an observable. ISampleSourceModel sets up the original 
observable so that it completes on a specified number of item or never.

8-DataService.DataSourceModel.StopDataReads
See (2). The original observable completes and the DataService.DataSourceModel.StopDataReads raises an event to notify the view models
DataSourceViewModel and DataGridViewModel.

9-10
The view models dispose of their observables.

--------------------------------------------------------------------------------------------------------------------------------------------