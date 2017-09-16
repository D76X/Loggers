"""
this is the docstring for this module
"""

# pylint: disable=invalid-name

# exception handling
# TypeError
astring = "some string"

try:
    impossible = astring + 3
except TypeError:
    print("TypeError: can't do the impossible string+number!")

try:
    impossible = 1/0
except Exception:
    print("Unknown Exception")

try:
    impossible = 1/0
except ZeroDivisionError:
    print("ZeroDivisionError")
except Exception:
    print("Any Exception")

# get some more info
try:
    impossible = astring + 3
except TypeError as error:
    print(f"TypeError: {error}")

# pylint: enable=invalid-name
