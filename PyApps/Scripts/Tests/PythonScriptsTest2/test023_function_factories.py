"""
This module illustrates local functions and closures and function factories in Python.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test023_function_factories as t
    t.test_module()
    
""" 

# function factories are parameterised functions which return functions. 
def raise_to(exp):
    """A factory function for a^b"""
    # closure
    def raise_to_exp(x):
        return pow(x, exp)
    return raise_to_exp

def test_module():
    """Module-level tests."""
    a = 2
    pwr2 = 2
    pwr3 = 3
    rt2 = raise_to(pwr2)
    print("{a}^{exp}={r}".format(a=a, exp=pwr2, r=rt2(a)))  
    rt3 = raise_to(pwr3)
    print("{a}^{exp}={r}".format(a=a, exp=pwr3, r=rt3(a)))    
