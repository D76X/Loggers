using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogXtreme.WinDsk.Infrastructure.Models {

    /// <summary>
    /// Refs
    /// http://www.siepman.nl/blog/post/2013/07/30/tree-node-nodes-descendants-ancestors.aspx
    /// https://stackoverflow.com/questions/169973/when-should-i-use-a-list-vs-a-linkedlist
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NodeBase<T> {

        private readonly IEnumerable<NodeBase<T>> children;

        public NodeBase<T> Parent { get; }

        public T Value { get; set; }

        public IEnumerable<NodeBase<T>> Children => this.children;       
    }

    public class Node<T> : NodeBase<T> {

        private readonly List<NodeBase<T>> children = new List<NodeBase<T>>();

        public Node(T value) {
            Value = value;
        }

        public Node<T> this[int index] => this.children[index];

        public bool IsRoot => this.Parent == null;
    }
}
