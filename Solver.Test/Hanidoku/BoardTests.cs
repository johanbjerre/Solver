using NUnit.Framework;
using Solver.Hanidoku;
using System.Collections.Generic;
using System.Linq;

namespace Solver.Test.Hanidoku
{
    public class BoardTests
    {
        private Board board;
        private List<List<Hexa>> combinations;

        [SetUp]
        public void Setup()
        {
            board = new Board();
            combinations = board.GetAllRowCombinations();
        }

        [Test]
        public void CheckCombinationsTotal()
        {
            Assert.AreEqual(27, combinations.Count);
        }

        [Test]
        public void CheckCombinationsRowsStraight()
        {
            CheckNumbersRowNumber(combinations, Enumerable.Range(0, 5).ToList(), 0, 5);
            CheckNumbersRowNumber(combinations, Enumerable.Range(5, 6).ToList(), 1, 6);
            CheckNumbersRowNumber(combinations, Enumerable.Range(11, 7).ToList(), 2, 7);
            CheckNumbersRowNumber(combinations, Enumerable.Range(18, 8).ToList(), 3, 8);
            CheckNumbersRowNumber(combinations, Enumerable.Range(26, 9).ToList(), 4, 9);
            CheckNumbersRowNumber(combinations, Enumerable.Range(35, 8).ToList(), 5, 8);
            CheckNumbersRowNumber(combinations, Enumerable.Range(43, 7).ToList(), 6, 7);
            CheckNumbersRowNumber(combinations, Enumerable.Range(50, 6).ToList(), 7, 6);
            CheckNumbersRowNumber(combinations, Enumerable.Range(56, 5).ToList(), 8, 5);
        }

        [Test]
        public void CheckCombinationsRowsLeftToRight()
        {
            CheckNumbersRowNumber(combinations, new List<int> { 0, 5, 11, 18, 26 }, 9, 5);
            CheckNumbersRowNumber(combinations, new List<int> { 1, 6, 12, 19, 27, 35 }, 10, 6);
            CheckNumbersRowNumber(combinations, new List<int> { 2, 7, 13, 20, 28, 36, 43 }, 11, 7);
            CheckNumbersRowNumber(combinations, new List<int> { 3, 8, 14, 21, 29, 37, 44, 50 }, 12, 8);
            CheckNumbersRowNumber(combinations, new List<int> { 4, 9, 15, 22, 30, 38, 45, 51, 56 }, 13, 9);
            CheckNumbersRowNumber(combinations, new List<int> { 10, 16, 23, 31, 39, 46, 52, 57 }, 14, 8);
            CheckNumbersRowNumber(combinations, new List<int> { 17, 24, 32, 40, 47, 53, 58 }, 15, 7);
            CheckNumbersRowNumber(combinations, new List<int> { 25, 33, 41, 48, 54, 59 }, 16, 6);
            CheckNumbersRowNumber(combinations, new List<int> { 34, 42, 49, 55, 60 }, 17, 5);
        }

        [Test]
        public void CheckCombinationsRowsRightToLeft()
        {
            CheckNumbersRowNumber(combinations, new List<int> { 4, 10, 17, 25, 34 }, 18, 5);
            CheckNumbersRowNumber(combinations, new List<int> { 3, 9, 16, 24, 33, 42 }, 19, 6);
            CheckNumbersRowNumber(combinations, new List<int> { 2, 8, 15, 23, 32, 41, 49 }, 20, 7);
            CheckNumbersRowNumber(combinations, new List<int> { 1, 7, 14, 22, 31, 40, 48, 55 }, 21, 8);
            CheckNumbersRowNumber(combinations, new List<int> { 0, 6, 13, 21, 30, 39, 47, 54, 60 }, 22, 9);
            CheckNumbersRowNumber(combinations, new List<int> { 5, 12, 20, 29, 38, 46, 53, 59 }, 23, 8);
            CheckNumbersRowNumber(combinations, new List<int> { 11, 19, 28, 37, 45, 52, 58 }, 24, 7);
            CheckNumbersRowNumber(combinations, new List<int> { 18, 27, 36, 44, 51, 57 }, 25, 6);
            CheckNumbersRowNumber(combinations, new List<int> { 26, 35, 43, 50, 56 }, 26, 5);
        }

        [Test]
        public void AllCombinationsUnique()
        {
            foreach(var combination in combinations)
            {
                var finds=combinations.Where(
                    c => c.Count() == combination.Count() &&
                    c.All(t=>combination.Contains(t))
                    ).Count();
                Assert.AreEqual(1, finds);
            }
        }


        private static void CheckNumbersRowNumber(List<List<Hexa>> combinations, List<int> numbers, int rowNumber, int total)
        {
            Assert.AreEqual(total, combinations[rowNumber].Count());
            numbers.ForEach(n =>
            {
                var finds = combinations[rowNumber].Where(r => r.Id == n).ToList();
                Assert.AreEqual(1, finds.Count());
            });
        }
    }
}