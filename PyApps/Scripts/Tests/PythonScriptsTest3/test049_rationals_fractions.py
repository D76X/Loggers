"""
This module illustrates the Fraction class and the fraction module in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test049_rationals_fractions as t49; t49.test_module()

    # The last two commands are specific to this module.
    import test049_rationals_fractions as t49
    t49.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t46)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()  
    
"""

import sys
import decimal
from decimal import Decimal
from fractions import Fraction

# some numbers cannot have exact binary representation as either float or decimal
# this is for example the case for the rational number 2/3.

def test_module(): 
    """Module-level tests."""

    print("some numbers cannot have exact binary representation as either float or decimal")
    print("this is for example the case for the rational number 2/3 = 0.666666...")
    
    print()
    print("these are both integer divisions")
    two_thirds_integer_division = 2 // 3
    two_thirds_decimal_division = Decimal('2') // Decimal('3')
    print("2 // 3 = {}".format(two_thirds_integer_division))
    print("Decimal('2') // Decimal('3') = {}".format(two_thirds_decimal_division))

    print()
    two_thirds_float = 2 / 3
    print("the float representation of 2 / 3 is truncated by the float type precision boundary")
    print("2 / 3 = {}".format(two_thirds_float))

    print()
    print("the decimal representation of 2 / 3 is truncated by the Decimal type precision boundary")
    two_thirds_decimal = Decimal('2') / Decimal('3')
    print("Decimal('2') / Decimal('3') = {}".format(two_thirds_decimal))

    print()
    print("rational numbers can be accurately described by means of the Fraction class")
    two_thirds_fraction = Fraction(2,3)
    print("Fraction(2,3) = {}".format(two_thirds_fraction))

    print()
    print("any integer can be represented as a fraction")
    print("Fraction(123456781234567812345678,1) = {}".format(Fraction(123456781234567812345678,1)))

    print()
    print("any fload or decimal that is exactly a rational can be used to construct a Fraction")
    print("Fraction(0.5) = {}".format(Fraction(0.5)))
    
    print()
    print("WARNING!")
    print("some rational number do not have an exact binary representation as a float i.e. 0.1")
    print("in these cases the Fraction contructor produces unexpected results")
    zero_dot_one = 0.1
    print("0.1 = {}".format(zero_dot_one))
    print("Fraction(0.1) = {}".format(Fraction(0.1)))

    print()
    print("Fraction(0.1) = 3602879701896397/36028797018963968")
    print("this is clearly not exactly 0.1")
    print("the problems lies in the precision of the floa 0.1 which is passed in Fraction contructor")

    print()
    print("Fraction supports interoperability with Decimal")
    print("Fraction(Decimal('0.1')) = {}".format(Fraction(Decimal('0.1'))))

    print()
    print("Fractions can be cosntructed from a string as in the Decimal constructor")
    print("Fraction('2/3') = {}".format(Fraction('2/3')))

    # Arithmetics with fractions
    print()
    two_thirds_fraction = Fraction('2/3')
    four_fifth_fraction = Fraction('4/5')
    
    # +
    result = two_thirds_fraction + four_fifth_fraction
    print("Fraction('2/3') + Fraction('4/5') = {} + {} = {}".
    format(two_thirds_fraction, four_fifth_fraction, result))

    # -
    print()
    result = two_thirds_fraction - four_fifth_fraction
    print("Fraction('2/3') - Fraction('4/5') = {} - {} = {}".
    format(two_thirds_fraction, four_fifth_fraction, result))

    # *
    print()
    result = two_thirds_fraction * four_fifth_fraction
    print("Fraction('2/3') * Fraction('4/5') = {} * {} = {}".
    format(two_thirds_fraction, four_fifth_fraction, result))

    # /
    print()
    result = two_thirds_fraction / four_fifth_fraction
    print("Fraction('2/3') / Fraction('4/5') = {} / {} = {}".
    format(two_thirds_fraction, four_fifth_fraction, result))

    # // (integer division)
    print()
    result = two_thirds_fraction // four_fifth_fraction
    print("Fraction('2/3') // Fraction('4/5') = {} // {} = {}".
    format(two_thirds_fraction, four_fifth_fraction, result))

    # %
    print()
    result = two_thirds_fraction % four_fifth_fraction
    print("Fraction('2/3') % Fraction('4/5') = {} % {} = {}".
    format(two_thirds_fraction, four_fifth_fraction, result))
