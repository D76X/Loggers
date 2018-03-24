"""
This module illustrates the use of comprehensions in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test054_multi_input_comprehensions as t54; t54.test_module()

    # The last two commands are specific to this module.
    import test054_multi_input_comprehensions as t54
    t54.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t54)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

Results:

"""

from math import factorial

def test_module(): 
    """Module-level tests."""

    # Python defines teh comprhension syntax
    print()
    print("Python has special syntax called comprehension syntax to build some iterable objetcs in a declarative fashion.")
    print("The are a number of comprehension syntax construct that are used to build iterables of different types.")

    # list comprehensions   
    print()
    print("List comprehensions")
    print("the syntax for list comprehensions is below.")
    print("[expr(item) for item in iterable]")

    print()
    print("list comprehension is syntax that when interpreted builds a list.")
    print("list comprehension syntax allows to build lists in a declarative manner as opposed to procedural.")
    
    print()
    print("any iterable object can be used in comprehension expressions.")
    print("any Python object that implements __iterable__ is an itrable.")
    print("any Python object that implements __iterable__ and __next__ is an iterator.")
    print("__iter__ returns the iterator objetc.")

    print()
    words = "This is just some text we can use to test comprehesions!"
    list_of_words = words.split()    
    list_of_length_of_words_in_text = [len(word) for word in list_of_words]
    print("words.split() = {}".format(list_of_words))
    print("[len(word) for word in list_of_words] = {}".format(list_of_length_of_words_in_text)) 
    print("type(of any list comprehension) = {}".format(type(list_of_length_of_words_in_text)))

    print()
    digits_in_first_20_factorials = [len(str(factorial(x))) for x in range(20)]
    print("[len(str(factorial(x))) for x in range(20)] = {}".format(digits_in_first_20_factorials))

    # set comprehensions
    print()
    print("set comprehensions")
    print("the syntax for set comprehensions is below.")
    print("{expr(item) for item in iterable}")

    print()
    print("it is the same syntax as for list comprehensions but the square brakets are replaced by curly brakets.")


    print()
    set_of_the_digits_in_first_20_factorials = {len(str(factorial(x))) for x in range(20)}
    print("{{len(str(factorial(x))) for x in range(20)}} = {}".format(set_of_the_digits_in_first_20_factorials))
    print("type(of any set comprehension)={}".format(type(set_of_the_digits_in_first_20_factorials)))

    print()
    print("Warning")
    print("In Python the type <set> is an unordered container!")
    
