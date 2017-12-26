"""
This script/module showcases Python functions.

Usage:
    Examne different aspects of functions in Python.
"""

class MinimalClass:
    """This is the most basic class definition."""
    pass


# an instance method must have self as its first param.
# the first argument is a reference to the instance of the class.
class ClassWithInstanceMethod:
    """This is a class with an instance method"""
    def someInstanceMethod(self):
        """This instance method does something"""
        return "something"


# class with initialization method.
# __init__ is an initializer not a costructor!
# __init__ configures an object that has already been constructed.
# In Pyhton it's the runtime that calls the constructor and then __init__.
# __init__ is normally used to enforce class invariance.
class Flight:

    def __init__(self, number):
        """
        Flight initializer
        
        Args:            
            number: the flight number i.e. 'NZ1234'.

        Returns: nothing
        """
        if not number[:2].isalpha():
            raise ValueError("No airline code in '{}'".format(number))

        if not number[:2].isupper():
            raise ValueError("Invalid airline code in '{}'".format(number))

        if not (number[2:].isdigit() and int(number[2:]) <= 9999):
            raise ValueError("Invalid route number in '{}'".format(number))            

        self._number = number

    def number(self):
        """
        The whole flight number.

        Returns: the whole flight number.
        """
        return self._number

    def airline(self):
        """
        The airline two uppercase letters code.

        Returns: the airline two uppercase letters code.
        """
        return self._number[:2]


    def route(self):
        """
        The four-digit route number.

        Returns: The four-digit route number.
        """
        return self._number[2:]
