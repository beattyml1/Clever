using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Mapping
{
    public static class Merge
    {
        public static TOut MergeExact<TOut, TBase, TMixin>(TBase baseVal, TMixin mixin, bool ignoreNulls = false)
            where TOut : TBase, TMixin
        {
            // TODO: Implement Merge
            throw new NotImplementedException();
        }

        public static TOut MergeEager<TOut, TBase, TMixin>(TBase baseVal, TMixin mixin, bool ignoreNulls = false)
            where TOut : TBase, TMixin
        {
            // TODO: Implement Merge
            throw new NotImplementedException();
        }

        public static TBase PoulateeExact<TBase, TMixin>(this TBase baseVal, TMixin mixin, bool ignoreNulls = false)
        {
            // TODO: Implement Merge
            throw new NotImplementedException();
        }

        public static TBase PoulateEager<TBase, TMixin>(this TBase baseVal, TMixin mixin, bool ignoreNulls = false)
        {
            // TODO: Implement Merge
            throw new NotImplementedException();
        }

        public static TDest ToExact<TSrc, TDest>(this TSrc src, bool ignoreNulls = false)
        {
            // TODO: Implement Merge
            throw new NotImplementedException();
        }

        public static TDest ToEager<TSrc, TDest>(this TSrc src, bool ignoreNulls = false)
        {
            // TODO: Implement Merge
            throw new NotImplementedException();
        }
    }
}
