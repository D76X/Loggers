Reactive Extensions - RX

Adding RX to a project

This is done via NuGet, type Reactive in the NuGet search tool and install 
System.Reactive at solution level or project level as necessary.

The Basics of RX
https://msdn.microsoft.com/en-us/library/hh242977(v=vs.103).aspx
https://app.pluralsight.com/library/courses/reactive-extensions/table-of-contents

--------------------------------------------------------
Basics ways to create an observable IObservable<T> 
more are presented leter but thsese are an introduction
to get started.
--------------------------------------------------------

In order to create an instance of IObservable<T> the System.Extension namespace makes available
static methods on the Observable class. The basic opsions to do so are as follows.

1-use range 
IObservable<int> source = Observable.Range(1, 10);

2-convert existing enumerators into observable sequences.
IEnumerable<int> e = new List<int> { 1, 2, 3, 4, 5 };
IObservable<int> source = e.ToObservable();

3-convert .NET events into observable sequences. 
This is discussed more later.

4-convert asynchronous patterns into observable sequences.

6-use timers
var source = Observable.
			Timer(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1)).
			Timestamp();

7-use a time Observable.Interval 

// COLD observable - subscription2 gets the values from 0
IObservable<int> source = Observable.Interval(TimeSpan.FromSeconds(1));
IDisposable subscription1 = source.Subscribe(...)
IDisposable subscription2 = source.Subscribe(...)

// HOT Observable - IObservable.Publish 
var source = Observable.Interval(TimeSpan.FromSeconds(1)); 
IConnectableObservable<long> hot = Observable.Publish<long>(source); // becomes hot with Publish
IDisposable subscription1 = source.Subscribe(...)
hot.Connect(); // hot is connected to the source and starts pushing values to subscribers
IDisposable subscription1 = hot.Subscribe(...)

Thread.Sleep(3000);  //idle for 3 seconds or just some time goes before subscribing to hot again

IDisposable subscription2 = hot.Subscribe(...) // this only gets values from the current hot value onwards

---------------------------------------------------------------------------------------------------------
In general RX provides two sets of mechanisms for the generation of IObservable<T>.
http://www.introtorx.com/content/v1.0.10621.0/04_CreatingObservableSequences.html

1- The first set of mechanisms are the factory methods of teh Observable class such as
	
	Observable.Range
	Observable.Timer
	Observable.Interval
	Observable.Create
	Onservable.Generate

	and some special cases

	Observable.Empty
	Observable.Return
	Observable.Never

In particulare Observable.Generate is the most flexible of all and can be employed to replicate the 
functionality of any of teh above.

2- The second set of mechanisms allow to use aynchronous and asynchronous .NET sources 

	From delegates
	From events
	From Task
	From IEnumerable
	From APM [Asynchronous Programming Model]
---------------------------------------------------------------------------------------------------------

The special cases

Observable.Return
When you need an observable that generates only a single value and then completes.
For example you have an IObservable<IObservable<Something>> and want to subscribe to the external stream and change 
the internal stream....
https://stackoverflow.com/questions/21443398/how-to-create-an-observable-that-produces-a-single-value-and-never-completes

=====================================================================================================================================

----------------------------------------------------------------------------------
Creational Pattern in RX - create IObservable<T> from RX
http://www.introtorx.com/content/v1.0.10621.0/04_CreatingObservableSequences.html
----------------------------------------------------------------------------------

// In order to produce instances of IObservable<T> we use factory memthodd defined on the Observable class.

// The following are sort of odd ones but are used in some scenarios such as for instance unit testing
var singleValue = Observable.Return<string>("Value");
var empty = Observable.Empty<string>();
var never = Observable.Never<string>();
var throws = Observable.Throw<string>(new Exception());

---------------------------------------------------------------------------------------------------------------------------

