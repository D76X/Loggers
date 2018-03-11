"""
This module illustrates the Decimal type class of the decimal built-in module in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test046_decimal as t46; t46.test_module()

    # The last two commands are specific to this module.
    import test046_decimal as t46
    t46.test_module()

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

Results:

    These are the default settings for the decimal module.
    Context(prec=28, rounding=ROUND_HALF_EVEN, Emin=-999999, Emax=999999, capitals=1, clamp=0, flags=[], traps=[InvalidOperation, DivisionByZero, Overflow])

    This is wrong when using floats!
    0.8-0.7=0.10000000000000009

    This is right with decimals!
    0.8-0.7=0.1

    This is wrong again despite using Decimal because we started with floats!
    0.8-0.7=0.8000000000000000444089209850062616169452667236328125-0.6999999999999999555910790149937383830547332763671875=0.1000000000000000888178419700

    the precision given in te costructor is maintained.
    3*2=6
    3.0*2=6.0
    3.00*2=6.00


    decimal also supports Infinity, -Infinity and NaN and they perform as expected in aritmethics.
    Decimal('Nan')=NaN
    Decimal('Infinity') = Infinity , Decimal('-Infinity') = -Infinity
    1+nan=NaN
    1+inf=Infinity
    -inf+1=-Infinity

    cannot do Decimal(1.0)+1.0 because 1.0 is a float.
    Unexpected error: <class 'decimal.FloatOperation'>

    with integers we have already seen this
    (-7) % 3 = 2
    -9 is the first divisor of 3 that is smaller of -7 hence reminder = -7 - (-9) = 2

    the same thing but this time with decimals gives -1 the remainder instead of 2 as in the case of the op with integers!
    Decimal(-7) % Decimal(3) = -1
    with the decimal -6 is considered as the largest divisor of decimal 3 that is smaller than decimal -7 hence reminder = -7 - (-6)
    this is because Decimal is designed to adhere to IEEE854 decimal floating point standard.
    this is a problem as it might lead to differences in the behavior of functions with the same code but different numeric types

    the function is_odd tests numbers with (n % 2 == 1)
    it works as expected with int and float but not with decimal
    is_odd(1)=True
    is_odd(-1)=True
    is_odd(2)=False
    is_odd(-2)=False
    is_odd(1.0)=True
    is_odd(-1.0)=True
    is_odd(2.0)=False
    is_odd(-2.0)=False

    with decimals is_odd gives the wrong answer on negatives!
    is_odd(Decimal('1'))=True
    is_odd(Decimal('-1'))=False
    is_odd(Decimal('2'))=False
    is_odd(Decimal('-2'))=False

    is_odd(Decimal('-1'))=False is wrong!
    This happens because Decimal(-1) % 2 = -1 instead of 1

    use is_odd_fixed instead of is_odd
    is_odd_fixed(1)=True
    is_odd_fixed(-1)=True
    is_odd_fixed(2)=False
    is_odd_fixed(-2)=False
    is_odd_fixed(1.0)=True
    is_odd_fixed(-1.0)=True
    is_odd_fixed(2.0)=False
    is_odd_fixed(-2.0)=False

    is_odd_fixed works well with decimals too!
    0
    >>> imp.reload(t46)
    <module 'test046_decimal' from 'C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3\\test046_decimal.py'>
    >>> t46_test_module()
    Traceback (most recent call last):
    File "<stdin>", line 1, in <module>
    NameError: name 't46_test_module' is not defined
    >>> t46.test_module()
    0
    >>> imp.reload(t46)
    <module 'test046_decimal' from 'C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3\\test046_decimal.py'>
    >>> t46_test_module()
    Traceback (most recent call last):
    File "<stdin>", line 1, in <module>
    NameError: name 't46_test_module' is not defined
    >>> t46.test_module()

    These are the default settings for the decimal module.
    Context(prec=28, rounding=ROUND_HALF_EVEN, Emin=-999999, Emax=999999, capitals=1, clamp=0, flags=[Inexact, FloatOperation, Rounded], traps=[InvalidOperation, DivisionByZero, FloatOperation, Overflow])

    cannot do Decimal(1.0)+1.0 because 1.0 is a float.
    Unexpected error: <class 'decimal.FloatOperation'>

    This is wrong when using floats!
    0.8-0.7=0.10000000000000009

    This is right with decimals!
    0.8-0.7=0.1

    This is wrong again despite using Decimal because we started with floats!
    0.8-0.7=0.8000000000000000444089209850062616169452667236328125-0.6999999999999999555910790149937383830547332763671875=0.1000000000000000888178419700

    the precision given in te costructor is maintained.
    3*2=6
    3.0*2=6.0
    3.00*2=6.00


    decimal also supports Infinity, -Infinity and NaN and they perform as expected in aritmethics.
    Decimal('Nan')=NaN
    Decimal('Infinity') = Infinity , Decimal('-Infinity') = -Infinity
    1+nan=NaN
    1+inf=Infinity
    -inf+1=-Infinity

    with integers we have already seen this
    (-7) % 3 = 2
    -9 is the first divisor of 3 that is smaller of -7 hence reminder = -7 - (-9) = 2

    the same thing but this time with decimals gives -1 the remainder instead of 2 as in the case of the op with integers!
    Decimal(-7) % Decimal(3) = -1
    with the decimal -6 is considered as the largest divisor of decimal 3 that is smaller than decimal -7 hence reminder = -7 - (-6)
    this is because Decimal is designed to adhere to IEEE854 decimal floating point standard.
    this is a problem as it might lead to differences in the behavior of functions with the same code but different numeric types

    the function is_odd tests numbers with (n % 2 == 1)
    it works as expected with int and float but not with decimal
    is_odd(1)=True
    is_odd(-1)=True
    is_odd(2)=False
    is_odd(-2)=False
    is_odd(1.0)=True
    is_odd(-1.0)=True
    is_odd(2.0)=False
    is_odd(-2.0)=False

    with decimals is_odd gives the wrong answer on negatives!
    is_odd(Decimal('1'))=True
    is_odd(Decimal('-1'))=False
    is_odd(Decimal('2'))=False
    is_odd(Decimal('-2'))=False

    is_odd(Decimal('-1'))=False is wrong!
    This happens because Decimal(-1) % 2 = -1 instead of 1

    use is_odd_fixed instead of is_odd
    is_odd_fixed(1)=True
    is_odd_fixed(-1)=True
    is_odd_fixed(2)=False
    is_odd_fixed(-2)=False
    is_odd_fixed(1.0)=True
    is_odd_fixed(-1.0)=True
    is_odd_fixed(2.0)=False
    is_odd_fixed(-2.0)=False

    is_odd_fixed works well with decimals too!
    is_odd_fixed(Decimal('1'))=True
    0
    >>> t46.test_module()

    These are the default settings for the decimal module.
    Context(prec=28, rounding=ROUND_HALF_EVEN, Emin=-999999, Emax=999999, capitals=1, clamp=0, flags=[Inexact, FloatOperation, Rounded], traps=[InvalidOperation, DivisionByZero, Overflow])

    cannot do Decimal(1.0)+1.0 because 1.0 is a float.
    Unexpected error: <class 'decimal.FloatOperation'>

    This is wrong when using floats!
    0.8-0.7=0.10000000000000009

    This is right with decimals!
    0.8-0.7=0.1

    This is wrong again despite using Decimal because we started with floats!
    0.8-0.7=0.8000000000000000444089209850062616169452667236328125-0.6999999999999999555910790149937383830547332763671875=0.1000000000000000888178419700

    the precision given in te costructor is maintained.
    3*2=6
    3.0*2=6.0
    3.00*2=6.00


    decimal also supports Infinity, -Infinity and NaN and they perform as expected in aritmethics.
    Decimal('Nan')=NaN
    Decimal('Infinity') = Infinity , Decimal('-Infinity') = -Infinity
    1+nan=NaN
    1+inf=Infinity
    -inf+1=-Infinity

    with integers we have already seen this
    (-7) % 3 = 2
    -9 is the first divisor of 3 that is smaller of -7 hence reminder = -7 - (-9) = 2

    the same thing but this time with decimals gives -1 the remainder instead of 2 as in the case of the op with integers!
    Decimal(-7) % Decimal(3) = -1
    with the decimal -6 is considered as the largest divisor of decimal 3 that is smaller than decimal -7 hence reminder = -7 - (-6)
    this is because Decimal is designed to adhere to IEEE854 decimal floating point standard.
    this is a problem as it might lead to differences in the behavior of functions with the same code but different numeric types

    the function is_odd tests numbers with (n % 2 == 1)
    it works as expected with int and float but not with decimal
    is_odd(1)=True
    is_odd(-1)=True
    is_odd(2)=False
    is_odd(-2)=False
    is_odd(1.0)=True
    is_odd(-1.0)=True
    is_odd(2.0)=False
    is_odd(-2.0)=False

    with decimals is_odd gives the wrong answer on negatives!
    is_odd(Decimal('1'))=True
    is_odd(Decimal('-1'))=False
    is_odd(Decimal('2'))=False
    is_odd(Decimal('-2'))=False

    is_odd(Decimal('-1'))=False is wrong!
    This happens because Decimal(-1) % 2 = -1 instead of 1

    use is_odd_fixed instead of is_odd
    is_odd_fixed(1)=True
    is_odd_fixed(-1)=True
    is_odd_fixed(2)=False
    is_odd_fixed(-2)=False
    is_odd_fixed(1.0)=True
    is_odd_fixed(-1.0)=True
    is_odd_fixed(2.0)=False
    is_odd_fixed(-2.0)=False

    is_odd_fixed works well with decimals too!
    is_odd_fixed(Decimal('1'))=True
    is_odd_fixed(Decimal('-1'))=True
    is_odd_fixed(Decimal('2'))=False
    is_odd_fixed(Decimal('-2'))=False

    preservationof the identity => x == (x // y) * y + x % y
    int and decimal behave differently also on this account for the same reason as above
    the operator // has semantic dependent on the types used as its terms!

    (-7) // 3 = -3
    Decimal('-7') // Decimal('3') = -2

    this behavior is justified by the preservation of the identities below
    -7 == (-7 // 3) * 3 + (-7) % 3 == -7
    Decimal('-7') == (Decimal('-7') // Decimal('3')) * Decimal('3') + Decimal('-7') % Decimal('3') == -7
    
"""

