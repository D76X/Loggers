"""
This module illustrates the extended argument syntax in Python.
In particualr the use of arbitrary keyed arguments **kargs.

Usage:
    In the REPL import the module and run the module level test function.

    >>> import test017_extended_formal_args as t
    t.test_module()
    <img src="monet.jpg" alt="Sunrise by Claude Monet" border="1">
"""

# *kargs where kargs is by convention but not necessary.
# *kargs are treated as a dictionary by the Python RT.
# remember that the order of the keys in the dictionary is not guaranteed.
# Rules
# 1- first come all regulat positinal arguments
# 2- then the *args non keyed positional args
# 3- then any keyed positional arguments
# 4- last the **kargs, the arbitrary keyed arguments
# my_funct(a, b, *args, keyed1, keyed2, **kargs) 
def tag(name, **kargs):
    """
    Produces an html tag with attributes.

    Args:
        name: the name of the html tag.
        **kargs: an arbitrary number of keyed arguments.
    """    
    attributes = ""
    for key, value in kargs.items():
        attributes += ' {k}="{v}"'.format(k=key, v=str(value))
    return "<{t}{a}>".format(t=name, a=attributes)


def test_module():
    """Module-level tests."""
    # create a <img> tag with three attributes
    print(tag("img", src="monet.jpg", alt="Sunrise by Claude Monet", border=1))
    
    

   
