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
from pprint import pprint as pp

# this module imports a function from custom module!
from ntt_utils import isPrime, distinct


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
    print("[len(word) for word in list_of_words] = {}".format(
        list_of_length_of_words_in_text))
    print("type(of any list comprehension) = {}".format(
        type(list_of_length_of_words_in_text)))

    print()
    digits_in_first_20_factorials = [len(str(factorial(x))) for x in range(20)]
    print("[len(str(factorial(x))) for x in range(20)] = {}".format(
        digits_in_first_20_factorials))

    # set comprehensions
    print()
    print("set comprehensions")
    print("the syntax for set comprehensions is below.")
    print("{expr(item) for item in iterable}")

    print()
    print("it is the same syntax as for list comprehensions but the square brakets are replaced by curly brakets.")

    print()
    set_of_the_digits_in_first_20_factorials = {
        len(str(factorial(x))) for x in range(20)}
    print("{{len(str(factorial(x))) for x in range(20)}} = {}".format(
        set_of_the_digits_in_first_20_factorials))
    print("type(of any set comprehension)={}".format(
        type(set_of_the_digits_in_first_20_factorials)))

    print()
    print("Warning")
    print("In Python the type <set> is an unordered container!")

    # dictionary comprehensions
    print()
    print("there are various ways to create a dictionary in Python")
    print("one of them is by dictionary  comprehensions syntax")
    print("but it is not the only way...")

    # by using the { } expression on a list of tuples
    print()
    print("by using the { } expression on a list of tuples")
    print("d = {(key, value) for (key, value) in iterable)}")
    d1 = {(str(value), value) for value in range(10)}
    pp(d1)

    # by using the dict contructor on a list of tuples
    print()
    print("from a zipped iterable of tuples and the dict contructor")

    values = [i for i in range(5)]
    keys = [str(i) for i in values]

    # Warning!!!!!
    # Contrary to Python 2 in Python3 the zip contructor returns a Generator that ia a lazily evaluated iterable
    # thus the first time the generator is run through by the invokation of the dict contructor it is consumed
    # and trying to iterate on it again will not produce any more tuple - this behaviors my catch you by surprise
    # if you do not know about it.

    # zipped is "generator"
    zipped = zip(keys, values)

    # this will print only the ID of the generator object!
    print(zipped)

    # the content of zipped cannot be printed beacuse it is not callable object!
    # print("zip(keys, values) = {}".format(zipped(keys, values))) #

    # the dict contructor will consume the generator zipped!
    d2 = dict(("@"+key, -value) for (key, value) in zipped)
    print("dict((""@""+key, -value) for (key, value) in zipped)")
    pp(d2)

    # in general you may convert a generator into an in-memory iterable although it is not advisable, of course.
    # if you try to just that on the consumed generator you get an empty iterable.
    print()
    print("cannot rewind a generator!")
    list_from_coinsumed_generator = list(zipped)
    print("list(zipped) = {}".format(list_from_coinsumed_generator))

    print()
    print("let's remake the generator")
    zipped = zip(keys, values)
    print("the pairwise dict contructor syntax can be used to build a dictionary straight from an iterable.")
    print("dict(zipped) = {}".format(dict(zipped)))

    print()
    print("the syntax for dictionary comprehensions depends on the iterable.")

    # by using comprehension syntax from a list
    print()
    print("by using comprehension syntax on a list where the key for the dictionary is built as an expression of the value")
    print("{expr(val):expr(val) for val in iterable}")
    d3 = {"&"+str(v+10)+"*": v+10 for v in range(10, 0, -1)}
    pp(d3)

    # by using comprehension syntax from a list of tuples
    print()
    print("by using comprehension syntax from a list of tuples you may use either which way you want")

    # remake the generator
    print()
    print("the keys of a dictinary must be immutable and hashable, strings and numbers can always be used as keys.")

    print()
    zipped = zip(keys, values)
    print("a dictionary with integer keys")
    d4 = {int(k): v-1 for k, v in zipped}
    print("{{int(k) :v-1 for k, v in zipped}} = {}".format(d4))

    print()
    zipped = zip(keys, values)
    print("a dictionary with keys of types string")
    d4 = {k: v-1 for k, v in zipped}
    print("{{int(k) :v-1 for k, v in zipped}} = {}".format(d4))

    # remake the generator
    print()
    zipped = zip(keys, values)
    # the same as above but with the () this time it does not matter
    d4 = {int(k): v-1 for (k, v) in zipped}
    print("{{int(k): v-1 for (k, v) in zipped}} = {}".format(d4))

    print()
    zipped = zip(keys, values)
    # the same as above but with the () this time it does not matter
    d4 = {k: v-1 for (k, v) in zipped}
    print("{{k: v-1 for (k, v) in zipped}} = {}".format(d4))

    # remake the generator
    zipped = zip(keys, values)
    d4 = {v+1111: v-1 for (k, v) in list(zipped)}
    print("{{v+1111: v-1 for (k, v) in list(zipped)}} = {}".format(d4))

    # inversion of dictionaries by means of the dictionary comprehensions
    print()
    print("Inversion of dictionaries by means of the dictionary comprehensions")
    print("this operation is easy in Python thanks to the dictionary comprehension syntax!")
    print("let's try to invert the following dictionary")
    print(d3)
    d5 = {value: key for key, value in d3.items()}
    print("{{value: key for key, value in d3.items()}} = {}".format(d5))
    print("notice that we must use d.items() on the dictionary")
    print("to iterate on its key-values")

    print()
    print("invert the same dictionary back this time iterating on it by the key")
    d6 = {d5[key]: key for key in d5}
    print("{{d5[key]: key for key in d5}} = {}".format(d6))

    # when a dictionary comprehension is used to build a dictionary if the key appears
    # multiple times in the comprehension expression only the last of the key-valure
    # # pairs is retained
    print()
    print("words={}".format(words))
    list_of_words = words.split()
    print("list_of_words = words.split() = {}".format(list_of_words))
    d7 = {w[0]: w for w in list_of_words}
    print("{{w[0]: w for w in list_of_words}} = {}".format(d7))
    print("notice that the key 't' is assigned the last value 'test' found for that key in the source iterable for the dictionary comprehension")

    # filtering with predicates - the predicate comes last in the comprehension syntax
    print()
    # a simple list comprehension - notice the syntax [ ]
    primes_in_first_100_int = [p for p in range(101) if isPrime(p)]
    print("[p for p in range(101) if isPrime(p)] = {}".format(
        primes_in_first_100_int))
    # also with dictionary comprehensions - notice the { : } syntax
    prime_square_divisors = {p*p: (1, p, p*p)
                             for p in range(101) if isPrime(p)}
    pp("{{p*p:(1, p, p*p) for p in range(101) if isPrime(p)}} = {}".format(prime_square_divisors))

    # iterable objects and iterator objects are two foundamental building blocks of the Python language
    # the iterable protocol dictates that we can pass an iterable object to the built-in iter() function to get an interator object
    # iterator_object = iter(iterable_object)
    # iterator objects support the iterator protocol that requires that the iterator object can be passed to the built-in next() function
    # to fetch the next value from the underlying collection
    # with an iterator object it is possible have fine-grained control on the iteration process better than with for loops or comprehentions.
    iterable_object = list_of_words
    iterator_object = iter(iterable_object)
    item = next(iterator_object)
    print("next = {}".format(item))
    item = next(iterator_object)
    print("next = {}".format(item))
    item = next(iterator_object)
    print("next = {}".format(item))
    print("etc..")
    print("when the iterator is exhausted the following call to next() throws a StopIteration exception")

    # Generators
    # In Python generators are used to describe iterable series using code and functions.
    # Generators are evaluated lazily.
    # Generators can model infinity sequences.
    # Generators can be used to make data pipelines.
    # Generators can be run only once that is if the generator yields a finite sequence once the last value of the sequence is reached
    # the generator cannot be rewound. If a new sequence of the same time is required then a new generator must be instantiated.
    # Generators are any functions that use the keywork "yield" at leas once.
    print()
    print("Generators!")
    print("Generators are evaluated lazily.")
    print("Generators can model infinity sequences.")
    print("Generators can be used to make data pipelines.")
    print("Generators can be run only once that is if the generator yields a finite sequence once the last value of the sequence is reached ")
    print("the generator cannot be rewound. If a new sequence of the same time is required then a new generator must be instantiated.")
    print("Generators are any functions that use the keywork yield at leas once.")

    def gen123():
        yield 1
        yield 2
        yield 3

    g = gen123()
    print()
    print("Generator objects are iterator objects that is the next() function can be used on them to fetch the next value of the sequence.")
    print("g = gen123() = {}".format(g))
    print("next(g) = {}".format(next(g)))
    print("next(g) = {}".format(next(g)))
    print("next(g) = {}".format(next(g)))
    print("The next call would cause the StopIteration exception...the nenerator g is exhausted.")

    print()
    # rewind the generator object by creating a new one
    print("rewind the generator object by creating a new one")
    print("as generators are iterator objects the for loop idiom can be used with them")
    print("notice that the for loop idiom takes care to hanlde the StoipIteration exception for you!")
    g = gen123()
    for v in g:
        print(v)

    # this is also valid
    print()
    for v in gen123():
        print(v)

    print()
    print("generators can be created as indipendednt objects and can be advanced indipendently!")
    g1 = gen123()
    g2 = gen123()
    for a, b in zip(g1, g2):
        print("a*b={}".format(a*b))

    # an example of a generator with some trace
    print()
    def gen246():
        print("About to yield 2")
        yield 2
        print("About to yield 4")
        yield 4
        print("About to yield 6")
        yield 6
        print("About to return")
    # there is an implicit return here!
    g = gen246()
    print("g = gen246() = {}".format(g))
    print("next(g) = {}".format(next(g)))
    print("next(g) = {}".format(next(g)))
    print("next(g) = {}".format(next(g)))

    print()
    print("Stateful generators.")
    print("Local variables may be used in a generator to retain some state.")

    # an example of a generator that emplyes some state.
    # in this case the counter is the state.
    # this is a generator as the definition uses yield.
    # any funtion with at least a yield statement is a generator function.
    # however generators may also have return explicit statements as below.
    def take(count, iterable):
        """
        Take count elements from the iterable.

        Args:
            count: the max number of items to retrieve.
            iterable: the source of the items.
        
        Yields:
            At most count items from iterable.
        """
        counter = 0
        for item in iterable:
            if counter == count:
                return
            counter += 1
            yield item

    items = [2, 4, 6, 8, 10]
    
    for item in take(3, items):
        print(item)

    print()
    items = [1, 1, 3, 3, 5, 9, 9]    
    print("items = {}".format(items))
    print("distinct(items) = ")
    for item in distinct(items):
        print(item)

    # Pipelines with generators!
    print()
    print("Pipelines with generators!")
    print("Generators can be composed as function to create sophiticated pipelines...")
    print("We can pipe distinct into take like in F# that is distinct >> take.")
    print("However in F# this is so much better!")
    for item in take(3, distinct(items)):
        print(item)

    # Laziness of generators is a great feature to model infinite sequences!
    print()
    print("Laziness of generators is a great feature to model infinite sequences!")
    


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
