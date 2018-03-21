"""
This module illustrates the dot priduct in Python.

Refs

https://docs.scipy.org/doc/numpy-1.14.0/reference/generated/numpy.dot.html

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Machine Learning\\MachineLearningScripts"); clear = lambda: os.system('cls'); import imp; import t0001_DotProduct as t1; t1.test_module()

    # The last two commands are specific to this module.
    import t0001.DotProduct as t2
    t1.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t1)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Machine Learning\\MachineLearningScripts")
    os.getcwd()

"""

# get numpy via Anaconda or PIP.
import numpy as np

def test_module(): 
    """
    Module-level tests.
    """
    v1 = [1,2,3]
    v2 = [-1,0,3]
    v1_dot_v2 = np.dot(v1, v2)
    print("np.dot({v1}, {v2}) = {dp}".format(v1=v1, v2=v2, dp=v1_dot_v2))

    print()
