<Query Kind="Expression" />

// Select C# Expression
// In this mode LINQPad execute the expressions.
// Notice that you do not need a semicolumn at the end of any of any expressions.
// Expressions are statements as well.
// Expressions are parts of statements.
// Expressions can also include function calls.
// Expressions produce at least one value.
// One way to think of this is that the purpose of an expression is to create a value (with some possible side-effects)
(new DateTime(DateTime.Today.Year, 12, 25) - DateTime.Today).TotalDays

// An expression evaluates to a value.
// Expression is a combination of variables, operations and values that yields a result value.
// Expressions only contain identifiers, literals and operators, where operators include arithmetic 
// and boolean operators, the function call operator () the subscription operator [] and similar, and 
// can be reduced to some kind of "value", which can be any Python object. 

// Examples of Expressions
//3 + 5
//map(lambda x: x*x, range(10))
//[a.x for a in some_iterable]
//yield 7

// Refs
// https://fsharpforfunandprofit.com/posts/expressions-vs-statements/

// A statement does something and are often composed of expressions or other statements.
// Statement is just a standalone unit of execution and doesn’t return anything. 
// the sole purpose of a statement is to have side-effects.
// a truly pure functional language cannot support statements at all!
// In F# everything is an expression!
// First, unlike statements, smaller expressions can be combined (or “composed”) into larger expressions. So if everything is an expression, then everything is also composable.
// Second, a series of statements always implies a specific order of evaluation, which means that a statement cannot be understood without looking at prior statements. But with pure expressions, the subexpressions do not have any implied order of execution or dependencies.
