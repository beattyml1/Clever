namespace Clever.Mapping
{
    public static class Merger
    {
        public static TOut Merge<TOut, TBase, TMixin>(TBase baseVal, TMixin mixin, bool ignoreNulls = false)
            where TOut : TBase, TMixin, new()
        {
            var result = new TOut();
            result.Populate(baseVal);
            result.Populate(mixin);
            return result;
        }

        public static TBase Populate<TBase, TMixin>(this TBase baseVal, TMixin mixin, bool ignoreNulls = false)
        {
            Mapping.Map(mixin.GetType(), baseVal.GetType(), mixin, baseVal);
            return baseVal;
        }

        public static TDest ConvertTo<TSrc, TDest>(this object src, bool ignoreNulls = false) where TDest: new()
        {
            var result = new TDest();
            result.Populate(src);
            return result;
        }
    }
}
