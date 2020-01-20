using Solver.Hanidoku;
using System;
using static Solver.Hanidoku.BoardHelper;

namespace Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();

            Example1(board);
            var solvedBoard = solveBoard(board, null, 0);

            Console.WriteLine(solvedBoard.ToString());

            var asd = "";
        }







    }
}
