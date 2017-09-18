"""
this is the docstring for this module
"""

# pylint: disable=invalid-name

# a simple function
def compute(n1, n2):
    return n1**n2-(n1+n2)

# read user input and convert string to float
# these are blocking calls
number1 = float(input("Enter a number:"))
number2 = float(input("Enter another number:"))

print(compute(number1, number2))

# pylint: enable=invalid-name
