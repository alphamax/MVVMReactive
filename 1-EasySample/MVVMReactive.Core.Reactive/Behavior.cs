using System.Reactive.Subjects;

namespace System.Reactive.Linq
{
    internal class Behavior<T> : IBehavior<T>, IObservable<T>, IDisposable
    {
        private BehaviorSubject<T> _behaviorSubject;

        private IDisposable _disposable;

        public T Value
        {
            get
            {
                return this._behaviorSubject.Value;
            }
        }

        public Behavior(IObservable<T> source, T defaultValue)
        {
            this._behaviorSubject = new BehaviorSubject<T>(defaultValue);
            source.Subscribe(this._behaviorSubject);
        }

        public void Dispose()
        {
            this._disposable.Dispose();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return this._behaviorSubject.Subscribe(observer);
        }
    }
}