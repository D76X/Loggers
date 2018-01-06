"""
This is module2 used to explore what a Python package is.

Usage: test.
"""

class SomeCrazyClass:
    """This class is cazy!"""
    def __init__(self, name, level):
        """
        SomeCrazyClass initializer.
        
        Args:
        name: the name of the crazy intsance.
        level: a fuzzy descriptor of crazyness.       
        """
        self._name = name
        self._levelOfCrazy = level

    def tell_me_how_crazy_you_are(self):
        print('I am crazy at level {}'.format(self._levelOfCrazy))

# this is a module-level convenience function to easily test the module.
def create_crazy_stuff():
    """Creates test crazies."""
    seven = SomeCrazyClass("Seven", 7)  
    noWay = SomeCrazyClass("No way", "all out")   

    return seven, noWay