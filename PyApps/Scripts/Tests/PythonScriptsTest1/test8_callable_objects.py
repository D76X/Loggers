"""
This module illustrates the definition and the use of a callable objetcs.

Usage:
    Run...
"""

import socket 
#from timeit import timeit 

class Resolver:
    """
    A class that illustrates state cashing all the usage of __call__ 
    to turn it into a callable aobjet.
    """
    def __init__(self):
        """Initialises the object."""
        self._cache = {}

    def __call__(self, host):
        """Makes the class into a callable object."""
        if host not in self._cache:
            self._cache[host] = socket.gethostbyname(host)
        return self._cache[host]

def test_resolver():
    """Module level function to test the module."""
    resolver = Resolver()
    resolver('google.com')
    resolver('sixty-north.com')
    # the callable syntax is sintactic sugar for
    resolver.__call__('bbc.co.uk')
    return resolver._cache

def test():
    """Stupid test function"""
    L = []
    for i in range(100):
        L.append(i)

def test_lookup_time():
    import timeit
    print(timeit.timeit("test()", setup="from __main__ import test"))
    #timeit(setup="import Resolver resolver = Resolver()", stmt="resolver('python.org')", number=1)


    