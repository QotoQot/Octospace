using System;
using System.Collections.Generic;

namespace Core.Basics.Utils
{
    public static class CollectionExtensions
    {
        public static List<T> MovedItems<T>(this List<T> list, HashSet<int> indexes, int targetIndex)
        {
            if (indexes.Count > list.Count)
                throw new ArgumentOutOfRangeException("Index count is larger than item count");

            var stableItems = new List<T>();
            var movingItems = new List<T>();

            var itemsBeforeTarget = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (indexes.Contains(i))
                {
                    movingItems.Add(list[i]);

                    if (i < targetIndex)
                        itemsBeforeTarget++;
                }
                else
                    stableItems.Add(list[i]);
            }

            var result = new List<T>();

            var firstSplit = targetIndex - itemsBeforeTarget;
            var secondSplit = firstSplit + movingItems.Count;

            for (int i = 0; i < list.Count; i++)
            {
                if (i < firstSplit)
                {
                    result.Add(stableItems[i]);
                }
                else if (i >= firstSplit && i < secondSplit)
                {
                    result.Add(movingItems[i - firstSplit]);
                }
                else
                {
                    result.Add(stableItems[i - movingItems.Count]);
                }
            }

            if (movingItems.Count != indexes.Count)
                throw new InvalidOperationException("Incorrect count of moving items");

            return result;
        }
    }
}
