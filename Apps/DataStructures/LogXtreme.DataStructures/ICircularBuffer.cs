
namespace LogXtreme.DataStructures {

    /// <summary>
    /// Refs
    /// http://geekswithblogs.net/blackrob/archive/2014/09/01/circular-buffer-in-c.aspx
    /// https://msdn.microsoft.com/en-us/library/dn440729(v=pandp.60).aspx#sec9
    /// https://github.com/LMAX-Exchange/disruptor/wiki 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICircularBuffer<T> {

        int Count { get; }
        int Capacity { get; set; }
        T Enqueue(T item);
        T Dequeue();
        void Clear();
        T this[int index] { get; set; }
        int IndexOf(T item);
        void Insert(int index, T item);
        T RemoveAt(int index);
    }
}