import sys
import decimal
from decimal import Decimal

# The Decimal type in the built-in module decimal is a fast correctly rounded type 
# for perfomring arithmetics in base 10. Crucially, it is still a floating point 
# type albeit with a base of 10 rather than 2. Moreover, with Decimal the precision
# is still finite as it is with the type float but in addition it is alos user
# configurable. The default is 28 digits of decimal precision.

def test_module(): 
    """Module-level tests."""
    
    # retrieve the user settings for decimal.
    print()
    print("These are the default settings for the decimal module.")
    print(decimal.getcontext())

    #-----------------------------------------------------------------------------------------
    # Change user settings on Decimal.Decimal

    # this line modify the user settings on the Decimal class to prevent Decimal instances 
    # from floats being accidentally created by the user code. Any attempt to do so will 
    # results in a decimal.FloatOperation exception.     
    decimal.getcontext().traps[decimal.FloatOperation]=True

    # From now on you cannot only pass strings to the Decimal constructor!

    # decimal can be useed in arithmetics with any integer 
    # however decimals cannot be used in arithmetics with float
    print()
    try:
        Decimal(1.0)+1.0
    except TypeError:
        print("cannot do Decimal(1.0)+1.0 because 1.0 is a float.")
        print("Unexpected error:", sys.exc_info()[0])   
    finally:
        decimal.getcontext().traps[decimal.FloatOperation]=False
    #-----------------------------------------------------------------------------------------

    # here we reset the decimal module to its default because changing the defaults of the 
    # decimal module is permanent that is a second execution of this module's test function
    # starts with the decimal module with the settings changed by the previous run.

    #-----------------------------------------------------------------------------------------

    # With floats 0.8-0.7 is not 0.1 because neither 0.7 nor 0.8 can be represented 
    # exactly as floating point numbers in binary format and the given precision.
    # To fix this problem and others it is best to use Decimal where applicable.
    print()
    print("This is wrong when using floats!")
    print("0.8-0.7={}".format(0.8-0.7))

    # notice that we have used "from decimal import Decimal" so that we can 
    # use the Decimal type in place of decimal.Decimal with the module name
    # as prefix.

    # Create a bunch of decimals
    # IMPORTANT - notice that we use a string as argument of Decimal!    
    zero_8 = Decimal('0.8')
    # from a string
    zero_7 = Decimal('0.7')
    print()
    print("This is right with decimals!")
    print("0.8-0.7={}".format(zero_8-zero_7))

    # Things go bad again when floats are passed to the Decimal constructor
    # because the binary of the floats 0.8 and 0.7 has already lost precision
    # by the time the corresponding decimal is constructed  
    zero_8_from_float = Decimal(0.8)
    # from a string
    zero_7_from_float = Decimal(0.7)
    print()
    print("This is wrong again despite using Decimal because we started with floats!")
    print("0.8-0.7={z8}-{z7}={r}".format(z8=zero_8_from_float, z7=zero_7_from_float, r=zero_8_from_float-zero_7_from_float))
    print()    

    # Preservetion of the precision.
    print("the precision given in te costructor is maintained.")
    three_0 = Decimal('3')
    three_1 = Decimal('3.0')
    three_2 = Decimal('3.00')

    print("{}*2={}".format(three_0, 2*three_0))
    print("{}*2={}".format(three_1, 2*three_1))
    print("{}*2={}".format(three_2, 2*three_2))

    # decimal like float supports Infinity, -Infinity and NaN
    print()    
    print()
    print("decimal also supports Infinity, -Infinity and NaN and they perform as expected in aritmethics.")
    nan = Decimal('Nan')
    print("Decimal('Nan')={}".format(nan))
    inf = Decimal('Infinity')
    minf = Decimal('-Infinity')
    print("Decimal('Infinity') = {} , Decimal('-Infinity') = {}".format(inf, minf))
    print("1+nan={}".format(1+nan))
    print("1+inf={}".format(1+inf))
    print("-inf+1={}".format(minf+1))         

    # oddities with decimals when compared to integers
    # some interesting things with integers
    # oddities with decimals
    print()
    print("with integers we have already seen this")
    print("(-7) % 3 = {}".format((-7) % 3))
    print("-9 is the first divisor of 3 that is smaller of -7 hence reminder = -7 - (-9) = 2")

    print()
    print("the same thing but this time with decimals gives -1 the remainder instead of 2 as in the case of the op with integers!")
    print("Decimal(-7) % Decimal(3) = {}".format(Decimal(-7) % Decimal(3)))
    print("with the decimal -6 is considered as the largest divisor of decimal 3 that is smaller than decimal -7 hence reminder = -7 - (-6)")
    print('this is because Decimal is designed to adhere to IEEE854 decimal floating point standard.')
    print('this is a problem as it might lead to differences in the behavior of functions with the same code but different numeric types')

    # exemple of a function that behaves differently for integer and floats with respect to decimals
    print()
    def is_odd(n):
        """Test whether a number is odd."""
        return n % 2 == 1
    print("the function is_odd tests numbers with (n % 2 == 1)")
    print("it works as expected with int and float but not with decimal")
    print("is_odd(1)={}".format(is_odd(1)))
    print("is_odd(-1)={}".format(is_odd(-1)))
    print("is_odd(2)={}".format(is_odd(2)))
    print("is_odd(-2)={}".format(is_odd(-2)))
    print("is_odd(1.0)={}".format(is_odd(1.0)))
    print("is_odd(-1.0)={}".format(is_odd(-1.0)))
    print("is_odd(2.0)={}".format(is_odd(2.0)))
    print("is_odd(-2.0)={}".format(is_odd(-2.0)))

    print()
    print("with decimals is_odd gives the wrong answer on negatives!")
    print("is_odd(Decimal('1'))={}".format(is_odd(Decimal('1'))))
    print("is_odd(Decimal('-1'))={}".format(is_odd(Decimal('-1'))))
    print("is_odd(Decimal('2'))={}".format(is_odd(Decimal('2'))))
    print("is_odd(Decimal('-2'))={}".format(is_odd(Decimal('-2'))))
    
    print()
    print("is_odd(Decimal('-1'))=False is wrong!")
    print("This happens because Decimal(-1) % 2 = {} instead of 1".format(Decimal(-1) % 2))

    # this can be fixed by rewriting is_odd to take into account of the way the module
    # operator % works on decimals which is different than the way it works with int and
    # floats
    def is_odd_fixed(n):
        """Tests whether a number is odd."""
        return n % 2 != 0

    print()
    print("use is_odd_fixed instead of is_odd")
    print("is_odd_fixed(1)={}".format(is_odd_fixed(1)))
    print("is_odd_fixed(-1)={}".format(is_odd_fixed(-1)))
    print("is_odd_fixed(2)={}".format(is_odd_fixed(2)))
    print("is_odd_fixed(-2)={}".format(is_odd_fixed(-2)))
    print("is_odd_fixed(1.0)={}".format(is_odd_fixed(1.0)))
    print("is_odd_fixed(-1.0)={}".format(is_odd_fixed(-1.0)))
    print("is_odd_fixed(2.0)={}".format(is_odd_fixed(2.0)))
    print("is_odd_fixed(-2.0)={}".format(is_odd_fixed(-2.0)))

    print()
    print("is_odd_fixed works well with decimals too!")
    print("is_odd_fixed(Decimal('1'))={}".format(is_odd_fixed(Decimal('1'))))
    print("is_odd_fixed(Decimal('-1'))={}".format(is_odd_fixed(Decimal('-1'))))
    print("is_odd_fixed(Decimal('2'))={}".format(is_odd_fixed(Decimal('2'))))
    print("is_odd_fixed(Decimal('-2'))={}".format(is_odd_fixed(Decimal('-2'))))

    # preservation of the identity
    # x == (x // y) * y + x % y
    # int and decimal behave differently also on this account
    # in particular the operator of integer division // behaves differently
    print()
    print("preservationof the identity => x == (x // y) * y + x % y")    
    print("int and decimal behave differently also on this account for the same reason as above")
    print("the operator // has semantic dependent on the types used as its terms!")
    print()
    print("(-7) // 3 = {}".format((-7) // 3))
    print("Decimal('-7') // Decimal('3') = {}".format(Decimal('-7') // Decimal('3')))
    print()
    print("this behavior is justified by the preservation of the identities below")
    print("-7 == (-7 // 3) * 3 + (-7) % 3 == {}".format((-7 // 3) * 3 + (-7) % 3))
    # with decimal
    print("Decimal('-7') == (Decimal('-7') // Decimal('3')) * Decimal('3') + Decimal('-7') % Decimal('3') == {}".
    format((Decimal('-7') // Decimal('3')) * Decimal('3') + Decimal('-7') % Decimal('3')))
