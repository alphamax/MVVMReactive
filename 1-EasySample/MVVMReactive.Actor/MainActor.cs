using MVVMReactive.Actor.CommandHandlers;
using MVVMReactive.Service.Service;
using MVVMReactive.State;
using MVVMReactive.State.State;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MVVMReactive.Actor
{
    public class MainActor : IMainCommandHandler
    {
        #region MainDataStream definition

        private BehaviorSubject<MainState> _mainDataStream { get; } = new BehaviorSubject<MainState>(DefaultMainDataValue);
        public IBehavior<MainState> MainDataStream => _mainDataStream.ToBehavior<MainState>(DefaultMainDataValue);

        /// <summary>
        /// First stream data
        /// </summary>
        public static MainState DefaultMainDataValue = new MainState(DateTime.Now, "Computing hours", Enumerable.Empty<LineState>());

        #endregion MainDataStream definition

        #region Services

        private TimeService _timeService = new TimeService();

        #endregion

        public MainActor()
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ =>
                {
                    _mainDataStream.OnNext(
                        _mainDataStream.Value.With(_timeService.AddOneSecond(_mainDataStream.Value.ActualHour))
                        );
                });
        }

        public void WriteHour()
        {
            LineState line = new LineState(Guid.NewGuid(), _mainDataStream.Value.ActualHour);
            _mainDataStream.OnNext(
                _mainDataStream.Value.With(mainData: "Hour pressed: " + _mainDataStream.Value.ActualHour.ToString(), mainLst: _mainDataStream.Value.MainLst.Append(line))
                );
        }

        public void ChangeHour()
        {
            _mainDataStream.OnNext(
                _mainDataStream.Value.With(actualHour: _timeService.AddHour(_mainDataStream.Value.ActualHour, 3))
                );
        }

        public void ClearList()
        {
            _mainDataStream.OnNext(
                _mainDataStream.Value.With(mainLst: Enumerable.Empty<LineState>())
                );
        }

        public void RemoveLine(Guid id)
        {
            _mainDataStream.OnNext(
                _mainDataStream.Value.With(mainLst: _mainDataStream.Value.MainLst.Where(c => c.Id != id).ToList())
                );
        }
    }
}