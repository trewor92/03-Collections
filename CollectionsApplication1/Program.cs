using CollectionApplication1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CollectionApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<int[]> i = Generate(3, 5);

          
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