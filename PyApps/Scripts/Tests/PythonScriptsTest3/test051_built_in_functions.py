"""
This module illustrates the use of some of the most useful built-in functions in Python.

Remember

0. Integer in Python have unlimited precision and canbe of arbitrary magnitude.
1. In general The math module is to work with floating point numbers and integer number only.
2. Use the Decimal class and its methods from the decimal module when you need more precision that it is available from floats.
4. complex number are a built-in type in Python
5. Use cmath instead of math to perform arithmetic on compex number  
6. Do not mix float types and Decimal type as there might be loss of precision.
7. Decimal is user configirable.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test051_built_in_functions as t51; t51.test_module()

    # The last two commands are specific to this module.
    import test051_bilt_in_functions as t51
    t51.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t51)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

Results:

    abs(1+j)=1.4142135623730951
    0
    >>> imp.reload(t51)
    <module 'test051_built_in_functions' from 'C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3\\test051_built_in_functions.py'>
    >>> t51.test_module()
    abs(1+j)=1.4142135623730951
    0
    >>> imp.reload(t51)
    0
    >>> imp.reload(t51)
    0
    >>> imp.reload(t51)
    <module 'test051_built_in_functions' from 'C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3\\test051_built_in_functions.py'>
    >>> t51.test_module()

    abs on complex numbers
    abs(1+j)=1.4142135623730951
    cmath.polar(z1) = (1.4142135623730951, 0.7853981633974483)
    m, p = cmath.polar(z1) => m = 1.4142135623730951 , p = 0.7853981633974483

    abs on any other number type
    abs(-10)=10
    abs(Decimal('-1') / Decimal('35'))=0.02857142857142857142857142857
    abs(Fraction(-10,3))=10/3

    round
    round(0.2812, 3)=0.281
    round(0.2812, 1)=0.3

    round to 1 figure from half biases to even
    0
    >>> t51.test_module()

    abs on complex numbers
    abs(1+j)=1.4142135623730951
    cmath.polar(z1) = (1.4142135623730951, 0.7853981633974483)
    m, p = cmath.polar(z1) => m = 1.4142135623730951 , p = 0.7853981633974483

    abs on any other number type
    abs(-10)=10
    abs(Decimal('-1') / Decimal('35'))=0.02857142857142857142857142857
    abs(Fraction(-10,3))=10/3

    round
    round(0.2812, 3)=0.281
    round(0.2812, 1)=0.3

    0
    >>> imp.reload(t51)
    <module 'test051_built_in_functions' from 'C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3\\test051_built_in_functions.py'>
    >>> t51.test_module()

    abs on complex numbers
    abs(1+j)=1.4142135623730951
    cmath.polar(z1) = (1.4142135623730951, 0.7853981633974483)
    m, p = cmath.polar(z1) => m = 1.4142135623730951 , p = 0.7853981633974483

    abs on any other number type
    abs(-10)=10
    abs(Decimal('-1') / Decimal('35'))=0.02857142857142857142857142857
    abs(Fraction(-10,3))=10/3

    round
    round(0.2812, 3)=0.281
    round(0.2812, 1)=0.3

    round to 1 figure from half biases to even
    round(1.5, 1)=1.5
    round(2.5, 1)=2.5

    0
    >>> imp.reload(t51)
    <module 'test051_built_in_functions' from 'C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3\\test051_built_in_functions.py'>
    >>> t51.test_module()

    abs on complex numbers
    abs(1+j)=1.4142135623730951
    cmath.polar(z1) = (1.4142135623730951, 0.7853981633974483)
    m, p = cmath.polar(z1) => m = 1.4142135623730951 , p = 0.7853981633974483

    abs on any other number type
    abs(-10)=10
    abs(Decimal('-1') / Decimal('35'))=0.02857142857142857142857142857
    abs(Fraction(-10,3))=10/3

    round
    round(0.2812, 3)=0.281
    round(0.2812, 1)=0.3

    round to 1 figure from half biases to even
    round(1.5, 1)=1.5
    round(2.5, 1)=2.5

    round(Decimal('3.251'), 1)=3.3

    round(Fraction(57, 100), 2)=57/100
    round(Fraction(57, 100), 1)=3/5
    round(Fraction(57, 100), 0)=1
    mind round on floats!
    round on floats does' always behave correctly
    f = 2.675 => round(f,2)=2.67
    it should have been 2.8 but it is 2.67 instead
    this happens due to teh fact that the literal decimal 2.675 cannot have an exact binary representation as a float
    the only way to fix this kind of problems is to use Decimal
    round(Decimal('2.675'), 2)=2.68

"""
import math
import cmath
from fractions import Fraction
from decimal import Decimal

def test_module(): 
    """Module-level tests."""
    
    # abs on complex numbers
    print()
    print("abs on complex numbers")
    z1=1+1j
    print("abs(1+j)={}".format(abs(z1)))
    print("cmath.polar(z1) = {}".format(cmath.polar(z1)))
    m, p = cmath.polar(z1)
    print("m, p = cmath.polar(z1) => m = {m} , p = {p}".format(m=m, p=p))
    
    # abs on any other number type
    print()
    print("abs on any other number type")
    print("abs(-10)={}".format(abs(-10)))
    print("abs(Decimal('-1') / Decimal('35'))={}".format(abs(Decimal('-1') / Decimal('35'))))
    print("abs(Fraction(-10,3))={}".format(abs(Fraction(-10,3))))

    # round
    print()
    print("round")
    print("round(0.2812, 3)={}".format(round(0.2812, 3)))
    print("round(0.2812, 1)={}".format(round(0.2812, 1)))
    # round to 1 figure from half biases to even
    print()
    print("round to 1 figure from half biases to even")
    print("round(1.5, 1)={}".format(round(1.5, 1)))
    print("round(2.5, 1)={}".format(round(2.5, 1)))
    print()
    print("round(Decimal('3.251'), 1)={}".format(round(Decimal('3.251'), 1)))
    print()
    # print("round on Fractions!")
    print("round(Fraction(57, 100), 2)={}".format(round(Fraction(57, 100), 2)))
    print("round(Fraction(57, 100), 1)={}".format(round(Fraction(57, 100), 1)))
    print("round(Fraction(57, 100), 0)={}".format(round(Fraction(57, 100), 0)))

    # mind round on floats!
    # floats are binary representation of real numbers while the other numeric 
    # types in Python are decimal representations.
    # round on floats does' always behave correctly
    print("mind round on floats!")
    print("round on floats does' always behave correctly")
    f = 2.675
    fr=round(f,2)
    print("f = 2.675 => round(f,2)={}".format(fr))
    print("it should have been 2.8 but it is {} instead".format(fr))
    print("this happens due to teh fact that the literal decimal 2.675 cannot have an exact binary representation as a float")
    print("the only way to fix this kind of problems is to use Decimal")
    print("round(Decimal('2.675'), 2)={}".format(round(Decimal('2.675'), 2)))