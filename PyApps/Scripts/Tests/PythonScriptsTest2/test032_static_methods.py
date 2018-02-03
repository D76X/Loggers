"""
This module illustrates class attributes and static methods in Python.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test032_static_methods as t
    t.test_module()      
    
"""

# When working with classes in Python there are two types of attributes.
# 1- Instance attributes which are normally defined in the __init__
# 2- Class level attributes.
# Remeber that classes in Python do not introduce a new scope.
class ShippingContainer:
    """
    Class that models a shipping container.
    """
    # class attributes are in fact attributes of teh class object.
    base_serial_number = 1336

    # the @staticmethod decorator is used to deine static methods
    # on class objects. Notice that a static method does not take 
    # the standard "self" as its first argument as opposed to any
    # other instance methods. Static methods in Python have no 
    # knowledge of the class within which they are defined. They 
    # simply allow a group of function to be defined within a class
    # as the functions are conceptually related to teh class.
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


