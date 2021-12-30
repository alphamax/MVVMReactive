using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MVVMReactive.Core.Reactive.Task
{
    public class TaskRunner : ITaskRunner, IDisposable
    {
        private ConcurrentBag<Action> _taskHighList = new ConcurrentBag<Action>();
        private ConcurrentBag<Action> _taskNormalList = new ConcurrentBag<Action>();
        private Action _defaultAction = null;
        private bool _stopRunning = false;
        private TimeSpan _minimumDelaiBetweenTasks = TimeSpan.FromMilliseconds(5);
        private Thread thread;

        public TaskRunner(int milisecondsDelaiBetweenTasksRun)
        {
            _minimumDelaiBetweenTasks = TimeSpan.FromMilliseconds(milisecondsDelaiBetweenTasksRun);
            thread = new Thread(new ThreadStart(MainLoopTaskRunner));
            thread.Priority = ThreadPriority.Lowest;
            thread.Start();
        }

        public void AddTask(TaskPriority priority, Action task)
        {
            if (priority == TaskPriority.Normal)
                _taskNormalList.Add(task);
            else if (priority == TaskPriority.High)
                _taskHighList.Add(task);
        }

        public void SetDefaultAction(Action task)
        {
            _defaultAction = task;
        }

        public void MainLoopTaskRunner()
        {
            Action actionToRun = null;
            while (!_stopRunning)
            {
                if (_taskHighList.TryTake(out actionToRun))
                    actionToRun.Invoke();
                else if (_taskNormalList.TryTake(out actionToRun))
                    actionToRun.Invoke();
                else if (_defaultAction != null)
                    //No tasks mean
                    _defaultAction.Invoke();
                else
                    Thread.Sleep(500); // Nothing configured. Slow latency running

                Thread.Sleep(_minimumDelaiBetweenTasks);
            }
        }

        public void Dispose()
        {
            _stopRunning = true;
        }
    }
}