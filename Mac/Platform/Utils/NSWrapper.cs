using Foundation;
using System;

namespace Mac.Platform.Utils
{
    public class NSWrapper<T> : NSObject
    {
        public readonly T Item;

        public NSWrapper(T item)
        {
            if (item == null)
                throw new NullReferenceException("Wrapped item is null");

            Item = item;
        }

        public override nuint GetNativeHash() => (nuint)Item!.GetHashCode();

        public override bool IsEqual(NSObject? anObject)
        {
            if (anObject == null)
                return false;

            return GetNativeHash() == anObject.GetNativeHash();
        }
    }
}
