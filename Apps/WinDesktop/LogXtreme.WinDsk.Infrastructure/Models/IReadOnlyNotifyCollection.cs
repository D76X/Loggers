using System.Collections.Generic;
using System.Collections.Specialized;

namespace LogXtreme.WinDsk.Infrastructure.Models {

    /// <summary>
    /// This interface is used for a contract-based approach as alternative to the direct use of
    /// ReadOnlyObservableCollection<typeparamref name="T"/>. 
    /// 
    /// ****************************************
    /// On .NET invariance of generic interaces
    /// ****************************************
    /// 
    /// Any interface IMyInterface<typeparamref name="T"/> defined in .NET is invariant by default.
    /// This means that in methods that are declared with parameters of IMyInterface<T> the compiler 
    /// enforces the compile-time contraint by which only references that are of exactly the type 
    /// IMyIntreface<T> can be passed to the method or returned by it.
    /// 
    /// **************************
    /// The problem on covariance 
    /// **************************
    /// 
    /// For example DoSomething(IPeople<Person> person) cannot be invoked with an IPeople<Employee>
    /// as DoSomething(p) when p is of type IPeople<Employee> and Employee: Person.
    /// 
    /// Often though we want DoSomething to be able to work on IPeople<Person> as well as on 
    /// IPeople<Employee> but the compiler will not allow this behaviour without us providing further
    /// information on the definition of IPeople<T>. 
    /// 
    /// Notice that in this case we want to be able to pass IPeople<Employee> in place of IPeople<Person> 
    /// where Employee is a more derived type with respect to Person, that is Employee: Person. 
    /// 
    /// One way to guarantee this constraint is to make sure that IPeople<T> only defines methods that 
    /// return T or some interface of T but never use T or some interface of T in their parameter declaration. 
    /// 
    /// In C# this is accomplished by means of the generict type modifier keyword "out" by declaring IPeople<out T> 
    /// for example. Now IPeople<out T> may declare methods such as IPeople<out T> { T Read(int Id)} but not methods 
    /// such as IPeople<out T> { Id Persist(ref T item)}. 
    /// 
    /// It is obvious from this example that if IPeople<T> were the interface of a repository to persist data to a 
    /// database the method declaration IPeople<out T> { Id Persist(ref T item)} becomes incompatible with the fact
    /// that IPeople<Employee> can be used instead of IPeople<Person>. 
    /// 
    /// --------------------------------------------------------------------------------------------------------------------
    /// 
    /// var employeeRepo = new HR<Employee>(); // HR implements IPeople<out T> and  HR<Employee>() access the Employee table
    /// IPeople<Person> peopleRepo = employeeRepo; // this is allowed by IPeople<out T> being covariant
    /// peopleRepo.Persist(new Person(101)); // this is now possible but it shouldn't!
    /// 
    /// Person employee = peopleRepo.Read(505); // I expect a Person but should read an employee from the Empoyee table - this is OK.
    /// 
    /// --------------------------------------------------------------------------------------------------------------------
    /// 
    /// In the preceeding example it seems we are allowed ro try to persist a person to the employee table which shouldn't be.
    /// The "out" generic modifier prevents IPeople<out T> from declaring the IPeople<out T> { Id Persist(ref T item)} method
    /// so that this cases are become impossible. IPeople<out T> can then only define read-only methods that return types T or
    /// based on T but no methods with such types in their parameter list.
    /// 
    /// ****************************************
    /// The problem of contravariance
    /// ****************************************
    /// 
    /// Conversely, sometimes we would like to defined a method on an interface that takes a parameter of a certain type by 
    /// also accept parameter of type that is a parent type of the declared type i.e. Pesron in place of Employee.
    /// 
    /// Let's consider the example of IRepository<TEntity> where I would like to be able to use an implementation such as
    /// an implementation IRepository<Employee> can also be used to persist a Person.
    /// 
    /// IRepository<T>.Add(T item)
    /// 
    /// By default Add will only allow the specific type T that is 
    /// --------------------------------------------------------------------------------------------------------------------
    /// 
    /// var employeeRepo = new Repo<Employee>();
    /// var id1 = employeeRepo.Add(new Empoyee(101)); // OK
    /// var id2 = employeeRepo.Add(new Person(102)); // can't do it
    /// 
    /// However by using the "in" generic type modifier 
    /// 
    /// IRepository<in T>
    /// 
    /// var id2 = employeeRepo.Add(new Person(102)); // now you can!
    /// --------------------------------------------------------------------------------------------------------------------
    /// 
    /// By using the "in" generic type modifier you tell the compiler that the implementations IRepository<in T> know how to 
    /// handle the cases where a parent type is passed as parameter of one of the methods defined on the interface. However,
    /// when the "in" generic type modifier is used the interface IRepository<T> can no longer declare methods that return T.
    /// 
    /// Let's consider an hypotetical example
    /// 
    /// --------------------------------------------------------------------------------------------------------------------
    /// 
    /// var repo = new Repo<Person>();
    /// IRepository<Employee> personRepo = repo; // now possible as IRepository<T> is contravariant!
    /// Employee e = personRepo.Get(101); // should not be possible!
    /// 
    /// --------------------------------------------------------------------------------------------------------------------    /// 
    /// 
    /// Refs
    /// https://stackoverflow.com/questions/1763696/how-can-i-make-a-read-only-observablecollection-property
    /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out-generic-modifier
    /// http://tomasp.net/blog/variance-explained.aspx/
    /// https://blogs.msdn.microsoft.com/csharpfaq/2010/02/16/covariance-and-contravariance-faq/
    /// https://app.pluralsight.com/player?course=csharp-generics&author=scott-allen&name=csharp-generics-m5-constraints&clip=9&mode=live
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadOnlyNotifyCollection<out T>
           : IReadOnlyCollection<T>,
             INotifyCollectionChanged { }
}
