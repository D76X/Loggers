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
    public abstract class Node<T> {

        private readonly List<Node<T>> children = new List<Node<T>>();

        public Node(T value) {
            Value = value;
        }

        public Node<T> Parent { get; }

        public T Value { get; set; }

        public IEnumerable<Node<T>> Children => this.children;

        public Node<T> this[int index] => this.children[index];

        public bool IsRoot => this.Parent == null;
    }
}
