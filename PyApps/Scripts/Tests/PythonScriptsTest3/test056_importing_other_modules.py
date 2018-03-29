"""
This module illustrates the details fo how modules are imported into other modules in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test056_importing_other_modules as t56; t56.test_module()

    # The last two commands are specific to this module.
    import test056_importing_other_modules as t56
    t56.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t56)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

Results:

"""

import sys
from pprint import pprint as pp

# this module imports a function from custom module!
from ntt_utils import isPrime

def test_module(): 
    """Module-level tests."""  
    print()
    print("when a import statement is encounter the Python interpreter checks the paths listed in the sys.path")
    print("one after the other and imports the content of the first *.py file that has the name of the module.")
    print("If sys.path does not list a path which contains a *.py whith the name of the module given in the")
    print("import statement the Python interpreter throws an error.")

    print()
    print("print the sys.path variable")
    pp(sys.path)    

    print()
    print("the sys.path[0] is the argument passed to the Python interpreter when it is lauched.")
    print("sys.path[0] == '' instructs the Python interpreter to search for modules starting from the current directory.")


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
