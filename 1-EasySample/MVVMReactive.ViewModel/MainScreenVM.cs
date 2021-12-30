using MVVMReactive.Actor.CommandHandlers;
using MVVMReactive.State;
using MVVMReactive.State.State;
using Reactive.Bindings;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

using System.Threading.Tasks;
using ReactiveCommand = Reactive.Bindings.ReactiveCommand;
using System.Windows.Threading;

namespace MVVMReactive.ViewModel
{
    public class MainScreenVM : ReactiveObject
    {
        public ReactiveProperty<DateTime> ActualHour { get; }
        public ReactiveProperty<String> MainData { get; }

        public ReactiveProperty<IEnumerable<LineVM>> MainLst { get; }

        public ReactiveCommand WriteHourCommand { get; } = new ReactiveCommand();

        public ReactiveCommand ChangeHourCommand { get; } = new ReactiveCommand();

        public ReactiveCommand ClearListCommand { get; } = new ReactiveCommand();

        public ReactiveCommand DeleteItemCommand { get; } = new ReactiveCommand();

        public MainScreenVM(IObservable<MainState> mainState, IMainCommandHandler mainCommandHandler)
        {
            ActualHour = mainState
                .Select(c => c.ActualHour).ToReactiveProperty();
            MainData = mainState
                .Select(c => c.MainData).ToReactiveProperty();

            MainLst = mainState
                .Select(c => c.MainLst)
                .ObserveOnDispatcher(DispatcherPriority.Normal)
                .ToReactiveListOfVM(lineStateStream => new LineVM(lineStateStream, mainCommandHandler))
                .ToReactiveProperty();

            WriteHourCommand.Subscribe(mainCommandHandler.WriteHour);

            ChangeHourCommand.Subscribe(mainCommandHandler.ChangeHour);

            ClearListCommand.Subscribe(mainCommandHandler.ClearList);
        }
    }
}