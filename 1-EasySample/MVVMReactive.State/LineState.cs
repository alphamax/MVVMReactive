using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMReactive.State
{
    public class LineState
    {
        public Guid Id { get; }
        public DateTime InstantHour { get; }

        public LineState(Guid id, DateTime instantHour)
        {
            Id = id;
            InstantHour = instantHour;
        }

        public LineState With(Guid? id, DateTime? instantHour)
            => new LineState(id ?? Id, instantHour ?? InstantHour);
    }
}