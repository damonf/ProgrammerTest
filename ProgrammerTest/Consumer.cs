using System;

namespace ProgrammerTest
{
    /// <summary>
    /// Removes the XML files from the queue and passes them to the transformer.
    /// </summary>
    public class Consumer
    {
        private readonly ITransformer _transformer;
        private readonly IWorkQueue _workQueue;

        public Consumer(IWorkQueue workQueue, ITransformer transformer)
        {
            if (workQueue == null)
                throw new ArgumentNullException(nameof(workQueue));
            if (transformer == null)
                throw new ArgumentNullException(nameof(transformer));

            _workQueue = workQueue;
            _transformer = transformer;
        }

        public void Consume()
        {
            var workItem = _workQueue.Dequeue();

            while (workItem != null)
            {
                _transformer.Transform(workItem.XmlDocument, workItem.OutFile);
                workItem = _workQueue.Dequeue();
            }
        }
    }
}
