"""
This script/module showcases Python classes and functions.

Usage:
    Illustrates different aspects of classes and functions in Python.
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
    """This class models a flight"""
    def __init__(self, number, aircraft):
        """
        Flight initializer.

        Args:
            number: the flight number i.e. 'NZ1234'.
            aircraft: the aircraft of the flight.

        Returns: nothing
        """
        if not number[:2].isalpha():
            raise ValueError("No airline code in '{}'".format(number))

        if not number[:2].isupper():
            raise ValueError("Invalid airline code in '{}'".format(number))

        if not (number[2:].isdigit() and int(number[2:]) <= 9999):
            raise ValueError("Invalid route number in '{}'".format(number))

        self._number = number

        self._aircraft = aircraft

    # the law of demeter or principle of least knowledge
    # do not expose the whole aircraft.
    # make available only the bits of teh aircraft that are required.
    def aircraft_model(self):
        """
        The aircraft model.

        Returns: the aircraft model.
        """
        return self._aircraft.model()

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


class Aircraft:
    """This class models an aircraft"""
    def __init__(self, registration, model, num_rows, num_seats_per_row):
        """
        Aircraft initializer.
        
        Args:
            registration: the reg number of the aircraft.
            model: the model of the aircraft.
            num_rows: the number of rows of the aircraft.
            num_steats_per_row: the number of sets per row of the aircraft.

        Returns: nothing
        """
        self._registration = registration
        self._model = model
        self._number_rows = num_rows
        self._num_seats_per_row = num_seats_per_row

    def registration(self):
        """
        The reg number of the aircraft.

        Returns: the reg number of the aircraft.
        """
        return self._registration

    def model(self):
        """
        The model of the aircraft.

        Returns: the model of the aircraft.
        """
        return self._model


    def seating_plan(self):
        """
        The seating plan for the aircraft.

        Returns:
            a tuple modelling the seating plan as a range and a string of seat letters.
        """
        # in "ABCDEFGHJK" the char I is skipped on purpose to avoid mistakes with 1.
        return (range(1, self._number_rows+1), "ABCDEFGHJK"[:self._num_seats_per_row])
