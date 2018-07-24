using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UniHospital.Test1 {
    [TestClass]
    public class Tester {

        private double epsilon = 1e-6;

        [TestMethod]
        public void AccountCannotOverstepItsOverdraftLimit() {

            Account account = new Account(1);
            Assert.AreEqual(false, account.Withdraw(1.01));
        }

        [TestMethod]
        public void DepositDoesNotAccpetNegativeNumber() {

            Account account = new Account(20);

            Assert.AreEqual(false, account.Deposit(-1.00));
        }

        [TestMethod]
        public void WithdrawDoesNotAccpetNegativeNumber() {

            Account account = new Account(20);

            Assert.AreEqual(false, account.Withdraw(-1.00));
        }

        [TestMethod]
        public void AccountCannotHavePositiveOverdraftLimit() {

            Account account = new Account(20);

            Assert.AreEqual(0, account.OverdraftLimit, epsilon);
        }
    }

    public class Account {
        public double Balance { get; private set; }
        public double OverdraftLimit { get; private set; }

        public Account(double overdraftLimit) {
            this.OverdraftLimit = overdraftLimit > 0 ? overdraftLimit : 0;
            this.Balance = 0;
        }

        public bool Deposit(double amount) {
            if (amount >= 0) {
                this.Balance += amount;
                return true;
            }
            return false;
        }

        public bool Withdraw(double amount) {
            if (this.Balance - amount >= -this.OverdraftLimit && amount >= 0) {
                this.Balance -= amount;
                return true;
            }
            return false;
        }
    }

    public class Node {
        public int Value { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public Node(int value, Node left, Node right) {
            Value = value;
            Left = left;
            Right = right;
        }
    }

    public class BinarySearchTree {

        /// <summary>
        /// Write a function that checks if a given binary search tree contains a given value.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Contains(Node root, int value) {

            // throw new NotImplementedException("Waiting to be implemented.");
            if (root == null) { return false; }            

            if (root.Value < value) {
                return Contains(root.Right, value);
            }
            else if(root.Value > value) {
                return Contains(root.Left, value);
            }
            else {
                return true;
            }
        }        
    }

    public class Song {
        private string name;
        public Song NextSong { get; set; }

        public Song(string name) {
            this.name = name;
        }

        public bool IsRepeatingPlaylistSlow() {

            Stack<Song> stack = new Stack<Song>();
            stack.Push(this);
            Song next = this.NextSong;

            while (next != null) {

                if (stack.Contains(next)) { return true; }

                stack.Push(next);

                next = next.NextSong;
            }

            return false;
        }

        public bool IsRepeatingPlaylistSlow2() {

            List<Song> stack = new List<Song>();
            stack.Add(this);
            Song next = this.NextSong;

            while (next != null) {

                if (stack.Contains(next)) { return true; }

                stack.Add(next);

                next = next.NextSong;
            }

            return false;
        }

        public bool IsRepeatingPlaylistSlow3() {

            SortedList<string,Song> stack = new SortedList<string, Song>();
            stack.Add(this.name, this);
            Song next = this.NextSong;

            while (next != null) {

                if (stack.Keys.Contains(next.name)) { return true; }

                stack.Add(next.name, next);

                next = next.NextSong;
            }

            return false;
        }

        public bool IsRepeatingPlaylist() {

            // https://stackoverflow.com/questions/823860/c-listt-contains-too-slow 
            // https://stackoverflow.com/questions/2705607/sorting-a-dictionary-in-place-with-respect-to-keys
            // SortedDictionary or HashSet

            HashSet<string> stack = new HashSet<string>();
            stack.Add(this.name);
            Song next = this.NextSong;

            while (next != null) {

                if (stack.Contains(next.name)) { return true; }

                stack.Add(next.name);

                next = next.NextSong;
            }

            return false;
        }        
    }

    public class TextInput {

        protected List<char> value = new List<char>();

        public virtual void Add(char c) {

            value.Add(c);
        }

        public string GetValue() => new string(this.value.ToArray());
    }

    public class NumericInput : TextInput {

        public override void Add(char c) {
            if (!char.IsDigit(c)) { return; }
            base.Add(c);
        }
    }

    class TwoSum {

        public static Tuple<int, int> FindTwoSum1(
            IList<int> list, 
            int sum) {

            for (int i = 0; i < list.Count-1; i++) {

                for (int k = i+1; k < list.Count; k++) {

                    if (list[i] + list[k] == sum) {
                        return new Tuple<int, int>(i, k);
                    }
                }
            }

            return null;            
        }

        public static Tuple<int, int> FindTwoSum2(
            IList<int> list,
            int sum) {

            // sort asc
            // https://coderbyte.com/algorithm/two-sum-problem
            // https://stackoverflow.com/questions/36506164/find-two-sum-function-in-c-sharp
            // https://www.c-sharpcorner.com/UploadFile/vendettamit/using-lookup-for-duplicate-key-value-pairs-dictionary/

            // convert the list in lookup
            // index of list becomes value in lookup
            // value of list becomes index in lookup
            // ILookup allows duplicate keys!
            var lookup = list
                .Select((x, i) => new { Index = i, Value = x })
                .ToLookup(x => x.Value, x => x.Index);

            // go thorugh the list and for each value 
            // check the difference to the sum.
            // Is the diff in the lookup?
            for (int i = 0; i < list.Count; i++) {

                int diff = sum - list[i];

                if (lookup.Contains(diff)) {
                    return Tuple.Create(i, lookup[diff].First());
                }               
            }

            return null;
        }

        public static Tuple<int, int> FindTwoSum(IList<int> list, int sum) {

            var hs = new HashSet<int>();

            list.ToList().ForEach(x => hs.Add(x));

            for (int i = 0; i < hs.Count; i++) {

                var diff = sum - list[i];

                if (hs.Contains(diff)) {
                    var index = list.IndexOf(diff);
                    return new Tuple<int, int>(i, index);
                }
            }

            return null;
        }
    }

    class Program {
        static void Main(string[] args) {

            var ti = new TextInput();
            ti.Add('a');
            var v1 = ti.GetValue();

            TextInput input = new NumericInput();
            input.Add('1');
            input.Add('a');
            input.Add('0');
            var v2 = input.GetValue();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        public static bool IsPalindrome(string word) {

            if (string.IsNullOrEmpty(word)) { return false; }

            if (word.Length == 1) { return true; }

            var array1 = word.ToLower().ToCharArray();

            int length = array1.Length;

            int last = length / 2;

            for (int i = 0; i < last; i++) {

                if (array1[i] != array1[length - i - 1]) {
                    return false;
                }
            }

            return true;
        }
    }
}
