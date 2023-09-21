using LeetCoding.DataStructures;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace LeetCoding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinearProgramming linearProgramming = new();

            int[] ints = { 1, 2, 3, 4, 5, 10, 6, 7, 8, 9 };


            Console.WriteLine(linearProgramming.CanArrange(ints , 5));

            Console.ReadKey();
        }
    }
}