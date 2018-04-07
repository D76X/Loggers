#!/usr/bin/env python3
"""
    This modules defines some utilities functions. 

    Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps"); clear = lambda: os.system('cls'); import imp; import ntt_utils as nttutils; nttutils.test_module()

    # The last two commands are specific to this module.
    import ntt_utils as nttutils
    nttutils.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(nttutils)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps")
    os.getcwd()

"""   

from math import sqrt

def sign(x):
    """
    Test the sign of a numeric value.

    Args:
        x: a numeric value.
    
    Returns:
        +1 for positive values
        -1 for negative values
         0 for zero
    
    Notes: 
        This function is a clever one-liner that exploits that fact that in Python 
        the following identities hold. 
        True  - True  = 0
        True  - False = 1
        False - True  = -1
        False - False = 0  
    """
    return (x > 0) - (x < 0)

def isPrime(n):
    """
    Test whether the input is a prime number.
    (very crude implementation)
    """
    if n < 2:
        return False
    for i in range(2, int(sqrt(n)+1)):
        if n % i == 0:
            return False
    return True

def distinct(iterable):
    """
    Eliminates duplicates that may be present in 'iterable'.

    Args:
        iterable: the source series.
    
    Yields:
        an iterable with unique elements from 'iterable'.
    """
    seen = set()
    for item in iterable:
        if item in seen:
            continue
        yield item
        seen.add(item)

def test_module(): 
    """Module-level tests."""
    print()    
    print("ntt_utils tests...")
    print("sign(1)={}".format(sign(1)))
    print("sign(1.6)={}".format(sign(1.06)))
    print("sign(-1)={}".format(sign(-1)))
    print("sign(-1.01)={}".format(sign(-1.01)))
    print("sign(0)={}".format(sign(0)))    

# ##########################################################################################

# 1-Run the file in Python as a script.

# if __name__ = '__main__' the files is executed as a script 
# to execute the file as a scrip in the cmd use : python filename.py

# 2-Import the module into the Python REPL.

# when  filename.py is imported into the Python REPL as in the following
# python
# import filename
# the __name__ variable is set to the name of the module that by default is filename

print("__name__ = {}".format(__name__))
if __name__ == '__main__':
    test_module()

# ##########################################################################################
