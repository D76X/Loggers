"""
This module shows that in Python a Class is a callable object.

Usage:
    In the REPL import the module and run the module level test function. 
    import test012_classes_are_callables
    test012_classes_are_callables.test_module()
    ('T', 'i', 'm', 'b', 'u', 'k', 't', 'u')
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
    if is_immutable:
        cls = tuple
    else: 
        cls = list
    return cls

def test_module():
    """Module-level test function"""
    seq = sequence_class(is_immutable=True)
    # seq is a callable because is a object of type class
    t = seq("Conditional expressions")
    print(t)
    print(type(t))
