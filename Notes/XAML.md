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

Probably it would have also worked by simply deleting just the folder for the XAML designer cache.

C:\Users\davidespano\AppData\Local\Microsoft\VisualStudio\...\Designer

***

## Readonly Attached Properties - a catch!

When a readonly attached property or dependency property is created it is important to get the 
order of the key and the property right as explained in the examples below.  

### This code breaks the XAML designer 

The following code breaks the XAML designer because the MousePositionPropertyKey is not yet
declared when MousePositionProperty uses it.

```cs
public static readonly DependencyProperty MousePositionProperty =
    MousePositionPropertyKey.DependencyProperty;

private static readonly DependencyPropertyKey MousePositionPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                @"MousePosition",
                typeof(Point),
                typeof(MouseTrackerDecorator),
                new FrameworkPropertyMetadata(new Point(0, 0)));
```

### This code works

The dependency property MousePositionProperty is declared __following__ the declaration of the
corresponding MousePositionPropertyKey which avoids the problem in the XAML designer.

```cs
private static readonly DependencyPropertyKey MousePositionPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                @"MousePosition",
                typeof(Point),
                typeof(MouseTrackerDecorator),
                new FrameworkPropertyMetadata(new Point(0, 0)));

public static readonly DependencyProperty MousePositionProperty =
    MousePositionPropertyKey.DependencyProperty;
```

***