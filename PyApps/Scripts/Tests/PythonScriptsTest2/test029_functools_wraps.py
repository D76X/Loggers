"""
This module illustrates function decorators in Python.
It also shows that decorators hide the attributes of the decorated callable
object. However, functools.wraps package provides a tool to override the
attributes of the decorator with those of the decorated function which is 
often the desired behavior i.e. in order for Python tools to provide docs 
or syntax for the decorated functions.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test029_functools_wraps as t
    t.test_module()  
    
    hello.__docs__ = A simple hello function
    hello.__name__ = hello
    
    Or in the REPL you can test the help() function that just prints the 
    __doc__ attribute of its argument as below
    
    help(t.hello)
    Help on function hello in module test029_functools_wraps:

    hello(name)
        A simple hello function
    
"""

import functools

# the @functools.wraps(f) replaces the attrinutes of the decorator
# with those of the decorated function.
def noop(f):
    """A simple no-op decorator function"""
    @functools.wraps(f)
    def noop_wrapper(*args, **kwargs):
        # keep f in a closure
        # *args, **kwargs allow any args to be passed
        return f(*args, **kwargs)

    return noop_wrapper

# a decorated function
@noop
def hello(name):
    """A simple hello function"""
    print("hello {}!".format(name))

def test_module():
    """Module-level tests."""
    # some of the attributes on a function...
    print("hello.__docs__ = {docs}".format(docs=hello.__doc__))
    print("hello.__name__ = {n}".format(n=hello.__name__))
