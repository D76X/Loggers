"""
    This modules defines some utilities functions for 2D geometry. 

    Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Modules"); clear = lambda: os.system('cls'); import imp; import ntt_geometry_2d as nttg2d; nttg2d.test_module()

    # The last two commands are specific to this module.
    import ntt_geometry_2d as nttg2d
    nttg2d.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(nttg2d)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Modules")
    os.getcwd()

""" 

import ntt_utils
from fractions import Fraction

def orientation(p, q, r):
    """
    Computes the oriantation discriminant for points on a 2D plane.

    Args:
        p: the (x,y) of the first point.
        q: the (x,y) of the second point.
        r: the (x,y) of the third point.
    
    Results:
        +1 if the sequence of vectors p=>q=>r procedes clockwise.
        -1 if the sequence of vectors p=>q=>r procedes counterclockwise.
         0 if the vectors p, q, r lies on the same straight line.
    
    Notes:
        
        This is the implementation of a simple algorithm that allows to 
        determine whether three point on a plane in 2D lie on the same 
        line. This is the case only when the discriminant amounts to 0.
        
        In this implementation the points coordinates are alos converted 
        to Fraction values to handle the arithmetic accurately as with 
        floating point the loss of binary precision causes the algorithm
        to produce wrong results when the coordinates do not have exact
        binary representatin as floats. By using Fraction this problem 
        is resoved entirely. 

        The determinant of co-linearity to work out is below.
        
        |1 px py|
        |1 qx qy|
        |1 rx ry|

    """

    # replace the vector with the same vectors but Fraction in place of floats
    # to avoid the possibility of loss of precision due to impossible binary 
    # representations. 
    p = (Fraction(p[0]), Fraction(p[1]))
    q = (Fraction(q[0]), Fraction(q[1]))
    r = (Fraction(r[0]), Fraction(r[1]))

    # calculate the discriminant for co-linearity
    d = (q[0]-p[0]) * (r[1] - p[1]) - (q[1]- p[1]) * (r[0] - p[0]) 

    # ntt_utils.sign extract the sign of a numeric value as this is not available 
    # in Python as built-in finction
    return ntt_utils.sign(d)

def test_module(): 
    """Module-level tests."""  

    print()
    print("ntt_utils tests...")
    
    print()
    print("test the fuction orientation")
    p = (0, 0)
    q = (1, 1)
    r = (2, 2)
    print("p={p}, q={q}, r={r} => orientation(p, q, r)={d}".format(p=p, q=q, r=r, d=orientation(p, q, r)))

    print()
    print("test the fuction orientation")
    print("1/3 and 2/3 have no exact float represenation!")
    print("However the orientation function fixes this by converting them to Fraction")
    print("and by carrying out the maths in Fractions instead of floats")
    p = (0, 0)
    q = (1/3, 1/3)
    r = (2/3, 2/3)
    print("p={p}, q={q}, r={r} => orientation(p, q, r)={d}".format(p=p, q=q, r=r, d=orientation(p, q, r)))

    print()
    print("test the fuction orientation")
    p = (0, 0)
    q = (1, 1)
    r = (2.00001, 2.00002)
    print("p={p}, q={q}, r={r} => orientation(p, q, r)={d}".format(p=p, q=q, r=r, d=orientation(p, q, r)))

    print()
    print("test the fuction orientation")
    p = (0, 0)
    q = (1, 1)
    r = (1.99999, 1.99999)
    print("p={p}, q={q}, r={r} => orientation(p, q, r)={d}".format(p=p, q=q, r=r, d=orientation(p, q, r)))

    print()
    print("test the fuction orientation")
    p = (0, 0)
    q = (1, 1)
    r = (2.1, 2.1)
    print("p={p}, q={q}, r={r} => orientation(p, q, r)={d}".format(p=p, q=q, r=r, d=orientation(p, q, r)))

    print()
    print("test the fuction orientation")
    p = (0, 0)
    q = (1, 1)
    r = (2.0000001, 2)
    print("p={p}, q={q}, r={r} => orientation(p, q, r)={d}".format(p=p, q=q, r=r, d=orientation(p, q, r)))

    print()
    print("test the fuction orientation")
    p = (0, 0)
    q = (1, 1)
    r = (2, 1.999999)
    print("p={p}, q={q}, r={r} => orientation(p, q, r)={d}".format(p=p, q=q, r=r, d=orientation(p, q, r)))
