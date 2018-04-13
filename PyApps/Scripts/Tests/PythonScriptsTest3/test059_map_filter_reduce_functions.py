"""
This module illustrates the use of the map function in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test059_map_function as t59; t59.test_module()

    # The last two commands are specific to this module.
    import test059_map_function as t59
    t54.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t59)

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
    print("map() takes an iterable and a function and return another iterable")
    print("whose elements are the elements of the original iterable to which the")
    print("give function has been applied.")

    print()
    # map returns a generator function!
    print("map returns a generator function!")
    unicode_points = map(ord, "The quick brown fox")
    print("map(ord, ""The quick brown fox"") = {}".format(unicode_points))

    print()
    codes = []
    for code in unicode_points:
        codes.append(code)

    print("codes = {}".format(codes))

    # map over multiple input sequences
    print()
    print("map can be used over multiple input sequences")
    print("for example a function that takes two params can be used with map and two input sequnces")
    print("ultimately the number of input sequences must match the number of argument of the function")

    print()
    print("if the input sequnces are not all of the same size then map terminates on the latest index of teh shortest of them.")

    def f(a, b, c):
        """
        Given three numbers it computes a*b+a*c+b*c
        """
        return a*b+a*c+b*c

    l1 = [x for x in range(3)]
    l2 = [x*x for x in range(4, 7, 1)]
    l3 = [1/x for x in range(7, 10, 1)]
    mapped = map(f, l1, l2, l3)
    results = list(mapped)

    print()
    print(l1)
    print(l2)
    print(l3)
    print("mapped = map(f, l1, l2, l3) = {}".format(results))

    # comprehension vs map
    print()
    print("comprehension vs map...")
    alist = [x*x for x in range(5)]
    aset = {x*2 for x in alist}
    adic = {x: y for x in alist for y in aset}
    print("alist = [x*x for x in range(5)] = {}".format(alist))
    print("aset = {{x*2 for x in alist}} = {}".format(aset))
    print("adic = {{x:y for x in alist for y in aset}} = {}".format(adic))

    print()
    print("the map function is another functional way to build a sequence")
    # the map function is another functional way to build a sequence
    alist = map(lambda x: x*x, range(5))
    aset = set(alist)
    print("alist = map(lambda x: x*x, range(5)) = {}".format(alist))
    print("aset = set(alist) = {}".format(aset))
