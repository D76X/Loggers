"""
This module illustrates the use of nested comprehensions in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test058_nested_comprehensions as t58; t58.test_module()

    # The last two commands are specific to this module.
    import test058_nested_comprehensions as t58
    t54.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t58)

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
    print("the output of a comprehension can itself be a comprehension!")
    vals = [[y*3 for y in range(x)] for x in range(10)]
    print("vals = [[y*3 for y in range(x)] for x in range(10)] = {}".format(vals))

    # the same as 
    outer = []
    for x in range(10):
        inner = []
        for y in range(x):
            inner.append(y*3)
        outer.append(inner)
    
    print("""
    
    # the same as the following nested fro loops...

    outer = []
    for x in range(10):
        inner = []
        for y in range(x):
            inner.append(y*3)
        outer.append(inner)
    
    """)

    print("outer = {}".format(outer))
