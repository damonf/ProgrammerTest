using System.Collections.Generic;
using System.Threading;

namespace ProgrammerTest
{
    public interface IWorkQueue
    {
        void Enqueue(WorkItem workItem);
        void Complete();
        WorkItem Dequeue();
    }

    /// <summary>
    /// Maintains the queue of work items.
    /// </summary>
    public class WorkQueue : IWorkQueue
    {
        private readonly Queue<WorkItem> _queue = new Queue<WorkItem>();
        private bool _complete;

        public void Enqueue(WorkItem workItem)
        {
            var acquiredLock = false;

            try
            {
                Monitor.Enter(_queue, ref acquiredLock);

                _queue.Enqueue(workItem);

                Monitor.Pulse(_queue);
            }
            finally
            {
                if (acquiredLock)
                {
                    Monitor.Exit(_queue);
                }
            }
        }

        public void Complete()
        {
            var acquiredLock = false;

            try
            {
                Monitor.Enter(_queue, ref acquiredLock);

                _complete = true;

                Monitor.Pulse(_queue);
            }
            finally
            {
                if (acquiredLock)
                {
                    Monitor.Exit(_queue);
                }
            }
        }

        public WorkItem Dequeue()
        {
            var acquiredLock = false;

            try
            {
                Monitor.Enter(_queue, ref acquiredLock);

                if (_queue.Count == 0)
                {
                    if (_complete)
                    {
                        return null;
                    }
                    else
                    {
                        Monitor.Wait(_queue);
                    }
                }

                return _queue.Dequeue();
            }
            finally
            {
                if (acquiredLock)
                {
                    Monitor.Exit(_queue);
                }
            }
        }
    }
}
