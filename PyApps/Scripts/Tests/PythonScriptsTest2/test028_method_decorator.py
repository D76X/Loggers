"""
This module illustrates function decorators in Python.
It also shows that decorators can be applied to class methods.

Usage:
    
    In the REPL import the module and run the module level test function.

    import test028_method_decorator as t
    t.test_module()    
    
""" 

class Trace:
    """
    A class that whose instances can be used as a decorator 
    to trace the calls to the decorated funtion.
    """
    def __init__(self):
        self.enabled = True
        self.count = 0

    def __call__(self, f):
        def wrap(*args, **kwargs):
            if self.enabled:
                self.count += 1
                print("calling {f} for the {c} time".format(f=f, c=self.count))
            return f(*args, **kwargs)

        return wrap

# a mudule leve instance of the Trace class
tracer_add_round_brackets = Trace()
tracer_remove_round_brackets = Trace()

class TextTool:
    """
    A class with a bunch of methods to do stuff on textual data.
    """

    def __init__(self, special_char):
        self.special_char = special_char

    @tracer_add_round_brackets
    def add_round_brackets(self, word):
        """
        Returs the input string in round brackets.
        """
        return "({})".format(word)

    @tracer_remove_round_brackets
    def remove_round_brackets(self, word):
        """
        Returs the input string from which the round brackets
        are removed when found.
        """
        word = word[1:] if word[0] == "(" else word
        word = word[0:-1] if word[-1] == ")" else word
        return word

def test_module():
    """Module-level tests."""

    input = "some text"
    print("input={}".format(input))
    text_tool = TextTool("*")
    output = text_tool.add_round_brackets(input)
    print("output={}".format(output))
    output = text_tool.remove_round_brackets(output)
    print("output={}".format(output))