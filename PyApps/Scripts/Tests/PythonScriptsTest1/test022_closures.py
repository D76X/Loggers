"""
This module illustrates local functions in Python.
It also illustrates the LEGB rule.

Usage:
    In the REPL import the module and run the module level test function.

    import test021_local_functions_and_LEGB as t
    t.test_module()
    ['one', 'two', 'four']
    ['one', 'two', 'four']
    [<function sort_by_last_letter.<locals>.last_letter at 0x000001C42879DE18>, <function sort_by_last_letter.<locals>.last_letter at 0x000001C42879DF28>]
    global param local
     
"""   

last_letter_objects_store = []

def sort_by_last_letter(strings):
    """
    Sorts an iterable fo strings according to the sort order 
    of their last letter i.e. ["two", "one", "four"] into 
    ["one", "two", "four"].
    
    Args:
        strings: an interable of strings.

    Results: the sorted iterable.        
    """
    # this is a local function.
    # Python binds symbols to definitions at run time, hence every 
    # time sort_by_last_letter in invoked an the symbol last_letter 
    # is bound to a new instance of its implementation.
    def last_letter(s):
        return s[-1]
    last_letter_objects_store.append(last_letter)
    return sorted(strings, key=last_letter)

# The LEGB rule is an acronym to remember how the Python RT 
# attempts to bind objects to symbols when it interprets the
# source code. It astarts from the point in the code where 
# the symbol is defined and walks upwards from the local scope
# towards the global scope but stops at the first possible
# valid binding.
# Local scope, Enclosing scope, Global scope, Built-in scope
g = "global"
def outer(p="param"):
    """Illustrates the essemce of the LEGB rule."""
    l = "local"
    def inner():
        print(g, p, l)
    inner()

def test_module():
    """Module-level tests."""
    tosort = ["two", "one", "four"]
    # first invokation
    sorted = sort_by_last_letter(tosort)
    print(sorted)
    # second invokation
    sorted = sort_by_last_letter(tosort)
    print(sorted)
    # see the separate instances 
    print(last_letter_objects_store)
    # the LEGB rule
    outer()
    