# Machine Learning & AI             

## Resouces

1. [Probabilistic Systems Analysis and Applied Probability](https://ocw.mit.edu/courses/electrical-engineering-and-computer-science/6-041-probabilistic-systems-analysis-and-applied-probability-fall-2010/)  
MIT OpenCourseWare

2. [Lecture 1 | Machine Learning (Stanford)](https://www.youtube.com/watch?v=UzxYlbK2c7E&t=3009s)  
Professor Andrew Ng  
[Lecture Notes](http://cs229.stanford.edu/notes/cs229-notes1.pdf)

3. [Machine Learning - overview and applications](https://www.youtube.com/watch?v=yDLKJtOVx5c&list=PLD0F06AA0D2E8FFBA) 

4. [Introduction to Convolutional Neural Networks for Visual Recognition](https://www.youtube.com/watch?v=vT1JzLTH4G4)  
Stanford University School of Engineering

5. [Machine Learning Gorup at Microsoft Research](https://www.microsoft.com/en-us/research/group/machine-learning-research-group/)  

6. [Machine Learning Carnegie Mellon University](http://www.cs.cmu.edu/~ninamf/courses/601sp15/lectures.shtml)

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
[Expected Error Prediction Derivation](https://stats.stackexchange.com/questions/189560/expected-error-prediction-derivation)  

***

## Other kinds of learning algorithms

### Unsupervised learning algorithms

[(ML 1.3) What is unsupervised learning?](https://www.youtube.com/watch?v=lANt56UOaSk&index=3&list=PLD0F06AA0D2E8FFBA)
- Clustering
- Dessity estimation
- Dimensionality reduction

### Semi-supervised learning algorithms & Psudo-labeling

[(ML 1.4) Variations on supervised and unsupervised](https://www.youtube.com/watch?v=pytUuJPOnVI&list=PLD0F06AA0D2E8FFBA&index=4)  
[Semi-supervised Learning explained](https://www.youtube.com/watch?v=b-yhKUINb7o)  

### Active learning algorithms

[(ML 1.4) Variations on supervised and unsupervised](https://www.youtube.com/watch?v=pytUuJPOnVI&list=PLD0F06AA0D2E8FFBA&index=4)  
[Active Learning and Annotations - Microsoft Research](https://www.youtube.com/watch?v=FE1r7_SQq6Y)

### Decision theory based on loss functions

[(ML 1.4) Variations on supervised and unsupervised](https://www.youtube.com/watch?v=pytUuJPOnVI&list=PLD0F06AA0D2E8FFBA&index=4)  

### Reinforcement learning (sequence of decisions)

[(ML 1.4) Variations on supervised and unsupervised](https://www.youtube.com/watch?v=pytUuJPOnVI&list=PLD0F06AA0D2E8FFBA&index=4)  
maximise the effects of a sequence of decisions 
[Lecture 1 | Machine Learning (Stanford)](https://www.youtube.com/watch?v=UzxYlbK2c7E&t=3009s)  
Professor Andrew Ng  
example of good dog/bad dog  

***
## Approaches to machine learning

### Generative vs Degenerative 

[(ML 1.5) Generative vs discriminative models](https://www.youtube.com/watch?v=oTtow2Ui8vg&list=PLD0F06AA0D2E8FFBA&index=5)  
[What is the difference between a Generative and Discriminative Algorithm?](https://stackoverflow.com/questions/879432/what-is-the-difference-between-a-generative-and-discriminative-algorithm)  
[On Discriminative vs. Generative
classifiers: A comparison of logistic
regression and naive Bayes](http://papers.nips.cc/paper/2020-on-discriminative-vs-generative-classifiers-a-comparison-of-logistic-regression-and-naive-bayes.pdf)  
[Generative Learning algorithms Andrew Ng](http://cs229.stanford.edu/notes/cs229-notes2.pdf)
[Generative vs. discriminative](https://stats.stackexchange.com/questions/12421/generative-vs-discriminative?newreg=939980896bbc40688e3a7a33a888fcdf)

Here the is one answer from [What is the difference between a Generative and Discriminative Algorithm?](https://stackoverflow.com/questions/879432/what-is-the-difference-between-a-generative-and-discriminative-algorithm)  
that is particular useful to gain an intuitive sense of teh difference between generative and discriminative learning models.

> imagine your task is to classify a speech to a language, you can do it either by:
> 1) Learning each language and then classifying it using the knowledge you just gained
> 2) Determining the difference in the linguistic models without learning the languages and then classifying the speech.  

> the first one is the generative approach and the second one is the discriminative approach.

***