// The most common factory method used is Observanle.Create<T>
// A typical use could be the following 

    // public static IObservable<TSource> Create<TSource>(Func<IObserver<TSource>, IDisposable> subscribe){...} 
	// return an istance of IObservable<T> to a consumer so that the consumer can subscribe a IObserver<T> on it.
	// the return value also implements IDisposable so that the consumer can invoke Dispose on it once it is done with it
	// to clean things up such as releasing memory or unsubscribing events.
	
	var ob = Observable.Create<string>(
		observer =>
		{
			var timer = new System.Timers.Timer();
			timer.Interval = 1000;
			timer.Elapsed += (s, e) => observer.OnNext("tick");
			timer.Elapsed += OnTimerElapsed;
			timer.Start();
			return timer; // Timer implements IDisposable
	});

	// The is an overload 
	// public static IObservable<TSource> Create<TSource>(Func<IObserver<TSource>, Action> subscribe){...}
	// This return an action instead of IDisposable so that the consumer can still invoke the action to clean up
	// even when this clean up does not happen as part of the Dispose logic.
	
	var ob = Observable.Create<string>(
		observer =>
		{
			var timer = new System.Timers.Timer();
			timer.Enabled = true;
			timer.Interval = 100;
			timer.Elapsed += OnTimerElapsed;
			timer.Start();
			
			return ()=>{
				timer.Elapsed -= OnTimerElapsed;
				timer.Dispose();
			};
		});


---------------------------------------------------------------------------------------------------------------------------

In the previous examples Observable.Create returned an IObservable<T> that could produce an infinite sequence of values
by using a timer in its definition. By replacing the timer with calls to OnNext, OnComplete, OnError of the IObserver
instance passed as argument of the Func the IOnservable<T> may also only produce a finite number of values or even none
at all.

A simple way to create an instance of IObservable ot model a finite sequence is provided by the following example

--------------------------------------
var range = Observable.Range(10, 15);
--------------------------------------

------------------------------------------------------------------------------------------------------------
RX provides better ways to create instances IObservable<T> that model infinite sequences of values than
using a Timer. 
------------------------------------------------------------------------------------------------------------
One way could be to use soem recursive code in place of the timer as in the following illustrative example. 
------------------------------------------------------------------------------------------------------------

1- Corecursion
   Corecursion is a function to apply to the current state to produce the next state. 

	private static IEnumerable<T> Unfold<T>(T seed, Func<T, T> accumulator)
	{
		var nextValue = seed;
		while (true)
		{
			yield return nextValue;
			nextValue = accumulator(nextValue);
		}
	}

	var naturalNumbers = Unfold(1, i => i + 1);
	
	foreach (var naturalNumber in naturalNumbers.Take(10))
	{
		Console.WriteLine(naturalNumber);
	}

---------------------------------------------------------------------------------------------------

Observable.Generate

RX provides a simple way to use corecursion to produce a IObservable.
Observable.Generate can be used to produce either finite or infinite sequences depending on the 
condition parameter in its signature. 

public static IObservable<TResult> Generate<TState, TResult>(
TState initialState, 
Func<TState, bool> condition, 
Func<TState, TState> iterate, 
Func<TState, TResult> resultSelector)

takes

an initial state
a predicate that defines when the sequence should terminate
a function to apply to the current state to produce the next state
a function to transform the state to the desired output

Values are generated as fast as the iteration allows.

------------------------------------------------------------------------------------
Note: there is another overload of Observable.Generate to specify timing that is 
      explained later
------------------------------------------------------------------------------------

------------------------------------------------------------------------------------
Example : definition of the Observable.Range via Observable.Generate and corecursion
------------------------------------------------------------------------------------

public static IObservable<int> Range(int start, int count)
{
	var max = start + count;
	
	return Observable.Generate(
		start, 
		value => value < max, 
		value => value + 1, 
		value => value);
}

---------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------
Create an IObservable that models a sequence of values emitted at a fixed time interval.

