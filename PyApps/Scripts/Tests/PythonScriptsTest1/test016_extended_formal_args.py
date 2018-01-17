"""
This module illustrates the extended argument syntax in Python.

Usage:
    In the REPL import the module and run the module level test function.
"""

def function_with_extended_args(*args, **kargs):
    "This is a function definition with extended args."
    return x % 2 == 0

# *args where args is by convention but not necessary
def hypervolume(*args):
    """Computes the volume of a n-dimentional cuboid."""    
    # typical Python pattern
    # take an iterator on the args
    i = iter(args)
    # move onto the first element of the iterator
    accumulator = next(i)
    for length in i:
        accumulator *= length
    return accumulator

def hypervolume_improved(length, *lengths):
    """
    Computes the volume of a n-dimentional cuboid.
    This version handles the case when no arguments
    are provided by the caller.

    Args:
        length: the length of a 1-dimentional cuboid.
                This is required argument.

        *length: the length of the sides of a n-dimentinal cuboid.
                 This collects all optional arguments.

    Returns: the volume of the hyper cuboid.
    """ 

    v = length
    # alternative to the iterator pattern.
    # Python handles length as a positional required argument,
    # a nice error is provided by the runtime when positional
    # required argument are not provided by the caller. This 
    # techniques spares us the effort to have to deal with 
    # exception cases and try...catch blocks.
    for item in lengths:
        v *= item
    return v


def test_module():
    """Module-level tests."""
    print("the print function", "accepts", "any number of args!")
    print("the string.format is another example...")
    print("{a}<===>{b}".format(a="London", b="Berlin"))
    line_length = hypervolume(1)
    print("the length of a line is {}".format(line_length))
    rect_area = hypervolume(2,3)
    print("the area of a rectangle is {}".format(rect_area))
    cuboid_volume = hypervolume(4,5,6)
    print("the volume of a cuboid is {}".format(cuboid_volume))
    hyper_cuboid_volume = hypervolume(4, 5, 6, 7)
    print("the volume of a hyper cuboid with sides of length 4, 5, 6, 7 is {}".format(hyper_cuboid_volume))
    print()
    print(hypervolume_improved(1, 2, 3, 4))    
    print(hypervolume_improved(1, 2, 3))
    print(hypervolume_improved(1, 2))
    print(hypervolume_improved(1))
    # this will cause an exception as the positional argument length is required.
    print(hypervolume_improved())
    
    

   
