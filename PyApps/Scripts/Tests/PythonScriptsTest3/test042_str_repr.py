"""
This module illustrates the use of __str__ and __repr__ in Python.

Usage:

    import test042_str_repr as t
    t.test_module()  

"""

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

class Point3D:
    """A class that models a point in a cartesian 3D space."""

    def __init__(self, x, y, z):
        self._x = x 
        self._y = y
        self._z = z

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

    def _get_z(self):
        return self._z

    @property
    def z(self):
        return self._get_z()

    def __repr__(self):
        return "{cn}(x={x}, y={y}, z={z})".format(
            cn=type(self).__name__,
            x=self.x,
            y=self.y,
            z=self.z)

    def __str__(self):
        return "({x}, {y}, {z})".format(x=self.x, y=self.y, z=self.z)

    # in this class the __format__ is also overridden to show what really happens
    # when the string.format is invoked on a class instance.
    def __format__(self, f):
        """
        Args:
            f: optinal formatting options 
                r: reflect with respect the origin
        """    
        if f == "r":
            return "[Formatted 3D point: {x}, {y}, {z}]".format(x=-self.x, y=-self.y, z=-self.z)
        else:
            return "[Formatted 3D point: {x}, {y}, {z}]".format(x=self.x, y=self.y, z=self.z)
               

def test_module():
    """Module-level tests."""
    p1 = Point2D(10,5)
    # invoke __repr__
    print(repr(p1))
    # invoke __str__
    print("the center of a circle is on {}".format(str(p1)))
    # __str__ is implicity called by the built-in print()
    print("__str__ is implicity called by the built-in print() function i.e. print(p1)={}".format(p1))
    print(p1)
    
    # when an object is part of a list, dict or any other built-in collections 
    # the __repr__ is used. In the lines below print() invokes __str__ on the
    # containers but __repr__ is invokes on the individula objects.
    print()
    list_of_points = [Point2D(i, 2*i) for i in range(3)]
    print(list_of_points)
    dict_of_points = {i: Point2D(i,-i) for i in range(3)}
    print(dict_of_points)

    # the same output is obtained when __repr__ is invoked on the container
    print()
    list_of_points = [Point2D(i, 2*i) for i in range(3)]
    print(repr(list_of_points))
    dict_of_points = {i: Point2D(i,-i) for i in range(3)}
    print(repr(dict_of_points))

    # Point3D overrides __format__
    print()
    p3d1 = Point3D(1,2,3)
    print(p3d1)
    print("p3d1 = {}".format(p3d1))

    # formatting instruction are passed by prefixing the instruction
    # with a colum i.e. "{:i}".format(value) where :i is the formatting
    # instraction applied to value by __format__
    print()
    print("reflected about the origin")
    print("-p3d1 = {:r}".format(p3d1))

    # __format__ defaults to invoking __str__ for its inputs but it 
    # is possible to override this default behavior using the formatting
    # instruction !r which instruct __format__ to use __repr__ instead
    print()
    print("(forced to using __repr__)")
    print("p3d1 = {!r}".format(p3d1))

    #likewise !s forces __format__ to ignore formatting instructions and 
    #invoke __str__
    print()
    print("p3d1 = {!s}".format(p3d1))