Most importantly the Rx operators are the preferred way of working with timers due to their ability 
to substitute in schedulers which is desirable for easy substitution of the underlying timer. There 
are at least three various timers you could choose from if you were to use an instance of Timer
directly in the Observable.Create method as it was done earlier.

https://stackoverflow.com/questions/10317088/why-there-are-5-versions-of-timer-classes-in-net
https://web.archive.org/web/20150329101415/https://msdn.microsoft.com/en-us/magazine/cc164015.aspx

System.Timers.Timer
System.Threading.Timer
System.Windows.Threading.DispatcherTimer

others are 

System.Windows.Forms.Timer
System.Web.UI.Timer

1-
By abstracting the timer away via a scheduler we are able to reuse the same code for multiple
platforms. 

2-
More importantly than being able to write platform independent code is the ability to substitute 
in a test-double scheduler/timer to enable testing.

--------------------
Observable.Interval
--------------------
//  you must dispose of your subscription to stop the sequence.
var interval = Observable.Interval(TimeSpan.FromMilliseconds(250));
interval.Subscribe(Console.WriteLine, () => Console.WriteLine("completed"));

output : 0 1 2 3...
--------------------
Observable.Timer
--------------------
// It has several overloads
// The Observable.Timer will however only publish one value (0) after the period of time has elapsed, and then it will complete.
// you can provide a DateTimeOffset for the dueTime parameter. This will produce the value 0 and complete at the due time.
var timer = Observable.Timer(TimeSpan.FromSeconds(1));
timer.Subscribe(Console.WriteLine, () => Console.WriteLine("completed"));

output: 0 completed

// another overload Observable.Timer(TimeSpan.Zero, period)
// behaves like Observable.Interval but the first parameter lets you decide when to begin to 
// produce values.

-----------------------------------------------------------
Observable.Generate overload with timing param parameters
-----------------------------------------------------------

public static IObservable<TResult> Generate<TState, TResult>(
TState initialState, 
Func<TState, bool> condition, 
Func<TState, TState> iterate, 
Func<TState, TResult> resultSelector, 
Func<TState, TimeSpan> timeSelector)

takes

an initial state
a predicate that defines when the sequence should terminate
a function to apply to the current state to produce the next state
a function to transform the state to the desired output
a time value to determine when the next recursion occurs

// This overload of Observable.Generate can be used to replicate the functionality of the other
// time based overloads of the same class

----------------------------------------------------------------------------
// single valuw 0 @ dueTime and then complete
public static IObservable<long> Timer(TimeSpan dueTime)
{
	return Observable.Generate(
		0l,
		i => i < 1,
		i => i + 1,
		i => i,
		i => dueTime);
}
----------------------------------------------------------------------------
// generate an infinite sequence of values starting at dueTime with 0 and
// at "period"
public static IObservable<long> Timer(TimeSpan dueTime, TimeSpan period)
{
	return Observable.Generate(
		0l,
		i => true,
		i => i + 1,
		i => i,
		i => i == 0 ? dueTime : period);
}
----------------------------------------------------------------------------
// generate an infinite sequence of interger values atrting on subscription
// and each period afterwards
public static IObservable<long> Interval(TimeSpan period)
{
	return Observable.Generate(
		0l,
		i => true,
		i => i + 1,
		i => i,
		i => period);
}
----------------------------------------------------------------------------

---------------------------------------------------------------------
It is also possible to use Obserable.Generate to generate values at 
a variable rate!
---------------------------------------------------------------------

---------------------------------------------------------------------------------------------------

=====================================================================================================================================

----------------------------------------------------------------------------------
http://www.introtorx.com/content/v1.0.10621.0/04_CreatingObservableSequences.html
Creational Pattern in RX - create IObservable<T> from RX based on .NET 
synchronous or asynchronous sources

	From delegates
	From events
	From Task
	From IEnumerable
	From APM [Asynchronous Programming Model]
----------------------------------------------------------------------------------

