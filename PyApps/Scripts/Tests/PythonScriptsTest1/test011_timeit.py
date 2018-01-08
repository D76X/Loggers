"""
This module shows how to use testit in a module with a more complex setup.

Usage:
    In the REPL import the module and run the module level test function.
    Example
    >>>import test011_timeit
    >>>test011_timeit.test_timeit()
"""

# some pre-defined constants
A = 1
B = 2

# function that does something critical
def foo(n, m):
    """"Function that does something critical given some values."""
    pass

def test_timeit():
    """"Module level test function"""
    import timeit   
    # notice that we use timeit.Timer
    t = timeit.Timer(stmt="t.foo(t.A, t.B)", setup='from __main__ import test011_timeit as t')
    print(t.timeit(5))
