# XAML

***

## Problem broken XAML designer

I have come accorss a problemwin which I had namespaces defined as below for a Window element.

- xmlns:infdecorators="clr-namespace:LogXtreme.WinDsk.Infrastructure.Decorators;assembly=LogXtreme.WinDsk.Infrastructure"

- xmlns:infbehaviours="clr-namespace:LogXtreme.WinDsk.Infrastructure.Behaviors;assembly=LogXtreme.WinDsk.Infrastructure"

- xmlns:inftriggeracts="clr-namespace:LogXtreme.WinDsk.Infrastructure.TriggerActions;assembly=LogXtreme.WinDsk.Infrastructure"

They all point to the same assembly **LogXtreme.WinDsk.Infrastructure** but while __infbehaviours__ and __inftriggeracts__ 
prefixes could actually find all the compiled classes in the respective namespaces and did not break the XAML designer
the __infdecorators__ would cause the XAML designer to break with a message such as __The name does not exist in the namespace error in XAML.__

### What was done to try to fix the problem

The error was not caused by the code. I tried to follow the advice given in post such 

(https://stackoverflow.com/questions/14665713/the-name-does-not-exist-in-the-namespace-error-in-xaml)  

However in my case it did not help.

### What worked - The Designer cach in Visual Studio

I succeded in fixing the propblem by deleting the folder below.

C:\Users\davidespano\AppData\Local\Microsoft\VisualStudio  

Probably it would have also worked by simply deleting just the folder for the XAM designer cache.

C:\Users\davidespano\AppData\Local\Microsoft\VisualStudio\...\Designer

***