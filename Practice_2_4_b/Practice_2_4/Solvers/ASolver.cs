using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_2_4
{
    public abstract class ASolver
    {
        public List<Operator> Operators = new List<Operator>();
        public ASolver()
        {
            generateOperators();
        }
        private void generateOperators()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Operators.Add(new Operator(i, j));
                }
            }
            Console.WriteLine();
        }
        public abstract State NextMove(State currentState);
    }
}
