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
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    for (int move = 0; move < 3; move++)
                    {
                        Operators.Add(new Operator(row, col, move));
                    }
                }
            }
        }
        public abstract State NextMove(State currentState);
    }
}
