"""
This module illustrates function decorators in Python.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test025_function_decorators as t
    t.test_module()
    
""" 

# Decorators are a way to modify and enhance existing functions in a non intrusive
# and maintainable fashion. In Python a decorator is a callable object which takes 
# a callable object and returns another callable object. In effect decorators are 
# a generalization of functions that take a function and return another function.
# Decorators are applied to the definition of a function which is then passed as 
# argument to the decorator which then returns another function with the same name 
# as the function passed to it and some changed implementation details.

def escape_unicode(f):
    """
    This is a decorator function that escapes all characters of a string
    that are unicode but not ASCII.

    Args: 
        f: the function (a callable object) to which the decorator is 
           applied. This must be a function that returns a string of 
           unicode characters.

    Returns: a function (a callable object) which applies f to any given 
             arguments and then passes the return of the invokation 
             through the ascii function.
    """
    # local functions are often used in the definition of decorators.
    # notice that f is part if a closure created by the local function
    # wrap.
    def wrap(*args, **kwargs):
        x = f(*args, **kwargs)
        return ascii(x)

    # a decorator function must return a function (a callable abject).
    return wrap

# here we use the decorator to enhance/modify a function that would 
# normally just return the input string in round brackets so that all
# the non ASCII chars are also escaped.
@escape_unicode
def add_round_brackets(word):
    """
    Returs the input string in round brackets.
    """
    return "({})".format(word)

def test_module():
    """Module-level tests."""
    
    german_words = ["groß", "über", "läßt"]
    
    for w in german_words:
        print("{w} => {r}".format(w=w,r=add_round_brackets(w)))
