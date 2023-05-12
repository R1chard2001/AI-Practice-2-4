using System;
using System.Collections.Generic;
using System.Drawing;
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
            while (currentState.GetStatus() == State.BLANK)
            {
                Console.WriteLine(currentState.GetStatus());
                if (playersMove)
                {
                    currentState = PlayersMove(currentState);
                }
                else
                {
                    currentState = AIsMove(currentState);
                }
                playersMove = !playersMove;
            }
            Console.Clear();
            print(currentState);
            Console.Write("Winner: ");
            if (currentState.GetStatus() == State.PLAYER1)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Blue;
            }
            Console.WriteLine("  ");
            Console.ResetColor();
        }
        private State PlayersMove(State currentState)
        {
            Operator op = null;
            List<Point> playersDiscs; // saját korongok lekérdezése
            if (currentState.CurrentPlayer == State.PLAYER1)
            {
                playersDiscs = currentState.Player1Discs;
            }
            else
            {
                playersDiscs = currentState.Player2Discs;
            }
            do
            {
                Console.Clear();
                print(currentState);
                int index; // index bekérése
                do
                {
                    Console.Write("Disc index: ");
                } while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > playersDiscs.Count);
                index--;
                int move;
                Console.WriteLine("1 -> left diagonal, 2 -> forward, 3 -> right diagonal");
                do // irány bekérése
                {
                    Console.Write("Move: ");
                } while (!int.TryParse(Console.ReadLine(), out move) || move < 1 || move > 3);
                move -= 2;
                op = new Operator(index, move);
            } while (op == null || !op.IsApplicable(currentState));
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

        private void print(State state) // kiiratás egy oldalon, mindig ugyanott kezdi
        {
            // pálya kiíratása
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("+---+---+---+---+---+---+---+");
            for (int row = 0; row < 6; row++)
            {
                Console.WriteLine("|   |   |   |   |   |   |   |");
                Console.WriteLine("+---+---+---+---+---+---+---+");
            }
            // pályán a piros korongok kiíratása
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < state.Player1Discs.Count; i++)
            {
                printDisc(state.Player1Discs[i], i + 1);
            }
            // pályán a kék korongok kiíratása
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < state.Player2Discs.Count; i++)
            {
                printDisc(state.Player2Discs[i], i + 1);
            }
            // pályán a fal kiíratása
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < state.Walls.Count; i++)
            {
                printDisc(state.Walls[i], i);
            }
            Console.ResetColor();
            Console.SetCursorPosition(0,13);
            Console.Write("Current player: ");
            if (state.CurrentPlayer == State.PLAYER1)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Blue;
            }
            Console.WriteLine("  ");
            Console.ResetColor();
        }
        private void printDisc(Point p, int index) // egy korong kiíratása a megfelelő mezőbe
        {
            int consoleRow = p.Y * 2 + 1;
            int consoleCol = p.X * 4 + 2;
            Console.SetCursorPosition(consoleCol, consoleRow);
            Console.Write(index);
        }
    }
}
