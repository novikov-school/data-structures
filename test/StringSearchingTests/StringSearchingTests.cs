using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using StringSearching;
using StringSearching.BoyerMoore;

namespace StringSearchingTests
{
    [TestFixture]
    public class StringSearchingTests
    {
        private readonly IStringSearchAlgorithm[] SearchAlgoritms = new IStringSearchAlgorithm[] {
                new NaiveStringSearch(),
                new BoyerMoore(),
        	};

        public void Example(IStringSearchAlgorithm algorithm)
        {
            string toFind = "he";
            string toSearch = "The brown cat ran through the kitchen";

            foreach (ISearchMatch match in algorithm.Search(toFind, toSearch))
            {
                Console.WriteLine("Match found at: {0}", match.Start);
            }
        }

        [TestCaseSource("SearchAlgoritms")]
        public void SearchForMissingMatch(IStringSearchAlgorithm algorithm)
        {
            string toFind = "missing data";
            string toSearch = "this string does not contain the missing string data";

            ISearchMatch[] matches = algorithm.Search(toFind, toSearch).ToArray();

            Assert.IsNotNull(matches, "The matches array should not be null.");
            Assert.AreEqual(0, matches.Length, "The matches array should have not items.");
        }

        [TestCaseSource("SearchAlgoritms")]
        public void EmptySourceString(IStringSearchAlgorithm algorithm)
        {
            string toFind = string.Empty;
            string toSearch = "this string does not contain the missing string data";

            ISearchMatch[] matches = algorithm.Search(toFind, toSearch).ToArray();

            Assert.IsNotNull(matches, "The matches array should not be null.");
            Assert.AreEqual(0, matches.Length, "The matches array should have not items.");
        }

