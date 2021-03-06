"""
This module illustrates the use of some of the built-in base conversion functions in Python.

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
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test052_base_conversion as t52; t52.test_module()

    # The last two commands are specific to this module.
    import test052_base_conversion as t52
    t52.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t52)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

Results:

    0b101010=42
    bin(b)=0b101010

    0o52=42
    oct(0o52)=0o52

    0x2a=42
    hex(0x2a)=0x2a

    use slicing to remove the prefixes 0x, 0b, 0o
    hs[2:]=2a

"""

## bin()
## oct()
## hex()
## int(x, base) with base from 2 to 36

def test_module(): 
    """Module-level tests."""
    
    print()
    b = 0b101010
    bs = bin(b)
    print("0b101010={}".format(b))
    print("bin(b)={}".format(bs))
    
    print()
    o = 0o52
    os = oct(o)
    print("0o52={}".format(o))
    print("oct(0o52)={}".format(os))

    print()
    h = 0x2a
    hs = hex(h)
    print("0x2a={}".format(h))
    print("hex(0x2a)={}".format(hs))

    print()
    print("use slicing to remove the prefixes 0x, 0b, 0o")    
    print("hs[2:]={}".format(hs[2:]))


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