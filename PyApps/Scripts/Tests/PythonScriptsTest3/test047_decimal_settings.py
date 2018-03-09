"""
This module illustrates of the Decimal type class of the decimal built-in module in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test047_decimal_settings as t47; t47.test_module()

    # The last two commands are specific to this module.
    import test047_decimal_settings as t47
    t47.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t47)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

Results:

    The defaul stettings of the decimal module.

    Context(prec=6, rounding=ROUND_HALF_EVEN, Emin=-999999, Emax=999999, capitals=1, clamp=0, flags=[FloatOperation], traps=[InvalidOperation, DivisionByZero, FloatOperation, Overflow])

    The default precision of 28 decimal digits is changed to just 6 decimal digits.
    Context(prec=6, rounding=ROUND_HALF_EVEN, Emin=-999999, Emax=999999, capitals=1, clamp=0, flags=[FloatOperation], traps=[InvalidOperation, DivisionByZero, FloatOperation, Overflow])


    Construct a decimal number with precision exceeding 6 decimal digit.
    The Decimal constructor preserves the original precision regardless of the settings.
    1.2345678 => 1.2345678

    The new decimal precision of 6 digits is enforced when operations are carried out.
    1.2345678 + 1 = 2.23457

    We change the settings on Decimal to throw a FloatOperation exception when you try to construct  a dicmal from a float.
    decimal.getcontext().traps[decimal.FloatOperation]=True
    Context(prec=6, rounding=ROUND_HALF_EVEN, Emin=-999999, Emax=999999, capitals=1, clamp=0, flags=[Inexact, FloatOperation, Rounded], traps=[InvalidOperation, DivisionByZero, FloatOperation, Overflow])

    Unexpected error: <class 'decimal.FloatOperation'>        
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
    print()
    print("The defaul stettings of the decimal module.")
    print()
    print(decimal.getcontext())

    #------------------------------------------------------------------------------------------------
    # Set the precision to other than the 28 digits of decimal precision.
    # Here we lower it down to 6 digit of decimal precision.
    print()
    print("The default precision of 28 decimal digits is changed to just 6 decimal digits.")
    decimal.getcontext().prec = 6
    print(decimal.getcontext())    
    print()
    
    print()
    print("Construct a decimal number with precision exceeding 6 decimal digit.")
    print("The Decimal constructor preserves the original precision regardless of the settings.")
    print("1.2345678 => {}".format(Decimal('1.2345678')))

    print()
    print("The new decimal precision of 6 digits is enforced when operations are carried out.")
    print("1.2345678 + 1 = {}".format(Decimal('1.2345678')+Decimal('1')))

    #------------------------------------------------------------------------------------------------
    # Prevent construction of Decimal from float

    # this line modify the user settings on the Decimal class to prevent Decimal 
    # instances from floats being accidentally created by the user code. Any 
    # attempt to do so will results in a decimal.FloatOperation exception. 
    print()
    print("We change the settings on Decimal to throw a FloatOperation exception when you try to construct  a dicmal from a float.")
    print("decimal.getcontext().traps[decimal.FloatOperation]=True")
    decimal.getcontext().traps[decimal.FloatOperation]=True
    print(decimal.getcontext())   
    print()

    try:
        cannot_do_this_anymore = Decimal(1.0)         
    except:        
        print("Unexpected error:", sys.exc_info()[0])
      
    