"""
This module illustrates the use of complex numbers in Python.
Complex numbers are built-in Python and do not need to be imprted from a module.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test050_complex as t50; t50.test_module()

    # The last two commands are specific to this module.
    import test050_rationals_fractions as t50
    t50.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t50)

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
    
    print("some example of complex numbers")
    
    # Python can construct complex number from literals 
    print()
    print("Python can construct complex number from literals")
    print("2j = {}".format(2j))
    print("3+4j = {}".format(3+4j))
    print("type(3+4j) = {}".format(type(3+4j)))

    # The Complex conctructor can also be used
    print()
    print("The Complex conctructor can also be used")
    print("parenthesis arond the complex string are optionals that is 'a+jb'='(a+jb)'")
    print("no spaces are allowed that is complex('a + jb') produces ValueError")
    print("complex('2+3j') = {}".format(complex('2+3j')))
    print("complex('(2+3j)') = {}".format(complex('(2+3j)')))
