"""
This module is to show how to use testit in a module.

Usage:
    In the REPL import the module and run the test with the line
    timeit.timeit("t.function_to_test()", setup="import test009_timeit as t")
"""

def function_to_test():
    """Stupid test function."""
    L = []
    for i in range(100):
        L.append(i)
