#!/usr/bin/env python3
"""
This module illustrates how to design custom collection in Python.
This module also illustrates teh basic of unit testiong in Python.

Usage:

    # as this module also contains unit tests you may prefer to run it as
    # python "C:\GitHub\Loggers\PyApps\Scripts\Tests\PythonScriptsTest3\test063_implementing_collections_and_unittests.py"

    # as the unittest module causes the control to return to the console after the test are run
    # rather than remaining in the Pyrhon REPL.

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system(
        'cls'); import imp; import test063_implementing_collections_and_unittests as t63; t63.test_module()

    # The last two commands are specific to this module.
    import test063_implementing_collections_and_unittests as t63
    t63.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t63)

    # clear th REPL
    clear = lambda: os.system('cls')
    clear()

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

Results:

"""

# ------------------------------
# The Proptocols of Collections
# ------------------------------

# Container             => str, list, range, tuple, set, bytes, dict
# Sized                 => str, list, range, tuple, set, bytes, dict
# Iterable              => str, list, range, tuple, set, bytes, dict
# Sequence              => str, list, range, tuple, bytes
# Set                   => set
# Mutable Sequence      => list
# Mutable Set           => set
# Mutable Mapping       => dict

# -------------------
# Container Protocol
# -------------------

# Allows to test for item membership in a collection => in/not in

# -------------------
# Sized Protocol
# -------------------

# Allows to determine the number of elements in a collection with the built-in function len()

# -------------------
# Iterable Protocol
# -------------------

# Allows to obtain an iterator via the built-in function iter()
# and use the (for item in iterable: dosomething(item)) idiom

# -------------------
# Sequence Protocol
# -------------------

# Allows randome read-write access to collections such as
# item = seq[index]
# index = seq.index(item)
# num = seq.count(item)
# r = reversed(seq)

# -------------------
# Set Protocol
# -------------------

# Allows to perform set algebraic operations on collections such as
# subset
# proper subset
# superste / proper superset
# intersection
# union
# difference
# symmetric difference
# equal / not equal

import unittest


class SortedSet():
    """
    An implementation of a sorted set.
    """

    def methodThatThrowsValueError(self):
        raise ValueError("There was an error!")

    def __init__(self, items=None):
        self._items = sorted(set(items)) if items is not None else []

    # Container Protocol
    def __contains__(self, item):
        return item in self._items

    # Size Protocol
    def __len__(self):
        return len(self._items)

    # Iterable Protocol
    def __iter__(self):
        return iter(self._items)

    # The Sequence Protocol implies the following
    # 1-Container Protocol
    # 2-Sized Protocol
    # 3-Iterable Protocol
    # The Sequence Protocol provides
    # 1- indexed access to the items in the container i.e. item = seq[index]
    # 2- slicing i.e. items = seq[start]
    # 3- a reverse iterator r = reversed(seq)
    # 4- index = seq.index(item)
    # 5- itemCount = seq.count(item)
    # 6- support for concatenation with the infix operator + i.e. seq3 = seq1 + seq2
    # 7- implementation of __add__
    # 8- support for repetition with the infix operator * i.e. seq3 = seq1*2
    # 9- implmentation of __mul__ and __rmul__
    def __getitem__(self, index):
        return self._items[index]


class TestConstruction(unittest.TestCase):
    """
    Tests the constructor of the SortedSet class.
    """

    def setUp(self):
        """
        Test fixture that is run before each test.
        """
        # for example load files, prepare db conetctions, etc.
        print("setUp")
        pass

    def tearDown(self):
        """
        Test fixture that is run after each test.
        """
        # for example close files, close connections, etc.
        print("tearDown")
        pass

    def test_that_fails(self):
        """This test showcases a failing test."""
        raise Exception("this is a failing test!")

    def test_exception_is_thown_on_non_existing_method_call(self):
        """This showcases testing for exceptions and errors."""
        with self.assertRaises(ValueError):
            s = SortedSet()
            s.methodThatThrowsValueError()

    def test_with_assertion(self):
        """This is to showcase a simple assertion."""
        self.assertEqual(1, 1)

    def test_empty(self):
        s = SortedSet([])

    def test_from_sequence(self):
        s = SortedSet([7, 8, 3, 1])

    def test_with_duplicates(self):
        s = SortedSet([8, 8, 8])

    def test_from_iterable(self):

        def gen6789():
            yield 6
            yield 7
            yield 8
            yield 9

        g = gen6789()
        s = SortedSet(g)


