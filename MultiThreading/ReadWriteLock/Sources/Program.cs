using Common;

namespace ReadWriteLock
{
    static class Program
    {
        static void Main(string[] args)
        {
            CustomLogger.LogMessage("ReadWriteLock readers priority");
            using (ReadWriteLockReadersPriority rwLock = new ReadWriteLockReadersPriority())
            using (ReadWriteLockRWRExample rwrExample = new ReadWriteLockRWRExample(rwLock))
            {
                rwrExample.Run();
            }

            using (ReadWriteLockReadersPriority rwLock = new ReadWriteLockReadersPriority())
            using (ReadWriteLockWRWExample wrwExample = new ReadWriteLockWRWExample(rwLock))
            {
                wrwExample.Run();
            }

            CustomLogger.LogMessage("ReadWriteLock writers priority");
            using (ReadWriteLockWritersPriority rwLock = new ReadWriteLockWritersPriority())
            using (ReadWriteLockRWRExample rwrExample = new ReadWriteLockRWRExample(rwLock))
            {
                rwrExample.Run();
            }

            using (ReadWriteLockWritersPriority rwLock = new ReadWriteLockWritersPriority())
            using (ReadWriteLockWRWExample wrwExample = new ReadWriteLockWRWExample(rwLock))
            {
                wrwExample.Run();
            }

            CustomLogger.LogMessage("ReadWriteLockSlim");
            using (ReadWriteLockSlimWrapper rwLock = new ReadWriteLockSlimWrapper())
            using (ReadWriteLockRWRExample rwrExample = new ReadWriteLockRWRExample(rwLock))
            {
                rwrExample.Run();
            }

            using (ReadWriteLockSlimWrapper rwLock = new ReadWriteLockSlimWrapper())
            using (ReadWriteLockWRWExample wrwExample = new ReadWriteLockWRWExample(rwLock))
            {
                wrwExample.Run();
            }

            using (ReadWriteLockUpgradeExample example = new ReadWriteLockUpgradeExample())
            {
                example.Run();
            }
        }
    }
}
