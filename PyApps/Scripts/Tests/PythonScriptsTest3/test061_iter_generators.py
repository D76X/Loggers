#!/usr/bin/env python3
"""
This module illustrates how to implement the iterable protocol in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test061_iter_generators as t61; t61.test_module()

    # The last two commands are specific to this module.
    import test061_iter_generators as t61
    t61.test_module()

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

import datetime
import time
from io import StringIO


def test_module():
    """Module-level tests."""

    print()
    print("Normally the the inter() fintion is invoked to obtain an iterator from an iplementation of iterable.")
    print("However, there is a special form of the type iter(callable, sentinel).")
    print("This special form is sometimens used to produce infinite sequences.")
    print("A callable is any object that has an implementation of __call_().")
    print("In an istance is callable then you can invoke it such as in someInstance() and some logic executes!")

    print()
    print("use the special iter(callable, sentinel) syntax to create an iterable for an infinite sequence")
    print("The Sentinel!")
    print("iter(callable, sentinel) => sentinel is the value that signal the end of the iterable...")
    print("an ")
    print("an infinite sequence can be generated by using a sentinel value taht cannot be produced by the callable")
    iterator = iter(datetime.datetime.now, None)
    print("iterator = iter(datetime.datetime.now, None)")
    print("next = next(iterator) = {}".format(next(iterator)))
    time.sleep(0.001)
    print("next = next(iterator) = {}".format(next(iterator)))
    time.sleep(0.001)
    print("next = next(iterator) = {}".format(next(iterator)))
    time.sleep(0.001)
    print("next = next(iterator) = {}".format(next(iterator)))

    # This example also illustrates how to use an in-memory file
    print()
    print("Read from a file until a match!")
    print()
    line1 = "This is an in memory file used to test the iter-callable...\n"
    line2 = "Another line of text...\n"
    line3 = "Another line of text...\n"
    line4 = "MATCH\n"
    line5 = "Another line of text...\n"

    # build the file
    # after each write the buffer pointer is at teh end of the file
    inMemoryFile = StringIO()
    inMemoryFile.write(line1)
    inMemoryFile.write(line2)
    inMemoryFile.write(line3)
    inMemoryFile.write(line4)
    inMemoryFile.write(line5)

    # the SringIO.getValue() gets all the content each time
    print(inMemoryFile.getvalue())
    print(inMemoryFile.getvalue())

    # but if you want to read the lines one by one you must reset
    # the buffer pointer to teh beginning of the file first
    inMemoryFile.seek(0)

    print(inMemoryFile.readline())
    print(inMemoryFile.readline())
    print(inMemoryFile.readline())
    print(inMemoryFile.readline())
    print(inMemoryFile.readline())

    # read from a text file in a for loop but do so by means of an iterable
    # on a callable and stop reading when something is matched. This example
    # can easily be adapted to teh case where the file is open from disk.

    inMemoryFile.seek(0)

    # use a guard just in case the loop might be endless
    guard = 0
    for line in iter(lambda: inMemoryFile.readline().strip(), "MATCH"):
        print("1- "+line)
        guard += 1
        if (guard > 10):
            break

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