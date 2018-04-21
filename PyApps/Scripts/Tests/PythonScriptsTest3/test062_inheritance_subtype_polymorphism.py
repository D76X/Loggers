
#!/usr/bin/env python3
"""
This module illustrates how to implement the iterable protocol in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test062_inheritance_subtype_polymorphism as t62; t62.test_module()

    # The last two commands are specific to this module.
    import test062_inheritance_subtype_polymorphism as t62
    t54.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t62)

    # clear th REPL
    clear = lambda: os.system('cls')
    clear()

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

Results:

"""

# ################################################################################################
# simple single inheritance


class Base:
    def __init__(self):
        print("Base initializer")
        print(type(self))

    def f(self):
        print("Base.f()")
        print(type(self))

# single inheritance syntax


class SubMinimal(Base):
    pass


class SubOne(Base):
    def f(self):
        print("SubOne.f()")
        print(type(self))


class SubTwo(Base):

    # call the base class initializer to make sure that the
    # instance can be properly initialized
    def __init__(self):
        super().__init__()                  # IMPORTANT! it is super() not super
        print("SubTwo initializer")

    def f(self):
        print("SubTwo.f()")
        print(type(self))

# ################################################################################################


class SimpleList:
    """
    A class that wraps a list.
    """

    def __init__(self, items):
        self._items = list(items)

    def add(self, item):
        self._items.append(item)

    def __getitem__(self, index):
        return self._items[item]

    def sort(self):
        self._items.sort()

    def __len__(self):
        return len(self._items)

    def __repr__(self):
        return "SimpleList({!r})".format(sle._items)

# ################################################################################################


def test_module():
    """Module-level tests."""

    # simple single inheritance
    print()
    print("simple single inheritance")

    print()
    base = Base()
    base.f()

    print()
    subm = SubMinimal()
    subm.f()

    print()
    sub1 = SubOne()
    sub1.f()

    print()
    sub2 = SubTwo()
    sub2.f()

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
