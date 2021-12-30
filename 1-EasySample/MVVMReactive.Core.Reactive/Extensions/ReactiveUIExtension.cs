using MVVMReactive.Core;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace System.Reactive.Linq
{
    public static class ReactiveUIExtension
    {
        public static IObservable<IEnumerable<TVM>> ToReactiveListOfVM<TState, TVM>(this IObservable<IEnumerable<TState>> stateEnumerableStream, Func<IObservable<TState>, TVM> converter)
            where TState : class
        {
            List<BehaviorSubject<TState>> observableListState = new List<BehaviorSubject<TState>>();

            BehaviorSubject<IEnumerable<TVM>> subject =
                new BehaviorSubject<IEnumerable<TVM>>(Enumerable.Empty<TVM>());

            stateEnumerableStream
                .Subscribe(stateEnumerable =>
                {
                    try
                    {
                        if (observableListState.Count < stateEnumerable.Count())
                        {
                            var initialiListCount = observableListState.Count;
                            for (int i = 0; i < stateEnumerable.Count() - initialiListCount; i++)
                                observableListState.Add(new BehaviorSubject<TState>(default(TState)));
                        }

                        for (int i = 0; i < stateEnumerable.Count(); i++)
                        {
                            if (observableListState[i].Value != stateEnumerable.ElementAt(i))
                                observableListState[i].OnNext(stateEnumerable.ElementAt(i));
                        }

                        if (observableListState.Count > stateEnumerable.Count())
                        {
                            observableListState.RemoveRange(stateEnumerable.Count(), observableListState.Count - stateEnumerable.Count());
                        }
                        var newList = observableListState.ToList().Select(c => converter(c)).ToList();
                        subject.OnNext(newList);
                    }
                    catch (Exception e)
                    {
                        Logger.Fatal(e, "Internal error on ReactiveUIExtension - ToReactiveListOfVM");
                    }
                });

            return subject;
        }

        public static IObservable<TVM> ToReactiveVM<TState, TVM>(this IObservable<TState> stateStream, Func<IObservable<TState>, TVM> converter)
            where TState : class
            where TVM : class
        {
            BehaviorSubject<TState> behavior = new BehaviorSubject<TState>(default(TState));
            BehaviorSubject<TVM> observableState = new BehaviorSubject<TVM>(default(TVM));

            stateStream
                .Subscribe(state =>
                {
                    if (state != null)
                    {
                        behavior.OnNext(state);
                        //We recreate the VM if it does not exists
                        if (observableState.Value == null)
                        {
                            observableState.OnNext(converter(behavior));
                        }
                    }
                    else
                    {
                        observableState.OnNext(default(TVM));
                    }
                });

            return observableState;
        }
    }
}