<Query Kind="Statements" />

// C# Statements
var customers = new[] {
 new {Name="Davide", Email="davide.job@gmail.com"},
 new {Name="Jane", Email="jane.job@gmail.com"},
 new {Name="John", Email=""},
 new {Name="Peter", Email="davide.job@gmail.com"},
};

// one way 
foreach(var customer in customers){
 if(!String.IsNullOrEmpty(customer.Email)){
  Console.WriteLine($"Send email to {customer.Email}");
 } 
};

// Epressions are also statements!
Console.WriteLine();

// better with LINQ Where IEnumerable<T> extension
foreach(var customer in customers.Where(c => !String.IsNullOrEmpty(c.Email))){ 
  Console.WriteLine($"Send email to {customer.Email}"); 
};

// Can also use the Query expressions syntax rather than the extension methods fluent API.
Console.WriteLine();

foreach(var customer in from c in customers where !String.IsNullOrEmpty(c.Email) select c) { 
  Console.WriteLine($"Send email to {customer.Email}"); 
};