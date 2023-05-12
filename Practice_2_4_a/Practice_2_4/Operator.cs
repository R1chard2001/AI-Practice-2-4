using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_2_4
{
    public class Operator
    {
        int row, col;
        int nextCol;
        public Operator(int row, int col, int nextMove)
        {
            this.row = row;
            this.col = col;
            switch (nextMove)
            {
                case 0:
                    this.nextCol = col - 1;
                    break;
                case 1:
                    this.nextCol = col;
                    break;

                case 2:
                    this.nextCol = col + 1;
                    break;
                default:
                    throw new Exception();
            }
        }
        public bool IsApplicable(State state)
        {
            if (state.Board[row, col] != state.CurrentPlayer)
            {
                return false;
            }
            if (nextCol < 0 || nextCol >= 7)
            {
                return false;
            }
            char otherPlayer;
            int newRow;
            if (state.CurrentPlayer == State.PLAYER1)
            {
                otherPlayer = State.PLAYER2;
                newRow = row + 1;
            }
            else
            {
                otherPlayer = State.PLAYER1;
                newRow = row - 1;
            }
            if (newRow < 0 || newRow >= 6)
            {
                return false;
            }
            if (state.Board[newRow, nextCol] == State.BLANK)
            {
                return true;
            }
            if (nextCol != col && state.Board[newRow, nextCol] == otherPlayer)
            {
                return true;
            }
            return false;
        }
        public State Apply(State state)
        {
            State newState = (State)state.Clone();
            newState.Board[row, col] = State.BLANK;
            if (state.CurrentPlayer == State.PLAYER1)
            {
                newState.Board[row + 1, nextCol] = state.CurrentPlayer;
            }
            else 
            {
                newState.Board[row - 1, nextCol] = state.CurrentPlayer;
            }
            newState.ChangePlayer();
            return newState;
        }
    }
}
