"""
This module illustrates the usage and meaning of the keywords global and 
nonlocal in Python.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test024_global_nonlocal as t
    t.test_module()
    f1 = enclosing_function_1() => f1() = local message
    f2 = enclosing_function_2() => f1() = global message

    how does the keyword nonlocal work?
    f3 = enclosing_function_3() => f1() = enclosing message*
    f3 = enclosing_function_3() => f1() = enclosing message**
    f3 = enclosing_function_3() => f1() = enclosing message***

    nonlocal can be used to capture and modify state on the enclosing scope as in this example
    ftimed()=None
    wait a bit...
    ftimed()=0.5020618438720703
    wait another bit...
    ftimed()=1.0031158924102783
    
""" 

import time

message = "global message"

def enclosing_function_1():
    """Illustrates the LEGB rule"""
    message = "enclosing message"

    def local_function():
        message = "local message"
        return message

    return local_function

def enclosing_function_2():
    """Illustrates the usage of the keyword global"""
    message = "enclosing message"

    def local_function():
        # look up message from the global scope
        global message
        return message

    return local_function

def enclosing_function_3():
    """Illustrates the usage of the keyword nonlocal"""
    message = "enclosing message"

    def local_function():
        # non local lets the enclosed function to act on data defined in
        # the enclosing scope
        nonlocal message
        message = message + "*"
        return message

    return local_function

def timed_function():
    last_called = None

    def elapsed():
        nonlocal last_called
        now = time.time()
        if last_called is None:
            last_called = now
            return None
        result = now - last_called
        return result

    return elapsed

def test_module():
    """Module-level tests."""
    f1 = enclosing_function_1()
    print("f1 = enclosing_function_1() => f1() = {}".format(f1()))
    f2 = enclosing_function_2()
    print("f2 = enclosing_function_2() => f1() = {}".format(f2()))
    print()
    print("how does the keyword nonlocal work?")
    f3 = enclosing_function_3()
    print("f3 = enclosing_function_3() => f1() = {}".format(f3()))
    print("f3 = enclosing_function_3() => f1() = {}".format(f3()))
    print("f3 = enclosing_function_3() => f1() = {}".format(f3()))
    print()
    print("nonlocal can be used to capture and modify state on the enclosing scope as in this example")
    ftimed = timed_function()
    print("ftimed()={}".format(ftimed()))
    print("wait a bit...")
    time.sleep(0.5)
    print("ftimed()={}".format(ftimed()))
    print("wait another bit...")
    time.sleep(0.5)
    print("ftimed()={}".format(ftimed()))
