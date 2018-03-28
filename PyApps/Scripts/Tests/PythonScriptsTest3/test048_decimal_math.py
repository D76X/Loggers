"""
This module illustrates of the Decimal type class of the decimal built-in module in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test048_decimal_math as t48; t48.test_module()

    # The last two commands are specific to this module.
    import test048_decimal_math as t48
    t46.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t46)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()  
    
"""

import sys
import decimal
from decimal import Decimal
import math

def test_module(): 
    """Module-level tests."""
    # The functions of the math module CANNOT be use with the decimal type!
    # Some alternatives are provided as methods on the Decimal class.
    print("The functions of the math module CANNOT be use with the decimal type!")
    print("Some alternatives are provided as methods on the Decimal class.")

    # example
    print("Decimal('0.81').sqrt()={}".format(Decimal('0.81').sqrt()))

    # more example here https://docs.python.org/3/library/decimal.html
    # ...


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