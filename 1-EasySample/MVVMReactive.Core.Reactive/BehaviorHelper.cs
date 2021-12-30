using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace System.Linq
{
    public static class Behavior
    {
        public static IBehavior<TResult> CombineLatest<TSource1, TSource2, TResult>(this IBehavior<TSource1> source1, IBehavior<TSource2> source2, Func<TSource1, TSource2, TResult> stateSelector)
        {
            IBehavior<TResult> behavior = source1.CombineLatest<TSource1, TSource2, TResult>(source2, stateSelector).ToBehavior<TResult>(stateSelector(source1.Value, source2.Value));
            return behavior;
        }

        public static IBehavior<TResult> CombineLatest<TSource1, TSource2, TSource3, TResult>(this IBehavior<TSource1> source1, IBehavior<TSource2> source2, IBehavior<TSource3> source3, Func<TSource1, TSource2, TSource3, TResult> stateSelector)
        {
            IBehavior<TResult> behavior = source1.CombineLatest<TSource1, TSource2, TSource3, TResult>(source2, source3, stateSelector).ToBehavior<TResult>(stateSelector(source1.Value, source2.Value, source3.Value));
            return behavior;
        }

        public static IBehavior<TResult> CombineLatest<TSource1, TSource2, TSource3, TSource4, TResult>(this IBehavior<TSource1> source1, IBehavior<TSource2> source2, IBehavior<TSource3> source3, IBehavior<TSource4> source4, Func<TSource1, TSource2, TSource3, TSource4, TResult> stateSelector)
        {
            IBehavior<TResult> behavior = source1.CombineLatest<TSource1, TSource2, TSource3, TSource4, TResult>(source2, source3, source4, stateSelector).ToBehavior<TResult>(stateSelector(source1.Value, source2.Value, source3.Value, source4.Value));
            return behavior;
        }

        public static IBehavior<T> Return<T>(T defaultValue)
        {
            return new Behavior<T>(Observable.Empty<T>(), defaultValue);
        }

        public static IBehavior<T> ToBehavior<T>(this BehaviorSubject<T> source)
        {
            return new Behavior<T>(source, source.Value);
        }

        public static IBehavior<T> ToBehavior<T>(this IObservable<T> source, T defaultValue)
        {
            return new Behavior<T>(source, defaultValue);
        }
    }
}