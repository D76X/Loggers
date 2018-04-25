
#!/usr/bin/env python3
"""
This module illustrates of single and multiple inheritance and polymorphism in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system(
        'cls'); import imp; import test062_inheritance_subtype_polymorphism as t62; t62.test_module()

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

# The basics of simple single inheritance


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

# A more realistics example of single inheritance


class SimpleList:
    """
    A class that wraps a list.
    """

    def __init__(self, items):
        self._items = list(items)

    def add(self, item):
        self._items.append(item)

    def __getitem__(self, index):
        return self._items[index]

    def sort(self):
        self._items.sort()

    def __len__(self):
        return len(self._items)

    # by overriding __reper__ instances of this class
    # can be passed to print()
    def __repr__(self):
        return "SimpleList({!r})".format(self._items)


class SortedList(SimpleList):
    """
    A class that wraps a list and keeps it sorted.
    """

    def __init__(self, items=()):
        super().__init__(items)  # IMPORTANT it is super() not super!
        self.sort()

    def add(self, item):
        super().add(item)
        self.sort()

    def __repr__(self):
        return "SortedList({!r})".format(list(self))

# ################################################################################################

# The basics of multiple inheritance


class IntList(SimpleList):
    """
    A list that can only take integers.
    """

    def __init__(self, items=()):
        for x in items:
            self._validate(x)
        super().__init__(items)

    @staticmethod
    def _validate(x):
        if not isinstance(x, int):
            raise TypeError("IntList only supports integer values.")

    def add(self, item):
        self._validate(item)
        super().add(item)

    def __repr__(self):
        return "IntList({!r})".format(list(self))

 # this class inherits from multiple base classes!
 # it leaves open some questions
 # 1- both base classes define the add method - which one is called on the descendant?


class SortedIntList(IntList, SortedList):
    """
    A sorted list of integers.
    """

    def __repr__(self):
        return "SortedIntList({!r})".format(list(self))

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

    # simple single inheritance
    print()
    print("simple single inheritance")

    sortedList = SortedList([3, 4, 1, 10])
    print("sortedList = SortedList([3,4,1,10]) = {}".format(sortedList))
    sortedList.add(-8)
    print("sortedList.add(-8)...")
    print(sortedList)

    # Type checking in Python
    print()
    print("Type checking in Python")

    # isinstance()

    print()
    print("use isinstance() ...to check the type of an istance")
    print("isinstance(3, int) = {}".format(isinstance(3, int)))
    print("isinstance(3, str) = {}".format(isinstance(3, str)))
    print("isinstance(""hello"", str) = {}".format(isinstance("hello", str)))
    print("isinstance(""hello"", int) = {}".format(isinstance("hello", int)))
    print("isinstance(sortedList, SortedList) = {}".format(
        isinstance(sortedList, SortedList)))
    print("isinstance(sortedList, SimpleList) = {}".format(
        isinstance(sortedList, SimpleList)))

    print()
    print("isinstance can be use with a tuple as its second argument!")
    print("isinstance(3, (int, str, SimpleList)) = {}".format(
        isinstance(3, (int, str, SimpleList))))
    print("isinstance(3, (list, str, SimpleList)) = {}".format(
        isinstance(3, (list, str, SimpleList))))

    print()
    intList = IntList([1, 2, 3, 4])
    print("intList = IntList([1, 2, 3, 4]) = {}".format(intList))
    intList.add(10)
    print(
        "intList.add(10) => intList = IntList([1, 2, 3, 4]) = {}".format(intList))

    # issubclass()
    print()
    print("use issubclass()...to check that a type is a subtype of some other type")
    print("issubclass() looks at the entire inheritance graph nt only the direct parent of the type used as its first arg")
    print("issubclass(IntList, SimpleList) = {}".format(
        issubclass(IntList, SimpleList)))
    print("issubclass(IntList, SortedList) = {}".format(
        issubclass(IntList, SortedList)))

    # multiple inheritance
    print("multiple inheritance")
    sortedIntList = SortedIntList([3, 5, 4, 19, 6])
    print("sortedIntList = SortedIntList([3,5, 4, 19, 6]) = {}".format(
        sortedIntList))
    sortedIntList.add(-100)
    print("sortedIntList.add(-100) => sortedList = {}".format(sortedIntList))
    print("isinstance(sortedIntList, IntList) = {}".format(
        isinstance(sortedIntList, IntList)))
    print("isinstance(sortedIntList, sortedList) = {}".format(
        isinstance(sortedIntList, SortedList)))
    print("issubclass(SortedIntList, SimpleList) = {}".format(
        issubclass(SortedIntList, SimpleList)))
    print("issubclass(SortedIntList, IntList) = {}".format(
        issubclass(SortedIntList, IntList)))
    print("issubclass(SortedIntList, SortedList) = {}".format(
        issubclass(SortedIntList, SortedList)))

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
