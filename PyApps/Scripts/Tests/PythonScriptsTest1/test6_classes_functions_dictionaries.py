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

class Aircraft:
    """This class models an aircraft"""
    def __init__(self, registration, model, num_rows, num_seats_per_row):
        """
        Aircraft initializer
        
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


# class with initialization method.
# __init__ is an initializer not a costructor!
# __init__ configures an object that has already been constructed.
# In Pyhton it's the runtime that calls the constructor and then __init__.
# __init__ is normally used to enforce class invariance.
class Flight:
    """This class models a flight"""
    def __init__(self, number, aircraft: Aircraft):
        """
        Flight initializer
        
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

        if not isinstance(aircraft, Aircraft):
            raise TypeError("Invalid type parameter")

        self._number = number
        self._aircraft = aircraft

        # deconstrunt the tuple
        rows, seats = self._aircraft.seating_plan()

        # use a list comprehention to build a list of dictionaries
        # the first index = 0 is left to None and is unused
        # the are as many list indexes starting from 1 as rows in the aircraft 
        # for each row there is a dictionary with as many entries as the number of seats per row
        # the keys of each dictionary are the letter of the seats
        # the value for each key of each dictionary is initilized to None
        # {letter: None for letter in seats} is a dictionary comprehension
        self._seating = [None] + [ {letter: None for letter in seats} for _ in rows]

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


    # any function or class method non meant for public 
    # consumption is preceded by underscore by convention
    def _parse_seat(self, seat):
        """
        Parse a seat designator into a valid row and letter.

        Args:
            seat: A seat designator such as '12C' or '21F'.

        Returns:
            A tuple containing an integer and a string for row and seat.
        """
        rows_numbers, seat_letters = self._aircraft.seating_plan()

        letter = seat[-1]
        if letter not in seat_letters:
            raise ValueError("Invalid seat letter {}".format(letter))

        row_text = seat[:-1]
        try:
            row = int(row_text)
        except ValueError:
            raise ValueError("Invalid seat row {}".format(row_text))

        if row not in rows_numbers:
            raise ValueError("Invalid row number {}".format(row))

        return row, letter
    
   
    def allocate_seat(self, seat, passenger):
        """
        Allocate a seat to a passenger.
        
        Args:
            seat: A seat designator such as '12C' or '21F'.
            passenger: The passenger name.

        Raises:
            ValueError: If the seat is unavailable.
        """
        row, letter = self._parse_seat(seat)

        if self._seating[row][letter] is not None:
            raise ValueError("Seat {} already occupied".format(seat))
        
        self._seating[row][letter] = passenger
        
    def relocate_passenger(self, from_seat, to_seat):
        """
        Relocate passengers to a different seat.

        Args:
            from_seat: The existing seat designator for the 
                       passenger to be moved.

            to_seat: The new seat designator.

        Raises:
            ValueError if the from seat is not yet assigned.
            Value Error if the destibnation seat is already taken.
        """
        from_row, from_letter = self._parse_seat(from_seat)
        if self._seating[from_row][from_letter] is None:
            raise ValueError("No passenger to relocate in seat {}".format(from_seat))

        to_row, to_letter = self._parse_seat(to_seat)
        if self._seating[to_row][to_letter] is not None:
            raise ValueError("Seat {} is already occupied".format(to_seat))
       
        self._seating[to_row][to_letter] = self._seating[from_row][from_letter]
        self._seating[from_row][from_letter] = None

    
    def num_available_seats(self):
        """
        Counts the number of seats on the flight that have not yet been assigned to pasengers.

        Returns:
            The number of available seats on the flight.
        """  
        # the seating is a list of dictionaries with one dictionary 
        # for each row. The fiest element of teh list in None so that
        # the first meaningful index is 1 instead of 0. Each dictionay
        # as keys that are the letters from ABCDEFGHJK according to 
        # the number of seats per row of the airplane for the flight. 
        # we go through the list self._seating except the first item 
        # that is None and per each of the dictinaries we look at the 
        # values and sum 1 when the seat is not assigned that is the 
        # value is None. Then we sum over the rows.
        return sum(sum(1 for s in row.values() if s is None)
                   for row in self._seating
                   if row is not None)                 


# this is a module-level conveninece function to easily test the module.
def create_test_flight():
    f = Flight("AB1234", Aircraft("REG123","AibsusX",num_rows=22,num_seats_per_row=6))
    f.allocate_seat('12A','Davide Spano') 
    f.allocate_seat('15F','Cinzia Nava')
    f.allocate_seat('15E','Lorentz Spano')
    f.allocate_seat('10B','Mark Truss')
    f.allocate_seat('18C','Bob Thames')
    return f