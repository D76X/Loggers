# Machine Learning & AI

## Resouces

1.  [Probabilistic Systems Analysis and Applied Probability](https://ocw.mit.edu/courses/electrical-engineering-and-computer-science/6-041-probabilistic-systems-analysis-and-applied-probability-fall-2010/)  
    MIT OpenCourseWare  
    Instructor: John Tsitsiklis

2.  [Probabilistic Systems Analysis and Applied Probability](https://ocw.mit.edu/courses/electrical-engineering-and-computer-science/6-041sc-probabilistic-systems-analysis-and-applied-probability-fall-2013/)  
    MIT OpenCourseWare  
    Instructor: Qing He

3.  [Lecture 1 | Machine Learning (Stanford)](https://www.youtube.com/watch?v=UzxYlbK2c7E&t=3009s)  
    Professor Andrew Ng  
    [Lecture Notes](http://cs229.stanford.edu/notes/cs229-notes1.pdf)

4.  [Deeplearning.ai](https://www.youtube.com/channel/UCcIXc5mJsHVYTZR1maL5l9w)  
    Professor Andrew Ng

5.  [Artificial Intelligence Courses](https://www.youtube.com/user/aicourses/videos)

6.  [Machine Learning - overview and applications](https://www.youtube.com/watch?v=yDLKJtOVx5c&list=PLD0F06AA0D2E8FFBA)

7.  [Introduction to Convolutional Neural Networks for Visual Recognition](https://www.youtube.com/watch?v=vT1JzLTH4G4)  
    Stanford University School of Engineering

8.  [Machine Learning Gorup at Microsoft Research](https://www.microsoft.com/en-us/research/group/machine-learning-research-group/)

9.  [Machine Learning Carnegie Mellon University](http://www.cs.cmu.edu/~ninamf/courses/601sp15/lectures.shtml)

10. [Towards Data Science - sharing concept, ideas and code](https://towardsdatascience.com/)

11. [OpenAI - Discovering and ancting the path to safe AGI Artificial General Intelligence](https://openai.com/)

---

## Probability Theory & Statistics

### What is a random variable?

1.  [Random variables KHANACADAMY](https://www.khanacademy.org/math/statistics-probability/random-variables-stats-library/random-variables-discrete/v/random-variables)
2.  [What is a random variable?](https://www.quora.com/What-is-a-random-variable)

---

### Why is the linear combination of random variables computed using teh convolution formula?

### What are mixed random variables?

1.  [Why is the sum of two random variables a convolution?](https://stats.stackexchange.com/questions/331973/why-is-the-sum-of-two-random-variables-a-convolution)

Explains the difference that exists between any linear combination of indipendent RVs which leads to the convolution formula and the distinct concept of **mixed RVs**
which leads to a linear function of expected values etc.

---

### What is the difference bewteen the Bernoulli distribution and the Binomial Distribution?

1.  [What is the difference and relationship between the binomial and Bernoulli distributions?](https://math.stackexchange.com/questions/838107/what-is-the-difference-and-relationship-between-the-binomial-and-bernoulli-distr)

---

### What is the difference between Bernoulli, Poisson and exponential distributions and processes?

1.  [Bernoulli Process Practice -MIT MIT 6.041SC Probabilistic Systems Analysis and Applied Probability, Fall 2013](https://www.youtube.com/watch?v=i0Jom_gR0t4)
2.  [Exponential distribution](https://en.wikipedia.org/wiki/Exponential_distribution)

Poisson or Binomial distribution?
http://personal.maths.surrey.ac.uk/st/J.Deane/Teach/se202/poiss_bin.html

When should I use Poisson distribution?
https://www.quora.com/When-should-I-use-Poisson-distribution

---

### What is the Law of the Unconscious Statistician (LOTUS)?

1.  [Law of the unconscious statistician](https://en.wikipedia.org/wiki/Law_of_the_unconscious_statistician)

---

### Why the variance estimator for a normal distribution uses N-1 as its denominator instead of N?

[Why divide the sample variance by N-1?](http://www.visiondummy.com/2014/03/divide-variance-n-1/)

This article explains well why the variance estimator for a normal distribution uses N-1 as a divisor instead of the more intuitive N.

---

### The definition and meaning of the joint probability density function

1.  [Expected value of joint probability density functions](https://math.stackexchange.com/questions/344128/expected-value-of-joint-probability-density-functions)

Provides a nice explanation of what the joint PDF is and an example where it is used.

---

### What is a confidence Intervals?

1.  https://www.mathsisfun.com/data/confidence-interval.html
2.  [Confidence intervals and margin of error](https://www.youtube.com/watch?v=hlM7zdf7zwU)
3.  [Confidence interval](https://en.wikipedia.org/wiki/Confidence_interval)

---

### What is the difference between the Maximum Likelihood Estmation [MLE] and the Method of Moments [MOM]?

1.  [What is the difference between Method Of Moment (MOM), Maximum A Posteriori (MAP), and Maximum Likelihood Estimation (MLE)?](https://www.quora.com/What-is-the-difference-between-Method-Of-Moment-MOM-Maximum-A-Posteriori-MAP-and-Maximum-Likelihood-Estimation-MLE#)

2.  [How do you explain maximum likelihood estimation intuitively?](https://www.quora.com/How-do-you-explain-maximum-likelihood-estimation-intuitively)

---

### Whar are Prior and Posterior probabilities?

1.  [Prior And Posterior - Intro to Statistics](https://www.youtube.com/watch?v=o2Tpws5C2Eg)
2.  [26 - Prior and posterior predictive distributions - an introduction](https://www.youtube.com/watch?v=R9NQY2Hyl14)
3.  [MIT - Bayesian Updating with Continuous Priors](https://ocw.mit.edu/courses/mathematics/18-05-introduction-to-probability-and-statistics-spring-2014/readings/MIT18_05S14_Reading13a.pdf)  
    I have copy if this document that gives a detailed explanation.
4.  [Prior and Posterior Distributions](https://www.youtube.com/watch?v=BqBuCpHN9u8)

---

### The concept of Loss Function and optimization problems

1.  [Is minimizing squared error equivalent to minimizing absolute error? Why squared error is more popular than the latter?](https://stats.stackexchange.com/questions/147001/is-minimizing-squared-error-equivalent-to-minimizing-absolute-error-why-squared)  
    [The concept of loss function on Wikipedia]([https://en.wikipedia.org/wiki/Loss_function#Selecting_a_loss_function)  
    [The robustfit function in Matlab](http://uk.mathworks.com/help/stats/robustfit.html?s_tid=gn_loc_drop)

- Squared error penalizes large errors more than does absolute error and is more forgiving of small errors than absolute error is. **Minimizing square errors (MSE)** is definitely not the same as **minimizing absolute deviations (MAD) of errors**. **MSE** provides the mean response of y conditioned on x, while **MAD** provides the median response of y conditioned on x.

- However, MSE is differentiable, thus, allowing for gradient-based methods, much efficient than their non-differentiable counterpart. MAD is not differentiable at x=0.

- Another reason of why MSE may have had the wide acceptance it has is that it is based on the euclidean distance (in fact it is a solution of the projection problem on an euclidean banach space) which is extremely intuitive given our geometrical reality.

- As another answer has explained, minimizing squared error is not the same as minimizing absolute error. The reason minimizing squared error is preferred is because it prevents large errors better.Say your empolyer's payroll department accidentally pays each of a total of ten employees $50 less than required. That's an absolute error of $500. It's also an absolute error of $500 if the department pays just one employee $500 less. But it terms of squared error, it's 25000 versus 250000.

2.  [Quadratic loss function implying conditional expectation](https://stats.stackexchange.com/questions/176313/quadratic-loss-function-implying-conditional-expectation)  
    This post present an agebraic chain of identities that shows how to interpret the problem of minimization of the quadratic loss function. It explains why it is the case that choosing the estimation function as the expected value of the conditioned training set of data results in the minimum quadratic error. This is useful as in some other parts of the theory this is a detail over which many gloss over although it is mentioned. This chain of identity is purely an exercise in algebra and the formalism of probability theory hence it is not extremely enlithening is many respects exept for the fact that in addition to clarify the result it forces on the reader some familiarity with the notations and the meaning of the quantities and theorems involved.

3.  [Minimizing the expected loss](https://stats.stackexchange.com/questions/50783/minimizing-the-expected-loss?rq=1)

4.  [Explain how Pr(dx,dy) appears in the derivation of the expected prediction error](https://stats.stackexchange.com/questions/256669/explain-how-prdx-dy-appears-in-the-derivation-of-the-expected-prediction-error?rq=1)  
    [Expected Error Prediction Derivation](https://stats.stackexchange.com/questions/189560/expected-error-prediction-derivation)

---

### The concept of Loss Function and ots optimization problems with categorical variables.

- [How does the classification using the 0-1 loss matrix method work?](https://math.stackexchange.com/questions/2623072/how-does-the-classification-using-the-0-1-loss-matrix-method-work)

- [Introduction to Machine Learning IITM](https://www.youtube.com/watch?time_continue=28&v=pFtiNSmJuoE)

- [0-1 Loss Function explanation](https://stats.stackexchange.com/questions/284028/0-1-loss-function-explanation)

---

### The curse of dimentionality

- [PCA 1: curse of dimensionality](https://www.youtube.com/watch?v=IbE0tbjy6JQ)  
  Victor Lavrenko

- [Machine learning curse of dimensionality explained?](https://stats.stackexchange.com/questions/65379/machine-learning-curse-of-dimensionality-explained)  
  This post provides very good intuitive examples.

- [The Curse of Dimensionality in classification - Computer vision for dummies](http://www.visiondummy.com/2014/04/curse-dimensionality-affect-classification/)  
  This article gives a very good explanation with examples and illustrations.

- [What is the curse of dimensionality?](https://stats.stackexchange.com/questions/15971/what-is-the-curse-of-dimensionality)  
  This post references to the **Elements of Statistical Learning** textbook.

---

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

---

## Approaches to machine learning

### Generative vs Degenerative

[Generative Models - The Math of Intellligence #8](https://www.youtube.com/watch?v=HyuBTMaKFmU)

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
>
> 1.  Learning each language and then classifying it using the knowledge you just gained
> 2.  Determining the difference in the linguistic models without learning the languages and then classifying the speech.

> the first one is the generative approach and the second one is the discriminative approach.

---

## Classification problems

[(ML 1.6) k-Nearest Neighbor classification algorithm](https://www.youtube.com/watch?v=4ObVzTuFivY&index=6&list=PLD0F06AA0D2E8FFBA)  
Deterministic and probabilistic interpretations.
This is a discriminative algorithm.

---

# Algorithms

## Batch gradient descent

[Lecture 2 | Machine Learning (Stanford)](https://www.youtube.com/watch?v=5u4G23_OohI)  
44:34/1:16:15  
Not suitable for large data sets

## Stocastic gradient descent or Incremental gradient descent

[Lecture 2 | Machine Learning (Stanford)](https://www.youtube.com/watch?v=5u4G23_OohI)  
44:34/1:16:15  
Not suitable for large data sets

[2. Stochastic Gradient Descent](https://www.youtube.com/watch?v=UfNU3Vhv5CA)  
Video from Coursera - Standford University - Course: Machine Learning:

[Difference between Batch Gradient Descnt and Tochastic Gradient Descent](https://towardsdatascience.com/difference-between-batch-gradient-descent-and-stochastic-gradient-descent-1187f1291aa1)  
This is an interesting article about the difference between the two algorithms which shows it in simple Python code.

[What's the difference between gradient descent and stochastic gradient descent?](https://www.quora.com/Whats-the-difference-between-gradient-descent-and-stochastic-gradient-descent)  
This is an excellent post on Quora.

[Gradient Descent vs Stochastic Gradient Descent algorithms](https://stackoverflow.com/questions/35711315/gradient-descent-vs-stochastic-gradient-descent-algorithms)
This is a good post on Stackoverflow.

## Gradient descent wit Momentum

[Gradient descent wit Momentum](https://www.youtube.com/watch?v=k8fTYJPd3_I)  
Deeplearning.ai

---

# Principal Component Analysis (PCA)

1.  [19. Principal Component Analysis](https://www.youtube.com/watch?v=WW3ZJHPwvyg)  
    MIT OpenCourseWare  
    MIT 18.650 Statistics for Applications, Fall 2016  
    Instructor: Philippe Rigollet

1.  [Feature extraction using PCA](http://www.visiondummy.com/2014/05/feature-extraction-using-pca/)

---

# Convex Optimization

---
