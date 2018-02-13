"""
This module illustrates class properties in Python. 

Usage:
    
    In the REPL import the module and run the module level test function.

    import test038_class_properties as t
    t.test_module() 
    
"""

class Temperature:
    """
    A class that models a measure of temperature.
    """

     # class methods can...
    @classmethod
    def revaluate_boxed_volume(cls):
        self._boxed_volume = self._volume

    # the @property defines a getter 
    @property
    def sizex(self):
        return self._sizex
    
    # a special decorator is used to provide a property with a setter.
    # validation may be performed within a setter. 
    @sizex.setter
    def sizex(self, value):
        self._sizex = value
        self.revaluate_volume 

    pass

def test_module():
    """Module-level tests."""
    
