"""
This module illustrates of the Decimal type class of the decimal built-in module in Python.

Usage:

    # all together!
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp;

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()
    
    # clear th REPL
    clear = lambda: os.system('cls')  
    clear()   
    
    # load the module and run the test function
    import test046_decimal as t
    t.test_module()

    # if you make changes reload the module and re-run the test function
    import imp
    imp.reload(t)  
    
"""

import decimal
from decimal import Decimal

# The Decimal type in the built-in module decimal is a fast correctly rounded type 
# for perfomring arithmetics in base 10. Crucially, it is still a floating point 
# type albeit with a base of 10 rather than 2. Moreover, with Decimal the precision
# is still finite as it is with the type float but in addition it is alos user
# configurable. The default is 28 digits of decimal precision.

def test_module(): 
    """Module-level tests."""
    
    # retrieve the configuration for Decimal.
    print(decimal.getcontext())

    # With floats 0.8-0.7 is not 0.1 because neither 0.7 nor 0.8 can be represented 
    # exactly as floating point numbers in binary format and the given precision.
    # To fix this problem and others it is best to use Decimal where applicable.
    print()
    print("This is wrong when using floats!")
    print("0.8-0.7={}".format(0.8-0.7))

    # notice that we have used "from decimal import Decimal" so that we can 
    # use the Decimal type in place of decimal.Decimal with the module name
    # as prefix.

    # Create a bunch of decimals
    # IMPORTANT - notice that we use a string as argument of Decimal!    
    zero_8 = Decimal('0.8')
    # from a string
    zero_7 = Decimal('0.7')
    print()
    print("This is right with decimals!")
    print("0.8-0.7={}".format(zero_8-zero_7))

    # Things go bad again when floats are passed to the Decimal constructor
    # because the binary of the floats 0.8 and 0.7 has already lost precision
    # by the time the corresponding decimal is constructed  
    zero_8_from_float = Decimal(0.8)
    # from a string
    zero_7_from_float = Decimal(0.7)
    print()
    print("This is wrong again despite using Decimal because we started with floats!")
    print("0.8-0.7={z8}-{z7}={r}".format(z8=zero_8_from_float, z7=zero_7_from_float, r=zero_8_from_float-zero_7_from_float))
    print()   

    #-----------------------------------------------------------------------------------------
    # Change user settings on Decimal.Decimal

    # this line modify the user settings on the Decimal class to prevent Decimal instances 
    # from floats being accidentally created by the user code. Any attempt to do so will 
    # results in a decimal.FloatOperation exception. 
    
    decimal.getcontext().traps[decimal.FloatOperation]=True
    #-----------------------------------------------------------------------------------------

    # From now on you cannot only pass strings to the Decimal constructor!

    # Preservetion of the precision.
    print("the precision given in te costructor is maintained.")
    three_0 = Decimal('3')
    three_1 = Decimal('3.0')
    three_2 = Decimal('3.00')

    print("{}*2={}".format(three_0, 2*three_0))
    print("{}*2={}".format(three_1, 2*three_1))
    print("{}*2={}".format(three_2, 2*three_2))