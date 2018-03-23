"""
    This modules defines some utilities functions. 

    Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Modules"); clear = lambda: os.system('cls'); import imp; import ntt_utils as nttutils; nttutils.test_module()

    # The last two commands are specific to this module.
    import ntt_utils as nttutils
    nttutils.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(nttutils)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Modules")
    os.getcwd()

"""   

def sign(x):
    """
    Test the sign of a numeric value.

    Args:
        x: a numeric value.
    
    Returns:
        +1 for positive values
        -1 for negative values
         0 for zero
    
    Notes: 
        This function is a clever one-liner that exploits that fact that in Python 
        the following identities hold. 
        True  - True  = 0
        True  - False = 1
        False - True  = -1
        False - False = 0  
    """
    return (x > 0) - (x < 0)

def test_module(): 
    """Module-level tests."""
    print()
    print("ntt_utils tests...")
    print("sign(1)={}".format(sign(1)))
    print("sign(1.6)={}".format(sign(1.06)))
    print("sign(-1)={}".format(sign(-1)))
    print("sign(-1.01)={}".format(sign(-1.01)))
    print("sign(0)={}".format(sign(0)))
