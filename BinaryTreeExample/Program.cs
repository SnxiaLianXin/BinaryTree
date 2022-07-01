using System;
using System.Collections.Generic;

namespace DataTreeDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> binary = new BinaryTree<int>();
            binary.Insert(3);
            binary.Insert(4, 9);
            binary.Insert(5, 10);
            binary.Insert(6, 11);
            binary.Insert(7, 12);

            foreach( var b in binary )
            {
                Console.WriteLine("{0},{1},{2}", b.Data, b.SubData, b.IsRoot);
                //b.Dispose();
            }

            Console.WriteLine(binary.Layer());
            Console.ReadKey();
        }
    }
}
