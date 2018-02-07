"""
This module illustrates static methods and class methods inheritance in
Python.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test034_static_methods_inheritance as t
    t.test_module()  
    
    item._item_designator()=I-1
    built_item._item_designator()=I-2-BUILT-1
    bought_item._item_designator()=I-3-BOUGHT-1
    
"""

# static methods in Python can be overridden on subclasses
class BaseItem:
    """
    A class that models an item.
    """

    _base_designator = "I"
    _item_tag = 1

    @staticmethod
    def _item_designator():
        result = "{}-{}".format(BaseItem._base_designator, BaseItem._item_tag)
        BaseItem._item_tag += 1
        return result

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
    # in all these thre cases we call the static method _item_designator on an instance
    # of a class and it is apparent from the result that there is polymorphic behavior. 
    item = BaseItem()
    print("item._item_designator()={}".format(item._item_designator()))
    built_item = BuiltItem()
    print("built_item._item_designator()={}".format(built_item._item_designator()))
    bought_item = BoughtItem()
    print("bought_item._item_designator()={}".format(bought_item._item_designator()))
