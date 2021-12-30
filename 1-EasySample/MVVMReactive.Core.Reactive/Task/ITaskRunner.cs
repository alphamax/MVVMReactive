using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMReactive.Core.Reactive.Task
{
    public interface ITaskRunner
    {
        void AddTask(TaskPriority priority, Action task);

        void SetDefaultAction(Action task);

        void Dispose();
    }
}