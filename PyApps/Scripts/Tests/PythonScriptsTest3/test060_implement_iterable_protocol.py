#!/usr/bin/env python3
"""
This module illustrates how to implement the iterable protocol in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test060_implement_iterable_protocol as t60; t60.test_module()

    # The last two commands are specific to this module.
    import test060_implement_iterable_protocol as t60
    t54.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t60)

    # clear th REPL
    clear = lambda: os.system('cls')
    clear()

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

Results:

"""


class ExampleIterator:

    def __init__(self, data):
        self.index = 0
        self.data = data
    
    def __iter__(self):
        return self
    
    def __next__(self):
        if self.index >= len(self.data):
            raise StopIteration()

        result = self.data[self.index]
        self.index += 1
        return result

class ExampleIterable:

    def __init__(self, data):
        self.data = data

    def __iter__(self):
        return ExampleIterator(self.data)

# An iterable type may also be implemented by 
# providing an implementation of __getitem__()
# but only for any object that supports consequtive
# integer indexing starting at index 0. Furthermore,
# when the index gets out of range __getitem__() must
# throw an exception of type IndexError. My types
# in Pyhton already support this behavior.
class AlternateIterable:
    
    def __init__(self, data):
        self.data = data

    def __getitem__(self, idx):    
        return self.data[idx]
    

def test_module():
    """Module-level tests."""

    # The Iterable object

    # An interable is any object that implements __iter__().
    # __inter__() is invoked on an object when the built-in function iter() is called on it.
    # The for loop construct automatically invoke __inter__().
    # __inter__() is required to return an iterator.

    # The iterator
    # An Iterator is an object that fullfills teh iterator protocol.
    # Also the iterator must implement __inter__() but generally it will returns iteself.
    # Iterator must implement the __next__() function.
    # When the built-in next() function is called on an iterator the __next__() is invoked.
    # __next__() must either return the next value or raise StopIteration exception when the sequence is exhausted.

    iterator = ExampleIterator([1,2,3])
    
    print()
    print("interator1 = ExampleIterator([1,2,3])")
    print("next(iterator) = {}".format(next(iterator)))
    print("next(iterator) = {}".format(next(iterator)))
    print("next(iterator) = {}".format(next(iterator)))

    # since iterators are ALSO iterable they can be used in for loops!
    print()
    print("since iterators are ALSO iterable they can be used in for loops!")
    for item in iterator:
        print(item)

    print()
    iterator = ExampleIterator("iterator on a string!")
    print("iterator = ExampleIterator(""iterator on a string!"")")
    for item in iterator:
        print(item)

    print()
    print("example with an iterable")
    iterable = ExampleIterable("I am an iterable!")
    print("iterable = ExampleIterable(""I am an iterable!"")")
    for item in iterable:
        print(item)

    print()
    print("example with an iterable that supports consequtive interger indexing")  
    list = [i for i in  AlternateIterable([10, 9, 8])]
    print("list = [i for i in  AlternateIterable([10, 9, 8])] = {}".format(list))
    
# ##########################################################################################

# 1-Run the file in Python as a script.

# if __name__ = '__main__' the files is executed as a script 
# to execute the file as a scrip in the cmd use : python filename.py

# 2-Import the module into the Python REPL.

# when  filename.py is imported into the Python REPL as in the following
# python
# import filename
# the __name__ variable is set to the name of the module that by default is filename

print("__name__ = {}".format(__name__))
if __name__ == '__main__':
    test_module()
    
# ##########################################################################################
