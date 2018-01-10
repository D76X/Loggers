"""
This module shows how to use lambdas Python.

Usage:
    In the REPL import the module and run the module level test function.    
    
    import test014_lambdas
    test014_lambdas.test_module()
    ['Marie Curie', 'Albert Einstein', 'Niels Bohr', 'Isaac Newton', 'Dmtri Mendeleev', 'Antoine Lavoisier', 'Carl Linnaeus', 'Alfred Wagener']
    ['Niels Bohr', 'Marie Curie', 'Albert Einstein', 'Antoine Lavoisier', 'Carl Linnaeus', 'Dmtri Mendeleev', 'Isaac Newton', 'Alfred Wagener']

    >>> import test014_lambdas
    f1, f2 = test014_lambdas.test_module_1()
    f1
    <function test_module_1.<locals>.<lambda> at 0x000001B12B4DCE18>
    f2
    <function test_module_1.<locals>.<lambda> at 0x000001B12B4DCF28>
    f1()
    a lamba with no arguments.
    f2('Hello', 'World!')
    arg1=Hello, arg2=World!
"""

def test_module():
    """Module-level test function."""
    scientists = ['Marie Curie', 'Albert Einstein', 'Niels Bohr', 
                  'Isaac Newton', 'Dmtri Mendeleev', 'Antoine Lavoisier',
                  'Carl Linnaeus', 'Alfred Wagener']
    print(scientists)
    # use a lamba to provide the function for the key argument
    s = sorted(scientists, key=lambda name: name.split()[-1])
    print(s)

def test_module_1():
    """More module-level tests."""
    lambda_with_no_args = lambda: print("a lamba with no arguments.")
    lamba_with_two_args = lambda one, two: print("arg1={}, arg2={}".format(one, two))
    return (lambda_with_no_args, lamba_with_two_args)

   
