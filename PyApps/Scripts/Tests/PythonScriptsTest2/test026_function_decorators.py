"""
This module illustrates function decorators in Python.
It also shows various ways in which decorators can be used beside the simple
case of a decorator applied to a function in order to augment it or modify 
it.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test026_function_decorators as t
    t.test_module()

    >>> import test026_function_decorators as t
    t.test_module()
    invokation 0 of hello(one)=hello one
    invokation 1 of hello(two)=hello two
    invokation 2 of hello(three)=hello three

    Is trace enabled = True
    rotation 0 => [1, 2, 3]
    calling <function test_module.<locals>.rotate_list at 0x000001DC5687B378>
    rotation 1 => [2, 3, 1]
    calling <function test_module.<locals>.rotate_list at 0x000001DC5687B378>
    rotation 2 => [3, 1, 2]
    calling <function test_module.<locals>.rotate_list at 0x000001DC5687B378>
    rotation 3 => [1, 2, 3]

    Is trace enabled = False
    rotation 3 => [1, 2, 3]
    rotation 3 => [2, 3, 1]
    rotation 3 => [3, 1, 2]
    rotation 3 => [1, 2, 3]
    >>> 
    
""" 

# Case 1 - class object as a decorator.

# Functions are callable objects on which decorators can be applied.
# However, beside functions also class objects are callables in that
# their constructor i.e. instance = MyClass() or with custructor args
# instance = MyClass("1") etc. A class object can be used as a decorator
# on a callable object usually a function. The function that is decorated
# with a class is passed to the __init__(self, f) of the class. 
# There is only a requirement to satisfy that is the class that is used
# as a decorator must be a callable that is it must implement __call__
# because it is required that any decorators takes a callable and return
# a callable. It is worth recalling that a class object as MyClass is 
# by definition callable in Python. However, this is not enough for the
# instances of such class to be callable objects, for the instances of 
# MyClass to be callable objects MyClass must implement __call__(self)
# and this is also required in order to use @MyClass as a decorator. 

class CallCount:
    """
    This class is intended to be used as a decorator on any function.
    It augments the function with a counter of the number of invokations.
    """
    def __init__(self, f):
        self.f = f
        self.count = 0

    def __call__(self, *args, **kwargs):
        self.count += 1
        return self.f(*args, **kwargs)

@CallCount
def hello(name):
    return "hello {}".format(name)

# Case 2 : class instance as a decorator.

# In this case the instance method __call__(self, f) takes f as the 
# decorated function and it returns another function (a callable)
# thus any instance of the Trace class can be used as a decorator.
# This is useful for example whne we wish to instrument functions.
# __call__ must return a callable, tipically a function.

class Trace:
    """
    A class that can be used as a decorator to trace the calls to 
    the decorated funtion.
    """
    def __init__(self):
        self.enabled = True
        self.count = 0

    def __call__(self, f):
        def wrap(*args, **kwargs):
            if self.enabled:
                    print("calling {}".format(f))
                    self.count += 1
            return f(*args, **kwargs)
        return wrap

def test_module():
    """Module-level tests."""
    # Tests of case 1 - class object used as a decorator
    print("invokation {i} of hello({n})={r}".format(i=hello.count, n="one", r=hello("one")))
    print("invokation {i} of hello({n})={r}".format(i=hello.count, n="two", r=hello("two")))
    print("invokation {i} of hello({n})={r}".format(i=hello.count, n="three", r=hello("three")))
    
    print()
    
    # trace is enabled in its __init__
    trace1 = Trace()
    
    @trace1
    def rotate_list(l):
        """Rotates a list"""
        return l[1:] + [l[0]]

    print("Is trace enabled = {}".format(trace1.enabled))
    l = [1,2,3]
    print("rotation {} => {}".format(trace1.count,l))
    l = rotate_list(l)
    print("rotation {} => {}".format(trace1.count,l))
    l = rotate_list(l)
    print("rotation {} => {}".format(trace1.count,l))
    l = rotate_list(l)
    print("rotation {} => {}".format(trace1.count,l))

    trace1.enabled = False
    print()
    print("Is trace enabled = {}".format(trace1.enabled))
    print("rotation {} => {}".format(trace1.count,l))
    l = rotate_list(l)
    print("rotation {} => {}".format(trace1.count,l))
    l = rotate_list(l)
    print("rotation {} => {}".format(trace1.count,l))
    l = rotate_list(l)
    print("rotation {} => {}".format(trace1.count,l))
