using Optional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ReactiveExtension
    {
        public static IDisposable SubscribeAndMatch<T>(this IObservable<Option<T>> source, Action<T> onNext)
        {
            return source.Subscribe(valueOption => valueOption.MatchSome(onNext));
        }
    }
}