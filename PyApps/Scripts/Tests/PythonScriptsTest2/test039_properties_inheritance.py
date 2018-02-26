"""
This module illustrates class properties inheritance in Python. 
It shows how to override property getters and setters.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test039_properties_inheritance as t
    t.test_module() 
    
    StateBase with state = some_state
    CumulativeState with state = 0 and 1 substates as [0]
    BoundState with state = -9 low = -10 high = 10
    BoundState with state = -10 low = -10 high = 10
    BoundState with state = 10 low = -10 high = 10
    BinaryState with state = 0 state1 = 0 state2 = 1
    BinaryState with state = up state1 = up state2 = down

    StateBase with state = has changed state
    CumulativeState with state = 6 and 4 substates as [0, 1, 2, 3]
    BoundState with state = 0 low = -10 high = 10
    BoundState with state = 10 low = -10 high = 10
    BoundState with state = -10 low = -10 high = 10
    BinaryState with state = 1 state1 = 0 state2 = 1
    BinaryState with state = down state1 = up state2 = down    
"""

class StateBase:
    """
    A class that stores some state in the form of a single value.
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
        self._state = []
        self._state.append(value) 
        self._acc = value      

    # to override a getter in a descendant it is enough to redefine 
    # it with the decorator @property or @Base.prop.getter as below
    
    #@StateBase.state.getter
    @property
    def state(self):
        return self._acc

    # to override a setter defined in the base class the name of the 
    # base class must be used as its accessor as below this is the 
    # case whne the getter is 
    @state.setter   
    def state(self, value):
        self._state.append(value)
        self._acc += value        

    def print(self):
        return "{cn} with state = {s} and {i} substates as {ss}".format(
            cn=type(self).__name__, 
            s=self.state,
            i=len(self._state),
            ss=self._state)

class BoundState(StateBase):
    """
    A class that models a bound state.
    """

    def __init__(self, state_low, state_high, value):
        self._state_low = state_low
        self._state_high = state_high  
        self._state = state_low
        # this is how the setter can be reused in the __init__
        self.state = value
    
    # it is also possible to override the setter alone
    @StateBase.state.setter
    def state(self, value):
        self._state = self._state_low if value <= self._state_low else self._state_high
        self._state = value if self._state_low <= value <= self._state_high else self._state

    def print(self):
        return "{cn} with state = {s} low = {l} high = {h}".format(
            cn=type(self).__name__, 
            s=self.state,
            l=self._state_low,
            h=self._state_high)

class BinaryState(StateBase):
    """
    A class that models a binary state.
    """
    
    def __init__(self, state_one, state_two):
        self._state_one = state_one
        self._state_two = state_two
        self.state = state_one  

    # it is also possible to override the setter alone
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
            s=self.state,
            one=self._state_one,
            two=self._state_two)

def test_module():
    """Module-level tests."""
    state_base = StateBase("some_state")    
    state_cumulative = CumulativeState(0)
    state_bound_1 = BoundState(-10,+10,-9)
    state_bound_2 = BoundState(-10,+10,-11)
    state_bound_3 = BoundState(-10,+10,99)
    state_binary_1 = BinaryState(0,1)
    state_binary_2 = BinaryState("up","down")

    print(state_base.print())
    print(state_cumulative.print())
    print(state_bound_1.print())
    print(state_bound_2.print())
    print(state_bound_3.print())
    print(state_binary_1.print())
    print(state_binary_2.print())

    state_base.state = "has changed state"
    state_cumulative.state = 1
    state_cumulative.state = 2
    state_cumulative.state = 3
    state_bound_1.state = 0
    state_bound_2.state = 100
    state_bound_3.state = -100
    state_binary_1.flip()
    state_binary_2.flip()

    print()
    print(state_base.print())
    print(state_cumulative.print())
    print(state_bound_1.print())
    print(state_bound_2.print())
    print(state_bound_3.print())
    print(state_binary_1.print())
    print(state_binary_2.print())
    

