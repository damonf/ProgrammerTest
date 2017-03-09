using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.XPath;

namespace ProgrammerTest
{
    /// <summary>
    /// Adds the XML file contents to the queue.
    /// </summary>
    public class Producer
    {
        private readonly IWorkQueue _workQueue;
        private readonly IEnumerable<IXPathNavigable> _documents;
        private readonly string _outputDir;

        public Producer(IWorkQueue workQueue, IEnumerable<IXPathNavigable> documents, string outputDir)
        {
            if (workQueue == null)
                throw new ArgumentNullException(nameof(workQueue));
            if (documents == null)
                throw new ArgumentNullException(nameof(documents));
            if (outputDir == null)
                throw new ArgumentNullException(nameof(outputDir));

            _workQueue = workQueue;
            _documents = documents;
            _outputDir = outputDir;
        }

        public void Produce()
        {
            var idx = 0;

            foreach (var document in _documents)
            {
                var outfilePath = Path.Combine(_outputDir, $"{idx++}.html");
                _workQueue.Enqueue(new WorkItem { OutFile = outfilePath, XmlDocument = document });
            }

            _workQueue.Complete();
        }
    }
}