------------------------------------
-  IObservable from delegates
------------------------------------
https://msdn.microsoft.com/en-us/library/system.reactive.linq.observable.start(v=vs.103).aspx
Observable.Start(Func<T>)
Observable.Start(Action<T>)

In this case Observable.Start gets a delegate function or action that represent the logic of a long running execution on the thread 
pool or some aysnchronous operation and returns an IObservable instance to the caller. The function/action logic will be triggered 
on subscription and the subscriber (caller) will get a value of type T if Observable.Start(Func<T>) was used or a special type Unit
is Observable.Start(Action<T>). After that the IObservable is completed.

------------------------------------------------------------------------
Overall you may think of Observable.Sart as the RX way to create a Task.
------------------------------------------------------------------------

-----------------------------------------------------------------------------------
-  IObservable from .NET events
http://www.introtorx.com/content/v1.0.10621.0/04_CreatingObservableSequences.html
-----------------------------------------------------------------------------------
-----------------------------------------------------------------------------------
-  Convert .NET events into observable sequences.
https://msdn.microsoft.com/en-us/library/hh242978(v=vs.103).aspx
-----------------------------------------------------------------------------------
-  In the following the weak event pattern is also explored and 
   RX provides a means to create weak subscription to events by 
   first using the RX overloads to turn .NET events into IObservable
   amd then using the custom exptension methods of IObservable 
   in LogXtrem.Reactive.Extension to subscribe weakly to the 
   observable seaquence of events.

   LogXtrem.Reactive.Extension
   IObservable.SubscribeWeakly => two variants

-  A special case of of .NET events to which we often wish to 
   subscribe weakly is the PropertyChangeEvent as it is used 
   in WPF for view models. For this reason a weak subscription
   to this .NET event as been formalized via an extension method 
   on any TSource : INotifyPropertyChanged as TSource.ObserveOn.
   There is also a variant for observable collections.
   
   LogXtrem.Reactive.Extension
   INotifyPropertyChanged.ObserveOn
   INotifyCollectionChanged.ObserveOn

   These methods return respectively
   
   IObservable<EventPattern<PropertyChangedEventArgs>>
   IObservable<EventPattern<NotifyCollectionChangedEventArgs>>

   the listeners will unsubscribe when the IObservable is disposed 
   by the caller and the caller only holds a weak reference to the
   source of events such that it will not prevent it from being 
   disposed.

   This if the caller wants to stop receiving vents from the 
   IObservable stream it disposes of it otherwise it will receive
   events as long as the source is alive but this will not be kept
   alive by the weak subscription.

------------------------------------------------------------------

Observable.FromEventPattern
Observable.FromEventPattern<T>

.NET events offer an asynchronous paradigm in .NET but lack of the ability to be composed as much it could be the case with 
IObservables so it makes sense to have mechanism to transalte sources of events into stream of events with RX. 

There exists several overloads of Observable.FromEventPatter to accomplish this and while the overloads can be confusing, 
they key is to find out what the event's signature is.

----------------------------------------------------------------------------------------------
//Activated delegate is a simple EventHandler

var appActivated = Observable.FromEventPattern(
h => Application.Current.Activated += h,
h => Application.Current.Activated -= h);
----------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------

// If the delegate is a sub-class of the EventHandler

var propChanged = Observable.FromEventPattern
<PropertyChangedEventHandler, PropertyChangedEventArgs>(
handler => handler.Invoke,
h => this.PropertyChanged += h,
h => this.PropertyChanged -= h);
----------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------
// If the delegate is a sub-class of the EventHandler<T> which was introduced in .NET 2.0

// FirstChanceException is EventHandler<FirstChanceExceptionEventArgs>

var firstChanceException = Observable.FromEventPattern<FirstChanceExceptionEventArgs>(
h => AppDomain.CurrentDomain.FirstChanceException += h,
h => AppDomain.CurrentDomain.FirstChanceException -= h);  
----------------------------------------------------------------------------------------------

