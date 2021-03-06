#!/usr/bin/env python3
"""
This module illustrates the use of the map and reduce functions in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test059_map_filter_reduce_functions as t59; t59.test_module()

    # The last two commands are specific to this module.
    import test059_map_filter_reduce_functions as t59
    t54.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t59)

    # clear th REPL
    clear = lambda: os.system('cls')
    clear()

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

Results:

"""

from ntt_utils import count_words
from functools import reduce
import operator


def test_module():
    """Module-level tests."""

    print()
    print("map() takes an iterable and a function and return another iterable")
    print("whose elements are the elements of the original iterable to which the")
    print("give function has been applied.")

    print()
    # map returns a generator function!
    print("map returns a generator function!")
    unicode_points = map(ord, "The quick brown fox")
    print("map(ord, ""The quick brown fox"") = {}".format(unicode_points))

    print()
    codes = []
    for code in unicode_points:
        codes.append(code)

    print("codes = {}".format(codes))

    # map over multiple input sequences
    print()
    print("map can be used over multiple input sequences")
    print("for example a function that takes two params can be used with map and two input sequnces")
    print("ultimately the number of input sequences must match the number of argument of the function")

    print()
    print("if the input sequnces are not all of the same size then map terminates on the latest index of teh shortest of them.")

    def f(a, b, c):
        """
        Given three numbers it computes a*b+a*c+b*c
        """
        return a*b+a*c+b*c

    l1 = [x for x in range(3)]
    l2 = [x*x for x in range(4, 7, 1)]
    l3 = [1/x for x in range(7, 10, 1)]
    mapped = map(f, l1, l2, l3)
    results = list(mapped)

    print()
    print(l1)
    print(l2)
    print(l3)
    print("mapped = map(f, l1, l2, l3) = {}".format(results))

    # comprehension vs map
    print()
    print("comprehension vs map...")
    alist = [x*x for x in range(5)]
    aset = {x*2 for x in alist}
    adic = {x: y for x in alist for y in aset}
    print("alist = [x*x for x in range(5)] = {}".format(alist))
    print("aset = {{x*2 for x in alist}} = {}".format(aset))
    print("adic = {{x:y for x in alist for y in aset}} = {}".format(adic))

    print()
    print("the map function is another functional way to build a sequence")
    # the map function is another functional way to build a sequence
    alist = map(lambda x: x*x, range(5))
    aset = set(alist)
    print("alist = map(lambda x: x*x, range(5)) = {}".format(alist))
    print("aset = set(alist) = {}".format(aset))

    # filters
    print()
    print("filters are used reduce the number of items passed to teh caller from any iterable")
    print("filter() laways returns a generator hence is always lazily evaluated - only in Python 3!")
    print("a filter can only be applied to a single iterable")

    print()
    filtered = filter(lambda x: x % 2, [x*x-1 for x in range(10)])
    print(
        "filterd = filter(lambda x: x % 2, [x*x-1 for x in range(10)]) = {}".format(filtered))
    results = list(filtered)
    print("results = list(filtered) = {}".format(results))

    # filter(None...)
    print()
    print("filter(None...)")
    print("passing null as the frist argument to the filter function will remove all the elements that evaluate to False")
    results = list(
        filter(None, [1, 0, 1, 0, 1, False, True, [], [1, 2, 3], '',  'some string']))
    print("results = list(filter(None, [1, 0, 1, 0, 1, False, True, [], [1,2,3], '',  'some string'])) = {}".format(
        results))

    # reduce
    print()
    print("reduce() applies a function to the elements of a sequence reducing them to a single value")
    print("reduce() takes a function of two variables - one an accumulator and each item from the sequence in turn")
    print("the initial value for the accumulator can be either the value of the first item in the sequence")
    print("or a specified value - reduce() in Python is equivalent to fold in F# or aggregate() in LINQ, etc.")

    print()
    result = reduce(operator.add,  [x for x in range(10)])
    print(
        "result = reduce(operator.add,  [x for x in range(10)]) = {}".format(result))

    # reduce with the (optional) initial value for the accumulator
    print()
    print("reduce with the (optional) initial value for the accumulator")
    result = reduce(operator.add,  [x for x in range(10)], -1)
    print(
        "result = reduce(operator.add,  [x for x in range(10)], -1) = {}".format(result))
    result = reduce(operator.mul,  [x for x in range(1, 10, 1)], -1)
    print(
        "result = reduce(operator.mul,  [x for x in range(1,10,1)], -1) = {}".format(result))

    # IMPORATANT!
    # While the map function is a built-in functiuon the reduce() function must be imported
    # from the functools module!

    # map() => reduce()
    print()
    print("map() => reduce()")

    print()
    print('''
        IMPORATANT!
        While the map function is a built-in functiuon the reduce() function must be imported 
        from the functools module!
    ''')

    print()
    documents = [
        "Some text to be tested.",
        "Has the text been tested yet?",
        "Please, test the text if it's not been tested yet."
        "In the case the text has been tested please do not test it twice.",
        "We are still not able to retest the text."
    ]

    # map results is a generator that produces disctinaries of teh word count
    # according to the outpot of the function count_word from the ntt_utils module
    word_count_dictionaries = map(count_words, documents)
    print("word_count_dictionaries = map(count_words, documents) = {}".format(
        word_count_dictionaries))

    # in any map-reduce operation the reduce step is to consolidate multiple results
    # produced during teh map phase into a single output value. We need a function
    # for the reduce step that knows how to combine the output values of the map step.
    def combine_count_dictionaries(d1, d2):
        d = d1.copy()
        for word, count in d2.items():
            d[word] = d.get(word, 0) + count
        return d

    total_word_count = reduce(
        combine_count_dictionaries, word_count_dictionaries)

    print()
    print("total_word_count = reduce(combine_count_dictionaries, word_count_dictionaries)={}"
          .format(total_word_count))

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