class TestContainerProtocol(unittest.TestCase):
    """
    Tests that the SortedSet class implements teh Container Protocol
    via __contains__.
    """

    def setUp(self):
        self.s = SortedSet([6, 7, 3, 9])

    def test_true_6_is_contained(self):
        """Tests IN on true."""
        self.assertTrue(6 in self.s)

    def test_false_2_is_not_contained(self):
        """Tests IN an false."""
        self.assertFalse(2 in self.s)

    def test_true_5_is_not_contained(self):
        """Tests NOT IN on true."""
        self.assertTrue(5 not in self.s)

    def test_false_9_is_not_contained(self):
        """Tests NOT IN on false."""
        self.assertFalse(9 not in self.s)


class TestSizeProtocol(unittest.TestCase):
    """
    Tests that the SortedSet class properly implements the Size Protocol.
    """

    def test_empty(self):
        s = SortedSet()
        self.assertEqual(len(s), 0)

    def test_one(self):
        s = SortedSet([91])
        self.assertEqual(len(s), 1)

    def test_10(self):
        s = SortedSet([-i for i in range(10)])
        self.assertEqual(len(s), 10)

    def test_duplicates_are_only_counted_once(self):
        s = SortedSet([i for i in range(10)]+[1, 2, 3])
        self.assertEqual(len(s), 10)


class TestIterableProtocol(unittest.TestCase):
    """
    Tests that the SortedSet class properly implements the Iterable Protocol.
    """

    def setUp(self):
        self.s = SortedSet([7, 2, 1, 1, 9])

    def test_iter(self):
        """
        Tests that the __iter__ is properly implemented.
        Each next() call returns the corresponding item in the iterable.
        The last call to the iterator raises a StopIteration exeption.
        """
        iterator = iter(self.s)
        self.assertEqual(next(iterator), 1)
        self.assertEqual(next(iterator), 2)
        self.assertEqual(next(iterator), 7)
        self.assertEqual(next(iterator), 9)
        # this is a bit special as it uses a lambda
        # this is how generally code raising exceptions should be tested
        # there is also another style for this which uses the keyword "with"
        self.assertRaises(StopIteration, lambda: next(iterator))

    def test_for_loop(self):
        """
        Tests that the for loop idiom works as expected.
        """
        index = 0
        expected = [1, 2, 7, 9]
        for item in self.s:
            self.assertEqual(item, expected[index])
            index += 1
        for idx, item in enumerate(self.s):
            self.assertEqual(item, expected[idx])


class TestSequenceProtocol(unittest.TestCase):
    """
    Tests that the SortedSet class properly implements the Sequence Protocol.
    """

    def setUp(self):
        self.s = SortedSet([1, 4, 9, 13, 15])

    def test_index_zero(self):
        self.assertEqual(self.s[0], 1)

    def test_index_one(self):
        self.assertEqual(self.s[1], 4)

    def test_index_two(self):
        self.assertEqual(self.s[2], 9)

    def test_index_three(self):
        self.assertEqual(self.s[3], 13)

    def test_index_four(self):
        self.assertEqual(self.s[4], 15)

    # in this case the alternative to using a lamba to test for exception is used
    # in this style the "with" keyboard (context manager protocol) is used.  
    def test_index_one_beyond_the_end_raises_index_error(self):
        with self.assertRaises(IndexError):
            self.s[5]   

    def test_index_minus_one(self):
        self.assertEqual(self.s[-1], 15)

    def test_index_minus_two(self):
        self.assertEqual(self.s[-2], 13)

    def test_index_minus_three(self):
        self.assertEqual(self.s[-3], 9)

    def test_index_minus_four(self):
        self.assertEqual(self.s[-4], 4)

    def test_index_minus_four(self):
        self.assertEqual(self.s[-5], 1)

    def test_index_one_beyond_the_beginning_raises_index_error(self):
        with self.assertRaises(IndexError):
            self.s[-6]


def test_module():
    """Module-level tests."""

    # You can also run the tests like this from the console
    # python "C:\GitHub\Loggers\PyApps\Scripts\Tests\PythonScriptsTest3\test063_implementing_collections_and_unittests.py"

    # run all the unit tests defined in this module.
    unittest.main(__name__)

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
