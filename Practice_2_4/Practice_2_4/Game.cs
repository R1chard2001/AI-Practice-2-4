using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_2_4
{
    internal class Game
    {
        private ASolver solver;

        public Game(ASolver solver)
        {
            this.solver = solver;
        }

        public void Play()
        {
            State currentState = new State();
            bool playersMove = true;
            colorfulWrite(currentState.ToString());
            while (currentState.GetStatus() == State.BLANK)
            {
                if (playersMove)
                {
                    currentState = PlayersMove(currentState);
                }
                else
                {
                    currentState = AIsMove(currentState);
                }
                colorfulWrite(currentState.ToString());
                playersMove = !playersMove;
            }
            Console.Write("Winner: ");
            colorfulWrite(currentState.GetStatus().ToString());
        }
        private State PlayersMove(State currentState)
        {
            Operator op;
            do
            {
                int row;
                int col;
                int move;
                do
                {
                    Console.Write("Row: ");
                } while (!int.TryParse(Console.ReadLine(), out row) || row < 1 || row > 6);
                do
                {
                    Console.Write("Col: ");
                } while (!int.TryParse(Console.ReadLine(), out col) || col < 1 || col > 7);
                do
                {
                    Console.WriteLine("1 -> left, 2 -> stay int the same column, 3 -> right");
                    Console.Write("Horizontal move: ");
                } while (!int.TryParse(Console.ReadLine(), out move) || move < 1 || move > 3);
                op = new Operator(row-1, col-1, move-1);
            } while (!op.IsAplicable(currentState));
            return op.Apply(currentState);
        }

        private State AIsMove(State currentState)
        {
            State nextState = solver.NextMove(currentState);
            if (nextState == null)
            {
                throw new Exception("The AI cannot select the next move.");
            }
            return nextState;
        }

        private void colorfulWrite(string str)
        {
            foreach (char ch in str)
            {
                if (ch == State.PLAYER1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                switch (ch)
                {
                    case 'R':
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Red;
                        break;
                    case 'B':
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.BackgroundColor = ConsoleColor.Blue;
                        break;
                    case 'W':
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;
                }

                Console.Write(ch);
                Console.ResetColor();
            }
        }
    }
}
