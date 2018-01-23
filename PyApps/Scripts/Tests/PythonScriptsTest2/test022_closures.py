"""
This module illustrates local functions and closures in Python.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test022_closures as t
    t.test_module()
    
""" 

# a closure is a reference to the containing environments to whose 
# data a contained scope refer to i.e. x is in the enclosing scope
# so Python RT keeps a refence to it as long as a reference to a
# instance of the local_function exists as this uses x from its  
# enclosing scope.
def enclosing_function():
    x = "this data is going to be kept alive by closure"
    def local_function():
        # the LF closes over data of the enclosing scope
        print(x)
    return local_function

def test_module():
    """Module-level tests."""
    # this creates a closure
    local = enclosing_function()
    local()
    # the __closure__ is the reference through which Python
    # maintains closures
    print("local.__closure__ = {}".format(local.__closure__))
