"""
This module illustrates class properties inheritance in Python. 
It shows hoe to override property getters and seeters.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test039_properties_inheritance as t
    t.test_module()    
    
"""

class StateBase:
    """
    A class that store some state in the form of a single value.
    """
    
    def __init__(self, value):
        self._state = value

    @property
    def state(self):
        return self._state

    @state.setter
    def state(self, value):
        self._state = value  

    def print(self):
        return "{cn} with state = {s}".format(cn=type(self).__name__, s=self._state)

class CumulativeState(StateBase):
    """
    A class that models cumulative state.
    """

    def __init__(self, value):
        self._state = [value]
        self._acc = value

    # to override a getter in a descendant it is
    # enough to redefine it
    @property
    def state(self):
        return self._acc

    # to override a setter defined in the base class
    # the name of the base class must be used as its
    # accessor
    @StateBase.state.setter
    def state(self, value):
        self._state = self._state.extend(value)
        self._acc += value

    def print(self):
        return "{cn} with state = {s} and {i} substates".format(cn=type(self).__name__, 
            s=self.state,
            i=len(self._state))

class BoundState(StateBase):
    """
    A class that models a bound state.
    """

    def __init__(self, state_low, state_high, value):
        self._state_low = state_low
        self._state_high = state_high         
        self.state(value)
    
    @StateBase.state.setter
    def state(self, value):
        self._state = state_low if value <= state_low else state_high
        self._state = value if state_low <= value <= state_high else self._state

    def print(self):
        return "{cn} with state = {s} low = {l} high = {h}".format(
            cn=type(self).__name__, 
            s=self._state,
            l=self._state_low,
            h=self._state_high)

class BinaryState(StateBase):
    """
    A class that models a binary state.
    """
    
    def __init__(self, state_one, state_two):
        self._state_one = state_one
        self._state_two = state_two
        self._state = state_one  

    @StateBase.state.setter
    def state(self, value):
        if value != self._state_one & value != self._state_two:
            ValueError("invalid state value")
        self._state = value

    def flip(self):
        self._state = self._state_two if self._state == self._state_one else self._state_one

    def print(self):
        return "{cn} with state = {s} state1 = {one} state2 = {two}".format(
            cn=type(self).__name__, 
            s=self._state,
            one=self._state_one,
            two=self._state_two)

def test_module():
    """Module-level tests."""