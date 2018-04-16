#!/usr/bin/env python3
"""
    This modules defines some utilities functions.

    Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps"); clear = lambda: os.system(
        'cls'); import imp; import ntt_utils as nttutils; nttutils.test_module()

    # The last two commands are specific to this module.
    import ntt_utils as nttutils
    nttutils.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(nttutils)

    # clear th REPL
    clear = lambda: os.system('cls')
    clear()

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps")
    os.getcwd()

"""

from math import sqrt


def sign(x):
    """
    Test the sign of a numeric value.

    Args:
        x: a numeric value.

    Returns:
        +1 for positive values
        -1 for negative values
         0 for zero

    Notes:
        This function is a clever one-liner that exploits that fact that in Python
        the following identities hold.
        True  - True  = 0
        True  - False = 1
        False - True  = -1
        False - False = 0
    """
    return (x > 0) - (x < 0)


def isPrime(n):
    """
    Test whether the input is a prime number.
    (very crude implementation)
    """
    if n < 2:
        return False
    for i in range(2, int(sqrt(n)+1)):
        if n % i == 0:
            return False
    return True


def distinct(iterable):
    """
    Eliminates duplicates that may be present in 'iterable'.

    Args:
        iterable: the source series.

    Yields:
        an iterable with unique elements from 'iterable'.
    """
    seen = set()
    for item in iterable:
        if item in seen:
            continue
        yield item
        seen.add(item)


def count_words(text):
    """
    Counts the occurrences of words in a piece of text.

    Args:
        text: the text.

    Returns:
        a dictionary of string vs number.

    Notes:
        Case is ignored.
    """
    normalised_text = ''.join(c.lower() if c.isalpha() else ' ' for c in text)
    frequencies = {}
    for world in normalised_text.split():
        frequencies[world] = frequencies.get(world, 0) + 1
    return frequencies


def test_module():
    """Module-level tests."""

    print()
    print("ntt_utils tests...")

    print()
    print("test sign(x)")
    print("sign(1)={}".format(sign(1)))
    print("sign(1.6)={}".format(sign(1.06)))
    print("sign(-1)={}".format(sign(-1)))
    print("sign(-1.01)={}".format(sign(-1.01)))
    print("sign(0)={}".format(sign(0)))

    print()
    print("isPrime(n)"
    print("isPrime(1) = {}".forma(isPrime(1)))
    print("isPrime(2) = {}".forma(isPrime(2)))
    print("isPrime(3) = {}".forma(isPrime(3)))
    print("isPrime(4) = {}".forma(isPrime(4)))
    print("isPrime(5) = {}".forma(isPrime(5)))
    print("isPrime(6) = {}".forma(isPrime(6)))

    print()
    print("distinct(iterable))
    print("distinct([1, 1, 2, 2, 3, 3]) = {}".format(distinct([1, 1, 2, 2, 3, 3])))

    print()
    print("count_words(text)")
    text = "There is some text to test the count or words with - is the count of words right?"
    print("text = {}".format(text))
    print("{}".format(count_words(text)))

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
