"""
This module illustrates static methods and class methods inheritance in
Python. This time the base class defines __init__ and exploits the poly
morphic behavior of overridden static methods.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test035_static_methods_inheritance as t
    t.test_module() 
    
    item.serial=I-1-ABC123-ATLANTA
    built_item.serial=I-2-BUILT-1-HGF668-NY
    bought_item.serial=I-3-BOUGHT-1-XZZ111-WSH
    
"""

# static methods in Python can be overridden on subclasses
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
    item = BaseItem("ABC123", "ATLANTA")
    print("item.serial={}".format(item.serial))
    built_item = BuiltItem("HGF668", "NY")
    print("built_item.serial={}".format(built_item.serial))
    bought_item = BoughtItem("XZZ111", "WSH")
    print("bought_item.serial={}".format(bought_item.serial))
