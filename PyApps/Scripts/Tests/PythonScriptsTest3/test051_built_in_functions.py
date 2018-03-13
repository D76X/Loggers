"""
This module illustrates the use of some of the most useful built-in functions in Python.

Remember

0. Integer in Python have unlimited precision and canbe of arbitrary magnitude.
1. In general The math module is to work with floating point numbers and integer number only.
2. Use the Decimal class and its methods from the decimal module when you need more precision that it is available from floats.
4. complex number are a built-in type in Python
5. Use cmath instead of math to perform arithmetic on compex number  
6. Do not mix float types and Decimal type as there might be loss of precision.
7. Decimal is user configirable.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test051_bilt_in_functions as t51; t51.test_module()

    # The last two commands are specific to this module.
    import test051_bilt_in_functions as t51
    t51.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t51)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

"""


def test_module(): 
    """Module-level tests."""
