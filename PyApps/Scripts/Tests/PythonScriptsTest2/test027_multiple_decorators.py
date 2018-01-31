"""
This module illustrates function decorators in Python.
It also shows how multiple decorator can be applied to the same callable
object.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test027_multiple_decorators as t
    t.test_module()    

    calling <function escape_unicode.<locals>.wrap at 0x000002530FD401E0>
    trace 1
    groß => '(gro\xdf)'
    calling <function escape_unicode.<locals>.wrap at 0x000002530FD401E0>
    trace 2
    über => '(\xfcber)'
    calling <function escape_unicode.<locals>.wrap at 0x000002530FD401E0>
    trace 3
    läßt => '(l\xe4\xdft)'

    tracing is disabled
    groß => '(gro\xdf)'
    über => '(\xfcber)'
    läßt => '(l\xe4\xdft)'    
""" 

class Trace:
    """
    A class that whose instances can be used as a decorator 
    to trace the calls to the decorated funtion.
    """
    def __init__(self):
        self.enabled = True
        self.count = 0

    def __call__(self, f):
        def wrap(*args, **kwargs):
            if self.enabled:
                    print("calling {}".format(f))
                    self.count += 1
            return f(*args, **kwargs)
        return wrap

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

# a mudule leve instance of the Trace class
tracer1 = Trace()

# the function add_round_brackets is a callable object and can be decorated 
# even multiple times as the output of each decorator is itself a callable
# object.
@tracer1
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
        m = "{w} => {r}".format(w=w,r=add_round_brackets(w))
        if(tracer1.enabled):
            print("trace {}".format(tracer1.count))
        print(m)

    print()
    
    # we can disable the tracing!
    tracer1.enabled = False
    print("tracing is disabled")
    for w in german_words:
        m = "{w} => {r}".format(w=w,r=add_round_brackets(w))
        if(tracer1.enabled):
            print("trace {}".format(tracer1.count))
        print(m)

