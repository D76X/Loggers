# Machine Learning

## Resouces

1. [Probabilistic Systems Analysis and Applied Probability](https://ocw.mit.edu/courses/electrical-engineering-and-computer-science/6-041-probabilistic-systems-analysis-and-applied-probability-fall-2010/)  
MIT OpenCourseWare


2. [Introduction to Convolutional Neural Networks for Visual Recognition](https://www.youtube.com/watch?v=vT1JzLTH4G4)  
Stanford University School of Engineering

3. [Machine Learning - overview and applications](https://www.youtube.com/watch?v=yDLKJtOVx5c&list=PLD0F06AA0D2E8FFBA) 

***

## Probability Theory & Statistics 

### Combination of random variables

1. [Why is the sum of two random variables a convolution?](https://stats.stackexchange.com/questions/331973/why-is-the-sum-of-two-random-variables-a-convolution)  

Explains the difference that exists between any linear combination of indipendent RVs which leads to the convolution formula and the distinct concept of **mixed RVs** which leads to a linear function of expected values etc.

***

### The concept of Loss Function and ots optimization problems

1. [Is minimizing squared error equivalent to minimizing absolute error? Why squared error is more popular than the latter?](https://stats.stackexchange.com/questions/147001/is-minimizing-squared-error-equivalent-to-minimizing-absolute-error-why-squared)  
[The concept of loss function on Wikipedia]([https://en.wikipedia.org/wiki/Loss_function#Selecting_a_loss_function)  
[The robustfit function in Matlab](http://uk.mathworks.com/help/stats/robustfit.html?s_tid=gn_loc_drop)  

- Squared error penalizes large errors more than does absolute error and is more forgiving of small errors than absolute error is. **Minimizing square errors (MSE)** is definitely not the same as **minimizing absolute deviations (MAD) of errors**. **MSE** provides the mean response of y conditioned on x, while **MAD** provides the median response of y conditioned on x.

- However, MSE is differentiable, thus, allowing for gradient-based methods, much efficient than their non-differentiable counterpart. MAD is not differentiable at x=0.

- Another reason of why MSE may have had the wide acceptance it has is that it is based on the euclidean distance (in fact it is a solution of the projection problem on an euclidean banach space) which is extremely intuitive given our geometrical reality.

- As another answer has explained, minimizing squared error is not the same as minimizing absolute error. The reason minimizing squared error is preferred is because it prevents large errors better.Say your empolyer's payroll department accidentally pays each of a total of ten employees $50 less than required. That's an absolute error of $500. It's also an absolute error of $500 if the department pays just one employee $500 less. But it terms of squared error, it's 25000 versus 250000.

2. [Quadratic loss function implying conditional expectation](https://stats.stackexchange.com/questions/176313/quadratic-loss-function-implying-conditional-expectation)  
This post present an agebraic chain of identities that shows how to interpret the problem of minimization of the quadratic loss function. It explains why it is the case that choosing the estimation function as the expected value of the conditioned training set of data results in the minimum quadratic error. This is useful as in some other parts of the theory this is a detail over which many gloss over although it is mentioned. This chain of identity is purely an exercise in algebra and the formalism of probability theory hence it is not extremely enlithening is many respects exept for the fact that in addition to clarify the result it forces on the reader some familiarity with the notations and the meaning of the quantities and theorems involved.

3. [Minimizing the expected loss](https://stats.stackexchange.com/questions/50783/minimizing-the-expected-loss?rq=1)

4. [Explain how Pr(dx,dy) appears in the derivation of the expected prediction error](https://stats.stackexchange.com/questions/256669/explain-how-prdx-dy-appears-in-the-derivation-of-the-expected-prediction-error?rq=1)  

***



