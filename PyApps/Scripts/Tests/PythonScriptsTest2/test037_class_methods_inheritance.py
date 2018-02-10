"""
This module illustrates static methods and class methods inheritance in
Python. 

Usage:
    
    In the REPL import the module and run the module level test function.

    import test037_class_methods_inheritance as t
    t.test_module() 
    
"""

import math

class ContainerBase:
    """
    A class that models a container
    """
    
    def __init__(self, sizex, sizey, sizez, *args, **kwargs):
        _sizex = sizex
        _sizey = sizey
        _sizez = sizez
        _volume = sizex*sizey*sizey  
        _boxed_volume = volume

    def revaluate_volume():
        self._volume = self._sizex*self._sizey*self._sizez
    
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
        
        
    # typically @classmethod is used to define factories.
    # factories are typically defined on the base class of a hierarchy.
    # factories use *args, **kwargs to forward extra arguments to the cls reference.
    @classmethod
    def create_box(cls, size, *args, **kwargs):
        return cls(size, size, size, *args, **kwargs)

    @classmethod
    def create_unit_box(cls, *args, **kwargs):
        return cls(1, 1, 1, *args, **kwargs)

    @classmethod
    def create_cylinder(cls, radius, height, *args, **kwargs):
        return cls(2*radius, 2*radius, height, *args, **kwargs)

    @classmethod
    def create_unit_cylinder(cls, *args, **kwargs):
        return cls(radius=0.5, height=1, *args, **kwargs)

class Box(ContainerBase):
    """
    A class that models a rectangular box.
    """

    # super is a reference to the base class.
    # in the __init__ override the super.__init__ must be called explicitly.
    # explicit is better than implicit.
    def __init__(self, sizex, sizey, sizez):
        return super().__init__(sizex, sizey, sizez)

class Cylinder(ContainerBase):
    """
    A class that models cylindrical container.
    """

    def __init__(self, radius, height):
        radius = radius
        height = height        
        s = super().__init__(2*radius, 2*radius, height)
        s.boxed_volume = math.pi*radius*radius*height
        return s

def test_module():
    """Module-level tests."""
    
