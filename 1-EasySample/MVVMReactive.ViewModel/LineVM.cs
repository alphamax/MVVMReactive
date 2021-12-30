using MVVMReactive.Actor.CommandHandlers;
using MVVMReactive.State;
using Reactive.Bindings;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMReactive.ViewModel
{
    public class LineVM : ReactiveObject
    {
        public ReactiveProperty<Guid> Id { get; }
        public ReactiveProperty<string> InstantHour { get; }

        public ReactiveCommand<Guid> RemoveLine { get; } = new ReactiveCommand<Guid>();

        public LineVM(IObservable<LineState> lineStream, IMainCommandHandler mainCommandHandler)
        {
            InstantHour = lineStream.Select(l => l.InstantHour.ToString()).ToReactiveProperty();
            Id = lineStream.Select(l => l.Id).ToReactiveProperty();

            RemoveLine.Subscribe((id) => mainCommandHandler.RemoveLine(id));
        }
    }
}