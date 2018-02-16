"""
This module illustrates class properties in Python. 

Usage:
    
    In the REPL import the module and run the module level test function.

    import test038_class_properties as t
    t.test_module() 
    
    temp 0 K = -273.15 C = -459.67 F
    temp 273.15 K = 0.0 C = 32.0 F
    temp 255.37 K = -17.78 C = 0.0 F
"""

class Temperature:
    """
    A class that models a measure of temperature.
    """  
    
    @staticmethod
    def celsius_to_kelvin(c):
        return c + 273.15 

    @staticmethod
    def kelvin_to_celsius(k):
        return k - 273.15

    @staticmethod
    def farhenheit_to_kelvin(f):
        return round((f - 32)*5/9 + 273.15, 2)

    @staticmethod
    def kelvin_to_fahrenheit(k):
        return round((k - 273.15)*9*0.20 + 32, 2)    

    @staticmethod
    def fahrenheit_to_celsius(f):
        return round((f-32)*5/9, 2)

    @staticmethod
    def celsius_to_fahrenheit(c):
        return round(c*9/5+32, 2)

    def __init__(self):
        self._kelvin = 0

    # the @property defines a getter 
    # use to convert internal data
    @property
    def kelvin(self):
        return round(self._kelvin,2)

    # a special decorator is used to provide a property with a setter.
    # validation may be performed within a setter. 
    @kelvin.setter
    def kelvin(self, value):
        if(value < 0):
            raise ValueError("temp in K cannot be negative.")            
        self._kelvin = value

    @property
    def celsius(self):
        return Temperature.kelvin_to_celsius(self._kelvin)

    @celsius.setter
    def celsius(self, value):
        if (value < -273.15):
            raise ValueError("temp in C cannot be lower than -273.15 C.") 
        self._kelvin = Temperature.celsius_to_kelvin(value) 

    @property
    def fahrenheit(self):
        #1/5=0.20
        return Temperature.kelvin_to_fahrenheit(self._kelvin)

    @fahrenheit.setter
    def fahrenheit(self, value):
        if(value < -459.67):
            raise ValueError("temp in F cannot be lower than -459.67 F.")
        #1/9=0.11
        self._kelvin = Temperature.farhenheit_to_kelvin(value)

    def print(self):
        return "temp {K} K = {C} C = {F} F".format(K=self.kelvin, C=self.celsius, F=self.fahrenheit)

def test_module():
    """Module-level tests."""
    t1 = Temperature()
    t1.kelvin = 0  
    print(t1.print())

    t2 = Temperature()
    t2.celsius = 0
    print(t2.print())
    
    t3 = Temperature()
    t3.fahrenheit = 0
    print(t3.print())