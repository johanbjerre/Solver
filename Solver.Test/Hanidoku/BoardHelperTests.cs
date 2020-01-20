using NUnit.Framework;
using System;
using Solver.Hanidoku;
using System.Collections.Generic;
using System.Linq;

namespace Solver.Test.Hanidoku
{
    public class BoardHelperTests
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetLargestNumberNoSolved()
        {
            var hexas = new List<Hexa> {
                new Hexa(1) { PossibleNumbers = new List<int> { 1, 2, 3 } }
            };
            Assert.AreEqual(0, BoardHelper.GetLargestNumber(hexas));
        }

        [Test]
        public void GetLargestNumberOneSolved()
        {
            var hexas = new List<Hexa> {
                new Hexa(1) { PossibleNumbers = new List<int> { 1, 2, 3 } },
                new Hexa(2) { PossibleNumbers = new List<int> { 4 } }
            };
            Assert.AreEqual(4, BoardHelper.GetLargestNumber(hexas));
        }

        [Test]
        public void GetLargestNumberManySolved()
        {
            var hexas = new List<Hexa> {
                new Hexa(1) { PossibleNumbers = new List<int> { 1, 2, 3 } },
                new Hexa(2) { PossibleNumbers = new List<int> { 4 } },
                new Hexa(2) { PossibleNumbers = new List<int> { 5 } }
            };
            Assert.AreEqual(5, BoardHelper.GetLargestNumber(hexas));
        }

        [Test]
        public void GetSmallestNumberNoSolved()
        {
            var hexas = new List<Hexa> {
                new Hexa(1) { PossibleNumbers = new List<int> { 1, 2, 3 } }
            };
            Assert.AreEqual(0, BoardHelper.GetSmallestNumber(hexas));
        }

        [Test]
        public void GetSmallestNumberOneSolved()
        {
            var hexas = new List<Hexa> {
                new Hexa(1) { PossibleNumbers = new List<int> { 1, 2, 3 } },
                new Hexa(2) { PossibleNumbers = new List<int> { 4 } }
            };
            Assert.AreEqual(4, BoardHelper.GetSmallestNumber(hexas));
        }

        [Test]
        public void GetSmallestNumberManySolved()
        {
            var hexas = new List<Hexa> {
                new Hexa(1) { PossibleNumbers = new List<int> { 1, 2, 3 } },
                new Hexa(2) { PossibleNumbers = new List<int> { 4 } },
                new Hexa(2) { PossibleNumbers = new List<int> { 5 } }
            };
            Assert.AreEqual(4, BoardHelper.GetSmallestNumber(hexas));
        }

        [Test]
        public void GetNumberOfCombinationsNewBoard()
        {
            var board = new Board();
            var expected = 61 * 9;
            Assert.AreEqual(expected, BoardHelper.NumberOfCombinations(board));
        }

        [Test]
        public void GetNumberOfCombinationsOneSolved()
        {
            var board = new Board();
            board.Rows[0][0].Solved(4);
            var expected = 60 * 9;
            Assert.AreEqual(expected, BoardHelper.NumberOfCombinations(board));
        }

        [Test]
        public void GetNumberOfCombinationsOneSolvedAndSomeProgress()
        {
            var board = new Board();
            board.Rows[0][0].Solved(4);
            board.Rows[0][1].PossibleNumbers = new List<int> { 1, 2, 3, 5, 6, 7, 8, 9 };
            var expected = 59 * 9 + 8;
            Assert.AreEqual(expected, BoardHelper.NumberOfCombinations(board));
        }

        [Test]
        public void RemovePossibleNumbersRow()
        {
            var numbers1 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var numbers1Expected = new List<int> { 1, 3, 4, 5, 6, 7, 8, 9 };
            var numbers2 = new List<int> { 1, 2, 3 };
            var numbers2Expected = new List<int> { 1, 3 };
            var numbers3 = new List<int> { 2 };

            var row = new List<Hexa> {
                new Hexa(1){PossibleNumbers=numbers1 },
                new Hexa(2){PossibleNumbers=numbers2 },
                new Hexa(3){PossibleNumbers=numbers3 }
            };
            var actual = BoardHelper.RemovePossibleNumbersRow(row);
            Assert.AreEqual(3, row.Count);
            Assert.AreEqual(8, row[0].PossibleNumbers.Count);
            Assert.AreEqual(2, row[1].PossibleNumbers.Count);
            Assert.AreEqual(1, row[2].PossibleNumbers.Count);

            Assert.IsTrue(numbers1Expected.All(n => row[0].PossibleNumbers.Contains(n)));
            Assert.IsTrue(numbers2Expected.All(n => row[1].PossibleNumbers.Contains(n)));
            Assert.IsTrue(row[2].PossibleNumbers.Contains(2));

        }

        [Test]
        public void RemovePossibleNumbersRowManySolved()
        {
            var numbers1 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var numbers1Expected = new List<int> { 1, 4, 5, 6, 7, 8, 9 };
            var numbers2 = new List<int> { 1, 2, 3 };
            var numbers2Expected = new List<int> { 1 };
            var numbers3 = new List<int> { 2 };
            var numbers4 = new List<int> { 3 };

            var row = new List<Hexa> {
                new Hexa(1){PossibleNumbers=numbers1 },
                new Hexa(2){PossibleNumbers=numbers2 },
                new Hexa(3){PossibleNumbers=numbers3 },
                new Hexa(4){PossibleNumbers=numbers4 }
            };
            var actual = BoardHelper.RemovePossibleNumbersRow(row);
            Assert.AreEqual(4, row.Count);
            Assert.AreEqual(7, row[0].PossibleNumbers.Count);
            Assert.AreEqual(1, row[1].PossibleNumbers.Count);
            Assert.AreEqual(1, row[2].PossibleNumbers.Count);
            Assert.AreEqual(1, row[3].PossibleNumbers.Count);

            Assert.IsTrue(numbers1Expected.All(n => row[0].PossibleNumbers.Contains(n)));
            Assert.IsTrue(numbers2Expected.All(n => row[1].PossibleNumbers.Contains(n)));
            Assert.IsTrue(row[2].PossibleNumbers.Contains(2));
            Assert.IsTrue(row[3].PossibleNumbers.Contains(3));

        }

        [Test]
        public void TestExample1()
        {
            var board = BoardHelper.Example1(new Board());
            var solvedBoard=BoardHelper.solveBoard(board,null,0);
            var asd = solvedBoard.Rows.SelectMany(r =>
                  r.Where(t => t.PossibleNumbers.Count() == 1)
                  .Select(h => h.PossibleNumbers[0])).ToList();
            var actual = String.Join("", solvedBoard.Rows.SelectMany(r =>
                r.Where(t => t.PossibleNumbers.Count() == 1)
                .Select(h => h.PossibleNumbers[0])).ToList());
            Assert.AreEqual("5463773526446975838234175667892314554681237753268489547646758", actual);
        }

    }
}