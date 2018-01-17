"""
This module shows how to test wether an object implements __call__ in Python.
Tn Python this is done via the standard function callable()

Usage:
    In the REPL import the module and run the module level test function. 
    
    import test016_extended_formal_args as t
    t.test_module()
    the print function accepts any number of args!
    the string.format is another example...
    London<===>Berlin
    the length of a line is 1
    the area of a rectangle is 6
    the volume of a cuboid is 120
    the volume of a hyper cuboid with sides of length 4, 5, 6, 7 is 840

    24
    6
    2
    1
    Traceback (most recent call last):
      File "<stdin>", line 1, in <module>
      File ".\test016_extended_formal_args.py", line 61, in test_module
        print(hypervolume_improved())
    TypeError: hypervolume_improved() missing 1 required positional argument: 'length'
    >>> 

"""

def is_even(x):
    "Tests whether x is even."
    return x % 2 == 0

class MyClass:
    """This is a callable class as all classes but not its instances."""

    def anInstanceMethod(self):
        """A class method."""
        print("I am an instance method.")
        return 0

class MyOtherClass:
    """This class is callable but also its instances are callables."""

    def __call__(self):
        """By implementing __call__ instances of the class are callables."""
        print("I have been called.")

    def anotherInstanceMethod(self):
        """A class method."""
        print("I am another instance method.")
        return 0


def test_module():
    """Module-level tests."""
    
    # the standard function callable() tests whether a name
    # implements __call__

    # functions ar callables
    if (callable(is_even)):
        print("is_even is callable")

    # lambdas are callable
    is_odd = lambda x: x % 2 == 1 
    if(callable(is_odd)):
        print("the is_odd lambda and all lambdas are callables")

    # class objets are callables becase calling a class such as
    # MyClass() invokes its constructor
    if(callable(list)):
        print("the class list and all classes are callables")
    
    # methods are callable
    if(callable(list.append)):
        print("the method list.class and all class methods are callables")

    # classes that implement __call__ produce callable instances.
    callableInstance = MyOtherClass()
    if(callable(callableInstance)):
        callableInstance()
        print("a callable instance has been called")

    # classes that do not implement __call__ do not produce callable instances.
    nonCallableInstance = MyClass()
    if not (callable(nonCallableInstance)):
        print("a non callable instance does not implement __call__")

    # not all Python objects are callable
    # strings are not callable
    astring = "astring"
    if not (callable(astring)):
        print("strings and other standard Pyhton objects are not callable istances")
    

   
