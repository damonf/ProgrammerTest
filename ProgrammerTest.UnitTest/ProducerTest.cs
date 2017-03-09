using System.Collections.Generic;
using System.Xml.XPath;
using FakeItEasy;
using NUnit.Framework;

namespace ProgrammerTest.UnitTest
{
    [TestFixture]
    public class ProducerTest
    {
        [Test]
        public void Produces()
        {
            var workQueue = A.Fake<IWorkQueue>();
            IEnumerable<IXPathNavigable> documents = A.CollectionOfFake<IXPathNavigable>(1);
            var outputDir = A.Dummy<string>();

            var producer = new Producer(workQueue, documents, outputDir);
            producer.Produce();

            A.CallTo(() => workQueue.Enqueue(A<WorkItem>.Ignored)).MustHaveHappened();
            A.CallTo(() => workQueue.Complete()).MustHaveHappened();
        }

        [Test]
        public void ProducesAllItems()
        {
            var workQueue = A.Fake<IWorkQueue>();
            IEnumerable<IXPathNavigable> documents = A.CollectionOfFake<IXPathNavigable>(10);
            var outputDir = A.Dummy<string>();

            var producer = new Producer(workQueue, documents, outputDir);
            producer.Produce();

            A.CallTo(() => workQueue.Enqueue(A<WorkItem>.Ignored)).MustHaveHappened(Repeated.Exactly.Times(10));
            A.CallTo(() => workQueue.Complete()).MustHaveHappened();
        }
    }
}
