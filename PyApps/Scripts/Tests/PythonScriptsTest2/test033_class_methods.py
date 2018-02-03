"""
This module illustrates class attributes, static methods class methods in 
Python.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test033_class_methods as t
    t.test_module()      
    
"""

# @staticmethod

# @staticmethod decorator is used to defined static methods within a class
# definition. Static methods in Python do not strickly belong to a class 
# object. However, they are normally logically related to the class object.
# Moreover static methods cannot have access to either "self" that is any
# instance of the class nor to the class object. This means that within a
# staic method it is not possible to refer to either.

# @classmethod 

# @classmethod can be used to the same effect as @staticmethod but the are 
# some differences. The method decorated with @classmethod must be passed a 
# refrence to the class object and such reference can be accessed within it.

# Guidelines

# Use static methods with @staticmethod when

#-1 You do not need to access either the class object or instance.
#-2 The static method is an internal implementation related to the 
#   class and not part of the interface of the class.
#-3 It is likely that the method may be relocated in module scope.

# Use static methods with @classmethod when

#-1 Access to the class object is required within the method i.e. to 
#   its constructor.


class ShippingContainer:
    """
    Class that models a shipping container.
    """
    # class attributes are in fact attributes of teh class object.
    base_serial_number = 1336

    # one typical example where @classmethod is used is when factory
    # functions are defined on a class. Factory functions produce 
    # instances of the class with a particular state. These methods 
    # are clearly part of the interface of the class object.
    @classmethod
    def create_empty(cls, owner_code):
        """
        Creates an instance of the shipping container with a given
        owner code but no content - empty shipping container.

        Args:
            cls: reference to the class object.
            owner_code: the owner code of the shipping container. 

        Returns: an instance of the shipping container.
        """
        return cls(owner_code, contents=None)

    @classmethod
    def create_with_items(cls, owner_code, items):
        """
        Creates an instance of the shipping container with a given
        owner code and content set to a given list of items.

        Args:
            cls: reference to the class object.
            owner_code: the owner code of the shipping container.
            items: the list of items for the shipping container.

        Returns: an instance of the shipping container.
        """
        return cls(owner_code, contents=list(items))

    # the @staticmethod decorator is used to deine static methods
    # on class objects. Notice that a static method does not take 
    # the standard "self" as its first argument as opposed to any
    # other instance methods. Static methods in Python have no 
    # knowledge of the class within which they are defined. They 
    # simply allow a group of function to be defined within a class
    # as the functions are conceptually related to the class.
    @staticmethod
    def _get_next_serial():
        result = ShippingContainer.base_serial_number
        ShippingContainer.base_serial_number += 1
        return result

    def __init__(self, owner_code, contents):
        # these are instance level attributes.
        # instance attributes are not shared by instances of the class.
        self.owner_code = owner_code
        self.contents = contents
        # invoke a static method
        self.serial_number = ShippingContainer._get_next_serial()         

    def print(self):
        print(
            "container ser.n.={sn} owner code ={oc} contents={c}".
            format(sn=self.serial_number, oc=self.owner_code, c=self.contents))


def test_module():
    """Module-level tests."""
    container1 = ShippingContainer("ABC123", "cheese")
    container1.print()
    container2 = ShippingContainer("DEF333", "tomatoes")
    container2.print()
    container3 = ShippingContainer.create_empty("XYZ788")
    container3.print()
    container4 = ShippingContainer.create_with_items("KSS001", ["glass", "wood"])
    container4.print()

