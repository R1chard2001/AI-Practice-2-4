using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_2_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //State state = new State();
            //Console.WriteLine(state);
            Game game = new Game(new TrialAndError());
            game.Play();
            Console.ReadLine();
        }
    }
}
