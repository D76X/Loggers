"""
This module illustrates the Machine Learning Algorithms

1-Batch Gradient Descent
2-Stochastic Gradient Descent

References:
    Difference between Batch Gradient Descent and Stochastic Gradient Descent
    https://towardsdatascience.com/difference-between-batch-gradient-descent-and-stochastic-gradient-descent-1187f1291aa1

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import t0002_GradientDescends as t2; t2.test_module()

    # The last two commands are specific to this module.
    import t0002_GradientDescends as t2
    t2.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t2)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

Results:    
"""

# after you install Anaconda you may use this
import numpy as np

def gradientDescent(X, y, theta, alpha, iterations):
    """
     Performs the gradient descent to leran the thetas.

     Args:
        
        X: a matrix of real values of size (p,m).
           m: is the number of data points in the training set.
           p: is the length of the coulumn vectors.
        It is the set of values of the independent variable (input) for the training 
        set.

        y: is a column vector thus a matrix of size (m,1) of real values.
           y[i] is the value of the dependent variable (output) that corresponds to 
           the input Xi in the training data.

        theta: a column vector that is a matrix of size [p,1] of real values.
               Each theta[i] is a coefficient of the linear hypothesis.
               The linear hypothesis is completely defined by theta.
               The given value of theta is the seed for the iterative process.

        iteration: the maximum number of iterations to allow the algorithm to 
                   to converge   
    """
    # m is the number of points in the training set.
    # m = y.size
    # for i in range(iterations):
    #     # y_hat is a column vector [m,1] [p,m]*
    #     y_hat = np.dot(X, theta)