"""
This module illustrates of the int and float built-in types in Python.

Usage:

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()
    
    # clear th REPL
    clear = lambda: os.system('cls')  
    clear()   
    
    # load the module and run the test function
    import test045_int_float as t
    t.test_module()

    # if you make changes reload the module and re-run the test function
    import imp
    imp.reload(t)  

    93326215443944152681699238856266700490715968264381621468592963895217599993229915608941463976156518286253697920827223758251185210916864000000000000000000000000
    sys.float_info(max=1.7976931348623157e+308, max_exp=1024, max_10_exp=308, min=2.2250738585072014e-308, min_exp=-1021, min_10_exp=-307, dig=15, mant_dig=53, epsilon=2.220446049250313e-16, radix=2, rounds=1)
    min_float=2.2250738585072014e-308
    max_float=1.7976931348623157e+308
    most_negative_float=-1.7976931348623157e+308

    int => float
    2^53=9007199254740992 => float(2**53)=9007199254740992.0

    any conversion int => float for number larger than 2^53 will be wrong!
    2^53+1=9007199254740993 => float(2**53+1)=9007199254740992.0
"""

import sys
from math import factorial as fact

# Important!

# int in Python stands for any signed integer number of unlimited precision and magnitude
# limited only by the available memory and the time required to manipulate large numbers.

# float in Python is IEE-754 double precision (64-bit).
# This is teh same as double in C derived languages such as C#.
# ---------------------------------------------------------------------------------------
# 53 bits of BINARY precision = 52+1
# which is equivalent to
# 15 to 17 bits of DECIMAL precision
# ---------------------------------------------------------------------------------------
# 1 bit for the sign
# 11 bits for the exponent
# 52 bits for the mantissa (significant)
# ---------------------------------------------------------------------------------------
# Conversion int => float
# As the mantissa of float is 53 bits no int larger than 2^53 can be converted to float
# without loss of precision! 
# ---------------------------------------------------------------------------------------

def test_module():   
    """Module-level tests."""
    # a very large integer!
    print(fact(100))    
    # the limits of float
    print(sys.float_info)
    print("min_float={}".format(sys.float_info.min))
    print("max_float={}".format(sys.float_info.max))
    print("most_negative_float={}".format(-sys.float_info.max))
    print()
    print("int => float")
    print("2^53={i} => float(2**53)={f}".format(i=2**53,f=float(2**53)))
    print()
    print("any conversion int => float for number larger than 2^53 will be wrong!")
    print("2^53+1={i} => float(2**53+1)={f}".format(i=2**53+1,f=float(2**53+1)))
