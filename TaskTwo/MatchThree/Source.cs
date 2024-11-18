using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchThree
{
    internal class Source
    {
        static void Main(string[] args)
        {
            Board gameBoard = new Board(10,10);

            var move = gameBoard.CalculateBestMoveForBoard();

            gameBoard.DrawBoard();

            Console.WriteLine("Best Move: " + move.x.ToString() + ", " + move.y.ToString());
            switch (move.direction)
            {
                case Board.MoveDirection.Right:
                    Console.WriteLine("Right");
                    break;
                case Board.MoveDirection.Up:
                    Console.WriteLine("Up");
                    break;
                default:
                    Console.WriteLine("Broke");
                    break;
            }

            return;
        }
    }
}
