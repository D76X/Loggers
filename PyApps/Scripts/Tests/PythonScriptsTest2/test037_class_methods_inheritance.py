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
        self._sizex = sizex
        self._sizey = sizey
        self._sizez = sizez
        self._volume = sizex*sizey*sizey  
        self._boxed_volume = self._volume    

    # the @property defines a getter 
    # a getter can be used to convert btween an internal representation
    # and an external representation.
    @property
    def sizex(self):
        return self._sizex
    
    @property
    def sizey(self):
        return self._sizey

    @property
    def sizez(self):
        return self._sizez

    @property
    def volume(self):
        return self._volume

    @property
    def boxed_volume(self):
        return self._boxed_volume
        
    # typically @classmethod is used to define factories.
    # factories are typically defined on the base class of a hierarchy.
    # factories use *args, **kwargs to forward extra arguments to the cls reference.
    @classmethod
    def create_box(cls, size, *args, **kwargs):
        return cls(sizex=size, sizey=size, sizez=size, *args, **kwargs)

    @classmethod
    def create_unit_box(cls, *args, **kwargs):
        return cls(sizex=1, sizey=1, sizez=1, *args, **kwargs)

    @classmethod
    def create_cylinder(cls, radius, height, *args, **kwargs):
        return cls(radius=radius, height=height, *args, **kwargs)

    @classmethod
    def create_unit_cylinder(cls, *args, **kwargs):
        return cls(radius=1, height=1, *args, **kwargs)
    
    def print(self):
        return "{cn} size x={x} y={y} z={z} volume={v} boxed_volume = {bv}".format(
            cn=type(self).__name__, x=self.sizex, y=self.sizey, z=self.sizez, v=self.volume, bv=self.boxed_volume)
                

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
        self._radius = radius
        self._height = height        
        s = super().__init__(2*radius, 2*radius, height)
        self._boxed_volume = math.pi*self._radius*self._radius*self._height
        return s

def test_module():
    """Module-level tests."""
    box1 = Box.create_box(size=2);
    box2 = Box(sizex=1,sizey=2,sizez=3)
    box3 = Box.create_unit_box();
    
    cylinder1 = Cylinder.create_cylinder(radius=3,height=2)
    cylinder2 = Cylinder(radius=6,height=4)
    cylinder3 = Cylinder.create_unit_cylinder()  

    print(box1.print())
    print(box2.print())
    print(box3.print())

    print(cylinder1.print())
    print(cylinder2.print())
    print(cylinder3.print())
