using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CollectionApplication1

{

    /// <summary>
    ///  Tree node item 
    /// </summary>
    /// <typeparam name="T">the type of tree node data</typeparam>
    public interface ITreeNode<T>
    {
        T Data { get; set; }                             // Custom data
        IEnumerable<ITreeNode<T>> Children { get; set; } // List of childrens
    }

    

    public class Task
    {

        /// <summary> Generate the Fibonacci sequence f(x) = f(x-1)+f(x-2) </summary>
        /// <param name="count">the size of a required sequence</param>
        /// <returns>
        ///   Returns the Fibonacci sequence of required count
        /// </returns>
        /// <exception cref="System.InvalidArgumentException">count is less then 0</exception>
        /// <example>
        ///   0 => { }  
        ///   1 => { 1 }    
        ///   2 => { 1, 1 }
        ///   12 => { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 }
        /// </example>
        public static IEnumerable<int> GetFibonacciSequence(int count)
        {
            if (count < 0)
                throw new ArgumentException("{0} is less then 0", count.ToString());

            int last = 0, lastlast = 0, current = 1;


            for (int i = 0; i < count; i++)
            {
                current = (last + lastlast) >= current ? (last + lastlast) : current; //поскольку последовательсть фибоначчи неубывающая

                yield return current;

                lastlast = last;
                last = current;

            }

            // TODO : Implement Fibonacci sequence generator
            //throw new NotImplementedException();
        }

        /// <summary>
        ///    Parses the input string sequence into words
        /// </summary>
        /// <param name="reader">input string sequence</param>
        /// <returns>
        ///   The enumerable of all words from input string sequence. 
        /// </returns>
        /// <exception cref="System.ArgumentNullException">reader is null</exception>
        /// <example>
        ///  "TextReader is the abstract base class of StreamReader and StringReader, which ..." => 
        ///   {"TextReader","is","the","abstract","base","class","of","StreamReader","and","StringReader","which",...}
        /// </example>
        public static IEnumerable<string> Tokenize(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader is null");

            char[] delimeters = new[] { ',', ' ', '.', '\t', '\n' };

            Dictionary<char,char> special= new Dictionary<char, char>
            {
                ['a']='\a',
                ['b']='\b',
                ['t']='\t',
                ['n']='\n',
                ['v']='\v',
                ['f']='\f',
                ['r']='\r'
            };


            List<char> currentCharList = new List<char>();
            int current, next;
            char currentChar, nextChar;
         

            while (true)
            {
                current = reader.Read();

                if (current < 0)   // похожие места
                {

                    if (currentCharList.Count != 0)
                        yield return new String(currentCharList.ToArray());
                    yield break;
                }

                currentChar = Convert.ToChar(current);

                if (currentChar == '\\')
                {
                    next = reader.Peek();
                    if (next >= 0)
                    {
                        nextChar = Convert.ToChar(next);
                        if (special.ContainsKey(nextChar))
                        {
                            currentChar = special[currentChar];
                            reader.Read();
                        }
                    }

                }

                if (Array.Exists(delimeters, x => x == currentChar)) // похожие места
                {
                    if (currentCharList.Count != 0)
                        yield return new String(currentCharList.ToArray());
                    currentCharList.Clear();
                }
                else
                {
                    currentCharList.Add(currentChar);
                }

            }

            // TODO : Implement the tokenizer
            //throw new NotImplementedException();
        }
      

        /// <summary>
        ///   Traverses a tree using the depth-first strategy
        /// </summary>
        /// <typeparam name="T">tree node type</typeparam>
        /// <param name="root">the tree root</param>
        /// <returns>
        ///   Returns the sequence of all tree node data in depth-first order
        /// </returns>
        /// <example>
        ///    source tree (root = 1):
        ///    
        ///                      1
        ///                    / | \
        ///                   2  6  7
        ///                  / \     \
        ///                 3   4     8
        ///                     |
        ///                     5   
        ///                   
        ///    result = { 1, 2, 3, 4, 5, 6, 7, 8 } 
        /// </example>
        public static IEnumerable<T> DepthTraversalTree<T>(ITreeNode<T> root)
        {
            if (root == null)
                throw new ArgumentNullException();

            var stack = new Stack<ITreeNode<T>>();
            var tempStack = new Stack<ITreeNode<T>>();

            stack.Push(root);

            while (stack.Count > 0)
            {
                var element = stack.Pop();
                yield return element.Data;

                if (element.Children != null)
                {
                    foreach (ITreeNode<T> e in element.Children)
                        tempStack.Push(e);

                    while (tempStack.Count > 0)
                        stack.Push(tempStack.Pop());
                }
            }

            // TODO : Implement the tree depth traversal algorithm
            //throw new NotImplementedException();
        }


        /// <summary>
        ///   Traverses a tree using the width-first strategy
        /// </summary>
        /// <typeparam name="T">tree node type</typeparam>
        /// <param name="root">the tree root</param>
        /// <returns>
        ///   Returns the sequence of all tree node data in width-first order
        /// </returns>
        /// <example>
        ///    source tree (root = 1):
        ///    
        ///                      1
        ///                    / | \
        ///                   2  3  4
        ///                  / \     \
        ///                 5   6     7
        ///                     |
        ///                     8   
        ///                   
        ///    result = { 1, 2, 3, 4, 5, 6, 7, 8 } 
        /// </example>
        public static IEnumerable<T> WidthTraversalTree<T>(ITreeNode<T> root)
        {
            if (root == null)
                throw new ArgumentNullException();

            var queue = new Queue<ITreeNode<T>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var element = queue.Dequeue();
                yield return element.Data;

                if (element.Children != null)
                    foreach (ITreeNode<T> e in element.Children)
                        queue.Enqueue(e);
            }
            // TODO : Implement the tree width traversal algorithm
            //throw new NotImplementedException();
        }



        /// <summary>
        ///   Generates all permutations of specified length from source array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">source array</param>
        /// <param name="count">permutation length</param>
        /// <returns>
        ///    All permuations of specified length
        /// </returns>
        /// <exception cref="System.InvalidArgumentException">count is less then 0 or greater then the source length</exception>
        /// <example>
        ///   source = { 1,2,3,4 }, count=1 => {{1},{2},{3},{4}}
        ///   source = { 1,2,3,4 }, count=2 => {{1,2},{1,3},{1,4},{2,3},{2,4},{3,4}}
        ///   source = { 1,2,3,4 }, count=3 => {{1,2,3},{1,2,4},{1,3,4},{2,3,4}}
        ///   source = { 1,2,3,4 }, count=4 => {{1,2,3,4}}
        ///   source = { 1,2,3,4 }, count=5 => ArgumentOutOfRangeException
        /// </example>
        /// 
        
        public static IEnumerable<T[]> GenerateAllPermutations<T>(T[] source, int count)
        {

            if (source.Length < count)
                throw new ArgumentOutOfRangeException("count>lenght of source!");
                
            if (count < 0)
                throw new ArgumentOutOfRangeException("count<0!");

            if (count == 0)
                yield break;

            T[] result = new T[count];

                foreach (int[] j in GenerateAllIntPermutations(count, source.Length))
                {
                    for (int i = 0; i < count; i++)
                    {
                        result[i] = source[j[i]];
                    }
                    yield return result;
                }

        }


        public static IEnumerable<int[]> GenerateAllIntPermutations(int m, int n)
        {
            int[] result = new int[m];
            Stack<int> stack = new Stack<int>(m);
            stack.Push(0);
            while (stack.Count > 0)
            {
                int index = stack.Count - 1; 
                int value = stack.Pop();
                while (value < n)
                {
                    result[index++] = value++;
                    stack.Push(value);
                    if (index != m) continue;

                    yield return (int[])result.Clone(); 
                                                        
                    break;
                }
            }
        }

    }

    public static class DictionaryExtentions
    {

        /// <summary>
        ///    Gets a value from the dictionary cache or build new value
        /// </summary>
        /// <typeparam name="TKey">TKey</typeparam>
        /// <typeparam name="TValue">TValue</typeparam>
        /// <param name="dictionary">source dictionary</param>
        /// <param name="key">key</param>
        /// <param name="builder">builder function to build new value if key does not exist</param>
        /// <returns>
        ///   Returns a value assosiated with the specified key from the dictionary cache. 
        ///   If key does not exist than builds a new value using specifyed builder, puts the result into the cache 
        ///   and returns the result.
        /// </returns>
        /// <example>
        ///   IDictionary<int, Person> cache = new SortedDictionary<int, Person>();
        ///   Person value = cache.GetOrBuildValue(10, ()=>LoadPersonById(10) );  // should return a loaded Person and put it into the cache
        ///   Person cached = cache.GetOrBuildValue(10, ()=>LoadPersonById(10) );  // should get a Person from the cache
        /// </example>
        public static TValue GetOrBuildValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> builder)
        {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, builder());

            return dictionary[key];
            // TODO : Implement GetOrBuildValue method for cache
            //throw new NotImplementedException();
        }

    }
}
