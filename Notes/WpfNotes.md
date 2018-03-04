# WPF Resources

http://www.wpf-tutorial.com/  
http://drwpf.com/blog/  
https://wpftutorial.net/Home.html

***

## WPF Canvas

[How to: Use the Attached Properties of Canvas to Position Child Elements](https://docs.microsoft.com/en-us/dotnet/framework/wpf/controls/how-to-use-the-attached-properties-of-canvas-to-position-child-elements)

***

## WPF Decorators

A Decorator class is responsible for wrapping a UI element to support additional behavior.

Decorator provides a base class for elements that apply effects onto or around a single child element,
for example the Border class derives from decorator. Decorator inherits from FrameworkElement, 
and implements a Child property (of type UIElement), as well as implements the IAddChild interface. 
Decorator is the most primitive element that can contain another element.

When you subclass a Decorator, you can expose some useful DependencyProperties to customize it.
For example, the Border class exposes properties like BorderBrush, BorderThickness, and CornerRadius 
that all affect how the border is drawn around its child content.

[WPF - how do I bind a control's position to the current mouse position?](https://stackoverflow.com/questions/6714663/wpf-how-do-i-bind-a-controls-position-to-the-current-mouse-position)  
[Is WPF Decorator class useful?](https://stackoverflow.com/questions/2529719/is-wpf-decorator-class-useful)  
[How to write Decorator XAML custom control for Windows Runtime?](https://stackoverflow.com/questions/27020326/how-to-write-decorator-xaml-custom-control-for-windows-runtime)  

***

## WPF Adorner

Adorners are rendered in an AdornerLayer, which is a rendering surface that is always on top of the 
adorned element or a collection of adorned elements.  Rendering of an adorner is independent from 
rendering of the UIElement that the adorner is bound to. An adorner is typically positioned relative 
to the element to which it is bound, using the standard 2-D coordinate origin located at the upper-left 
of the adorned element.

While a Decorator is responsible for drawing decoration around the outside of a piece of child content, 
the Adorner class allows you to overlay visuals on top of existing visual elements. An easy way to think 
of adorners is that they are secondary interactive visuals that provide additional means to interact with 
the primary visual i.e. widgets such as rezising or rotation handles. Those are a secondary visual that 
sit on top of the elements that they are adorning and provide additional functionality and interaction.

Common applications for adorners include:

- Adding functional handles to a UIElement that enable a user to manipulate the element in some way 
(resize, rotate, reposition, etc.).

- Provide visual feedback to indicate various states, or in response to various events.

- Overlay visual decorations on a UIElement.

- Visually mask or override part or all of a UIElement.

## AdornerDecorator

Adorner classes work in conjunction with the AdornerDecorator, which is an invisible surface on which the 
adorners rest. To be part of the visual tree, adorners have to have a container. The AdornerDecorator acts 
as this container.

https://docs.microsoft.com/en-us/dotnet/framework/wpf/controls/adorners-overview

***

## AdornerDecorator + TabControl + Validation Errors

https://stackoverflow.com/questions/1369643/wpf-error-styles-only-being-rendered-properly-on-visible-tab-of-a-tab-control

***

## Capturing mouse coordinates

[Binding Mouse Position in MVVM - is it possible?](https://social.msdn.microsoft.com/Forums/vstudio/en-US/b59975f9-3039-42af-b6b6-9c6d17079d24/binding-mouse-position-in-mvvm-is-it-possible?forum=wpf)  
[Adding mouse location as parameter to command wpf mvvm](https://stackoverflow.com/questions/26957187/adding-mouse-location-as-parameter-to-command-wpf-mvvm)  
[WPF - how do I bind a control's position to the current mouse position?](https://stackoverflow.com/questions/6714663/wpf-how-do-i-bind-a-controls-position-to-the-current-mouse-position)  
[Mouse Position with respect to image in WPF using MVVM](https://stackoverflow.com/questions/34984093/mouse-position-with-respect-to-image-in-wpf-using-mvvm)

***
## Design Time Data

https://www.codeproject.com/Tips/879109/Using-Design-time-Databinding-While-Developing-a-W
https://stackoverflow.com/questions/2716757/how-to-set-properties-of-a-ddesigninstance-in-xaml
https://msdn.microsoft.com/en-us/library/ee823176.aspx
https://joshsmithonwpf.wordpress.com/2010/04/04/design-time-data-is-still-data/
http://blog.qmatteoq.com/the-mvvm-pattern-design-time-data/

### d:DesignInstance
https://stackoverflow.com/questions/35695456/what-is-a-designinstance-in-xaml
https://stackoverflow.com/questions/2716757/how-to-set-properties-of-a-ddesigninstance-in-xaml

### d:DesignData
https://msdn.microsoft.com/en-us/library/ee823176.aspx

### Blendability with Prims
https://www.codeproject.com/Articles/1059235/Blendability-Adding-design-time-support-for-region

***

## Markup Extensions
https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/markup-extensions-and-wpf-xaml

### Create Custom Markup Extensions
http://10rem.net/blog/2011/03/09/creating-a-custom-markup-extension-in-wpf-and-soon-silverlight

***

## Nested and composite view models
https://stackoverflow.com/questions/4366728/mvvm-and-nested-view-models  

***

## Control the size of an observable cllection

https://stackoverflow.com/questions/4305623/how-to-resize-observablecollection

***

## Validation in WPF 

https://blog.magnusmontin.net/2013/08/26/data-validation-in-wpf/  
https://docs.microsoft.com/en-us/dotnet/framework/wpf/data/how-to-implement-binding-validation

### Data Annotations
https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations(v=vs.110).aspx

### INotifyDataErrorInfo and async (server-side) validation
https://social.technet.microsoft.com/wiki/contents/articles/19490.wpf-4-5-validating-data-in-using-the-inotifydataerrorinfo-interface.aspx  
https://www.codeproject.com/Tips/876349/WPF-Validation-using-INotifyDataErrorInfo

### The Validation Rule Class
https://msdn.microsoft.com/en-us/library/system.windows.controls.validationrule.aspx

### Binding Groups for the Transactional Validation of multiple bound properties
https://blogs.msdn.microsoft.com/vinsibal/2008/08/12/wpf-3-5-sp1-feature-bindinggroups-with-item-level-validation/  
http://blog.scottlogic.com/2008/11/28/using-bindinggroups-for-greater-control-over-input-validation.html  

### Exceptin Validation Rules
https://msdn.microsoft.com/en-us/library/system.windows.controls.exceptionvalidationrule(v=vs.110).aspx  
https://stackoverflow.com/questions/2747924/exceptionvalidationrule-doesnt-react-to-exceptions  

***

## AdornerDecorator + TabControl + Validation Errors

https://stackoverflow.com/questions/1369643/wpf-error-styles-only-being-rendered-properly-on-visible-tab-of-a-tab-control

***

## Multibinding and Multivalue Converters

https://www.codeproject.com/articles/328978/introduction-to-multi-binding-and-multi-value-conv
http://www.codearsenal.net/2013/12/wpf-multibinding-example.html#.WpfzFujFKHs

***

## Hit Testing

https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/hit-testing-in-the-visual-layer

***

## Property Triggers + Data Triggers + Event Triggers


### Multitriggers
http://www.wpf-tutorial.com/styles/multi-triggers-multitrigger-multidatatrigger/

***

## Behaviors

[Add Microsoft.SDK.Expression.Blend to project](https://stackoverflow.com/questions/46469674/cant-find-the-gotostateaction-behaviour-in-blend-2017)  

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