        [TestCaseSource("SearchAlgoritms")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullSourceString(IStringSearchAlgorithm algorithm)
        {
            string toFind = null;
            string toSearch = "this string does not contain the missing string data";

            ISearchMatch[] matches = algorithm.Search(toFind, toSearch).ToArray();
        }

        [TestCaseSource("SearchAlgoritms")]
        public void EmptyTargetString(IStringSearchAlgorithm algorithm)
        {
            string toFind = "missing data";
            string toSearch = string.Empty;

            ISearchMatch[] matches = algorithm.Search(toFind, toSearch).ToArray();

            Assert.IsNotNull(matches, "The matches array should not be null.");
            Assert.AreEqual(0, matches.Length, "The matches array should have not items.");
        }

        [TestCaseSource("SearchAlgoritms")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullTargetString(IStringSearchAlgorithm algorithm)
        {
            string toFind = "missing data";
            string toSearch = null;

            ISearchMatch[] matches = algorithm.Search(toFind, toSearch).ToArray();
        }

        [TestCaseSource("SearchAlgoritms")]
        public void EmptyEmpty(IStringSearchAlgorithm algorithm)
        {
            string toFind = string.Empty;
            string toSearch = string.Empty;

            ISearchMatch[] matches = algorithm.Search(toFind, toSearch).ToArray();

            Assert.IsNotNull(matches, "The matches array should not be null.");
            Assert.AreEqual(0, matches.Length, "The matches array should have not items.");
        }

        [TestCaseSource("SearchAlgoritms")]
        public void ExactSingleCharMatch(IStringSearchAlgorithm algorithm)
        {
            string toFind = "f";
            string toSearch = "f";

            ISearchMatch[] matches = algorithm.Search(toFind, toSearch).ToArray();

            Assert.IsNotNull(matches, "The matches array should not be null.");
            Assert.AreEqual(1, matches.Length, "The matches array should have not items.");
            Assert.AreEqual(0, matches[0].Start, "The start of the string match should be 0");
            Assert.AreEqual(toFind.Length, matches[0].Length, "The length of the string match should equal the string found");
        }

        [TestCaseSource("SearchAlgoritms")]
        public void ExactMatch(IStringSearchAlgorithm algorithm)
        {
            string toFind = "found";
            string toSearch = "found";

            ISearchMatch[] matches = algorithm.Search(toFind, toSearch).ToArray();

            Assert.IsNotNull(matches, "The matches array should not be null.");
            Assert.AreEqual(1, matches.Length, "The matches array should have not items.");
            Assert.AreEqual(0, matches[0].Start, "The start of the string match should be 0");
            Assert.AreEqual(toFind.Length, matches[0].Length, "The length of the string match should equal the string found");
        }

        [TestCaseSource("SearchAlgoritms")]
        public void MultipleMatchesExactString(IStringSearchAlgorithm algorithm)
        {
            string toFind = "found";
            string toSearch = "foundfound";

            ISearchMatch[] matches = algorithm.Search(toFind, toSearch).ToArray();

            Assert.IsNotNull(matches, "The matches array should not be null.");
            Assert.AreEqual(2, matches.Length, "The matches array should have not items.");

            Assert.AreEqual(0, matches[0].Start, "The start of the string match should be 0");
            Assert.AreEqual(toFind.Length, matches[0].Length, "The length of the string match should equal the string found");

            Assert.AreEqual(5, matches[1].Start, "The start of the string match should be 5");
            Assert.AreEqual(toFind.Length, matches[1].Length, "The length of the string match should equal the string found");
        }

        [TestCaseSource("SearchAlgoritms")]
        public void MultipleMatchesLeadingJunk(IStringSearchAlgorithm algorithm)
        {
            string toFind = "found";
            string toSearch = "leadingfoundfound";

            ISearchMatch[] matches = algorithm.Search(toFind, toSearch).ToArray();

            Assert.IsNotNull(matches, "The matches array should not be null.");
            Assert.AreEqual(2, matches.Length, "The matches array should have not items.");

            Assert.AreEqual(7, matches[0].Start, "The start of the string match should be 0");
            Assert.AreEqual(toFind.Length, matches[0].Length, "The length of the string match should equal the string found");

            Assert.AreEqual(12, matches[1].Start, "The start of the string match should be 5");
            Assert.AreEqual(toFind.Length, matches[1].Length, "The length of the string match should equal the string found");
        }

        [TestCaseSource("SearchAlgoritms")]
        public void MultipleMatchesTrailingString(IStringSearchAlgorithm algorithm)
        {
            string toFind = "found";
            string toSearch = "foundfoundtrailing";

            ISearchMatch[] matches = algorithm.Search(toFind, toSearch).ToArray();

            Assert.IsNotNull(matches, "The matches array should not be null.");
            Assert.AreEqual(2, matches.Length, "The matches array should have not items.");

            Assert.AreEqual(0, matches[0].Start, "The start of the string match should be 0");
            Assert.AreEqual(toFind.Length, matches[0].Length, "The length of the string match should equal the string found");

            Assert.AreEqual(5, matches[1].Start, "The start of the string match should be 5");
            Assert.AreEqual(toFind.Length, matches[1].Length, "The length of the string match should equal the string found");
        }

        [TestCaseSource("SearchAlgoritms")]
        public void MultipleMatchesMiddleString(IStringSearchAlgorithm algorithm)
        {
            string toFind = "found";
            string toSearch = "found and found";

            ISearchMatch[] matches = algorithm.Search(toFind, toSearch).ToArray();

            Assert.IsNotNull(matches, "The matches array should not be null.");
            Assert.AreEqual(2, matches.Length, "The matches array should have not items.");

            Assert.AreEqual(0, matches[0].Start, "The start of the string match should be 0");
            Assert.AreEqual(toFind.Length, matches[0].Length, "The length of the string match should equal the string found");

            Assert.AreEqual(10, matches[1].Start, "The start of the string match should be 10");
            Assert.AreEqual(toFind.Length, matches[1].Length, "The length of the string match should equal the string found");
        }

        [TestCaseSource("SearchAlgoritms")]
        public void MultipleMatchesLeadingMiddleTrailing(IStringSearchAlgorithm algorithm)
        {
            string toFind = "found";
            string toSearch = "leadingfound and foundtrailing";

            ISearchMatch[] matches = algorithm.Search(toFind, toSearch).ToArray();

            Assert.IsNotNull(matches, "The matches array should not be null.");
            Assert.AreEqual(2, matches.Length, "The matches array should have not items.");

            Assert.AreEqual(7, matches[0].Start, "The start of the string match should be 0");
            Assert.AreEqual(toFind.Length, matches[0].Length, "The length of the string match should equal the string found");

            Assert.AreEqual(17, matches[1].Start, "The start of the string match should be 10");
            Assert.AreEqual(toFind.Length, matches[1].Length, "The length of the string match should equal the string found");
        }
    }
}