-----------------------------------------------------------------------------------
-  IObservable from Task.
http://www.introtorx.com/content/v1.0.10621.0/04_CreatingObservableSequences.html
-----------------------------------------------------------------------------------

Task<T>.ToObservable
Task.Observable => overload for non generic Task to IObservable<Unit>.

var t = Task.Factory.StartNew(()=>"Test");
var source = t.ToObservable();
source.Subscribe(Console.WriteLine,() => Console.WriteLine("completed"));

if the task is already in a status of RanToCompletion then the value is added to the sequence and then the sequence completed
if the task is Cancelled then the sequence will error with a TaskCanceledException
if the task is Faulted then the sequence will error with the task's inner exception
if the task has not yet completed, then a continuation is added to the task to perform the above actions appropriately

There are two reasons to use the extension method:

-1 From Framework 4.5, almost all I/O-bound functions return Task<T>

-2 If Task<T> is a good fit, it's preferable to use it over IObservable<T>
   The reason bing that a function that returns a single value in the future should return a Task<T>, not an IObservable<T>.
   Then if you need to combine it with other observables, use ToObservable().

-----------------------------------------------------------------------------------
-  IObservable from IEnumerable<T>
http://www.introtorx.com/content/v1.0.10621.0/04_CreatingObservableSequences.html
-----------------------------------------------------------------------------------
IEnumerable<T>.ToObservable<T>

However, caution must be applied has often it does not make much sense to produce an IObservable<T> from an IEnumerable<T> and 
perhaps all that you rally want to do is to use some overload of Observable.Generate instead.


-----------------------------------------------------------------------------------
-  IObservable from APM [Asynchronous Programming Model Pattern]
http://www.introtorx.com/content/v1.0.10621.0/04_CreatingObservableSequences.html
-----------------------------------------------------------------------------------

APM is the old style of asynchronous programming that predates the introduction of the Task and Task<T> base models.
Sometimes old APIs still expose APM only and if you need to work in an architecture that rather use IObservable then you must have a
way to turn APM APIs into IObservable APIs.

-----------------------------------------------------------------------------------
Basic APM 

//Standard Begin signature
IAsyncResult BeginXXX(AsyncCallback callback, Object state);

//Standard Begin signature with data
IAsyncResult BeginYYY(string someParam1, AsyncCallback callback, object state);

//Standard EndXXX Signature with or withput data
TResult EndYYY(IAsyncResult asyncResult);

-----------------------------------------------------------------------------------

This is accomplished by any of the RX overloads of 

Observable.FromAsyncPattern

for example

Observable.FromAsyncPattern<TInput1,...,TInputN,TResult>
----------------------------------------------------------

The first part of the generic arguments TInput1,...,TInputN are he set of types for BeginRead except for the last two 
AsyncCallback callback, object state. TResult is the type returned by EndYYY.

// example of usage on a Stream
var read = Observable.FromAsyncPattern<byte[], int, int, int>(
stream.BeginRead, 
stream.EndRead);

=====================================================================================================================================


=====================================================================================================================================

RX - injecting side effects
http://www.introtorx.com/content/v1.0.10621.0/09_SideEffects.html

-------------------------------------------------------
-1 Loging with Observable.Do
   Wire tapping into an Observable 
   Appending operations to an Observable pipeline
-------------------------------------------------------

// get hold of an observable
var source = Observable
				.Interval(TimeSpan.FromSeconds(1))
				.Take(3);

// use any of the Observable.DO overloads to create another Observable
// that takes Actions which implements the desired side effets i.e.
// logging.
var result = source.Do(
					i => Log(i),
					ex => Log(ex),
					() => Log());

// subscribe to the observable returned by Observable.Do to get the 
// values from the stream as usual AND have the side effect actions
// executed.
result.Subscribe(
			Console.WriteLine,
			() => Console.WriteLine("completed"));


