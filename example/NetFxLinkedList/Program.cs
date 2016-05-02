using System;
using System.Collections.Generic;

namespace NetFxLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(3);
            list.AddLast(5);
            list.AddLast(7);

            foreach (int value in list)
            {
                Console.WriteLine(value);
            }
        }
    }
}
