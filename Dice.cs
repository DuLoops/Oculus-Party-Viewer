using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    class Dice
    {
        static Random Die = new Random();
        public static int Roll(int dice, int sides)
        {
            int total = 0;
            for (int i = 0; i < dice; i++)
            {
                total += Die.Next(sides) + 1;
            }
            return total;
        }
    }
}
