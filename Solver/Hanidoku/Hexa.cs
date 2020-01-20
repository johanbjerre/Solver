using System;
using System.Collections.Generic;
using System.Linq;

namespace Solver.Hanidoku
{
    public class Hexa
    {
        public Hexa(int id)
        {
            PossibleNumbers = Enumerable.Range(1, 9).ToList();
            Id = id;
        }

        public int Id { get; set; }
        public List<Int32> PossibleNumbers { get; set; }

        public void Solved(int number)
        {
            this.PossibleNumbers.Clear();
            this.PossibleNumbers.Add(number);
        }

        public void Remove(List<Int32> numbers)
        {
            foreach (var i in numbers)
            {
                if (PossibleNumbers.Count < 2 && PossibleNumbers.Contains(i))
                {
                    throw new Exception("Cannot remove the last possibility.");
                }

                PossibleNumbers.Remove(i);
            }
        }

        public void Remove(Int32 number)
        {
            if (PossibleNumbers.Count < 2 && PossibleNumbers.Contains(number))
            {
                throw new Exception("Cannot remove the last possibility.");
            }

            PossibleNumbers.Remove(number);
        }

        public Boolean IsSolved()
        {
            return PossibleNumbers.Count == 1;
        }
    }
}
