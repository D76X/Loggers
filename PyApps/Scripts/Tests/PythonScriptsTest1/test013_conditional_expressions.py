"""
This module shows how to use conditional expressions in Python.

Usage:
    In the REPL import the module and run the module level test function.    
    import test012_classes_are_callables
    test012_classes_are_callables.test_module()
    ('C', 'o', 'n', 'd', 'i', 't', 'i', 'o', 'n', 'a', 'l', ' ', 'e', 'x', 'p', 'r', 'e', 's', 's', 'i', 'o', 'n', 's')
    <class 'tuple'>
"""

def sequence_class(is_immutable):
    """
    Illustrates a function that returns an instance of a class type.
    Args:
        is_immutable: is True/defined by the caller to return the class tuple.
                      is  False or not defined to return the class list.  

    Returns: either tuple (immutable) or list (mutable).
    """
    # this can be translated into a conditional expression

    #if is_immutable:
    #    cls = tuple
    #else: 
    #    cls = list
    #return cls

    # result true_value if condition else false_value
    return tuple if is_immutable else list 

def test_module():
    """Module-level test function"""
    seq = sequence_class(is_immutable=True)
    # seq is a callable because is a object of type class
    t = seq("Timbuktu")
    print(t)
    print(type(t))
