#!/usr/bin/env python3
"""
This module provides a model for a sensor.

Usage:

# Copy and paste all these commands in the terminal to see the outputs.
import os; os.chdir("C:\\GitHub\\Loggers\\PyApps"); clear = lambda: os.system(
        'cls'); import imp; import ntt_sensor as nttsensor; nttsensor.test_module()

# The last two commands are specific to this module.
    import ntt_sensor as nttsensor
    nttsensor.test_module()

# Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(nttsensor)

    # clear th REPL
    clear = lambda: os.system('cls')
    clear()

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps")
    os.getcwd()

"""


import datetime
import itertools
import random
import time


class Sensor:
    """
    Model of a sensor.

    An iterable that provides some random numbers.

    Notes:
        This class is an iterable because it implements __iter__().
        This class is also an iterator because it implements __next__().
        This class returns itself in the call __iter__() because it is an iterator.        
    """

    def __iter__(self):
        return self

    def __next__(self):
        # could read the CPU temp or some other system stats!
        return random.random()


def test_module():
    """Module-level tests."""

    print()

    sensor = Sensor()

    # this is the iter on callable synstax with a sentinel of None
    # it is a way of creating iterables ofr endless sequences as
    # the sentinel None will never be matched by the callable
    # datetime.datetime.now thus every invokation of next() on this
    # iterable provides a new timestamp
    timestamps = iter(datetime.datetime.now, None)

    # we drive the logic using a for loop and the intertools.islice so that we can limit
    # the number of iterations although the iterable object is an endless sequence i.e.
    # a generator or similar.

    # Notice that zip(iterable, iterable) in Python 3 always returns a generator hence it
    # is lazily evaluated.

    for stamp, value in itertools.islice(zip(timestamps, sensor), 10):
        print(stamp, value)
        time.sleep(0.1)


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
