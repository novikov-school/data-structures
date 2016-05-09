using System;
using System.Linq;
using StringSearching;

namespace SimpleTextReplacement
{
    class AlgorithmSelector
    {
        public string Name { get; private set; }
        public IStringSearchAlgorithm Algorithm { get; private set; }

        public AlgorithmSelector(string name, IStringSearchAlgorithm algorithm)
        {
            Name = name;
            Algorithm = algorithm;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
