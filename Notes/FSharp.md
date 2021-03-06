
F# shortcuts

Execute in interactive			[Alt+Enter]


===================================================================================

F# Community Projects
http://fsharp.org/community/projects/

===================================================================================

SET UP UNIT TESTTING IN F#

SET UP UNIT TESTING WITH xUnit 
https://mallibone.com/post/f-unit-testing-with-xunit
IN PARTICULAR YOU MUST INSTALL THE TEST RUNNER TO SEE THE TESTS!
PM> Install-Package xunit.runner.visualstudio -Version 2.0.1

This installs the runner at solution level, then it needs to be installed by project. 

GIVE UP MS TEST
https://stackoverflow.com/questions/5103964/using-mstest-with-f

===================================================================================

TO GET UNQUOTE TO WORK ON TOP OF xUnit
https://stackoverflow.com/questions/34802706/error-running-xunit-test-with-unquote-library-and-visual-studio-2015-method-no
http://php.wekeepcoding.com/article/13289568/Error+running+xunit+test+with+unquote+library+and+Visual+Studio+2015+-+Method+not+found

Add App.config to the F# project with the assembly redirect.

This will prevent the System.MissingMethodException : Method not found: 
'Swensen.Unquote.UnquotedExpression ...

<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
        <assemblyIdentity name="FSharp.Core"
                          publicKeyToken="b03f5f7f11d50a3a"
                          culture="neutral"/>
        <bindingRedirect oldVersion="4.3.1.0"
                         newVersion="4.4.0.0"/>
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration>

===================================================================================

BUT THIS ALONE IS NOT ENOUGH IF YOU WANT Unquote TO WORK NICELY IN DEBUGGER AND 
IN THE OUTPUT WINDOW YOU MUST MATCH THE BINDING REDIRECT TO THE VERSION OF THE 
FSharp.Core.dll

As suggested at the post below
https://stackoverflow.com/questions/7828852/unquote-what-am-i-missing

In the binding redirect above of which a significant fragment is repeated below 
for clarity

<bindingRedirect oldVersion="4.3.1.0"
                 newVersion="4.4.0.0"/>


must be so that newVersion="4.4.0.0" matches the exact version of 

C:\Program Files (x86)\Reference Assemblies\Microsoft\FSharp\.NETFramework\v4.0\
4.4.1.0\FSharp.Core.dll

Hence, in order for Unquote to work with FSharp.Core.dll 4.4.1.0 the binding redirect
should be 

<bindingRedirect oldVersion="4.3.1.0"
                 newVersion="4.4.1.0"/>

===================================================================================

IT IS ALSO NECESSARY TO USE THE RIGHT VERSION OF THE .NET Framework IN CONJUNCTION 
WITH THE INSTALLED PACKAGES.

For example this is how a package.config should look like 

<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="System.ValueTuple" version="4.3.0" targetFramework="net452" />
  <package id="Unquote" version="3.2.0" targetFramework="net452" />
  <package id="xunit" version="2.2.0" targetFramework="net452" />
  <package id="xunit.abstractions" version="2.0.1" targetFramework="net452" />
  <package id="xunit.assert" version="2.2.0" targetFramework="net452" />
  <package id="xunit.core" version="2.2.0" targetFramework="net452" />
  <package id="xunit.extensibility.core" version="2.2.0" targetFramework="net452" />
  <package id="xunit.extensibility.execution" version="2.2.0" targetFramework="net452" />
  <package id="xunit.runner.visualstudio" version="2.2.0" targetFramework="net452" developmentDependency="true" />
</packages>


===================================================================================

EXTENSION METHODS IN F#

VISIBILITY OF EXTENSION METHODS DEFINED IN F# TO C# REQUIRE ATTRIBUTES
http://theburningmonk.com/2012/09/f-make-extension-methods-visible-to-c/
https://stackoverflow.com/questions/1523500/how-do-i-create-an-extension-method-f

VISIBILITY OF EXTENSION METHODS DEFINED IN C# TO F#
https://stackoverflow.com/questions/777247/using-extension-methods-defined-in-c-sharp-from-f-code

===================================================================================

LAMBDAS AND FUNCTIONS IN F# AND C#

