"""
This module illustrates forwarding arguments in Python.

Usage:
    In the REPL import the module and run the module level test function.

    import test019_forwarding_arguments as t
    t.test_module()
    >>> import test019_forwarding_arguments as t
    t.test_module()
    args= ('ff',)
    kargs= {'base': 16}
    result=255
    int("ff", base=16)=255
     
"""

def trace(f, *args, **kargs):
    """
    This function forwards all the arguments *args and **kargs
    to the function f. Notice that in order for trace to work 
    with any function f it must be able to take any arbitrary 
    arguments. This is where *args and **kargs are helful.
    
    Args:
        f: the traced function.
        *args, **kargs: the arguments to forward to f.

    Returns: the result of f given *args, **kargs as its arguments. 
    """
    print("args=", args)
    print("kargs=", kargs)   
    # notice that we pass the arguments to f by prefixing them with 
    # * and ** repectively. This is Python is known as EXTENDED CALL 
    # SYNTAX where * or ** are applied to any iterable. 
    
    # The * used is the DEFINITION of a function will cause an arbitrary
    # number of argumnets passed as an arguments to a function to be 
    # packed into a tuple then is finally passed as argument to the 
    # fucntion at the calling site. The ** allows any number of keywored 
    # arguments to be packed into a dictionary than is then passed as 
    # argument to a function at the call site.
    
    # Coversely, the * at the call site of a function unpacks an iterable
    # series into a tuple that is then passed to the called function as 
    # its args.

    result = f(*args, **kargs)
    print ("result={}".format(result))
    return result
    

def test_module():
    """Module-level tests."""
    # trace the callable int used to convert the hex "ff" into int.
    sixteen = trace(int, "ff", base=16)
    print('int("ff", base=16)={}'.format(sixteen))
