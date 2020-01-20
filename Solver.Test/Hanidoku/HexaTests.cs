using NUnit.Framework;
using Solver.Hanidoku;
using System.Collections.Generic;
using System.Linq;

namespace Solver.Test.Hanidoku
{
    public class HexaTests
    {
        private Hexa hexa;
        private const int ID = 2;
        private const int NUMBER = 5;

        [SetUp]

        public void Setup()
        {
            hexa = new Hexa(ID);
        }

        [Test]
        public void CheckPossibleNumbersTotal()
        {
            Assert.AreEqual(9, hexa.PossibleNumbers.Count());
        }

        [Test]
        public void CheckId()
        {
            Assert.AreEqual(ID, hexa.Id);
        }

        [Test]
        public void CheckAllNumbers()
        {
            List<int> expectedNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            foreach (var exectedNumber in expectedNumbers)
            {
                Assert.IsTrue(hexa.PossibleNumbers.Contains(exectedNumber));
            }
        }

        [Test]
        public void ClearWhenSolved()
        {
            Assert.AreEqual(9, hexa.PossibleNumbers.Count());
            Assert.IsFalse(hexa.IsSolved());
            hexa.Solved(NUMBER);
            Assert.AreEqual(1, hexa.PossibleNumbers.Count());
            Assert.AreEqual(NUMBER, hexa.PossibleNumbers.First());
            Assert.IsTrue(hexa.IsSolved());
        }

        [Test]
        public void RemoveExistingNumber()
        {
            var idToBeRemoved = 2;

            foreach (var exectedNumber in new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 })
            {
                Assert.IsTrue(hexa.PossibleNumbers.Contains(exectedNumber));
            }

            hexa.Remove(idToBeRemoved);

            foreach (var exectedNumber in new List<int> { 1, 3, 4, 5, 6, 7, 8, 9 })
            {
                Assert.IsTrue(hexa.PossibleNumbers.Contains(exectedNumber));
            }
            Assert.IsFalse(hexa.PossibleNumbers.Contains(idToBeRemoved));

        }

        [Test]
        public void RemoveExistingNumberList()
        {
            var toBeRemoved = new List<int> { 2, 3 };

            foreach (var exectedNumber in new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 })
            {
                Assert.IsTrue(hexa.PossibleNumbers.Contains(exectedNumber));
            }

            hexa.Remove(toBeRemoved);

            foreach (var exectedNumber in new List<int> { 1, 4, 5, 6, 7, 8, 9 })
            {
                Assert.IsTrue(hexa.PossibleNumbers.Contains(exectedNumber));
            }
            foreach (var number in toBeRemoved)
            {
                Assert.IsFalse(hexa.PossibleNumbers.Contains(number));
            }

        }

        [Test]
        public void RemoveExistingNumberTooFew()
        {
            var idToBeRemoved = 2;
            hexa.PossibleNumbers = new List<int> { idToBeRemoved };

            var message = Assert.Catch(() => hexa.Remove(idToBeRemoved)).Message;
            Assert.AreEqual("Cannot remove the last possibility.", message);
        }

        [Test]
        public void RemoveExistingNumberListTooFew()
        {
            var idToBeRemoved = new List<int> { 2,3};
            hexa.PossibleNumbers = new List<int> { 2,3 };

            var message = Assert.Catch(() => hexa.Remove(idToBeRemoved)).Message;
            Assert.AreEqual("Cannot remove the last possibility.", message);
        }

        [Test]
        public void RemoveNotExistingNumberTooFew()
        {
            var idToBeRemoved = 2;
            var someOtherNumber = 3;
            hexa.PossibleNumbers = new List<int> { someOtherNumber };
            hexa.Remove(idToBeRemoved);

            Assert.AreEqual(1, hexa.PossibleNumbers.Count);
            Assert.Contains(3, hexa.PossibleNumbers);
        }


        [Test]
        public void RemoveNotExistingNumberListTooFew()
        {
            var idToBeRemoved = new List<int> { 2,3};
            hexa.PossibleNumbers = new List<int> { 3,4 };
            hexa.Remove(idToBeRemoved);

            Assert.AreEqual(1, hexa.PossibleNumbers.Count);
            Assert.Contains(4, hexa.PossibleNumbers);
        }

    }
}