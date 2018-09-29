<Query Kind="Program" />

// C# Program
// ITs like you are in the body of the classic static class Program of a console app

enum OrderStatus {
 Pending,
 Delivered,
 Refunded
}

class Order {
 public int Id {get; set;}
 public decimal Amount {get; set;}
 public OrderStatus Status {get; set;}
 public string CustomerName {get; set;}
 public string Email {get; set;}
 
};

// C# Program cannot use var because it is not a local variable
List<Order> orders = new List<Order>{
	 new Order {Id=1, CustomerName="Davide", Amount=1000, Status=OrderStatus.Pending, Email="davide.job@gmail.com"},
	  new Order {Id=2, CustomerName="Davide", Amount=1100, Status=OrderStatus.Pending, Email="davide.job@gmail.com"},
	 new Order {Id=3, CustomerName="Jane",   Amount=2000, Status=OrderStatus.Pending, Email="jane.job@gmail.com"},
	 new Order {Id=4, CustomerName="Jane",   Amount=2200, Status=OrderStatus.Pending, Email="jane.job@gmail.com"},
	 new Order {Id=5, CustomerName="John",   Amount=3000, Status=OrderStatus.Pending, Email=""},
	 new Order {Id=6, CustomerName="Peter",  Amount=4000, Status=OrderStatus.Pending, Email="davide.job@gmail.com"},
	};
	
void Main() {	

	// IEnumerable<T>.GroupBy
	// The GorupBy needs a key to create gorups by that key
	// returns an IEnumerable<IGrouping<TKey,TValue>>
	var groups = orders.GroupBy(o=> o.CustomerName);
	groups.Dump();
	
	// often though you may want to extract a disctionary 
	// from a IEnumerable<IGrouping<TKey,TValue>> like this
	var dictionary = groups.ToDictionary(g => g.Key, g => g.ToList());
	dictionary.Dump();
}