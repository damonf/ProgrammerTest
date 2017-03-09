using FakeItEasy;
using NUnit.Framework;

namespace ProgrammerTest.UnitTest
{
    [TestFixture]
    public class WorkQueueTest
    {
        [Test]
        public void CanEqueueDeque()
        {
            var workItem = A.Dummy<WorkItem>();
            var workQueue = new WorkQueue();

            workQueue.Enqueue(workItem);
            var result = workQueue.Dequeue();

            Assert.AreEqual(workItem, result);
        }

        [Test]
        public void DequeFromEmptyCompletedQueueReturnsNull()
        {
            var workQueue = new WorkQueue();

            workQueue.Complete();
            var result = workQueue.Dequeue();

            Assert.IsNull(result);
        }

        [Test]
        public void CanDequeAllItemsFromCompletedQueue()
        {
            var workQueue = new WorkQueue();

            var workItem1 = A.Dummy<WorkItem>();
            var workItem2 = A.Dummy<WorkItem>();

            workQueue.Enqueue(workItem1);
            workQueue.Enqueue(workItem2);
            workQueue.Complete();

            var result1 = workQueue.Dequeue();
            var result2 = workQueue.Dequeue();
            var result3 = workQueue.Dequeue();

            Assert.AreEqual(workItem1, result1);
            Assert.AreEqual(workItem2, result2);
            Assert.IsNull(result3);
        }
    }
}
