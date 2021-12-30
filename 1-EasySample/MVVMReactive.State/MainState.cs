using Optional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMReactive.State.State
{
    public class MainState
    {
        public DateTime ActualHour { get; }
        public string MainData { get; }
        public IEnumerable<LineState> MainLst { get; }

        public MainState(DateTime actualHour, string mainData, IEnumerable<LineState> mainLst)
        {
            ActualHour = actualHour;
            MainData = mainData;
            MainLst = mainLst;
        }

        public MainState With(DateTime? actualHour = null, string mainData = null, IEnumerable<LineState> mainLst = null)
            => new MainState(actualHour ?? ActualHour, mainData ?? MainData, mainLst ?? MainLst);
    }
}