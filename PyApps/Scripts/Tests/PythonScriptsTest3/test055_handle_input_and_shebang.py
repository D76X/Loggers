#!/usr/bin/env python3
"""
This module illustrates the basics of how to capture user input from the console in Python.

It also add the shebang #!/usr/bin/env python3 on top of the *.py file. 
On both linux and windows the shebang is used to document which interpreter version the 
script is meant to be run by and when run it is used by the OS to execute it with the right
interpreter if present on the machine.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test055_handle_input as t55; t55.test_module(["one", "two", 3])

    # WARNING!
    # This is different than usual because we are passing the list of arguments ["one", "two", 3] ot the test_module() function
    # The last two commands are specific to this module.
    import test055_handle_input as t55
    t55.test_module(["one", "two", 3])                              <<< This  is different than usual!

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t55)     

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

# IMPORTANT!
# This example shows the basics of how to capture and handle the input from the console.
# There are specialized packages to deal with this kind of things.
# 
# ARGPARSE
# DOCOPT 

def test_module(argv):
    """Module-level tests."""
    
    # there is always one argument 
    # the name of the file
    # print("args[0] = {}".format(args[0]))
    
    # enumerate allows the extraction of the index idx
    # args[0:] slices args from its head
    for idx, arg in enumerate(argv[0:]):        
        print("args[{x}] = {arg}".format(x=idx,arg=arg))

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
    # when I run it as a script such as below
    # python test055_handle_input.py "first" "second" 3
    # sys.argv = ["test055_handle_input.py", "first", "second", 3]
    test_module(sys.argv)

# ##########################################################################################