----------------------------------------------------------------------
-2 Encapsulating with AsObservable
   What is the purpose of AsObservable and when should it be used?
   https://social.msdn.microsoft.com/Forums/en-US/f8728ec3-ab0c-4d8e-849e-c419c87a42a7/what-is-the-purpose-of-asobservable-and-when-should-it-be-used?forum=rx
----------------------------------------------------------------------

----------------------------------------------------------------------
-3 Mutable elements cannot be protected   
----------------------------------------------------------------------

Consumers of IObservable must be sure that the data they get is the data that 
the source produced. While Observable.Do procides a mechanism to inject side
effects into an Observable care should be taken that in IObservable<T> the 
type T is immutable.

Consider the following example.

var source = new Subject<Account>();

//Evil code. It modifies the Account object.
source.Subscribe(account => account.Name = "Garbage");

//unassuming well behaved code
source.Subscribe(
		account=>Console.WriteLine("{0} {1}", account.Id, account.Name),
		()=>Console.WriteLine("completed"));

source.OnNext(new Account {Id = 1, Name = "Microsoft"});
source.OnNext(new Account {Id = 2, Name = "Google"});
source.OnNext(new Account {Id = 3, Name = "IBM"});

source.OnCompleted();

Output:

1 Garbage
2 Garbage
3 Garbage
completed

Clearly the first subscription to source runs first and modify the state of 
the instance of type Account before the second subscription to the source 
can pass the observed value to the observer. This happens because the type 
T in this example is Account and it is not immutable.

----------------------------------------------------------------------
Use Transformation of sequences
http://www.introtorx.com/content/v1.0.10621.0/08_Transformation.html
----------------------------------------------------------------------



=====================================================================================================================================

RX Scheduling and Threading
Observables and Theads
Observable.Context, Observable.SubscribeOn & Observable.ObserveOn
http://www.introtorx.com/content/v1.0.10621.0/15_SchedulingAndThreading.html
https://blogs.msdn.microsoft.com/rxteam/2009/11/20/observable-context-observable-subscribeon-and-observable-observeon/
https://stackoverflow.com/questions/44984730/rxandroid-whats-the-difference-between-subscribeon-and-observeon


1-Rx is a free-threaded model

  Being free-threaded means that you are not restricted to which thread you choose to do your work.
  You can choose to do your work such as invoking a subscription, observing or producing notifications, 
  on any thread you like.
  The alternative to a free-threaded model is a Single Threaded Apartment (STA) model where you must 
  interact with the system on a given thread.

  ----------------------------------------------------------------------------------------------------
  In RX f you do not introduce any scheduling, your callbacks will be invoked on the same thread that 
  the OnNext/OnError/OnCompleted methods are invoked from
  ----------------------------------------------------------------------------------------------------

2-Rx is a sinlge threaded model 

  The assumption that RX is multithreaded is a misconception
  Dispelling event myths - events a sinlge threaded and synchronous
  http://www.introtorx.com/content/v1.0.10621.0/19_DispellingMyths.html#DispellingEventMyths

  In the Rx world, there are generally two things you want to control the concurrency model for:

  -1 The invocation of the subscription =>  IObservable<T>.SubscribeOn
  -2 The observing of notifications		=>  IObservable<T>.ObserveOn

  Both methods have an overload that take either IScheduler or SynchronizationContext and return
  an IObservable

  ------------------------------------
  How to use IObservable.SubscribeOn
  https://blogs.msdn.microsoft.com/rxteam/2009/11/20/observable-context-observable-subscribeon-and-observable-observeon/
  ------------------------------------
  
  SubscribeOn controls the thread on which the actual call to subscribe happens. 
 
  For example if you have a stream of .NET events from the UI wrapped up in IObservable any call
  to IObservable.Subscribe would cause the delegates to process the On
  
=================================================================================================================================

Transformation of IObservable<T>

Refs
http://www.introtorx.com/content/v1.0.10621.0/08_Transformation.html

=================================================================================================================================