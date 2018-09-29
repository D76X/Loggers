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
	 new Order {Id=2, CustomerName="Jane",   Amount=2000, Status=OrderStatus.Pending, Email="jane.job@gmail.com"},
	 new Order {Id=3, CustomerName="John",   Amount=3000, Status=OrderStatus.Pending, Email=""},
	 new Order {Id=4, CustomerName="Peter",  Amount=4000, Status=OrderStatus.Pending, Email="davide.job@gmail.com"},
	};

// This is an imperative implementation
void RefundOrder(int orderId){
	
	Order orderToRefund = null;
	
	foreach(var order in orders){
		
		if(order.Id == orderId){
			orderToRefund = order;
			break;
		}
	}
	
	if(orderToRefund != null) {
	 Console.WriteLine($"Refunded Amount={orderToRefund.Amount} for order Id={orderId}");
	 return;
	}
	
	Console.WriteLine($"Order Id={orderId} could not be found.");
}

//This is more functional thanks to LINQ
void RefundOrderFunctional1(int orderId){
	
	// IEnumerable<T>.Select is not executed until the collection is enumerated
	// The ToList().ForEach enumerates the results of the query and causes it to execute
	// The Select is not striclty necessary but you may do things in it
	orders
	.Where(o => o.Id == orderId)
	.Select(o => o)
	.ToList()
	.ForEach(o => Console.WriteLine($"Refunded Amount={o.Amount} for order Id={o.Id}"));
} 

void RefundOrderFunctional2(int orderId){
	
	var refunded = orders.FirstOrDefault(o => o.Id == orderId);
	
	if(refunded!=null){
	 Console.WriteLine($"Refunded Amount={refunded.Amount} for order Id={refunded.Id}");
	 return;
	}
	
	Console.WriteLine($"Order Id={orderId} could not be found.");
}

void Main() {	

	RefundOrder(1);
	RefundOrderFunctional1(2);
	RefundOrderFunctional2(3);
	
	Console.WriteLine();
	
	var or = orders.FirstOrDefault().Status = OrderStatus.Refunded;
	
	if(orders.Any(o => o.Status == OrderStatus.Refunded)){
		Console.WriteLine("There have been returns");
	}
	
	orders.ToList().ForEach(o => o.Status = OrderStatus.Delivered);
	
	if(orders.All(o => o.Status == OrderStatus.Delivered)){
		Console.WriteLine("All orders have been delivered");
	}
	
	Console.WriteLine();
	int deliveredCounter = orders.Count(o => o.Status == OrderStatus.Delivered);
	decimal total = orders.Sum(o => o.Amount);
	Console.WriteLine($"delivered {deliveredCounter} for total Amount = {total}");
}