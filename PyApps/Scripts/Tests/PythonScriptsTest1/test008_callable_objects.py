"""
This module illustrates the definition and the use of a callable objetcs.

Usage:
    Run...
"""

import socket  

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

def test_resolver_caching():
    pass

def time_test_resolver():
    import timeit
    t = timeit.timeit("test008_callable_objects.test_resolver_caching", setup="from __main__ import test008_callable_objects")    
    return t
        