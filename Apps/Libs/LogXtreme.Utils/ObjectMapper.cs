using LogXtreme.Extensions;
using System;
using System.Linq.Expressions;

namespace LogXtreme.Utils {

    public class ObjectMapper<TSource, TTarget> 
        where TTarget : class, new() {

        public readonly TSource Source;
        public readonly TTarget Target;

        public ObjectMapper(TSource source) {

            this.Source = source;
            this.Target = new TTarget();
        }

        public ObjectMapper<TSource, TTarget> Populate<T>(
            Expression<Func<TTarget, T>> targetAccessor,
            Func<TSource, T> sourceValue) {

            var targetPropertyInfo = targetAccessor.ToPropertyInfo();
            targetPropertyInfo.SetValue(this.Target, sourceValue(this.Source));

            return this;
        }
    }
}
