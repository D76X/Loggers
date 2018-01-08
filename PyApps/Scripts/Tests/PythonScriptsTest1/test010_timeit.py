"""
This module is to show how to use testit in a module.

Usage:
    In the REPL import the module and run the test with the line
    timeit.timeit("t.function_to_test()", setup="import test9_timeit as t")
"""

def function_to_test():
    """Stupid test function."""
    L = []
    for i in range(100):
        L.append(i)

def time_of_function_to_test():
    """"Module level test function"""
    import timeit
    t = timeit.timeit("test010_timeit.function_to_test", setup="from __main__ import test010_timeit") 
    return t
