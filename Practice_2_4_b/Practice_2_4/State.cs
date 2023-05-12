using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Practice_2_4
{
    public class State : ICloneable
    {
        public static char PLAYER1 = 'R';
        public static char PLAYER2 = 'B';
        public static char BLANK = ' ';
        public static char WALL = 'W';
        // állapottér csak a játékosok korongjainak és a falaknak a koordinátái
        public List<Point> Player1Discs = new List<Point>()
        {
            new Point(0,0),
            new Point(1,0),
            new Point(2,0),
            new Point(3,0),
            new Point(4,0),
            new Point(5,0),
            new Point(6,0),
        };
        public List<Point> Player2Discs = new List<Point>()
        {
            new Point(0,5),
            new Point(1,5),
            new Point(2,5),
            new Point(3,5),
            new Point(4,5),
            new Point(5,5),
            new Point(6,5),
        };
        public List<Point> Walls = new List<Point>()
        {
            new Point(4, 2),
            new Point(2, 3)
        };
        // ha kéne a pálya
        public char[,] Board
        {
            get
            {
                char[,] board = new char[6, 7];
                foreach (Point p in Player1Discs)
                {
                    board[p.X, p.Y] = PLAYER1;
                }
                foreach (Point p in Player2Discs)
                {
                    board[p.X, p.Y] = PLAYER2;
                }
                foreach (Point p in Walls)
                {
                    board[p.X, p.Y] = WALL;
                }
                return board;
            }
        }
        

        public char CurrentPlayer = PLAYER1;
        public void ChangePlayer()
        {
            if (CurrentPlayer == PLAYER1)
            {
                CurrentPlayer = PLAYER2;
            }
            else
            {
                CurrentPlayer = PLAYER1;
            }
        }

        public object Clone()
        {
            State newState = new State();
            newState.Player1Discs = new List<Point>(Player1Discs);
            newState.Player2Discs = new List<Point>(Player2Discs);
            newState.Walls = new List<Point>(Walls); // ezt nem kötelező, a falak nem változnak
            newState.CurrentPlayer = CurrentPlayer;
            return newState;
        }

        public override bool Equals(object obj)
        {
            State other = obj as State;
            if (other.Player1Discs.Count != Player1Discs.Count
                || other.Player2Discs.Count != Player2Discs.Count
                || other.Walls.Count != Walls.Count)
                return false;
            foreach (Point p in other.Player1Discs)
            {
                if (!Player1Discs.Contains(p))
                {
                    return false;
                }
            }
            foreach (Point p in other.Player2Discs)
            {
                if (!Player2Discs.Contains(p))
                {
                    return false;
                }
            }
            foreach (Point p in other.Walls)
            {
                if (!Walls.Contains(p))
                {
                    return false;
                }
            }
            return other.CurrentPlayer == CurrentPlayer;
        }

        public char GetStatus()
        {
            // jelenlegi játékos, másik játékos és az irány lekérdezése
            int rowDif;
            char otherPlayer;
            List<Point> currentPlayerDiscs;
            List<Point> otherPlayerDiscs;
            if (CurrentPlayer == PLAYER1)
            {
                rowDif = 1;
                otherPlayer = PLAYER2;
                currentPlayerDiscs = Player1Discs;
                otherPlayerDiscs = Player2Discs;
            }
            else
            {
                rowDif = -1;
                otherPlayer = PLAYER1;
                currentPlayerDiscs = Player2Discs;
                otherPlayerDiscs = Player1Discs;
            }
            // saját korongokon végigmenni
            foreach (Point p in currentPlayerDiscs)
            {
                // új sor kiesik a pályáról, akkor az a korong nem mozoghat
                int newRowI = p.Y + rowDif;
                if (newRowI < 0 || newRowI >= 6)
                {
                    continue;
                }

                for (int i = -1; i <= 1; i++)
                {
                    int newColI = p.X + i;
                    // új oszlop kiesik a pályán, akkor arra nem mozoghat
                    if (newColI < 0 || newColI >= 6)
                    {
                        continue;
                    }
                    Point newPoint = new Point(newColI, newRowI);
                    // átlósan megy és BLANK vagy másik játékos van a mezőn
                    if (i != 0 && !currentPlayerDiscs.Contains(newPoint) 
                        && (otherPlayerDiscs.Contains(newPoint) || !Walls.Contains(newPoint)))
                    {
                        return BLANK;
                    }
                    // előre megy, sem másik játékos, sem fal nincs a mezőn
                    else if (i == 0 && !currentPlayerDiscs.Contains(newPoint) 
                        && !otherPlayerDiscs.Contains(newPoint) && !Walls.Contains(newPoint))
                    {
                        return BLANK;
                    }
                }
            }
            return otherPlayer;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("    1   2   3   4   5   6   7");
            sb.AppendLine("  +---+---+---+---+---+---+---+");
            char[,] board = Board;
            for (int row = 0; row < 6; row++)
            {
                sb.AppendFormat("{0} |", row+1);
                for (int col = 0; col < 7; col++)
                {
                    sb.AppendFormat(" {0} |", board[row, col]);
                }
                sb.AppendLine();
                sb.AppendLine("  +---+---+---+---+---+---+---+");
            }
            sb.AppendLine("Current player: " + CurrentPlayer);
            return sb.ToString();
        }
    }
}
