using CollectionApplication1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CollectionApplication1
{
    public class MockLargeStringReader : StringReader
    {
        public MockLargeStringReader(string text) : base(text) { }
        //public override string ReadLine() { throw new OutOfMemoryException(); }
        public override string ReadToEnd() { throw new OutOfMemoryException(); }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //List<int[]> i = Generate(3, 5);
            /*
            string testText = "TextReader is the abstract base " +
            "class of StreamReader and StringReader, which read " +
            "characters from streams and strings, respectively.\n\n" +
            "Create an instance of TextReader to open a text file " +
            "for reading a specified range of characters, or to " +
            "create a reader based on an existing stream.\n\n" +
            "You can also use an instance of TextReader to read " +
            "text from a custom backing store using the same " +
            "APIs you would use for a string or a stream.\n\n";
            var reader = new MockLargeStringReader(testText);
    */
      

            //foreach (int i in Task.GetFibonacciSequence(3))
                //Console.WriteLine(i);

          

            //Console.WriteLine("");

            Console.ReadLine();


        }



        public static List<int[]> Generate(int m, int n)
        {
            int[] result = new int[m];
            List<int[]> list = new List<int[]>();
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

                    list.Add( (int[])result.Clone());

                    break;
                }
            }
            return list;
        }


    }
}