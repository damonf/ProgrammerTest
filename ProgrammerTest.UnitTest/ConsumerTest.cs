using System.Xml.XPath;
using FakeItEasy;
using NUnit.Framework;

namespace ProgrammerTest.UnitTest
{
    [TestFixture]
    public class ConsumerTest
    {
        [Test]
        public void Consumes()
        {
            var workQueue = A.Fake<IWorkQueue>();

            A.CallTo(() => workQueue.Dequeue())
                .Returns(A.Dummy<WorkItem>()).Once()
                .Then.Returns(null);

            var transformer = A.Fake<ITransformer>();

            var consumer = new Consumer(workQueue, transformer);
            consumer.Consume();

            A.CallTo(() => transformer.Transform(A<IXPathNavigable>.Ignored, A<string>.Ignored)).MustHaveHappened();
            A.CallTo(() => workQueue.Dequeue()).MustHaveHappened();
        }

        [Test]
        public void ConsumesAllItems()
        {
            var workQueue = A.Fake<IWorkQueue>();

            A.CallTo(() => workQueue.Dequeue())
                .Returns(A.Dummy<WorkItem>()).NumberOfTimes(10)
                .Then.Returns(null);

            var transformer = A.Fake<ITransformer>();

            var consumer = new Consumer(workQueue, transformer);
            consumer.Consume();

            A.CallTo(() => transformer.Transform(A<IXPathNavigable>.Ignored, A<string>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Times(10));
            A.CallTo(() => workQueue.Dequeue()).MustHaveHappened(Repeated.Exactly.Times(11));
        }
    }
}
