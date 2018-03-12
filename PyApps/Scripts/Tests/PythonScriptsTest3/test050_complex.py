"""
This module illustrates the use of complex numbers in Python.
Complex numbers are built-in Python and do not need to be imprted from a module.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test050_complex as t50; t50.test_module()

    # The last two commands are specific to this module.
    import test050_rationals_fractions as t50
    t50.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t50)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

Results:

    Python can construct complex number from literals
    2j = 2j
    3+4j = (3+4j)
    type(3+4j) = <class 'complex'>

    The Complex conctructor can also be used
    parenthesis arond the complex string are optionals that is 'a+jb'='(a+jb)'
    no spaces are allowed that is complex('a + jb') produces ValueError
    complex('2+3j') = (2+3j)
    complex('(2+3j)') = (2+3j)
    the components of a complex number are stored as floats!
    (3+5j)
    z1.real = 3.0
    z1.imag = 5.0
    conjugates!
    (3-5j)
    z1.conjugate() = (3-5j)

    cmath.sqrt(-1) = 1j
    cmath.pase(1+1j) = 0.7853981633974483
    the module of a complex number is obtained by using the standard function abs
    abs(1+1j) = 1.4142135623730951
    cmath.polar(z1) = (1.4142135623730951, 0.7853981633974483)
    can use tuple unpacking
    m, p = cmath.polar(z1) => m = 1.4142135623730951 , p = 0.7853981633974483
    z2 = cmath.rect(m, p) = (1.0000000000000002+1.0000000000000002j)

    calculate the impedance Z of a LRC circuit
    impedance([inductive(10), resistive(10), capacitive(5)]) = (10+5j)
    m, p = cmath.polar(Z) => m = 11.180339887498949 , p = 0.4636476090008061
    math.degrees(p) = 26.5650  
    
"""

# the functions of the math module cannot be used with complex numbers so 
# the c math module is provided which defines functions that takes complex 
# args and return complex values
import cmath
import math

# sample application
def inductive(ohms):
    """Returns 0+j*ohms=ZL"""
    return complex(0.0, ohms)

def capacitive(ohms):
    """Returns 0-j*ohms=ZC"""
    return complex(0.0, -ohms)

def resistive(ohms):
    """Returns ohms-j*0=ZC"""
    return complex(ohms)

def impedance(components):
    return sum(components)

def test_module(): 
    """Module-level tests."""
    
    print("some example of complex numbers")
    
    # Python can construct complex number from literals 
    print()
    print("Python can construct complex number from literals")
    print("2j = {}".format(2j))
    print("3+4j = {}".format(3+4j))
    print("type(3+4j) = {}".format(type(3+4j)))

    # The Complex conctructor can also be used
    print()
    print("The Complex conctructor can also be used")
    print("parenthesis arond the complex string are optionals that is 'a+jb'='(a+jb)'")
    print("no spaces are allowed that is complex('a + jb') produces ValueError")
    print("complex('2+3j') = {}".format(complex('2+3j')))
    print("complex('(2+3j)') = {}".format(complex('(2+3j)')))

    # the components of a complex number are stored as floats!
    print("the components of a complex number are stored as floats!")
    z1 = 3+5j
    print(z1)
    print("z1.real = {}".format(z1.real))
    print("z1.imag = {}".format(z1.imag))
    # conjugates!
    z2 = z1.conjugate()
    print("conjugates!")
    print(z2)    
    print("z1.conjugate() = {}".format(z2))

    # the functions of the math module cannot be used with complex numbers so 
    # the c math module is provided which defines functions that takes complex 
    # args and return complex values
    print()
    print("cmath.sqrt(-1) = {}".format(cmath.sqrt(-1)))
    z1 = 1+1j
    print("cmath.pase(1+1j) = {}".format(cmath.phase(z1)))
    # the module of a complex number is obtained by using the standard function abs
    print("the module of a complex number is obtained by using the standard function abs")
    print("abs(1+1j) = {}".format(abs(z1)))
    print("cmath.polar(z1) = {}".format(cmath.polar(z1)))
    # can use tuple unpacking
    print("can use tuple unpacking")
    m, p = cmath.polar(z1)
    print("m, p = cmath.polar(z1) => m = {m} , p = {p}".format(m=m, p=p))
    # the reverse operation of cmath.polar is cmath.rect
    # but there might be floating point rounding errors
    z2 = cmath.rect(m, p)
    print("z2 = cmath.rect(m, p) = {}".format(z2))

    # calculate the impedance Z of a LRC circuit
    print()
    print("calculate the impedance Z of a LRC circuit")
    Z = impedance([inductive(10), resistive(10), capacitive(5)])
    print("impedance([inductive(10), resistive(10), capacitive(5)]) = {}".format(Z))
    m, p = cmath.polar(Z)
    print("m, p = cmath.polar(Z) => m = {m} , p = {p}".format(m=m, p=p))
    print("math.degrees(p) = {}".format(math.degrees(p)))
    print("m = {}".format(m))
