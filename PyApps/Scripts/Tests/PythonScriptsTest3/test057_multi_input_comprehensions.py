"""
This module illustrates the use of multi-input comprehensions in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test057_multi_input_comprehensions as t57; t57.test_module()

    # The last two commands are specific to this module.
    import test057_multi_input_comprehensions as t57
    t54.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t57)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

Results:

"""

from math import factorial
from pprint import pprint as pp

# when working with iterables this package is very useful
# from itertools import islice, count
import itertools as its

# this module imports a function from custom module!
from ntt_utils import isPrime, distinct


def test_module():
    """Module-level tests."""

    print()
    print("basic examples as a refresher")

    print()
    print("list comprension")
    print("syntax :")
    print("[expr(i) for in in iterable]")
    list = [i*2 for i in range(10)]
    print("list = [i*2 fro i in range(10)] = {}".format(list))

    print()
    print("set comprension")
    print("syntax :")
    print("{expr(item) for item in iterable}")
    set = {i*i for i in [1, 4, 4, 9, 9, 2, 2]}
    print("set = {{i*i for i in [1, 4, 4, 9, 9, 2, 2]}} = {}".format(set))

    print()
    print("dictionary comprension")
    print("syntax :")
    print("[expr(i): expr(i) for in in iterable]")
    dic = {str(i): i*2 for i in range(10)}
    print("dic = {{str(i): i*2 for i in range(10)}} = {}".format(dic))

    print()
    print("all thsese comprehensions may also include filters")
    list = [i*2 for i in range(10) if isPrime(i)]
    print("list = [i*2 for i in range(10) if isPrime(i)] = {}".format(list))
    set = {i*i for i in [1, 4, 4, 9, 9, 2, 2] if i % 2 == 0}
    print(
        "set = {{i*i for i in [1, 4, 4, 9, 9, 2, 2] if i % 2 == 0}} = {}".format(set))
    dic = {str(i): i*2 for i in range(10) if i % 2 != 0}
    print("{{str(i): i*2 for i in range(10) if i % 2 != 0}} = {}".format(dic))

    print()
    print("generator comprension")

    def generate(n):
        yield 1
        last = 1
        while True:
            next = last + 1
            yield next
            last = next

    g = generate(100000000)
    print("g = generate(100000000) = {}".format(g))
    print("next(g) = {}".format(next(g)))
    print("next(g) = {}".format(next(g)))
    print("next(g) = {}".format(next(g)))
    print("etc..")
