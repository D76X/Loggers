"""
This module illustrates the template method pattern in Python. 
One of the most common applications of the template method pattern in Python 
is to simplify overriding property setters.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test040_template_method as t
    t.test_module()  
    
    >>> import test040_template_method as t
    t.test_module()
    Part 1 done!
    Part 2 done!
    Part 3 done!
"""

class AbstractClass:
    """
    An abstract class to illustrates the template method pattern. 
    """

    def template_method(self):
        """The template method."""
        # a template may not be defined in the abstract base
        # class but must be defined in any derived class
        self._part1()
        self._part2()
        self._part3()

        def _part2(self):
            raise NotImplementedError("Must override this method")

        def _part3(self):
            # Optionally override this
            pass


class ConcreteClass(AbstractClass):
    """
    A concrete implementation of the template's parts.
    """

    def _part1(self):
        print("Part 1 done!")

    def _part2(self):
        print("Part 2 done!")

    def _part3(self):
        print("Part 3 done!")


def test_module():
    """Module-level tests."""
    concrete = ConcreteClass()
    concrete.template_method()


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
