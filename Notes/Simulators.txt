
=======================================================================
Random Generator - .NET Random

https://msdn.microsoft.com/en-us/library/system.random(v=vs.110).aspx

-------------------------------
Seeeding the Random generator
-------------------------------

If the same seed is used for separate Random objects, they will generate 
the same series of random numbers. This can be useful for creating a test 
suite that processes random values, or for replaying games that derive 
their data from random numbers. However, Random objects in processes 
running under different versions of the .NET Framework may return different 
series of random numbers even if they're instantiated with identical seed 
values.

To produce different sequences of random numbers, you can make the seed 
value time-dependent, thereby producing a different series with each new 
instance of Random.

--------------------------
Random(Int32) constructor 
--------------------------

Can take an Int32 value based on the number of ticks in the current time

------------------------
Random() constructor 
------------------------

Uses the system clock to generate its seed value. However, because the 
clock has finite resolution, using the parameterless constructor to create 
different Random objects in close succession creates random number 
generators that produce identical sequences of random numbers.

-----------------------------------------------------------------------
Random objects created within 15 milliseconds of one another are likely 
to have identical seed values and therefore generate the same sequence 
of random number!

This is caused by the finite resulution of the system clock.
-----------------------------------------------------------------------

----------------------------------------
System.Random class is not thread safe!
----------------------------------------

----------------------------------------------------------
-Better to use a single Random instance for the whole app
----------------------------------------------------------

Instead of instantiating individual Random objects, we recommend that 
you create a single Random instance to generate all the random numbers 
needed by your app.

However, Random objects are not thread safe. If your app calls Random 
methods from multiple threads, you must use a synchronization object 
to ensure that only one thread can access the random number generator 
at a time.

-----------------------------------------------------------------------
If you don't ensure that the Random object is accessed in a thread-safe 
way, calls to methods that return random numbers return 0. 
-----------------------------------------------------------------------

=======================================================================