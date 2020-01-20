using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver.Hanidoku
{
    public static class BoardHelper
    {

        public static Board solveBoard(Board board, Hexa hexa, int s, Boolean debug = false)
        {
            if (hexa != null)
            {
                var foundHexa = board.Rows.SelectMany(r => r.Select(t => t)).Where(r => r.Id == hexa.Id).First();
                foundHexa.Solved(s);
            }

            PerformStrategies(board);

            if (NumberOfCombinations(board) == 0)
            {
                return board;
            }

            foreach (var row in board.Rows)
            {
                foreach (var h in row.Where(h => h.PossibleNumbers.Count() > 1).ToList())
                {
                    foreach (var sn in h.PossibleNumbers.Select(r => r).ToList())
                    {
                        try
                        {
                            var serialized = JsonConvert.SerializeObject(board.Rows);
                            var boardClone = new Board();
                            boardClone.Rows = JsonConvert.DeserializeObject<List<List<Hexa>>>(serialized);
                            return solveBoard(boardClone, h, sn);
                        }
                        catch (Exception e)
                        {
                            //ignore error
                            var dsf = 4;
                        }
                    }
                }
            }
            return null;
        }

        public static Board PerformStrategies(Board board)
        {
            var numberOfCombinations = -1L;

            while (numberOfCombinations == -1L || numberOfCombinations > NumberOfCombinations(board))
            {
                numberOfCombinations = NumberOfCombinations(board);
                if (numberOfCombinations == 0)
                {
                    //Found solution
                    return board;
                }

                RemovePossibleNumbers(board);
                RemoveNumbersOutOfRange(board);
                RemoveImpossibleNumbers(board);
                FindUniqueNumbers(board);
                FindGroupOfNumbers(board);

                Console.Write(NumberOfCombinations(board) + ", ");

            }
            return board;
        }


        //group of numbers. for example if two hexas both have 1,2 then remove 1,2 from all other hexas
        public static Board FindGroupOfNumbers(Board board)
        {
            foreach (var row in board.GetAllRowCombinations())
            {
                foreach (var hexa in row)
                {
                    var foundMatches = row.Where(h =>
                      h.PossibleNumbers.Count == hexa.PossibleNumbers.Count &&
                      hexa.PossibleNumbers.All(g => h.PossibleNumbers.Contains(g))
                    );
                    if (foundMatches.Count() < row.Count && foundMatches.Count() > 1 && foundMatches.Count() == foundMatches.ToList()[0].PossibleNumbers.Count())
                    {
                        //find all other
                        var allOtherHexas = row.Where(g => !foundMatches.Select(t => t.Id).Contains(g.Id)).ToList();
                        if (allOtherHexas.Count > 0)
                        {
                            var toBeRemoved = foundMatches.ToList().First().PossibleNumbers;
                            allOtherHexas.ForEach(h => h.Remove(toBeRemoved));
                        }
                    }
                }
            }
            return board;
        }

        //for example if 5 is to be included, but not 4. Then remove 1,2,3 as well
        public static Board RemoveImpossibleNumbers(Board board)
        {
            foreach (var row in board.GetAllRowCombinations())
            {
                var smallestNumber = GetSmallestNumber(row);
                if (smallestNumber == 0)
                    continue;

                //find next excluded number
                while (smallestNumber-- > 2)
                {
                    if (!row.Any(r => r.PossibleNumbers.Contains(smallestNumber)))
                    {
                        //remove all number from 1 to smallestNumber
                        var toBeRemoved = Enumerable.Range(1, smallestNumber).ToList();
                        toBeRemoved.ForEach(f =>
                        {
                            var asd = row.Any(w => w.PossibleNumbers.Contains(f));
                            if (asd)
                            {
                                var sdff = 3;
                            }
                        });
                        row.ForEach(hexa => hexa.Remove(toBeRemoved));
                    }
                }
            }
            return board;
        }

        private static void findIncludedNumbersInRange(List<int> mustBeIncluded, List<Hexa> row)
        {
            var smallestToBeIncluded = mustBeIncluded.OrderBy(t => t).First();
            var largestToBeIncluded = mustBeIncluded.OrderByDescending(t => t).First();
            var numbersLeftToBeIncluded = row.Count() - mustBeIncluded.Count();

            if (numbersLeftToBeIncluded > 0)
            {
                //check low, can all be low?
                var includeUpper = numbersLeftToBeIncluded - smallestToBeIncluded + 1;
                if (includeUpper > 0)
                {
                    var asd = Enumerable.Range(largestToBeIncluded + 1, includeUpper).ToList();
                    mustBeIncluded.AddRange(asd);
                }

                var includeLower = (numbersLeftToBeIncluded + largestToBeIncluded) - 9;
                if (includeLower > 0)
                {
                    var asd = Enumerable.Range(smallestToBeIncluded - includeLower, includeLower).ToList();
                    mustBeIncluded.AddRange(asd);
                }
            }
        }

        private static void checkIfAllPossibleAreHigher(List<int> mustBeIncluded, List<Hexa> row)
        {
            //check if all possible are higher
            var largestToBeIncluded = mustBeIncluded.OrderByDescending(t => t).First();
            var ttt = row.Where(h => h.PossibleNumbers.All(r => r > largestToBeIncluded)).ToList();
            ttt.ForEach(h =>
            {
                var lowestInc = h.PossibleNumbers.OrderBy(f => f).First();
                var asd = Enumerable.Range(largestToBeIncluded + 1, lowestInc - largestToBeIncluded).ToList();
                mustBeIncluded.AddRange(asd);
            });
        }

        private static void checkIfAllPossibleAreLower(List<int> mustBeIncluded, List<Hexa> row)
        {
            //check if all possible are lower
            var smallestToBeIncluded = mustBeIncluded.OrderBy(t => t).First();
            var sddsds = row.Where(h => h.PossibleNumbers.All(r => r < smallestToBeIncluded)).ToList();
            sddsds.ForEach(h =>
            {
                var highestInc = h.PossibleNumbers.OrderByDescending(f => f).First();
                var asd = Enumerable.Range(highestInc, smallestToBeIncluded - highestInc).ToList();
                mustBeIncluded.AddRange(asd);
            });
        }

        private static void checkIfFoundAllIncluded(List<int> mustBeIncluded, List<Hexa> row)
        {
            //remove all other
            if (row.Count() == mustBeIncluded.Count())
            {
                row.ForEach(hexa =>
                {
                    var removeList = hexa.PossibleNumbers.Where(t => !mustBeIncluded.Contains(t)).ToList();
                    if (removeList.Count() > 0)
                        hexa.Remove(removeList);
                });
            }
        }

        private static void checkIfOnlyExistsAtOnePlace(List<int> mustBeIncluded, List<Hexa> row)
        {
            //only exists at one place
            mustBeIncluded.ForEach(number =>
            {
                var hexas = row.Where(r => r.PossibleNumbers.Contains(number));
                if (hexas.Count() == 1)
                    hexas.ToList()[0].Solved(number);
            });
        }

        public static Board FindUniqueNumbers(Board board)
        {
            foreach (var row in board.GetAllRowCombinations())
            {
                var largestNumber = GetLargestNumber(row);
                if (largestNumber == 0)
                    continue;

                var smallestNumber = GetSmallestNumber(row);

                var mustBeIncluded = Enumerable.Range(smallestNumber, largestNumber - smallestNumber + 1).ToList();

                findIncludedNumbersInRange(mustBeIncluded, row);

                checkIfAllPossibleAreHigher(mustBeIncluded, row);

                checkIfAllPossibleAreLower(mustBeIncluded, row);

                checkIfFoundAllIncluded(mustBeIncluded, row);

                checkIfOnlyExistsAtOnePlace(mustBeIncluded, row);

            }
            return board;
        }


        //remove numbers that are not possible due to the length of the row and the already solved numbers
        public static Board RemoveNumbersOutOfRange(Board board)
        {
            board.GetAllRowCombinations().ForEach(row =>
            {
                var length = row.Count;
                var largestNumber = BoardHelper.GetLargestNumber(row);

                if (largestNumber != 0)
                {

                    var smallestNumber = GetSmallestNumber(row);

                    var largestPossible = smallestNumber + length - 1;
                    var smallestPossible = largestNumber - length + 1;
                    var toBeRemoved = Enumerable.Range(1, 9).Where(n => n > largestPossible || n < smallestPossible).ToList();
                    row.ForEach(hexa => hexa.Remove(toBeRemoved));
                }
            });
            return board;
        }

        public static Board RemovePossibleNumbers(Board board)
        {
            board.GetAllRowCombinations().ForEach(row => RemovePossibleNumbersRow(row));

            return board;
        }

        public static Int64 NumberOfCombinations(Board board)
        {
            return board.Rows.Sum(row =>
            {
                return row.Sum(hexa => hexa.IsSolved() ? 0 : hexa.PossibleNumbers.Count());
            });
        }

        public static int GetLargestNumber(List<Hexa> row)
        {
            return row.Where(r => r.PossibleNumbers.Count == 1)
                    .SelectMany(y => y.PossibleNumbers).OrderByDescending(h => h)
                    .FirstOrDefault();
        }

        public static int GetSmallestNumber(List<Hexa> row)
        {
            return row.Where(r => r.PossibleNumbers.Count == 1)
                .SelectMany(y => y.PossibleNumbers).OrderBy(h => h)
                .FirstOrDefault();
        }

        //remove solved numbers from other hexas in the same row
        public static List<Hexa> RemovePossibleNumbersRow(List<Hexa> row)
        {
            var allSolved = row.Where(r => r.PossibleNumbers.Count == 1).ToList();

            allSolved.ForEach(solved =>
            {
                var rowsForDeletion = row.Where(h => h.Id != solved.Id).ToList();
                rowsForDeletion.ForEach(hexa => hexa.Remove(solved.PossibleNumbers.First()));
            });
            return row;
        }

        public static Board Example1(Board board)
        {
            //board.Rows[0][0].Solved(5);
            board.Rows[0][4].Solved(7);
            board.Rows[2][6].Solved(3);
            //board.Rows[3][0].Solved(8);

            board.Rows[4][1].Solved(7);
            board.Rows[4][2].Solved(8);


            board.Rows[5][0].Solved(5);
            //board.Rows[5][1].Solved(4);
            board.Rows[5][4].Solved(1);
            board.Rows[6][1].Solved(5);
            board.Rows[7][1].Solved(9);
            board.Rows[7][2].Solved(5);
            board.Rows[8][0].Solved(4);
            board.Rows[8][1].Solved(6);
            //board.Rows[8][3].Solved(5);

            return board;
        }

    }
}
