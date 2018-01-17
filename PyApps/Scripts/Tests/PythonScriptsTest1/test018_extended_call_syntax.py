"""
This module illustrates the extended call syntax in Python.

Usage:
    In the REPL import the module and run the module level test function.

    import test018_extended_call_syntax as t
    t.test_module()
    1
    2
    (3, 4)

    r= 21
    g= 68
    b= 120
    {'alpha': 52}

    {'age': 25, 'weight': 70, 'height': 1.75, 'name': 'Luke'}
    >>> 
"""

def print_args(arg1, arg2, *args):
    """
    Illustrates call syntax.
    
    Args:
        arg1, arg2: required positional arguments.
        args: any additional optional positional arguments. 
    """
    print(arg1)
    print(arg2)
    print(args)

def print_color(red, green, blue, **kargs):
    """
    Illustrates call syntax.
    
    Args:
        red, green, blue: RGB code via positional required arguments.
        kargs: any additional optional keyed arguments. 
    """
    print("r=", red)
    print("g=", green)
    print("b=", blue)
    print(kargs)


def test_module():
    """Module-level tests."""
    
    # the extended call syntax allows to pass arguments to function
    # as tuples or other suitable data structures.
    some_args_tuple = (1,2,3,4)
    print_args(*some_args_tuple)
    
    print()
    
    # this is one way to build a dictinary - literal.
    a_dictionary = {"red":21, "green":68, "blue":120, "alpha": 52}
    print_color(**a_dictionary)
    
    print()
    
    # a dictionary uses **kargs technique to allow the creation of a dict
    # from keyworded arguments using the callable constructor of its class
    # dict
    another_dictionary = dict(age=25, weight=70, height=1.75, name="Luke")
    print(another_dictionary)   
