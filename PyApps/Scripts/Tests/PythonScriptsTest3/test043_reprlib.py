"""
This module illustrates the use of __str__ and __repr__ in Python.
With the aid of the standard library reperlib which provides an alternative 
implementation of the built-in reper() function.

1-Limits the length of the string that can be returned by __reper__ 
  i.e. if list is a very long list then reper(list) is limited.

Usage:

    import test043_reprlib as t
    t.test_module()  
    
    [Point2D(x=0, y=0), Point2D(x=0, y=1), Point2D(x=0, y=2), Point2D(x=0, y=3), Point2D(x=0, y=4), Point2D(x=0, y=5), ...]


"""

import reprlib

class Point2D:
    """A class that models a point on a geometrical plane."""

    def __init__(self, x, y):
        self._x = x         
        self._y = y

    def _get_x(self):
        return self._x
                                
    @property
    def x(self):
        return self._get_x()

    def _get_y(self):
        return self._y

    @property
    def y(self):
        return self._get_y()

    # __repr__ stands for representation of the object
    # should produce exact and unambiguos representaion     
    # do not rely on the default implementatin which returns
    # only class name and object ID - you must override it
    # it is normally used for and by debugging tools
    # the output of repr(someclass) must debugger-friendly
    def __repr__(self):
        return "{cn}(x={x}, y={y})".format(
            cn=type(self).__name__,
            x=self.x,
            y=self.y)

    # __str__ should produce human-readable/-friendly output
    # one use case is integration into readable text i.e. docs
    # i.e. here we skip the class name
    # the default implementation of __str__ calls __repr__
    # normally you do not want that
    def __str__(self):
        return "({x}, {y})".format(x=self.x,y=self.y)

def test_module():
    """Module-level tests."""
    # this reults in a 1000*1000 matrix of points!
    matrix_of_points = [Point2D(x, y) for x in range(1000) for y in range(1000)]
    #print(repr(points)) would not be a good idea!
    print(reprlib.repr(matrix_of_points))

