"""
This module illustrates function decorators in Python.
In particular it illustrates a technique where decorators are employed to
validate the arguments to a function or any callable object to which the
decorator is applied.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test030_ducktails_arguments_validation as t
    t.test_module()   

    create_list('hello',3) =>
    ['hello', 'hello', 'hello']

    create_list('hello',-1) =>
    Traceback (most recent call last):
      File "<stdin>", line 1, in <module>
      File ".\test030_ducktails_arguments_validation.py", line 57, in test_module
        print(create_list("hello",-1))
      File ".\test030_ducktails_arguments_validation.py", line 32, in wrap
        raise ValueError("Argument {} must be non-negative".format(index))
    ValueError: Argument 1 must be non-negative
    
"""

# remember that a decorator is a function that takes a callable object
# and return a callable object. In general the return value is a wrapper
# function that capture the callable object given to the decorator as
# its input in the closure. This function does not take a callable object
# as "index" is an integer not a function thus it is not strickly speaking
# a decorator.
def check_non_negative(index):
    """
    A function that returns a decorator for argument validation.
    It checks that an argument is not negative.

    Args: 
        index: the index of the argument to the decorated function
               on which the validation is run. 
    
    Returns: the decorator function used to validate the argument of
             the decorated function.
    """
    # validator(f) is the real decorator!
    def validator(f):
        def wrap(*args):
            # wrap forms a closure over "index" and "f".
            if args[index] < 0:
                raise ValueError("Argument {} must be non-negative".format(index))
            return f(*args)
        return wrap
    return validator

# the "decorator" is used to validate the second argument.
# check_non_negative(1) invokes check_non_negative with argument 1 and the 
# return value of this invokation is a decorator!
@check_non_negative(1)
def create_list(value, size):
    """
    Create a list where value is repeated size times.

    Args:
        value: the value to repeat.
        size: the length of the list to create.
    
    Returns: a list where value is repeated size times.    
    """
    return [value] * size

def test_module():
    """Module-level tests."""
    print("create_list('hello',3) =>")
    print(create_list("hello",3))
    print()
    print("create_list('hello',-1) =>")
    print(create_list("hello",-1))
