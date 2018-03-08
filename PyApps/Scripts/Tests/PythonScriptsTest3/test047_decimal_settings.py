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
    import test047_decimal_settings as t
    t.test_module()

    # if you make changes reload the module and re-run the test function
    import imp
    imp.reload(t)  

Results:

    Context(prec=28, rounding=ROUND_HALF_EVEN, Emin=-999999, Emax=999999, capitals=1, clamp=0, flags=[], traps=[InvalidOperation, DivisionByZero, Overflow])

    We have changed settings on Decimal to throw when you try to construct from floats.
    decimal.getcontext().traps[decimal.FloatOperation]=True
    
    Traceback (most recent call last):
    File "<stdin>", line 1, in <module>
    File "C:\GitHub\Loggers\PyApps\Scripts\Tests\PythonScriptsTest3\test047_decimal.py", line 45, in test_module
        cannot_do_this_anymore = Decimal(1.0)
    decimal.FloatOperation: [<class 'decimal.FloatOperation'>]
        
"""

import sys
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

    #------------------------------------------------------------------------------------------------
    # Set the precision to other than the 28 digits of decimal precision.
    # Here we lower it down to 6 digit of decimal precision.
    decimal.getcontext().prec = 6
    print(decimal.getcontext())    
    print("1.2345678 => {}".format(Decimal('1.2345678')))

    #------------------------------------------------------------------------------------------------
    # Prevent construction of Decimal from float

    # this line modify the user settings on the Decimal class to prevent Decimal 
    # instances from floats being accidentally created by the user code. Any 
    # attempt to do so will results in a decimal.FloatOperation exception. 
    decimal.getcontext().traps[decimal.FloatOperation]=True
    print(decimal.getcontext())

    print()
    print("We have changed settings on Decimal to throw when you try to construct from floats.")
    print("decimal.getcontext().traps[decimal.FloatOperation]=True")
    
    print()

    try:
        cannot_do_this_anymore = Decimal(1.0)         
    except:
        print(cannot_do_this_anymore)
        print("Unexpected error:", sys.exc_info()[0])
      
    