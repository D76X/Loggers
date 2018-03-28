"""
This module illustrates of the int and float built-in types in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test045_int_float as t45; t45.test_module()

    # The last two commands are specific to this module.
    import test045_int_float as t45
    t45.test_module()    

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t45)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()  
    
Results: 

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
    print()
    print("Python can handle integers of any size limited only by memory and manipulation time.")
    print(fact(100))    

    # the limits of float
    print()
    print("The available range of floating numbers.")
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

    # float supports Infinity, -Infinity and NaN
    print()
    print("float also supports Infinity, -Infinity and NaN and they perform as expected in aritmethics.")
    nan = float('Nan')
    print("float('Nan')={}".format(nan))
    inf = float('Infinity')
    minf = float('-Infinity')
    print("float('Infinity') = {} , float('-Infinity') = {}".format(inf, minf))
    print("1+nan={}".format(1+nan))
    print("1+inf={}".format(1+inf))
    print("-inf+1={}".format(minf+1))

    # some interesting things with integers
    # oddities with decimals
    print()
    print("some things to watch out!")
    print("(-7) % 3 = {}".format((-7) % 3))
    print("-9 is the first divisor of 3 that is smaller of -7 hence reminder = -7 - (-9) = 2")


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
