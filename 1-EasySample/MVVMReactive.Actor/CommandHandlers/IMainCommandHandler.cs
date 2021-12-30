using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMReactive.Actor.CommandHandlers
{
    public interface IMainCommandHandler
    {
        void WriteHour();

        void ChangeHour();

        void ClearList();

        void RemoveLine(Guid id);
    }
}