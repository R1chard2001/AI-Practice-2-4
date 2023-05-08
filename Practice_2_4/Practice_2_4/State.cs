using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_2_4
{
    public class State : ICloneable
    {
        public static char PLAYER1 = 'R';
        public static char PLAYER2 = 'B';
        public static char BLANK = ' ';
        public static char WALL = 'W';

        public char[,] Board = new char[6, 7]
        {
            { PLAYER1, PLAYER1, PLAYER1, PLAYER1, PLAYER1, PLAYER1, PLAYER1 },
            { BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK },
            { BLANK, BLANK, BLANK, BLANK, WALL, BLANK, BLANK },
            { BLANK, BLANK, WALL, BLANK, BLANK, BLANK, BLANK },
            { BLANK, BLANK, BLANK, BLANK, BLANK, BLANK, BLANK },
            { PLAYER2, PLAYER2, PLAYER2, PLAYER2, PLAYER2, PLAYER2, PLAYER2 }
        };
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
            newState.Board = (char[,])Board.Clone();
            newState.CurrentPlayer = CurrentPlayer;
            return newState;
        }

        public override bool Equals(object obj)
        {
            State other = obj as State;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (this.Board[i, j] != other.Board[i,j])
                    {
                        return false;
                    }
                }
            }
            return other.CurrentPlayer == CurrentPlayer;
        }

        public char GetStatus()
        {
            int rowDif;
            char otherPlayer;
            if (CurrentPlayer == PLAYER1)
            {
                rowDif = 1;
                otherPlayer = PLAYER2;
            }
            else
            {
                rowDif = -1;
                otherPlayer = PLAYER1;
            }

            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    // ha nem a saját korongunk, akkor megyünk tovább
                    if (Board[row, col] != CurrentPlayer)
                    {
                        continue;
                    }
                    int nextRow = row + rowDif;
                    // ha az új sorunk kiesik, akkor
                    if (nextRow < 0 || nextRow > 5)
                    {
                        continue;
                    }
                    // ha az oszlopban lévő új pozíció blank, akkor mozoghatunk
                    if (Board[nextRow, col] == BLANK)
                    {
                        return BLANK;
                    }
                    // átlósan, ha az új pozíció blank11 vagy a másik játékos korongja, akkor mozoghatunk
                    if (col - 1 >= 0 &&
                        (Board[nextRow, col - 1] == otherPlayer ||
                        Board[nextRow, col - 1] == BLANK))
                    {
                        return BLANK;
                    }
                    // másik átlóval is ellenőrzés
                    if (col + 1 < 6 &&
                        (Board[nextRow, col + 1] == otherPlayer ||
                        Board[nextRow, col + 1] == BLANK))
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
            for (int row = 0; row < 6; row++)
            {
                sb.AppendFormat("{0} |", row+1);
                for (int col = 0; col < 7; col++)
                {
                    sb.AppendFormat(" {0} |", Board[row, col]);
                }
                sb.AppendLine();
                sb.AppendLine("  +---+---+---+---+---+---+---+");
            }
            sb.AppendLine("Current player: " + CurrentPlayer);
            return sb.ToString();
        }
    }
}
