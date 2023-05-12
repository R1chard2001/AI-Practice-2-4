using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_2_4
{
    public class Operator
    {
        int index;
        int colDiff;
        public Operator(int index, int colDiff)
        {
            this.index = index;
            this.colDiff = colDiff;
        }
        public bool IsApplicable(State state)
        {
            List<Point> playersDiscs;
            List<Point> othersDiscs;
            int rowDiff;
            if (state.CurrentPlayer == State.PLAYER1)
            {
                playersDiscs = state.Player1Discs;
                othersDiscs = state.Player2Discs;
                rowDiff = 1;
            }
            else
            {
                playersDiscs = state.Player2Discs;
                othersDiscs = state.Player1Discs;
                rowDiff = -1;
            }
            if (index < 0 || index >= playersDiscs.Count)
            {
                return false;
            }
            Point nextCoords = new Point(playersDiscs[index].X + colDiff, playersDiscs[index].Y + rowDiff);
            // kiesik a pályáról
            if (nextCoords.Y < 0 || nextCoords.Y >= 6 || nextCoords.X < 0 || nextCoords.X >= 7)
            {
                return false;
            }
            // fal/saját korong van az új helyen
            if (state.Walls.Contains(nextCoords) || playersDiscs.Contains(nextCoords))
            {
                return false;
            }
            // előre megyünk és másik játékos korongja van az új helyen
            if (colDiff == 0 && othersDiscs.Contains(nextCoords))
            {
                return false;
            }
            return true;
        }
        public State Apply(State state)
        {
            State newState = (State)state.Clone();
            List<Point> playersDiscs;
            List<Point> othersDiscs;
            int rowDiff;
            if (newState.CurrentPlayer == State.PLAYER1)
            {
                playersDiscs = newState.Player1Discs;
                othersDiscs = newState.Player2Discs;
                rowDiff = 1;
            }
            else
            {
                playersDiscs = newState.Player2Discs;
                othersDiscs = newState.Player1Discs;
                rowDiff = -1;
            }
            Point newCoords = new Point(playersDiscs[index].X + colDiff, playersDiscs[index].Y + rowDiff);
            if (colDiff != 0)
            {
                othersDiscs.Remove(newCoords);
            }
            playersDiscs[index] = newCoords;
            newState.ChangePlayer();
            return newState;
        }
    }
}
