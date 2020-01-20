using System;
using System.Collections.Generic;
using System.Linq;

namespace Solver.Hanidoku
{
    public class Board
    {
        public Board()
        {
            var id = 0;
            Rows = new List<List<Hexa>>();
            Enumerable.Range(5, 4).ToList().ForEach(i =>//5 6 7 8
            Rows.Add(Enumerable.Range(1, i).ToList().Select(u => new Hexa(id++)).ToList())
            );
            Rows.Add(Enumerable.Range(1, 9).Select(h => new Hexa(id++)).ToList());
            Enumerable.Range(5, 4).Reverse().ToList().ForEach(i =>//5 6 7 8
            Rows.Add(Enumerable.Range(1, i).ToList().Select(u => new Hexa(id++)).ToList())
            );

        }

        private List<List<Hexa>> getExtraRows1()
        {
            var output = new List<List<Hexa>>();
            var row = 0;
            var col1 = 0;
            var col2 = 0;
            output.Add(new List<Hexa> { Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1] });

            row = 0;
            col1 = 1;
            col2 = 0;
            output.Add(new List<Hexa> { Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col2] });

            row = 0;
            col1 = 2;
            col2 = 1;
            output.Add(new List<Hexa> { Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col2--], Rows[row++][col2--] });

            row = 0;
            col1 = 3;
            col2 = 2;
            output.Add(new List<Hexa> { Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col2--], Rows[row++][col2--], Rows[row++][col2--] });

            row = 0;
            col1 = 4;
            col2 = 3;
            output.Add(new List<Hexa> { Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col2--], Rows[row++][col2--], Rows[row++][col2--], Rows[row++][col2--] });

            row = 1;
            col1 = 5;
            col2 = 4;
            output.Add(new List<Hexa> { Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col2--], Rows[row++][col2--], Rows[row++][col2--], Rows[row++][col2--] });

            row = 2;
            col1 = 6;
            col2 = 5;
            output.Add(new List<Hexa> { Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col2--], Rows[row++][col2--], Rows[row++][col2--], Rows[row++][col2--] });

            row = 3;
            col1 = 7;
            col2 = 6;
            output.Add(new List<Hexa> { Rows[row++][col1], Rows[row++][col1], Rows[row++][col2--], Rows[row++][col2--], Rows[row++][col2--], Rows[row++][col2--] });

            row = 4;
            col1 = 8;
            col2 = 7;
            output.Add(new List<Hexa> { Rows[row++][col1], Rows[row++][col2--], Rows[row++][col2--], Rows[row++][col2--], Rows[row++][col2--] });

            return output;

        }

        private List<List<Hexa>> getExtraRows2()
        {
            var output = new List<List<Hexa>>();
            var row = 0;
            var col1 = 4;
            output.Add(new List<Hexa> { Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1++] });

            row = 0;
            col1 = 3;
            output.Add(new List<Hexa> { Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1], Rows[row++][col1] });

            row = 0;
            col1 = 2;
            output.Add(new List<Hexa> { Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1] });

            row = 0;
            col1 = 1;
            output.Add(new List<Hexa> { Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1] });

            row = 0;
            col1 = 0;
            output.Add(new List<Hexa> { Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1] });

            row = 1;
            col1 = 0;
            output.Add(new List<Hexa> { Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1] });

            row = 2;
            col1 = 0;
            output.Add(new List<Hexa> { Rows[row++][col1++], Rows[row++][col1++], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1] });

            row = 3;
            col1 = 0;
            output.Add(new List<Hexa> { Rows[row++][col1++], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1] });

            row = 4;
            col1 = 0;
            output.Add(new List<Hexa> { Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1], Rows[row++][col1] });


            return output;

        }
        public List<List<Hexa>> GetAllRowCombinations()
        {
            var output = new List<List<Hexa>>();
            output.AddRange(Rows);
            output.AddRange(getExtraRows1());
            output.AddRange(getExtraRows2());
            return output;


        }

        public List<List<Hexa>> Rows;

        public override string ToString()
        {
            var output = "";
            foreach (var row in Rows)
            {
                foreach (var hexa in row)
                {
                    output += hexa.IsSolved() ? hexa.PossibleNumbers[0].ToString() : "("+String.Join(',',hexa.PossibleNumbers)+")";
                }
                output += "\n";
            }
            return output;
        }

    }
}
