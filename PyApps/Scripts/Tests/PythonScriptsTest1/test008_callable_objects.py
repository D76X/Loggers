"""
This module illustrates the definition and the use of a callable objetcs.

Usage:
    In the REPL
    >>> import test008_callable_objects
    >>> test008_callable_objects.time_resolver_without_and_with_caching()
    0.5042847158598106
    0.48669231602153795
    0.4976048976097829
    0.49145116699946656
    >>> print(test008_callable_objects.testResolverInstance._cache)
    {'google.com': '216.58.204.14', 'sixty-north.com': '93.93.131.30'}
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

    def clear(self):
        """Clears the cache."""
        self._cache.clear()

    def has_host(self, host):
        """Checks whether a host is alrrady in the cache."""
        return host in self._cache

def test_resolver():
    """Module level function to test the module."""
    resolver = Resolver()
    resolver('google.com')
    resolver('sixty-north.com')
    # the callable syntax is sintactic sugar for
    resolver.__call__('bbc.co.uk')
    return resolver._cache

# constant for testing
testResolverInstance = Resolver()

def time_resolver_without_and_with_caching():
    """Shows the improvement obtained by caching."""
    import timeit
    import pprint as pp    
    t1 = timeit.timeit("t.testResolverInstance('google.com')", setup="from __main__ import test008_callable_objects as t")
    print(t1)
    t2 = timeit.timeit("t.testResolverInstance('google.com')", setup="from __main__ import test008_callable_objects as t")
    print(t2)
    t3 = timeit.timeit("t.testResolverInstance('sixty-north.com')", setup="from __main__ import test008_callable_objects as t")
    print(t3)
    t4 = timeit.timeit("t.testResolverInstance('sixty-north.com')", setup="from __main__ import test008_callable_objects as t")
    print(t4)
    pp.pprint(testResolverInstance._cache)
        