"""
This module illustrates how to implement the iterable protocol in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test061_iter_generators as t61; t61.test_module()

    # The last two commands are specific to this module.
    import test061_iter_generators as t61
    t54.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t61)

    # clear th REPL
    clear = lambda: os.system('cls')
    clear()

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

Results:

"""


def test_module():
    """Module-level tests."""

    print()
    print("Normally the the inter() fintion is invoked to obtain an iterator from an iplementation of iterable.")
    print("However, there is a special form of the type iter(callable, sentinel).")
    print("This special form is sometimens used to produce infinite sequences.")
    print("A callable is any object that has an implementation of __call_().")
    print("In an istance is callable then you can invoke it such as in someInstance() and some logic executes!")
