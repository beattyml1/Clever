using System;

namespace Clever.Mapping
{
    public static class Merger
    {
        public static TOut Merge<TOut, TBase, TMixin>(TBase baseVal, TMixin mixin, bool ignoreNulls = false)
            where TOut : TBase, TMixin
        {
            // TODO: Implement Merge
            throw new NotImplementedException();
        }

        public static TBase Poulate<TBase, TMixin>(this TBase baseVal, TMixin mixin, bool ignoreNulls = false)
        {
            // TODO: Implement Merge
            throw new NotImplementedException();
        }

        public static TDest ConverTo<TSrc, TDest>(this TSrc src, bool ignoreNulls = false)
        {
            // TODO: Implement Merge
            throw new NotImplementedException();
        }
    }
}
