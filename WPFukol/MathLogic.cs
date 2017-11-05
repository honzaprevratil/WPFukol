using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFukol
{
    class MathLogic
    {
        public int[] NumbersByLevel(int level, int[] levelsNumber)
        {
            Random rand =new Random();
            int number1 = int.Parse(""+rand.Next(1, levelsNumber[level]));
            int number2 = int.Parse(""+rand.Next(1, levelsNumber[level]));
            int[] numbers = {number1, number2 };
            return numbers;

        }
    }
}
