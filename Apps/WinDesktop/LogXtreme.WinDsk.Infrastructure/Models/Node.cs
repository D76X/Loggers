using System.Collections.Generic;

namespace LogXtreme.WinDsk.Infrastructure.Models {

    public interface INode<T> {

        INode<T> Parent { get; }

        T Value { get; }

        IEnumerable<INode<T>> Children { get; }
    }

    /// <summary>
    /// Refs
    /// http://www.siepman.nl/blog/post/2013/07/30/tree-node-nodes-descendants-ancestors.aspx
    /// https://stackoverflow.com/questions/169973/when-should-i-use-a-list-vs-a-linkedlist
    /// Refs
    /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract
    /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/knowing-when-to-use-override-and-new-keywords
    /// https://stackoverflow.com/questions/2705163/c-abstract-classes-need-to-implement-interfaces
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NodeBase<T> : INode<T> {

        protected readonly List<NodeBase<T>> children;

        protected NodeBase(IEnumerable<NodeBase<T>> children) {
            this.children = new List<NodeBase<T>>();
            this.children.AddRange(children);
        }

        public INode<T> Parent { get; protected set; }

        public T Value { get; protected set; }   

        public virtual IEnumerable<INode<T>> Children => this.children;

        public abstract void Add(NodeBase<T> node);
    }

    public class Node<T> : NodeBase<T> {        

        public Node(T value, Node<T> parent, IEnumerable<Node<T>> children) :
            base(children) {

            Value = value;
            Parent = parent;
        }

        public Node<T> this[int index] => this.children[index] as Node<T>;

        public bool IsRoot => this.Parent == null;

        public override void Add(NodeBase<T> node) { this.children.Add(node); }
        
    }
}
