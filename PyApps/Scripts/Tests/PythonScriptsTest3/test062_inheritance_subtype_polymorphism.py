
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

from pprint import pprint as pp

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

 # Rules
 # For a class with multiple bases which does nor define __init__ only the insit of the first base is invoked
 # __bases__ is a tuple of base classes in the same order give in the class definition

 # MRO - method resolution order in multi-inheritance.
 # __mro__ stores the MRO for a class or you can use the mro() method on the class.
 # the MRO is essential because is a flat view of the inheritance chain used by the Pyhton interpreter to
 # decide how to resolve the calls to the methods on an instance. Given obj.method() Pyhton walks the MRO.
 # upwards and the first base class that provides a matching method is used for the invokation.
 # Python employs an algorithm known as C3 to determine the MRO of a class.

 # C3 Algorithm Rules for determing the MRO
 # 1- Subclasses came always before Base classes regadless the order in which they appear in the definition of a class.
 # 2- Given rule 1 any other class appears in MRO in the order given with the definition of the class.

 # WARNING!
 # In Python it is possible to define classes whose inheritance definitiion violates the C3 algorithm rules.
 # In these cases the Python interpreter will fail to compile the code.

 # Example of classes that fail the C3 algorithm rules
 # D fails to compile because B and C must both come before A in any MRO as B and C inherit from A.
 # However the class definition for D places A explicitly before C which fails the C3 algorithm rules
 # as A cannot at the same time come before C and after C.
 # class A: pass
 # class B(A): pass
 # class C(A): pass
 # class D(B, A, C): pass


class SortedIntList(IntList, SortedList):
    """
    A sorted list of integers.
    """

    def __repr__(self):
        return "SortedIntList({!r})".format(list(self))

# A bunch of classes to illustrate MRO


class A:
    def func(self):
        return "A.func"


class B:
    def func(self):
        return "B.func"


class C(A):
    def func(self):
        return "C.func"

# which implementation of func is actually resolved on D?
# this depends on the MRO of the class D!


class D(B, C):
    pass

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

    print()
    print("__bases__ is a tuple of base classes in the same order give in the class definition")
    print("SortedIntList.__bases__ = {}".format(SortedIntList.__bases__))

    print()
    print("MRO - method resolution order in multi-inheritance")

    print()
    pp("SortedIntList.__mro__ = {}".format(SortedIntList.__mro__))

    print()
    pp("SortedIntList.mro() = {}".format(SortedIntList.mro()))

    print()
    d = D()
    pp("D.mro() = {}".format(D.mro()))

    print()
    print("d = D => d.func() = {}".format(d.func()))

    print()
    print("on instances of type D the call to the method func() is resolved by the first class in the MRO that provide an implementation for func()")

    # ----------------------------
    # How super() works in Python
    # ----------------------------

    # Given a method resolution order (MRO) and a class C in the MRO, super() gives you an object which resolves methods using only the
    # part of the MRO which comes after C. In other words, super() does not work with the base classes of a class but instead it works
    # with the MRO of the type of the object on which a method was originally invoked.

    # A call to super() returns an object that is called "proxy" object which routes methods calls.
    # There are two types of proxy obejcts.

    # 1-Bound proxy - these are bound to instances or classes objects.
    # These are relevant in the contest of the object returned by the call to super() and sometimes they are also called super()-proxies.

    # 2-Unbound proxy - are not bound to either instances or classes objects.
    # Unbound proxies do not do any method call dispatching and are a bit of oddity in Python.

    # Bound Proxy

    # ----------------------
    # 1 Class-bound proxy
    # ----------------------

    # proxy = super(base-class, derived-class)

    # Method Resolution Strategy

    # Python finds the MRO for derived-class.
    # Python finds the base-class in the MRO.
    # Pyhton takes the part of the MRO that comes AFTER base-class.
    # Python resolves the method call to the first implementation available on the MRO fragment by matching on the methid name.

    # Example of Class-bound proxy
    print()
    print("Example of class-bound super()-proxy")

    print()
    class_bound_proxy = super(SortedList, SortedIntList)
    print("class_bound_proxy = super(SortedList, SortedIntList) = {}".format(
        class_bound_proxy))

    print()
    print("Python takes the MRO of the derived-class...")
    pp("SortedIntList.mro() = {}".format(SortedIntList.mro()))

    print()
    print("Python takes only the part of MRO that comes after the base-class SimpleList > Object")

    print()
    print("Python resolved the call to a method to the first class that has an implementation of a method with the same name on the MRO fragment.")
    print("Hence...")
    print("super(SortedList, SortedIntList).add = {}".format(class_bound_proxy.add))

    print()
    print("class-bound proxies cannot be use for direct invokation of instance methods!")
    print("class-bound proxies can be use for direct invokation of static or class methods!")
    super(SortedIntList, SortedIntList)._validate(5)
    print("super(SortedIntList, SortedIntList)._validate(5) = {}".format(
        super(SortedIntList, SortedIntList)._validate(5)))

    # ----------------------
    # 2 Instance-bound proxy
    # ----------------------

    # proxy = super(class, instance-of-class)
    # instance-of-class must be any instance of class or any descendant.

    # Method Resolution Strategy

    # Python finds the MRO for the type used as second argument.
    # Pyton finds the location of the first argument in the MRO.
    # Python takes the MRO fragment AFTER the location above to resolve method calls by matching on teh method name.
    # With instance-bound super()-proxies class, static and instance methods call all be invoked.

    # Example of Instance-bound proxy
    print()
    print("Example of instance-bound super()-proxy")

    print()
    pp("SortedIntList.mro() = {}".format(SortedIntList.mro()))

    print()
    sil = SortedIntList([3, 2, 1, -1])
    print("sil = SortedIntList([3, 2, 1, -1]) = {}".format(sil))

    print()
    instance_bound_proxy = super(SortedList, sil)
    print("instance_bound_proxy = super(SortedList, sil) = {}".format(
        instance_bound_proxy))

    print()
    print("super(SortedList, sil).add(6) = {}".format(
        instance_bound_proxy.add(6)))
    print("super(SortedList, sil).add(""wrong data!"") = {}".format(
        instance_bound_proxy.add("wrong data!")))

    print()
    print("In the last example we are able to insert a string intop an instance of SortedIntList!")
    print("This is not what we want nevertheless it happens because the call to add is resolved to SimpleList which follows SortedList in the MRO!")

    print()
    print("This example shows the danger of super() and multi-inheritance in Python in general!")

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