(the question you asked on SO - pass expressions and lambdas parameter from F# code to C# code)
https://stackoverflow.com/questions/45896796/pass-expressions-and-lambdas-parameter-to-c-sharp-extension-method-in-f1
All other related posts
https://stackoverflow.com/questions/3392000/interop-between-f-and-c-sharp-lambdas
https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/functions/lambda-expressions-the-fun-keyword

PowerPack
https://stackoverflow.com/questions/13874032/how-to-pass-linq-expressions-from-f-to-c-sharp-code
https://stackoverflow.com/questions/15322888/what-is-the-most-up-to-date-way-to-compile-code-quotations-in-f
https://stackoverflow.com/questions/3238406/in-f-how-can-i-produce-an-expression-with-a-type-of-funcobj
https://stackoverflow.com/questions/6524242/calling-c-sharp-functions-from-f 
https://stackoverflow.com/questions/9914527/call-f-function-that-has-a-function-parameter-from-c-sharp

===================================================================================

1- HOW TO STRUCTURE AND ORGANISE F# CODE
https://fsharpforfunandprofit.com/posts/recipe-part3/

2- THE PROBLEM OF DECLARATION OF F# TYPES WITH MUTUAL DEPENDENCIES
https://fsharpforfunandprofit.com/posts/cyclic-dependencies/

3-HOW TO ORGANISE F# FUNCTIONS - MODULES (top level & nested) & NAMEPSACES
REMEMBER: If you realize that modules are just static classes, and that functions are static methods
REMEMBER: Top level modules are slightly different from any other modules
https://fsharpforfunandprofit.com/posts/organizing-functions/

4-SPECIAL ATTRIBUTES USED 

[<AutoOpen>]
Used to declare global types and functions
https://fsharpforfunandprofit.com/posts/recipe-part3/

[<RequireQualifiedAccess>]
Used to get around the problem of modules shadowing
https://fsharpforfunandprofit.com/posts/organizing-functions/

5-ACCESS CONTROL FOR MODULES, TYPES AND FUNCTIONS
https://fsharpforfunandprofit.com/posts/organizing-functions/

6-NAMEPSACES
You can also declare a namespace implicitly by adding dots to the module name.
Example : module Utilities.MathStuff  
Namespaces can directly contain type declarations, but not function declarations. 
https://fsharpforfunandprofit.com/posts/organizing-functions/

7-FROM OBECT ORIENTED CODE TO FUNCTIONAL ORIENTED CODE 
https://fsharpforfunandprofit.com/posts/organizing-functions/
Mixing types and functions in modules
REMEMBER
In an object oriented program, the data structure and the functions that act on it 
would be combined in a class. However in functional-style F#, a data structure and 
the functions that act on it are combined in a module instead.

There are two common patterns for mixing types and functions together:
1-having the type declared separately from the functions
2-having the type declared in the same module as the functions

==============================================================================================

Conversions and Casting in F#
https://stackoverflow.com/questions/31616761/f-casting-operators
https://msdn.microsoft.com/en-us/visualfsharpdocs/conceptual/casting-and-conversions-%5Bfsharp%5D

Conversions are possible between primitive types

(int) "4" // the number 4 - this is a conversion, not a cast
int "4"   // same thing, but idiomatic
int "NaN" // compiles but throws an exception at runtime
(int) (box 4) // doesn't compile because int doesn't do downcasts, just known conversions

Some conversions are not possible

(bigint) 1 // no such conversion function, so this is a compile-time error

Upcasting 

The :> operator performs a static cast which means that the success of the cast is determined 
at compile time i.e. anything can be upcast to obj - it is a valid cast and has no chance of 
failure at run time. You can also use the upcast operator to perform such a conversion. 

Downcasting

The :?> operator performs a dynamic cast, which means that the success of the cast is determined 
at run time. A cast that uses the :?> operator is not checked at compile time. At run time, an 
attempt is made to cast to the specified type. If the object is compatible with the target type, 
the cast succeeds. If the object is not compatible with the target type, the runtime raises an 
InvalidCastException. You can also use the downcast operator to perform a dynamic type conversion. 

upcast and downcast operators

5 :> obj                 // upcast int to obj - upcast to object is always possible
(upcast 5 : obj)         // same
(box 5) :?> int          // downcast an obj to int (successfully) - you know it will succeed
(downcast (box 5) : int) // same
(box "5") :?> int        // downcast an obj to int (unsuccessfully) - cannot cast int to str

==============================================================================================

Printing strings in F#

Formatted text using printf
https://fsharpforfunandprofit.com/posts/printf/

-----------------------------------------------------------------------------------------------
This is the C-style format string:
printf preferred and considered idiomatic for F#

-1 It is statically type checked - both for the types of the parameters and number.
-2 It is a well-behaved F# function and so supports partial application, etc.
-3 It supports native F# types.

// Typical usage
printfn "A string: %s. An int: %i. A float: %f. A bool: %b" "hello" 42 3.14 true

// wrong parameter type
printfn "A string: %s" 42 

// wrong number of parameters
printfn "A string: %s" "Hello" 42
-----------------------------------------------------------------------------------------------

-----------------------------------------------------------------------------------------------
This is the typical .NET style

// Typical usage
Console.WriteLine("A string: {0}. An int: {1}. A float: {2}. A bool: {3}","hello",42,3.14,true)

// wrong parameter type
Console.WriteLine("A string: {0}", 42)   //works!

// wrong number of parameters
Console.WriteLine("A string: {0}","Hello",42)  //works!
Console.WriteLine("A string: {0}. An int: {1}","Hello") //FormatException
-----------------------------------------------------------------------------------------------

-------------------------------------------
Other advantages of the C-style printf(n)
-------------------------------------------

-1 printf supports partial application


-----------------------------------------------------------------------------------------------

------------------------------------------------------------
F# printf string and TextWriterFormat<`T> 
https://stackoverflow.com/questions/9440204/f-printf-string
------------------------------------------------------------

Examples

// does not work 
// printfn expects a TextWriterFormat<`T> not a string
// and cannot convert a string to TextWriterFormat<`T>
let test = "aString"
let callMe = printfn test

// it works
// ("%s" test) is of type TextWriterFormat<`T>
printfn "%s" test

==============================================================================================