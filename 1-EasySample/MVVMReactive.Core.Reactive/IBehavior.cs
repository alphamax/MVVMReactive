namespace System.Reactive.Linq
{
    public interface IBehavior<T> : IObservable<T>
    {
        T Value
        {
            get;
        }
    }
}