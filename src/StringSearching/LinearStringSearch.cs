using System;
using System.Collections.Generic;
using System.Linq;
using Tracker;

namespace StringSearching
{
    public class NaiveStringSearch : Tracker<char>, IStringSearchAlgorithm
    {
        public IEnumerable<ISearchMatch> Search(string toFind, string toSearch)
        {
            #region Validate Parameters are not null
            if (toFind == null)
            {
                throw new ArgumentNullException("toFind");
            }

            if (toSearch == null)
            {
                throw new ArgumentNullException("toSearch");
            }
            #endregion

            if (toFind.Length > 0 && toSearch.Length > 0)
            {
                for (int startIndex = 0; startIndex <= toSearch.Length - toFind.Length; startIndex++)
                {
                    int matchCount = 0;

                    while (Compare(toFind[matchCount], toSearch[startIndex + matchCount]) == 0)
                    {
                        matchCount++;

                        if (toFind.Length == matchCount)
                        {
                            yield return new StringSearchMatch(startIndex, matchCount);

                            startIndex += matchCount - 1;
                            break;
                        }
                    }
                }
            }
        }
    }
}
