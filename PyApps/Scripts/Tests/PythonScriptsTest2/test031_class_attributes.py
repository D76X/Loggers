"""
This module illustrates class attributes in Python.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test031_class_attributes as t
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

    def __init__(self, owner_code, contents):
        # these are instance level attributes.
        # instance attributes are not shared by instances of the class.
        self.owner_code = owner_code
        self.contents = contents
        # Classes in Python do not introduce a new scope hence the
        # following would not work according the LEGB rule.
        # self.serial_nume = base_serial_number 
        # The following is correct.
        self.serial_number = ShippingContainer.base_serial_number 
        ShippingContainer.base_serial_number += 1

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