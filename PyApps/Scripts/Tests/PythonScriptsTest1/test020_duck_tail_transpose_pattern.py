"""
This module illustrates the extended call synstax in Python applied in
the pattern of transposition of matrixes via the zip-star idiom.

Usage:
    In the REPL import the module and run the module level test function.

    import test020_duck_tail_transpose_pattern as t
    t.test_module()
    (12, 13, 14)
    (13, 14, 15)
    (14, 15, 16)
    (15, 16, 17)
    (16, 17, 18)
    (17, 18, 19)
    (18, 19, 18)
    (17, 18, 17)
    (16, 17, 16)
    (15, 16, 15)
    (14, 15, 14)

    temperatures during the last three days
    [[12, 13, 14, 15, 16, 17, 18, 17, 16, 15, 14, 13, 12],
     [13, 14, 15, 16, 17, 18, 19, 18, 17, 16, 15, 14, 13],
     [14, 15, 16, 17, 18, 19, 18, 17, 16, 15, 14]]

    (12, 13, 14)
    (13, 14, 15)
    (14, 15, 16)
    (15, 16, 17)
    (16, 17, 18)
    (17, 18, 19)
    (18, 19, 18)
    (17, 18, 17)
    (16, 17, 16)
    (15, 16, 15)
    (14, 15, 14)

    (12, 13, 14)
    (13, 14, 15)
    (14, 15, 16)
    (15, 16, 17)
    (16, 17, 18)
    (17, 18, 19)
    (18, 19, 18)
    (17, 18, 17)
    (16, 17, 16)
    (15, 16, 15)
    (14, 15, 14)
    
    the transposed
    [(12, 13, 14),
     (13, 14, 15),
     (14, 15, 16),
     (15, 16, 17),
     (16, 17, 18),
     (17, 18, 19),
     (18, 19, 18),
     (17, 18, 17),
     (16, 17, 16),
     (15, 16, 15),
     (14, 15, 14)]  
"""   

temp_on_sun = [12, 13, 14, 15, 16, 17, 18, 17, 16, 15, 14, 13, 12]
temp_on_mon = [13, 14, 15, 16, 17, 18, 19, 18, 17, 16, 15, 14, 13]
# with list comprehensions
temp_on_tue = [t for t in range(14, 20)] + [t for t in range(18, 13, -1)]

last_three_days_temps = [temp_on_sun, temp_on_mon, temp_on_tue]

def test_module():
    """Module-level tests."""
    import pprint as pp
    # zip(*args) takes any number of equal length vectors and zip them
    # into a list of tuple
    temps = zip(temp_on_sun, temp_on_mon, temp_on_tue)
    for item in temps:
        print(item)

    print()
    print("temperatures during the last three days")
    pp.pprint(last_three_days_temps)

    # we can index into the list of lists structure
    d1 = last_three_days_temps[0]
    d2 = last_three_days_temps[1]
    d3 = last_three_days_temps[2]

    # then use zip to obtain an iterable structure  
    print()
    for item in zip(d1, d2, d3):
        print(item)

    # but this is ugly!
    # a better approach is to employ the EXTENDED CALL SYNTAX which 
    # allows to pass any iterable series as argument to a function.
    # This syntax uses the * prefix to the argument. 

    # The * used is the DEFINITION of a function will cause an arbitrary
    # number of argumnets passed as an arguments to a function to be 
    # packed into a tuple then is finally passed as argument to the 
    # fucntion at the calling site. The ** allows any number of keywored 
    # arguments to be packed into a dictionary than is then passed as 
    # argument to a function at the call site.
    
    # Coversely, the * at the call site of a function unpacks an iterable
    # series into a tuple that is then passed to the called function as 
    # its args.

    print()
    iterable = zip(*last_three_days_temps)
    for item in iterable:
        print(item)

    # we can easily transpose the list of lists using the ECS again.
    # last_three_days_temps can be thought of as a matrix in which 
    # the rows represent the temperatures on a particular day while 
    # the cols are the temperatures at a particular hour of the day. 
    # The transposed instead reverses this order by holding in the 
    # row the temps at a particular hour of the day and in the cols 
    # the temperatures of a single day. 
    print()
    print("the transposed")    
    # transposed = list(iterable) --> this won't work
    # this is known as the the zip-star idiom in Python.
    transposed = list(zip(*last_three_days_temps))
    pp.pprint(transposed)
