"""
This module illustrates the dot priduct in Python.

Refs

Elementary Matrix Operations In Python
https://www.ibm.com/developerworks/community/blogs/jfp/entry/Elementary_Matrix_Operations_In_Python?lang=en

https://docs.python.org/3/whatsnew/3.5.html#whatsnew-pep-465
https://stackoverflow.com/questions/34142485/difference-between-numpy-dot-and-python-3-5-matrix-multiplication
https://stackoverflow.com/questions/5919530/what-is-the-pythonic-way-to-calculate-dot-product
https://stackoverflow.com/questions/4093989/dot-product-in-python
http://www.pradeepadiga.me/blog/2017/04/18/finding-dot-product-in-python-without-using-numpy/

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Machine Learning\\MachineLearningScripts"); clear = lambda: os.system('cls'); import imp; import t0002_DotProduct as t2; t2.test_module()

    # The last two commands are specific to this module.
    import t0002.DotProduct as t2
    t2.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t2)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Machine Learning\\MachineLearningScripts")
    os.getcwd()

"""

from array import array

def test_module(): 
    """
    Module-level tests.
    """
    v1 = array([1,2,3])
    v2 = array([-1,0,3])
    v1_dot_v2 = v1 @ v1
    print("{v1} @ {v2} = {dp}".format(v1=v1, v2=v2, d=v1_dot_v2)) 