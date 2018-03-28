"""
This module illustrates static methods and class methods inheritance in
Python. 

Usage:
    
    In the REPL import the module and run the module level test function.

    import test036_class_methods_inheritance as t
    t.test_module() 
    
"""

# static methods in Python can be overridden on subclasses.
# class methods defined ina base class are inherited by its subclasses.

class BaseItem:
    """
    A class that models an item.
    """

    _designator = "I"
    _item_tag = 1    

    @staticmethod
    def _item_designator():
        result = "{}-{}".format(BaseItem._designator, BaseItem._item_tag)
        BaseItem._item_tag += 1
        return result

    # a @classmethod takes a reference to the class object as its first 
    # argument 
    @classmethod
    def create_internal_item(cls):
        """
        Factory method.

        Args:
            cls: reference to teh class object.

        Returns: an instance of BaseItem
        """
        return cls(order="INTERNAL", store="X")

    # notice that d=self._item_designator uuse the reference self to the 
    # class instance so to exploit the polymorfic behavior of the static 
    # methods overridden in inherited classes 
    def __init__(self, order, store):
        self.serial = "{d}-{o}-{s}".format(
            d=self._item_designator(),
            o=order,
            s=store)

class BuiltItem(BaseItem):
    """
    A class that model an item that is built.
    """
    
    _designator = "BUILT"
    _tag = 1

    # static methods in Python can be overridden on subclasses
    @staticmethod
    def _item_designator():
        result = "{}-{}-{}".format(BaseItem._item_designator(), BuiltItem._designator, BuiltItem._tag)
        BuiltItem._tag += 1
        return result

class BoughtItem(BaseItem):
    """
    A class that model an item that is bought.
    """

    _designator = "BOUGHT"
    _tag = 1

    # static methods in Python can be overridden on subclasses
    @staticmethod
    def _item_designator():
        result = "{}-{}-{}".format(BaseItem._item_designator(), BoughtItem._designator, BoughtItem._tag)
        BoughtItem._tag += 1
        return result


def test_module():
    """Module-level tests."""
    # here we create 3 internal items suing the factory method defined in 
    # the base class BaseItem. However thanks to the polymorphic behavior 
    # of @classmethod the constructor of the invoking class is used rather
    # than that of the base class BaseItem. 
    item = BaseItem.create_internal_item()
    print("item.serial={}".format(item.serial))
    built_item = BuiltItem.create_internal_item()
    print("built_item.serial={}".format(built_item.serial))
    bought_item = BoughtItem.create_internal_item()
    print("bought_item.serial={}".format(bought_item.serial))


# ##########################################################################################

# 1-Run the file in Python as a script.

# if __name__ = '__main__' the files is executed as a script 
# to execute the file as a scrip in the cmd use : python filename.py

# 2-Import the module into the Python REPL.

# when  filename.py is imported into the Python REPL as in the following
# python
# import filename
# the __name__ variable is set to the name of the module that by default is filename

print("__name__ = {}".format(__name__))
if __name__ == '__main__':
    test_module()

# ##########################################################################################

